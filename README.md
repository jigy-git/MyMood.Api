## MyMood Backend API

## Overview
This is the backend for the MyMood web application. The API is designed to handle mood submissions and provide various reports (daily, weekly, monthly mood ratings) to admins. This project follows the Onion Architecture and implements the CQRS (Command Query Responsibility Segregation) pattern to ensure clean separation of concerns, maintainability, and scalability.

In this project, Swagger is used for quick demo purposes, and user accounts are created via Swagger using BCrypt for password encoding. In the production environment, authentication will be handled using Okta or Entra for secure authorization.

## Project Structure
The backend follows `Onion Architecture`, which divides the application into distinct layers that interact with each other in a way that promotes separation of concerns and reduces coupling. The core of the project follows a `CQRS pattern`.

Here's a brief explanation of each layer:
1. Web API Layer (Presentation Layer)
Purpose: The Web API Layer exposes the application's functionalities via HTTP endpoints. It handles HTTP requests and returns appropriate responses. 
Contents: Controllers, Action Filters, Validation
2. Application Layer
Purpose: The Application Layer coordinates/orchestrates the application logic and handles the use cases by using the domain models and interacting with the data persistence layer.
Contents: CQRS Handlers, Mappers from domain/infrastructure layer to external responses
3. Core Layer (Domain Layer)
Purpose: This , containing all the domain models and business logic. It is independent of any external libraries or frameworks.
Contents: Classes with business rules only
4. Infrastructure Layer
Purpose: The Infrastructure Layer contains all the code that interacts with external systems such as databases, APIs, file storage, etc.
Contents: Repositories, External Services like email notifications, third-party integrations, etc.

## Debugging/Running the application
Swagger is enabled for quick demo purposes, allowing easy testing of API endpoints without requiring user login or authentication.

Please note:
------------
-> You will need to create database & data to run this application. 
-> SQL.Md has all SQLs needed to bring up the database 
-> Appsettings need to be updated for this SQL connection string
-> Please use Swagger to Create Users. 
-> You have to specify "admin" as role for system to understand it is admin
(This user creation feature is strictly for demo purposes and should not be used in production.)
-> There is CreateData.SQL used to generate some user mood data for testing

The password are hashed using BCrypt before being stored in the database. 
Production environment have authentication handled using Okta or Entra.


# PROBLEMS
-----------
1. No authentication/authorization checks to access the API
2. The logic when 2 Moods have exactly same entries within a day or week or month not clear
3. Stream of comments not implemented 


