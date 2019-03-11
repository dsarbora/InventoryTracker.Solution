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


-----

**recipe_ingredient:**

|id PRIMARY KEY++|recipe_id INT|ingredient_id INT|
|------:|:-------------:|-----------------|

------

**ingredient_shipment**

|id PRIMARY KEY++|ingredient_id|shipment_id|
|-----------------|------|----|


-----

**recipes**

|id PRIMARY KEY++|name|
|-----|------|

------

**shipments**

|id PRIMARY KEY++|date|
|--------------------|-----|


-----

**maint_items**

|id PRIMARY KEY++|name|last_date|
|------------|---------|----|

-----

**employee_maintenance**

|id PRIMARY KEY++|employee_id|maintenance_id|
|-----|-----|------|

-----

**employees**

|id PRIMARY KEY++|name|hired_date|
|--------------|----|-----|


-----

**employee_position**

|id PRIMARY KEY++|employee_id|position_id|
|-------|-------|------|

-----

**positions**

|id PRIMARY KEY++|name|
|----|----|

-----
