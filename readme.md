# Requirements
- Build and run scripts are provided for Powershell. A **Windows** host with **Powershell** scripting enabled needed to execute these scripts.
- A **Docker** deamon to prepare and run containers.
- Current configuration listens on ports **6379 (redis)** and **5000 (WebApi)**, so these ports should be available.

# Running
1. Execute *build.ps1* to build the project and create a Docker containers.
2. Execute *run.ps1* to start the docker containers.
2. Execute *clean.ps1* to stop and remove docker containers

# Unit and Integration Tests
- Automated tests are written with [xUnit](https://xunit.net/). You can use any kind of runner to execute them.
(VS runners already included)

# End-to-End Tests
- See Swagger documentation at: http://localhost:5000
- You can use any kind of HTTP client software (e.g. *Postman*)

# Remarks
- Build script creates two docker containers named: **cart-api** and **redis-api**.
 Make sure there are no docker containers with the same names.
- If the containers exists, *build.ps1* **will not** create new containers and give failure.
Make sure to execute *clean.ps1* between successive builds.
