# DistanceCalculator

A simple web application written in .Net Core

## The units

1. WebAPP
    - Dotnet core MVC web application
2. WebAPI
    - Dotnet core API
3. GeoService
    - Service to calculate distance between 2 geo-points
4. Core
    - Core of system. Currently Database layer is only component.

## What is used

1. NServiceBus:
    - NService with RabbitMQ used together
2. RabbitMQ:
    - Management UI of RabbitMQ let us to monitor and manage the transferring messages between service/components
3. DatabaseLayer:
    - Custom In-Memory database
4. WebAPI:
    - Auth and Geo controller
5. Swagger:
    - http://localhost:4500/swagger/index.html
6. Collection of postman
