#!/bin/bash
# deployLocal/
echo "-- begin to pull micro service images..."


docker pull eclipse-mosquitto:1.6.9
docker pull minio/minio:edge
docker pull confluentinc/cp-kafka:5.5.1
docker pull confluentinc/cp-zookeeper:5.5.1
docker pull mcr.microsoft.com/dotnet/core/aspnet:3.1

echo "-- pull micro service images done"


echo "-- begin to build micro service images..."
for var in *
do
    if test -d "$var" -a "$var" != deploy 
    then
        cd "$var" || exit
        echo "-- start build micro service $var..."
        ls .
        docker build -t "loc-""$var" .
        if test $? -ne 0
        then
            echo "-- build micro service $var failed"
            exit 1
        else
            echo "-- build micro service $var done"
        fi
        cd ..
    fi
done
echo "-- build micro service images done"

echo "-- rm old dangling images..."
docker rmi "$(docker images -qa -f 'dangling=true')" 2> /dev/null
echo "-- rm old dangling images done"

#deploy/
cd deploy || exit
echo "-- begin to deploy micro services..."
for var in *
do
    cd "$var" || exit
    echo "-- start the micro service $var..."
    docker stack deploy -c docker-compose.yml "dep" &
    echo "-- micro service $var started"
    sleep 3
    cd ..
done
echo "-- all micro services deploy done"
