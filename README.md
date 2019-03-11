# InventoryTracker.Solution
To help a restaurant keep track of business

## Specifications

#### DATABASES

* restaurant
* restaurant_Test

Each class has is represented in the database by a table, and linked to others through join tables.

restaurant and restaurant_test are identical in structure.


----
## Tables

**ingredients:**

|id PRIMARY KEY++|name VARCHAR|expiration|
|------:|----------|--------------------------|
|0      |Spinach   | 3/17/2019 |
|1      |Potatoes  | 3/31/2019|
|2      |Meat      | 3/17/2019|

-----

**recipe_ingredient:**

|id PRIMARY KEY++|recipe_id INT|ingredient_id INT|
|------:|:-------------:|-----------------|
|0|3|1|
|1|3|2|
|2|1|0|
|3|1|2|

------

**ingredient_shipment**

|id PRIMARY KEY++|ingredient_id|shipment_id|
|-----------------|------|----|
|0|0|0|
|0|1|0|
|0|2|0|

-----

**recipes**

|id PRIMARY KEY++|name|
|-----|------|
|0| Boiled Eggs|
|1| Spinach and meat|
|2| Ham Salad|
|3| Meat and potatoes|

------

**shipments**

|id PRIMARY KEY++|date|
|--------------------|-----|
|0|3/10/2019|

-----

**maint_items**

|id PRIMARY KEY++|name|last_date|
|------------|---------|----|
|0|Wash dishes|3/10/2019|

-----

**employee_maintenance**

|id PRIMARY KEY++|employee_id|maintenance_id|
|-----|-----|------|
|0|1|0|

-----

**employees**

|id PRIMARY KEY++|name|hired_date|
|--------------|----|-----|
|0 | Mario Batali|3/10/2019|
|1|Guy Fieri|3/10/2019|
|2|Gordon Ramsey|3/10/2019|

-----

**employee_position**

|id PRIMARY KEY++|employee_id|position_id|
|-------|-------|------|
|0|0|0|
|1|1|1|
|2|2|2|

-----

**positions**

|id PRIMARY KEY++|name|
|----|----|
|0|Meatball maker|
|1|Dishwasher|
|2|Chef|

-----
