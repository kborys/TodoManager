# TodoManager

Plan, organize and do what you have to do.

## Purpose of the project

Practice C# and SQL, learn the fundamentals of software development, .NET, Angular and related technologies.

## Implemented API features

#### 1. Authentication using JWT

#### 2. User CRUD operations

#### 3. Group operations

###### - Group CRUD operations

###### - Assign a user as a group member

#### 4. Todo operations

###### - Create a todo

###### - Get a todo by id

## Implemented Angular features

#### 1. Authentication

###### - Login/logout using guards

###### - User registration

#### 2. Groups

###### - View your groups

#### 3. Todos

###### - View todos by group and todo status

###### - Create new todos

## What is the goal?

Create an Angular Kanban-style todo-list management applicaiton that utilizates my .NET WebApi.

## Technologies used:

- Angular, RxJS
- Bootstrap, ngBootstrap
- .NET 6.0
- SWAGGER
- Dapper
- JWT
- MSSQL

## How to run

1. Publish the TodoManager.SqlDb
2. Fill in TodoManager.Api user secrets with your db connection string according to appsettings.json

- Authentication infos are temporarly stored in appsettings.Development.json and don't need to be changed.
