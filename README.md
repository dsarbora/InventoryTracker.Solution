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

|id PRIMARY KEY++|name VARCHAR|expiration DATE|
|------:|----------|--------------------------|


-----

**recipe_ingredient:**

|id PRIMARY KEY++|recipe_id INT|ingredient_id INT|
|------:|:-------------:|-----------------|

------

**ingredient_shipment**

|id PRIMARY KEY++|ingredient_id INT|shipment_id INT|
|-----------------|------|----|


-----

**recipes**

|id PRIMARY KEY++|name VARCHAR|
|-----|------|

------

**shipments**

|id PRIMARY KEY++|date DATE|
|--------------------|-----|


-----

**maint_items**

|id PRIMARY KEY++|name VARCHAR|last_date DATE|
|------------|---------|----|

-----

**employee_maintenance**

|id PRIMARY KEY++|employee_id INT|maintenance_id INT|
|-----|-----|------|

-----

**employees**

|id PRIMARY KEY++|name VARCHAR|hired_date DATE|
|--------------|----|-----|


-----

**employee_position**

|id PRIMARY KEY++|employee_id INT|position_id INT|
|-------|-------|------|

-----

**positions**

|id PRIMARY KEY++|name VARCHAR|
|----|----|

-----
