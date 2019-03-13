CREATE DATABASE restaurant;
USE restaurant;
CREATE TABLE ingredients (id serial PRIMARY KEY, name VARCHAR(255), quantity INT);
CREATE TABLE dishes (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE ingredients_dishes (id serial PRIMARY KEY, ingredient_id INT, dish_id INT, ingredient_quantity INT);

CREATE TABLE shipments (id serial PRIMARY KEY, shipment_date date);
CREATE TABLE ingredients_shipments (id serial PRIMARY KEY, ingredient_id INT, shipment_id INT, quantity INT);

-- updated -- CREATE TABLE orders (id serial PRIMARY KEY, dish_id INT, quantity INT, order_date DATE);
DROP TABLE orders;
CREATE TABLE table_orders (id serial PRIMARY KEY, table_number VARCHAR(10), order_date DATE);
-- new --
-- update : add dish_quantity INT --
-- ALTER TABLE `orders` ADD `dish_quantity` INT NOT NULL AFTER `dish_id`;
CREATE TABLE orders (id serial PRIMARY KEY, table_order_id int, dish_id int, dish_quantity INT);