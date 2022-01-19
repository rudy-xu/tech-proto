# How to deploy
## **Note:** If you use 'Git Bash', please remember to add 'winpty' in the head of all commands.
### Step 1: Pull images from Aliyun Docker
Open terminal, to run:
```sh
    docker login --username=凯德带你去爬山 registry.cn-hangzhou.aliyuncs.com
    password: dycsyald12**
```
### Step 2: Deploy
* Enter into ```docker-swarm/deploy``` folder:
```sh
    docker pull registry.cn-hangzhou.aliyuncs.com/scp_docker/scp-ionic:latest
    docker stack deploy -c docker-compose.yml dep
```
* Login website:
```sh
    http://localhost:8080 or http://127.0.0.1:8080