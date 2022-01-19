# How to deploy locally
## Requirements
* Docker 19.03.x
* JDK 1.8 or higher
* .Net Core SDK 3.1
* Git Bash
## Quick Start
### Step 1: 
Change the ip to the local ip or corresponding server ip ( **'127.0.0.1' is not to recommand** ):
* ```docker-swarm/deployLocal/deploy/kafka/docker-compose.yml```  

   **KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://10.86.5.205:9092**
* ```docker-swarm/deployLocal/deploy/s_consumer/config/config.yml```   

    **mysqlServer: 10.86.5.205**  
    **kafkaServer: 10.86.5.205**
* ```docker-swarm/deployLocal/deploy/s_dashboard/config/config.yml```  

    **mysqlServer: 10.86.5.205**  
    **minioServer: 10.86.12.127**
* ```docker-swarm/deployLocal/deploy/s_datacollector/config/config.yml```

    **kafkaServer: 10.86.5.205**  
    **mqttServer: 10.86.5.205**

### Step 2: 
Open git bash, enter into ```docker-swarm/deployLocal``` floder and run the following command:

    ./deploy.sh

### Step 3:
Login minio and create bucket:
* Url: https://127.0.0.1:9000
* Access Key: sioux
* Secret Key: 1234Abcd
* When you enter into the minio website, you should click '+' in the lower right corner of the screen. Then select the second Item and create bucket named 'scp-bucket'.

### Step 5:
Upload video:
* we provide a test video in ```../../doc/temp```   
* Put the original video file into ```C:\temp\```  
**(Note: You can change the path in the program)**
* Enter into ```src/thermalCam``` to run the program
```sh
    dotnet build
    dotnet run
```

### Step 6:
Use ```json-data-generator``` to publish messages  
See: [../../scaffold](../../scaffold/json-data-generator/README.md)

### Step 7:
Local access:

    Url: http://localhost:5000  
External access:

    Url: http://ip:5000  
 **(Note: 'Ip' is your machine ip)**
