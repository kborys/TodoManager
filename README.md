# TodoManager
Plan, organize and do what you have to do.

## Why do I do it?
Practice C# and SQL, learn the fundamentals of software development, .NET and related technologies.

## Implemented API features
#### 1. Authentication using JWT
#### 2. User CRUD operations
#### 3. Group operations
###### - Create a group. Assigns membership and ownership to the currently authenticated user.
###### - Get all groups of currently authenticated user.
###### - Get a group by its id only if you are a member of that group.
###### - Add user to a group using username. You can only add a member if you are the owner.

## What are my goals?
Create a Blazor Server app that manages Todos and utilizates my WebAPI.

## DB Schema
Here you can see the actual [Database Schema](https://dbdiagram.io/d/634a9a8cf0018a1c5f0cfe88).

## Technologies used:
- .NET 6.0
- SWAGGER
- Dapper
- JWT
- MSSQL

## How to run
1. Publish the TodoManager.SqlDb
2. Fill in TodoManager.Api user secrets with your db connection string according to appsettings.json
  - Authentication infos are temporarly stored in appsettings.Development.json, you don't need to change it.