# BFF(Backend For Frontend)
The BFF is the middleware between backend and frontend. The main function is Data conversion, requiring and frowarding. To meet the requirements of different client and API and let single backend service many frontends. It reduce front end dependence and pay more attention to Web. At the same time, let backend service more pure. In current market, the main way of implementing has two: GraphQL and REST. This project uses 'REST'.

## Related Technology
* REST
* ASP.NET Core
* Python
* EntityFrameworkCore
* Mysql

## Communication protocol of BFF
### Http Protocol
#### 1. All Message
- Request
    - Method: **GET**
    - URL: ```http://x.x.x.x:5000/api/Devicemessage```
    - Headers:
        ```json
        {
            "Content-Type": "application/json",
            "Authorization": "SDSDFSFS.SDFSDFSDFSFD.SDFSDFSF"
        }
        ```
- Respone
    - Body: [See the following data format](#Messgage-Data-Format)

#### 2. Newest Message By DeviceId
- Request
    - Method: **GET**
    - URL: ```http://x.x.x.x:5000/api/DeviceMessage/top/1?device=plc-2```
    - Headers:
        ``` json
        {
            "Content-Type": "application/json",
            "Authorization": "SDSDFSFS.SDFSDFSDFSFD.SDFSDFSF"
        }
        ```
- Respone
    - Body: [See the following data format](#Messgage-Data-Format)

#### 3. Device List By AgentId
- Request
    - Method: **GET**
    - URL: ```http://x.x.x.x:5000/api/DeviceMessage/DeviceList/Gateway-1```
    - Headers:
        ```json
        {
            "Content-Type": "application/json",
            "Authorization": "SDSDFSFS.SDFSDFSDFSFD.SDFSDFSF"
        }
        ```
- Respone
    - Body: [See the following data format](#Device-List-Format)

#### 4. Agent List
- Request
    - Method: **GET**
    - URL: ```http://x.x.x.x:5000/api/DeviceMessage/AgentList```
    - Headers:
        ```json
        {
            "Content-Type": "application/json",
            "Authorization": "SDSDFSFS.SDFSDFSDFSFD.SDFSDFSF"
        }
        ```
- Respone
    - Body: [See the following data format](#Agent-List-Format)

#### 5. Video Data
- Request
    - Method: **GET**
    - URL: ```http://x.x.x.x:5270/objects```
    - Headers:
        ```json
            {
                "Content-Type": "application/json"
            }
        ```
- Reponse
    - Body: [See the following data format](#Video-Data-Format)

### Messgage Data Format
> **A:** normal  
> **W:** warning
```json
[{
    "timeStamp":"20200618T20:19:24.33Z",
    "deviceId":"plc-2",
    "data":
    [
        {
            "key":"Cars",  
            "value":248,             //[0,1000],A:<700,W:<900
            "unit":"cars/h"
        },
        {
            "key":"Pedestrians",  
            "value":61,              //[0,1000],A:<500,W:<800
            "unit":"people/h"
        },
        {
            "key":"Noise_Level",  
            "value":50,              //[0,100],A:<50,B:<70
            "unit":"dB"
        },
        {
            "key":"Electricity",  
            "value":8.1,             //[0,100.0],A:<40,W:60
            "unit":"kWh"
        },
        {
            "key":"Pavement_Temp.",  
            "value":21,              //[-20.0,80.0],A:<30 W:<50
            "unit":"℃"
        },
        {
            "key":"PM2.5",  
            "value":21,              //[0,200],A:<100,W:<200
            "unit":"μ/m³"
        },
        {
            "key":"PM10",  
            "value":2,                //[0.0,100.0],A:<40,W:<100
            "unit":"μg/m³"
        },
        {
            "key":"CO2",  
            "value":91,               //[0.0,5000.0],A:<450,W:<1000
            "unit":"ppm"
        }
    ]
}]
```


### Device List Format
```json
[
  "plc-1",
  "plc-2"
]
```

### Agent List Format
```json
[
  "Gateway-1",
  "Gateway-2"
]
```

### Video Data Format
```json
{
    "data":[
        {
            "id": 0,
            "name": "screencast20200818182218.mp4",     //(screencast + time).mp4
            "timeStamp": 1597803431361,
            "url": "http://x.x.x.x:9000/scp-bucket/xxxxxxxxxx",     //source video
            "url2": "http://x.x.x.x:9000/face-bucket/xxxxxxxx"      //analysis video
        }
    ]
}
```