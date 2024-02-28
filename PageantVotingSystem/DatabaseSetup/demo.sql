DELETE FROM user;
INSERT INTO user (full_name, age) VALUES ('X', 1);
INSERT INTO user (full_name, age) VALUES ('Y', 2);
INSERT INTO user (full_name, age) VALUES ('Z', 3);
UPDATE user SET full_name = 'eh' WHERE age = 1;
SELECT * FROM user ORDER BY id DESC LIMIT 100;