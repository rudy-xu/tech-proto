#!/bin/bash
#deploy

echo "Begin to build new image......"

cd myql || exit
docker build -t "new-mysql:latest" .


echo "Begin to deploy micro service......"
cd ../dockerSwarm || exit
docker stack deploy -c docker-compose.yml "dep" &

echo "Mirco services deploying done"