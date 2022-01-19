#!/usr/bin/env bash

# docker login 172.18.4.224:1180 -u admin -p Harbor12345

docker stop keysys-vueapp
docker rm keysys-vueapp

docker image rm --force 172.18.4.224:1180/key/keysys-vueapp:latest


docker build -t 172.18.4.224:1180/key/keysys-vueapp:latest .


docker push 172.18.4.224:1180/key/keysys-vueapp:latest


docker run -d --name keysys-vueapp -p 4400:4400  172.18.4.224:1180/key/keysys-vueapp:latest
