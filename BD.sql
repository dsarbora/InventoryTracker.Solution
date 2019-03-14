CREATE DATABASE restaurant;
USE restaurant;
CREATE TABLE ingredients (id serial PRIMARY KEY, name VARCHAR(255), quantity INT);
CREATE TABLE dishes (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE ingredients_dishes (id serial PRIMARY KEY, ingredient_id INT, dish_id INT, ingredient_quantity INT);

CREATE TABLE shipments (id serial PRIMARY KEY, shipment_date date);
CREATE TABLE ingredients_shipments (id serial PRIMARY KEY, ingredient_id INT, shipment_id INT, quantity INT);

-- updated -- CREATE TABLE orders (id serial PRIMARY KEY, dish_id INT, quantity INT, order_date DATE);
-- DROP TABLE orders;
CREATE TABLE table_orders (id serial PRIMARY KEY, table_number VARCHAR(10), order_date DATE);
-- new --
-- update : add dish_quantity INT --
-- ALTER TABLE `orders` ADD `dish_quantity` INT NOT NULL AFTER `dish_id`;
CREATE TABLE orders (id serial PRIMARY KEY, table_order_id int, dish_id int, dish_quantity INT);



-- DON'T RUN : THAT'S ONLY FOR TESTING ------

--------------- ORDERS -----------
-- On insert
INSERT INTO orders...;

UPDATE ing SET ing.quantity = ing.quantity - ord.total
FROM ingredients ing JOIN 
(SELECT i_d.ingredient_id AS ingredient_id, i_d.quantity * dish_quantity AS total 
FROM ingredients_dishes i_d JOIN orders ON i_d.dish_id=orders.dish_id 
WHERE table_order_id=@tableOrderId and orders.dish_id=@dish_id) ord 
ON ing.id=ord.ingredient_id

-- On update and delete
UPDATE ing SET ing.quantity = ing.quantity - ord.total
FROM ingredients ing JOIN 
(SELECT i_d.ingredient_id AS ingredient_id, i_d.quantity * (@newQuantity - dish_quantity) AS total 
FROM ingredients_dishes i_d JOIN orders ON i_d.dish_id=orders.dish_id 
WHERE table_order_id=@tableOrderId and orders.dish_id=@dish_id) ord 
ON ing.id=ord.ingredient_id

UPDATE / DELETE orders...;

-------------- SHIPMENTS ------------

-- On insert
UPDATE ing SET ing.quantity = ing.quantity + i_s.quantity FROM 







UPDATE 
ingredients ing INNER JOIN 
(SELECT i_d.ingredient_id AS ingredient_id, i_d.ingredient_quantity * (@newQuantity - dish_quantity) AS total 
FROM ingredients_dishes i_d JOIN orders ON i_d.dish_id=orders.dish_id 
WHERE table_order_id=@id and orders.dish_id=@dish_id) ord 
ON ing.id=ord.ingredient_id
SET ing.quantity = ing.quantity - ord.total;

UPDATE ingredients ing INNER JOIN (SELECT i_d.ingredient_id AS ingredient_id, i_d.ingredient_quantity * (@newQuantity - dish_quantity) AS total FROM ingredients_dishes i_d JOIN orders ON i_d.dish_id=orders.dish_id WHERE table_order_id=@id and orders.dish_id=@dish_id) ord ON ing.id=ord.ingredient_id SET ing.quantity = ing.quantity - ord.total;