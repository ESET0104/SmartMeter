## Daily Report
 
Team Name: Nexus
Project Name: SmartMeterWeb
Team Member: Surendra Melam
Date: 28 - 09
Task/Tasks: 
	1. Added Entities for the SmartMeter project
	2.planned tasks and split works and created branches for the task  ,understood merge conflicts

description:authentication and authorization is done for own project in the morning
Time Consumed:4 hrs


## db data  USer
{
  "userName": "surendra",
  "displayName": "surendra",
  "email": "surendra@gmail.com",
  "phone": "7386152390",
  "password": "surendra",
  "orgUnitId": 1,
  "tariffId": 1,
  "role": "Consumer"
}




## login for user
{
 
  "usernameOrEmail": "msurendra.nitw@gmail.com",
  "password": "surendra",
  "role": "User"
}



-------------------------

### Consumer
{
  "userName": "surendra",
  "displayName": "surendra",
  "email": "msurendra.nitw@gmail.com",
  "phone": "7386152390",
  "password": "surendra",
  "orgUnitId": 1,
  "tariffId": 1,     
  "role": "Consumer"
}

## login for consumer 
{
  "usernameOrEmail": "msurendra.nitw@gmail.com",
  "password": "surendra",
  "role": "Consumer"   
}

## customer care 

 {
    "consumerId": 0,
    "name": "surendra",
    "phone": "7386152390",
    "message": "consumer to user message "
  }



  ## dotnet migration error in powerahell 
dotnet ef database drop
dotnet ef database update
dotnet ef migrations add YourMigrationName
dotnet build
dotnet ef database update
dotnet build



## update tarrif by user id 1
{
  "name": "Residential Basic Updated",
  "baseRate": 95,
  "taxRate": 0.10,
  "effectiveFrom": "2024-01-01",
  "effectiveTo": null
}


## update ToD rule id 1 
{
  "name": "Peak Hours Updated",
  "startTime": "18:30:00",
  "endTime": "22:00:00",
  "ratePerKwh": 11.25,
  "tariffId": 2
}


## to update slab rates
{
  "fromKwh": 0,
  "toKwh": 120,
  "ratePerKwh": 5.25,
  "tariffId": 1
}


## to generate bill json fromat
{
  "consumerId": 1,
  "meterId": "GN24A00187",
  "year": 2025,
  "month": 8
}



## to kill proces  
taskkill /IM SmartMeterWeb.exe /F




### daily  report 


Daily Report
 Team Name: Nexus
 Project Name: SmartMeterWeb 
Team Member: Surendra Melam 
Date: 03-11
Task/Tasks: 
 1.fixed email functionality -- working
2. adding method to generate a pdf for bill with bill id and download the pdf 
Time Consumed: 3 hrs. 
-----------


# customer reply entity added 
{
  "consumerID": 1,
  "messageText": "we will take care of it "
}

## What is Middleware?

Think of middleware as a chain of filters or pipes that every HTTP request and response passes through.

When a request hits your backend, ASP.NET Core doesn’t directly jump into your controller — it passes through a series of middleware components first.

Each middleware can:

Look at the request

Do something (like logging, auth, error catching, etc.)

Optionally pass it to the next middleware in the pipeline


## Api stnadardzation 

## Standard API Response Structure
# Success Response
{
  "success": true,
  "message": "Tariff updated successfully",
  "data": {
    "tariffId": 1,
    "name": "Residential",
    "baseRate": 5.5
  },
  "time": "2025-11-10T20:55:00Z"
}

# Error Response
{
  "success": false,
  "message": "Tariff not found",
  "data": null,
  "time": "2025-11-10T20:55:00Z"
}

# next commit 
added looger for few services
# after implementing base controller


TariffController → BaseController → ControllerBase


tarrif controll
customercare cotroller
consumercontroller
reportcontroller



| Field       | Type       | Purpose                                                                                                                        |
| ----------- | ---------- | ------------------------------------------------------------------------------------------------------------------------------ |
| **success** | `bool`     | Tells frontend if the operation worked or failed — *regardless of HTTP code*. Example: `true` for 200, `false` for 404 or 500. |
| **message** | `string`   | Human-readable message. Explains what happened (“Created successfully”, “Not found”, “Validation failed”).                     |
| **data**    | `T?`       | The actual response payload — an object, list, or null (if you just want to return a message).                                 |
| **errors**  | `object?`  | Optional field for validation or exception details. Usually null unless something goes wrong.                                  |
| **time**    | `DateTime` | Timestamp of the response (helps logging / debugging).                                                                         |

3️⃣ Why sometimes you see Success<object>

Sometimes, when you don’t have specific data to send, but still want to return a success message, you can call:

return Success<object>(null, "Operation completed successfully.");


Here:

object means: “I’m not returning any specific type.”

null means: “There’s no actual data in this response


Why we need it

### Without global handling:

Every controller or service might need its own try-catch.

Repeated error logging and response formatting.

Harder to maintain and debug.

### With global handling:

One single place for catching all exceptions.

Unified and clean error responses.

Automatic logging.

Easier debugging and consistent API responses

### Global Exception Handling Middleware

Every incoming request passes through here.

_next(context) calls the next middleware or controller.

If no exception occurs → it moves normally.

If something fails → it jumps to the catch blocks.


## How it works

          Client Request
               ↓
 ErrorHandlingMiddleware
    ├── Catches exceptions
    ├── Handles validation errors
    └── Returns ApiResponse (error form)
               ↓
 Controller (inheriting BaseController)
    ├── Uses Success() or Error()
    └── Returns ApiResponse (success form)
               ↓
 Standard JSON sent to client


 ## to get files directory

 tree "D:\SmartMeter" /A /F | findstr /V /R "^[A-Z]:\\\|\. "
tree "D:\SmartMeter" /A /F | findstr /V /R "^[A-Z]:\\\|\. " > folder_structure.txt


