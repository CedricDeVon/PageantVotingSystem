CREATE DATABASE demo_development;
CREATE DATABASE demo_testing;
CREATE DATABASE demo_production;

USE demo_development;
CREATE TABLE user
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    age INT
);

USE demo_testing;
CREATE TABLE user
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    age INT
);

USE demo_production;
CREATE TABLE user
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    age INT
);

INSERT INTO demo_development.user VALUES(100, "John Smith", 21);

DELETE FROM demo_development.user
WHERE id = 10;

SELECT * FROM demo_development.user WHERE true;
SELECT * FROM demo_testing.user;
SELECT * FROM demo_production.user;