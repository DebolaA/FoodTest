# FoodTest
QA Food Testing

A simple web application for submitting food review.

the application consist of 3 projects as follows:

Project 1 - QAFoodDb which contains the database context and objects. the database is created by running the Update-Database command on the PM Console.
Project 2 - QAFoodApi list all our apis that feed the main app from the database.
Project 3 - QAFood is the web app that allows users to submit, view and modify their review.

note: to use the application, set the sql database server connection string to what will be revelant to you in the Project 1 and Project 2
to test the app; start two instances of visual studio. 
run project 2 on one instance of the visual studio and run project 3 on the other.

