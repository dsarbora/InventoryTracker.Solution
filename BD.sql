CREATE DATABASE restaurant;
USE restaurant;
CREATE TABLE ingredients (id serial PRIMARY KEY, name VARCHAR(255), quantity INT);
CREATE TABLE dishes (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE ingredients_dishes (id serial PRIMARY KEY, ingredient_id INT, dish_id INT, ingredient_quantity INT);
CREATE TABLE orders (id serial PRIMARY KEY, dish_id INT, quantity INT, order_date DATE);
CREATE TABLE shipments (id serial PRIMARY KEY, shipment_date date);
CREATE TABLE ingredients_shipments (id serial PRIMARY KEY, ingredient_id INT, shipment_id INT, quantity INT);

