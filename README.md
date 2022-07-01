
# MyTrucks
Application made for work optimalization in Transport industry and  for class project. :)

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Screenshots](#screenshots)
* [Prerequisites](#setup)
* [Setup](#setup)
* [Usage](#usage)
* [Contact](#contact)



## General Information
- Application is easy for usage, but with rich features.
- Nowadays we all need to have everything optimized, that's why we have invented our app.
- Application was creates as a Project for our classes.
<!-- You don't have to answer all the questions - just the ones relevant to your project. -->


## Technologies Used
- Application is writted in C# in WPF XAML
- Microsoft.EntityFrameworkCore.SQLServer 5.0.17



## Features
Most of the important features:
- Add and manage your trucks (add repairs, set end of insurance date, etc) 
- Adding new login accounts 
- Add and manage your workers
- You can manage database with MyTruck application


## Sample Screenshots
<a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/Kj3QkMqC/1.png" alt="1"/></a><br/><br/>
<a href="https://postimages.org/" target="_blank"><img src="https://i.postimg.cc/3WM1gxzN/2.png" alt="2"/></a><br/><br/>
<a href='https://postimages.org/' target='_blank'><img src='https://i.postimg.cc/Z5NR7K2Z/3.png' border='0' alt='3'/></a>
<!-- If you have screenshots you'd like to share, include them here. -->


## Prerequisites

You need the following tools in order to edit the solution.

-   Microsoft Visual Studio (Latest recommended)
    
-   Microsoft SQL Server (Latest recommended)

And to just Run it you will need:
-   Microsoft SQL Server  (Latest recommended)

- .Net 5.0.7 Desktop Runtime


## Setup

The application requires a database to store the data. Follow the below step to setup database.

-  Please run script called "[createtrans.sql](https://github.com/dawidtyburski/lab_projekt/blob/master/lab_projekt/createtrans.sql "createtrans.sql")", this will create database required for this application.

After that please run project/application via Visual studio or simply with .exe file located in [AppExe](https://github.com/dawidtyburski/lab_projekt/tree/master/lab_projekt/AppExe "AppExe") folder (please copy whole folder, as it contains all .dll required to run Application).

Also there's installation file located in [Installation file](https://github.com/dawidtyburski/lab_projekt/tree/master/lab_projekt/Installation%20file "Installation file") folder. 
    

## Usage
For tests we have created an admin account, please use it for further tests.
login: admin
password: admin

After login at top you can see 4 options (Odswiez, Dodaj pojazd, Dodaj kierowce, Zamknij).
In table rows you can see all cars you have added, and after right click on that you can view more details of this car ("Informacje") and manage a list of repairs of this car etc.


## Contact
Created by [@dawidtyburski](https://github.com/dawidtyburski) and [@zaeroxik](https://github.com/zaeroxik) - feel free to contact us!


<!-- Optional -->
<!-- ## License -->
<!-- This project is open source and available under the [... License](). -->

<!-- You don't have to include all sections - just the one's relevant to your project -->
