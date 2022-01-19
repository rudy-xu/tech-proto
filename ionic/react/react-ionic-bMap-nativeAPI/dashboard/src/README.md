### Quick Start

There are two way to run program:

### First Way: Step by step manual deployment
#### Requirements
* Node.js v12.18.2  

#### Setup ionic CLI:
```sh
    npm update
    npm i -g ionic
```
#### Enter into ```/dashboard``` folder:  
```sh
    ionic serve
```
**Note: If using powershell, please execute the following command in the powershell:**
```sh
    Set-ExecutionPolicy -Scope Process Unrestricted
    ionic serve
```
#### Login website:
```sh
    http://localhost:8100 or http://127.0.0.1:8100
```
### Second Way: Use docker-swarm to deploy app
* deployCloud: [../../docker-swarm/deploy](../../docker-swarm/deploy/README.md)
* deployLocal: [../../docker-swarm/dockerfile](../../docker-swarm/dockerfile/README.md)