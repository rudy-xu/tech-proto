# Deploy locally
## **Note:** If you use 'Git Bash', please remember to add 'winpty' in the head of all commands.

* Enter into ```docker-swarm/dockerfile``` floder:  , to run:
```sh
    docker build -t loc-ionic .
    docker stack deploy -c docker-compose.yml dep
```
* Login website:
```sh
    http://localhost:8080 or http://127.0.0.1:8080