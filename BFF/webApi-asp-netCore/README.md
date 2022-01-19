# webApi-asp.net-core
This part is about webApi. Use `ASP.NET Core` and `RESTFul` to implement.

## Requirements
* .NET Core SDK 3.1
* MySql8.0

## Design Process
1. Model &rarr; Build connection between database and program
2. Data &rarr; Design interface and class that implements the interface
3. Controller &rarr; Implement controller class, use to `http`
4. AutoMapper &rarr; Realize internal and external mapping (prevent data exposure)
5. CROS &rarr; Solve `CROS` problem

## Quick Start
* Automatic deploy  
**Use `git` to run the following command:**
    ```sh
        ./quickDeploy.sh
    ```
* Step by step manual deploy
    * Step 1: Set up DB
        * Related information
            ```sh
                Server=localhost;
                Port=13306;
                Database=selCourse_db;
                User=root;
                Password=123456
            ```
            * methods:
                * manual
                * docker(This program used `docker swarm` to set up)
        * Step 2: Run
        ```sh
            cd webApi
            dotnet build
            dotnet run
        ```
## test
* test Items:
    * GET &rarr; Get data from server
    * POST &rarr; Add data to server
    * PUT &rarr; Modify data
    * PATCH &rarr; Mordify partial data
    * DELETE &rarr; Delete data from server
* tool:  
    Use Postman to test each interface
    * Preparation
        1. Add User  
        ![alt](./imgs/add.png)
        2. Get Token(authentication)
        ![alt](./imgs/auth.png)

    * test `Get`:  
        ![alt](./imgs/get.png)
