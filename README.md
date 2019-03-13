# InventoryTracker.Solution
To help a restaurant keep track of business

## Specifications

#### SITE LAYOUT

**Homepage**

The homepage of the site gives access to each subsection. The links wrap around "well" HTML classes and contain text to let the user know what content lies ahead.

_Each route has the following structure, and the following will be written using the "order" route as an example_

**New**

A page designed to recieve an order. Here a user can input the ingredients they recieved along with a quantity, and the program will output a corresponding change in the database.

**Show**

Shows the details of a specific order. It will show the order ID, the date recieved, which ingredients were in the order, and how many of each ingredient were recieved.

**Edit**

This is where a user can edit the various data that show up on the "show" page. This page is a form containing the same input fields as the "new" page, but it will modify an existing order.


#### CLASSES

The data for this program are stored and manipulated using several object classes. These classes each contain a set of methods to allow them to interact with the databases. The object classes found in this program are:

* Dish

_A dish contains the name and ID of a specific menu item._

* Ingredient

_An ingredient has an ID and a name and represents a single type of food, whether in a dish, in a shipment, or in the inventory._

* Shipment

_A shipment contains a unique ID identifier and a date representing when that shipment was recieved._

* TableOrder

_A TableOrder represents a grouped order placed by guests at the same table. It contains its own ID, a Dish ID, and a table number._

#### OTHER CLASSES

In addition to the object classes, this program also contains unique classes used to store data. Each of these objects contains a single object and a number representing a quantity. These classes are called:

* DishQuantity

_For storing the number of dishes ordered by a group of people sitting at the same table._

* IngredientQuantity

_For storing the amount of each ingredient in a shipment._

#### DATABASES

* restaurant
* restaurant_Test

Each class has is represented in the database by a table, and linked to others through join tables.

restaurant and restaurant_test are identical in structure.


----
## Tables
**Primary Tables**

|dishes|ingredients|shipments|table_orders|
|------|-----------|---------|------------|
|id    |id         |id       |id          |
|name  |name       |date     |table_number|
|      |           |         |order_date  |

**Join tables**

|orders|ingredients_dishes|ingredients_shipments|
|------|------------------|---------------------|
|id    |id                |id                   |
|table_order_id|ingredient_id|ingredient_id     | 
|dish_id|dish_id          |shipment_id          |
|      |ingredient_quantity|quantity


-----

## _Setup/Installation Requirements_
**Setup**
----
* Download .NET Core 1.1.4 SDK and .NET Core Runtime 1.1.2 and install.
Download Mono and install.

* Download and install MAMP.

* Open the Control Panel and visit System > Advanced System Settings > Environment Variables...

* Then select PATH..., click Edit..., then Add.

* Add the exact location of your MySQL installation, as mentioned in step two above, and click OK. (This location is likely C:\MAMP\bin\mysql\bin, but may differ depending on your specific installation.)

* If you receive error stating the command mySQL is "not recognized", the location you provided is likely inaccurate. Double-check it and try again.

**Running the project**
----

* _Clone the project from [https://github.com/dsarbora/HairSalon.Solution](https://github.com/dsarbora/HairSalon.Solution)
* _Navigate in the command line to HairSalon.Solution/HairSalon.Test/_
* _Use the command **$dotnet restore** to import the packages used for this project._
* _To run the tests, use the command **$dotnet test.**_
* _To run the program, navigate in the command line to HairSalon.Solution/HairSalon._
* _If not not previously restored, use the command **$dotnet restore,** followed by **$dotnet build,** and then **$dotnet run** to run the program in browser.
* _Unzip the databases._
* _Navigate to http://localhost:5000/ in your web browser._
## _Technologies Used_
* _C#_
* _HTML & CSS_
* _Bootstrap_
* _MAMP_
* _Visual Studio Code_
* _Microsoft ASP.NET Core_
* _MSTest_

### _License_

*MIT*

Copyright (c) 2019 **_Yulia ShidLovskaya and Dave Sarbora_**

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.