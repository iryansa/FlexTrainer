# FlexTrainer
FlexTrainer is a C# Winforms application developed in Visual Studio and use MS SQL as backend.
The application is an implementation of Gym Management and Fitness tracking system in which different types of users like Gym members, trainers, gym owners, and administrators can join the available gyms, get approval, create or follow existing workout and diet plans, book trainers, etc.

## Before Running
Remember to create all the necessary tables and triggers using the FlexTrainerTables.sql file.
Also, You need to first insert data of the admins into the users table manually, otherwise it could cause problems. 

## How to run
If you want to run the application, then run the FlexTrainer.exe file in the bin/Debug forlder. If you want to run the solution then you should run the FlexTrainer.sln file (Visual Studio solution file). After opening the solution in the Microsoft Visual Studio, copy and paste your connection string to all the cs files where it is needed (Comments are added where it is needed).

## Problems you may face
While copying and pasting the connection string, if the last attribute is `Trust Server Certificate = true`, then remove the spaces between it to make it look like: `TrustServerCertificate=true`. Also, While running the application, do not enter single quotation mark `'` into the textboxes, as it may cause exceptions while submiting. 
