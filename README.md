# Technologies, IDE and Instructions for setup

## Tech Stack

* SQL (Database)
* React Typescript (UI)
* Net API- C#
* git

## IDE
* Rider (UI and API)
* SQL Server Management Studio (SSMS)
* Visual Studio (Generate Universe base testing structure)

## Setup

* Create database in SSMS and use Sql File to create tables and populate the tables
* Open API in Rider.
* Change connection string to name of database (in the appsettings.Development.json)
* Make sure run configuration is set to ProjectAPI then run the program. You should get a webpage that shows the API swagger UI.
* Open UI folder terminal.
* Run "npm install" command.
* Run "npm start" command to start the UI

## Tests

* Open API in Rider
* For safety sake (in case data has been cleaned) drop all tables from the database created and re-run the sql script to get fresh data
* Under the build menu; select the build solution option
* Once the build id completed, under the tests menu. Select run all tests in solution

## How It Works

* Create a user to login
* Once logged in; select products from either home page (by clicking on product and giving an amount)
* Or through the products page
* Once done you can checkout by clicking the cart button in the header.
* Only Admin types can edit create or update products
* To Create an admin user (create a normal user then in SSMS change their userrole to 0)
