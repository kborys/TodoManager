# TodoManager

An Angular Kanban-style todo-list management application that utilizates .NET WebApi.
Purpose of the project is purely educational.

## Implemented API features

#### 1. Authentication using JWT

#### 2. User CRUD operations

#### 3. Group operations

###### - Group CRUD operations

###### - Add/Remove a group member

#### 4. Todo operations

###### - Create a todo

###### - Get a todo by id

###### - Update todo status

## Implemented Angular features

#### 1. Authentication

###### - Login/logout using guards

###### - User registration

#### 2. Groups

###### - View your groups

###### - View group members and add/remove them

#### 3. Todos

###### - View todos by group and todo status

###### - Create new todos

###### - Change status with drag-and-drop

## Technologies used:

- Angular, Angular Material CDK, RxJS
- Bootstrap, ngBootstrap
- .NET 6.0 WebAPI documented with SWAGGER
- JWT auth
- Dapper for db communication
- MSSQL

## How to run

1. Publish the TodoManager.SqlDb
2. Fill in TodoManager.Api user secrets with your db connection string according to appsettings.json
3. Use the npm install in Angular project
