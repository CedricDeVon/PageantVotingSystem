
DROP DATABASE IF EXISTS pageant_voting_system_development;
CREATE DATABASE IF NOT EXISTS pageant_voting_system_development;
USE pageant_voting_system_development;

CREATE TABLE IF NOT EXISTS resource
(
    path VARCHAR(512) PRIMARY KEY,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS judge_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS contestant_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS round_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS type_constraint
(
    name VARCHAR(64) PRIMARY KEY,
    value VARCHAR(256) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS result_remark
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS user_role
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS marital_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS gender
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS scoring_system
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event_layout_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS round_contestant_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS contestant
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    order_number INT UNSIGNED NOT NULL,
    height_in_centimeters FLOAT DEFAULT 0,
    weight_in_kilograms FLOAT DEFAULT 0,
    birth_date DATETIME DEFAULT NOW(),
    email TEXT,
    phone_number TEXT,
    motto TEXT,
    home_address TEXT,
    talents_and_skills TEXT,
    hobbies TEXT,
    languages TEXT,
    work_experiences TEXT,
    education TEXT,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    contestant_status_type VARCHAR(64) NOT NULL,
    marital_status_type VARCHAR(64) NOT NULL,
    gender_type VARCHAR(64) NOT NULL,
    image_resource_path VARCHAR(512)
);

CREATE TABLE IF NOT EXISTS user
(
    email VARCHAR(128) PRIMARY KEY,
    full_name VARCHAR(128) NOT NULL,
    password VARCHAR(128) NOT NULL,
    description TEXT,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    user_role_type VARCHAR(64) NOT NULL,
    image_resource_path VARCHAR(512)
);

CREATE TABLE IF NOT EXISTS event
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
    host_address TEXT,
    datetime_start DATETIME,
    datetime_end DATETIME,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    scoring_system_type VARCHAR(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS segment
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
    maximum_contestant_count INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    event_id INT UNSIGNED NOT NULL
);

CREATE TABLE IF NOT EXISTS round
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    segment_id INT UNSIGNED NOT NULL
);

CREATE TABLE IF NOT EXISTS criterium
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
    maximum_value INT UNSIGNED NOT NULL,
    percentage_weight FLOAT NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    round_id INT UNSIGNED NOT NULL
);

CREATE TABLE IF NOT EXISTS event_layout
(
    event_id INT UNSIGNED NOT NULL,
    segment_id INT UNSIGNED NOT NULL,
    round_id INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    event_layout_status_type VARCHAR(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS round_contestant
(
    round_id INT UNSIGNED NOT NULL,
    contestant_id INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    round_contestant_status_type VARCHAR(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS event_manager
(
    event_id INT UNSIGNED PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    manager_user_email VARCHAR(128) NOT NULL
);

CREATE TABLE IF NOT EXISTS event_contestant
(
    event_id INT UNSIGNED NOT NULL,
    contestant_id INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event_judge
(
    event_id INT UNSIGNED NOT NULL,
    judge_user_email VARCHAR(128) NOT NULL,
    order_number INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    judge_status_type VARCHAR(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS result
(
    event_id INT UNSIGNED NOT NULL,
    segment_id INT UNSIGNED NOT NULL,
    round_id INT UNSIGNED NOT NULL,
    criterium_id INT UNSIGNED NOT NULL,
    base_value FLOAT NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    result_remark_type VARCHAR(64),
    contestant_id INT UNSIGNED NOT NULL,
    judge_user_email VARCHAR(128) NOT NULL
);

CREATE TABLE IF NOT EXISTS application_save_state
(
    name VARCHAR(128) PRIMARY KEY,
    value VARCHAR(128),
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS judge_save_state
(
    judge_user_email VARCHAR(128) PRIMARY KEY,
    round_id INT UNSIGNED,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

ALTER TABLE contestant
ADD FOREIGN KEY(contestant_status_type)
REFERENCES contestant_status(type)
ON DELETE CASCADE;
ALTER TABLE contestant
ADD FOREIGN KEY(marital_status_type)
REFERENCES marital_status(type)
ON DELETE CASCADE;
ALTER TABLE contestant
ADD FOREIGN KEY(gender_type)
REFERENCES gender(type)
ON DELETE CASCADE;
ALTER TABLE contestant
ADD FOREIGN KEY(image_resource_path)
REFERENCES resource(path)
ON DELETE SET NULL;

ALTER TABLE user
ADD FOREIGN KEY(user_role_type)
REFERENCES user_role(type)
ON DELETE CASCADE;
ALTER TABLE user
ADD FOREIGN KEY(image_resource_path)
REFERENCES resource(path)
ON DELETE SET NULL;

ALTER TABLE event
ADD FOREIGN KEY(scoring_system_type)
REFERENCES scoring_system(type)
ON DELETE CASCADE;

ALTER TABLE segment
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

ALTER TABLE round
ADD FOREIGN KEY(segment_id)
REFERENCES segment(id)
ON DELETE CASCADE;

ALTER TABLE criterium
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;

ALTER TABLE event_layout
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;
ALTER TABLE event_layout
ADD FOREIGN KEY(segment_id)
REFERENCES segment(id)
ON DELETE CASCADE;
ALTER TABLE event_layout
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;
ALTER TABLE event_layout
ADD FOREIGN KEY(event_layout_status_type)
REFERENCES event_layout_status(type)
ON DELETE CASCADE;

ALTER TABLE round_contestant
ADD PRIMARY KEY(round_id, contestant_id);
ALTER TABLE round_contestant
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;
ALTER TABLE round_contestant
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;
ALTER TABLE round_contestant
ADD FOREIGN KEY(round_contestant_status_type)
REFERENCES round_contestant_status(type)
ON DELETE CASCADE;

ALTER TABLE event_manager
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;
ALTER TABLE event_manager
ADD FOREIGN KEY(manager_user_email)
REFERENCES user(email)
ON DELETE CASCADE;

ALTER TABLE event_contestant
ADD PRIMARY KEY(event_id, contestant_id);
ALTER TABLE event_contestant
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;
ALTER TABLE event_contestant
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;

ALTER TABLE event_judge
ADD PRIMARY KEY(event_id, judge_user_email);
ALTER TABLE event_judge
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;
ALTER TABLE event_judge
ADD FOREIGN KEY(judge_status_type)
REFERENCES judge_status(type)
ON DELETE CASCADE;

ALTER TABLE result
ADD PRIMARY KEY(event_id, segment_id, round_id, criterium_id, contestant_id, judge_user_email);
ALTER TABLE result
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;
ALTER TABLE result
ADD FOREIGN KEY(segment_id)
REFERENCES segment(id)
ON DELETE CASCADE;
ALTER TABLE result
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;
ALTER TABLE result
ADD FOREIGN KEY(criterium_id)
REFERENCES criterium(id)
ON DELETE CASCADE;
ALTER TABLE result
ADD FOREIGN KEY(result_remark_type)
REFERENCES result_remark(type)
ON DELETE CASCADE;
ALTER TABLE result
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;
ALTER TABLE result
ADD FOREIGN KEY(judge_user_email)
REFERENCES user(email)
ON DELETE CASCADE;

ALTER TABLE judge_save_state
ADD FOREIGN KEY(judge_user_email)
REFERENCES user(email)
ON DELETE CASCADE;
ALTER TABLE judge_save_state
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;

INSERT INTO resource (path) VALUES
('../../Profiles/DefaultProfile.png');

INSERT INTO judge_status (type) VALUES ('Present'), ('Abscent');
INSERT INTO contestant_status (type) VALUES ('Qualified'), ('Eliminated'), ('Forfeited');
INSERT INTO scoring_system (type) VALUES ('Percentage'), ('Ranking');
INSERT INTO result_remark (type) VALUES ('Winner'), ('1st Runner-Up'), ('2nd Runner-Up');
INSERT INTO user_role (type) VALUES ('Manager'), ('Judge');
INSERT INTO round_status (type) VALUES ('Incomplete'), ('Complete'), ('Cancelled');
INSERT INTO marital_status (type) VALUES ('Single'), ('Married'), ('Divorced'), ('With Significant Other');
INSERT INTO gender (type) VALUES ('Male'), ('Female'), ('Non-Binary');
INSERT INTO event_layout_status (type) VALUES ('Ongoing'), ('Complete'), ('Incomplete');
INSERT INTO round_contestant_status (type) VALUES ('Complete'), ('Incomplete');
INSERT INTO type_constraint (name, value) VALUES
('minimum_percentage_weight', '0'),
('maximum_percentage_weight', '1'),

('minimum_judge_remark_value', '-1000'),
('maximum_judge_remark_value', '1000'),

('minimum_order_number', '1'),
('maximum_order_number', '1000'),

('minimum_height_in_centimeters', '0'),
('maximum_height_in_centimeters', '1000'),

('minimum_weight_in_kilograms', '0'),
('maximum_weight_in_kilograms', '1000'),

('minimum_text_character_length', '0'),
('maximum_text_character_length', '1024'),

('minimum_email_character_length', '5'),
('maximum_email_character_length', '128'),
('valid_email_characters', 'qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_.@'),

('minimum_person_name_character_length', '1'),
('maximum_person_name_character_length', '128'),
('valid_person_name_characters', 'qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890.,_ '),

('minimum_password_character_length', '8'),
('maximum_password_character_length', '32'),
('valid_password_characters', 'qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890`-=[]\;,.<>?:{}|~!@#$%^&*()_+');

INSERT INTO user (email, full_name, password, user_role_type, description, image_resource_path)
VALUES
    ('manager_1@gmail.com', 'John A. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Manager', 'The official profile of John A. Doe', '../../Profiles/DefaultProfile.png'),
    ('manager_2@gmail.com', 'Jasmine B. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Manager', 'The official profile of Jasmine B. Doe', '../../Profiles/DefaultProfile.png'),
    ('manager_3@gmail.com', 'Johnson C. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Manager', 'The official profile of Johnson C. Doe', '../../Profiles/DefaultProfile.png'),
    ('judge_1@gmail.com', 'James D. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Judge', 'The official profile of James D. Doe', '../../Profiles/DefaultProfile.png'),
    ('judge_2@gmail.com', 'Jessie E. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Judge', 'The official profile of Jessie E. Doe', '../../Profiles/DefaultProfile.png'),
    ('judge_3@gmail.com', 'Jam F. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Judge', 'The official profile of Jam F. Doe', '../../Profiles/DefaultProfile.png');

INSERT INTO event (name, description, host_address, datetime_start, scoring_system_type)
VALUES
    ('Event - A', 'Description - A', 'Host Address - A', NOW(), 'Percentage'),
    ('Event - B', 'Description - B', 'Host Address - B', NOW(), 'Percentage');

INSERT INTO segment (name, description, maximum_contestant_count, event_id)
VALUES
    ('Segment - A', 'Description - A', 5, 1),
    ('Segment - B', 'Description - B', 3, 1),
    
    ('Segment - C', 'Description - C', 3, 2),
    ('Segment - D', 'Description - D', 2, 2);

INSERT INTO round (name, description, segment_id)
VALUES
    ('Round - A', 'Description - A', 1),
    ('Round - B', 'Description - B', 1),
    ('Round - C', 'Description - C', 2),
    
    ('Round - D', 'Description - D', 3),
    ('Round - E', 'Description - E', 3),
    ('Round - F', 'Description - F', 4);
    
INSERT INTO contestant (full_name, order_number, birth_date, contestant_status_type, marital_status_type, gender_type, image_resource_path)
VALUES
    ('Contestant - A', 1, '2000-01-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png'),
    ('Contestant - B', 2, '2002-02-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png'),
    ('Contestant - C', 3, '2003-03-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png'),
    ('Contestant - D', 4, '2001-04-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png'),
    ('Contestant - E', 5, '2002-05-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png'),
    
	('Contestant - F', 1, '2003-06-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png'),
    ('Contestant - G', 2, '2001-07-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png'),
    ('Contestant - H', 3, '2002-08-01', 'Qualified', 'Single', 'Female', '../../Profiles/DefaultProfile.png');

INSERT INTO criterium (name, description, maximum_value, percentage_weight, round_id)
VALUES
    ('Criterium - A', 'Description - A', 100, 50, 1),
    ('Criterium - B', 'Description - B', 100, 30, 1),
    ('Criterium - C', 'Description - C', 100, 20, 1),
    ('Criterium - D', 'Description - D', 100, 60, 2),
    ('Criterium - E', 'Description - E', 100, 40, 2),
    ('Criterium - F', 'Description - F', 100, 100, 3),
    
    ('Criterium - G', 'Description - G', 100, 50, 4),
    ('Criterium - H', 'Description - H', 100, 30, 4),
    ('Criterium - I', 'Description - I', 100, 20, 4),
	('Criterium - J', 'Description - J', 100, 60, 5),
    ('Criterium - K', 'Description - K', 100, 40, 5),
    ('Criterium - L', 'Description - L', 100, 100, 6);

INSERT INTO event_layout (event_id, segment_id, round_id, event_layout_status_type)
VALUES
	(1, 1, 1, 'Complete'),
    (1, 1, 2, 'Complete'),
    (1, 2, 3, 'Complete'),
    
    (2, 3, 4, 'Ongoing'),
    (2, 3, 5, 'Incomplete'),
    (2, 4, 6, 'Incomplete');
    
INSERT INTO round_contestant (round_id, contestant_id, round_contestant_status_type)
VALUES
	(4, 6, 'Incomplete'),
    (4, 7, 'Incomplete'),
    (4, 8, 'Incomplete');

INSERT INTO event_contestant (event_id, contestant_id)
VALUES
    (1, 1),
    (1, 2),
    (1, 3),
    (1, 4),
    (1, 5),
    
    (2, 6),
    (2, 7),
    (2, 8);



INSERT INTO event_manager (event_id, manager_user_email)
VALUES
    (1, 'manager_1@gmail.com'),
    (2, 'manager_2@gmail.com');

INSERT INTO event_judge (event_id, order_number, judge_user_email, judge_status_type)
VALUES
    (1, 1, 'judge_1@gmail.com', 'Present'),
    (1, 2, 'judge_2@gmail.com', 'Present'),
    (1, 3, 'judge_3@gmail.com', 'Present'),
    
    (2, 1, 'judge_1@gmail.com', 'Present');

INSERT INTO result (event_id, segment_id, round_id, criterium_id, contestant_id, judge_user_email, base_value)
VALUES
    (1, 1, 1, 1, 1, 'judge_1@gmail.com', 50), (1, 1, 1, 1, 1, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 1, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 2, 1, 'judge_1@gmail.com', 50), (1, 1, 1, 2, 1, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 1, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 3, 1, 'judge_1@gmail.com', 50), (1, 1, 1, 3, 1, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 1, 'judge_3@gmail.com', 59.5),

    (1, 1, 1, 1, 2, 'judge_1@gmail.com', 60), (1, 1, 1, 1, 2, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 2, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 2, 2, 'judge_1@gmail.com', 60), (1, 1, 1, 2, 2, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 2, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 3, 2, 'judge_1@gmail.com', 60), (1, 1, 1, 3, 2, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 2, 'judge_3@gmail.com', 59.5),

    (1, 1, 1, 1, 3, 'judge_1@gmail.com', 70), (1, 1, 1, 1, 3, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 3, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 2, 3, 'judge_1@gmail.com', 70), (1, 1, 1, 2, 3, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 3, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 3, 3, 'judge_1@gmail.com', 70), (1, 1, 1, 3, 3, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 3, 'judge_3@gmail.com', 59.5),

    (1, 1, 1, 1, 4, 'judge_1@gmail.com', 80), (1, 1, 1, 1, 4, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 4, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 2, 4, 'judge_1@gmail.com', 80), (1, 1, 1, 2, 4, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 4, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 3, 4, 'judge_1@gmail.com', 80), (1, 1, 1, 3, 4, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 4, 'judge_3@gmail.com', 59.5),

    (1, 1, 1, 1, 5, 'judge_1@gmail.com', 90), (1, 1, 1, 1, 5, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 5, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 2, 5, 'judge_1@gmail.com', 90), (1, 1, 1, 2, 5, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 5, 'judge_3@gmail.com', 59.5),
    (1, 1, 1, 3, 5, 'judge_1@gmail.com', 90), (1, 1, 1, 3, 5, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 5, 'judge_3@gmail.com', 59.5),



    (1, 1, 2, 4, 1, 'judge_1@gmail.com', 50), (1, 1, 2, 4, 1, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 1, 'judge_3@gmail.com', 59.5),
    (1, 1, 2, 5, 1, 'judge_1@gmail.com', 50), (1, 1, 2, 5, 1, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 1, 'judge_3@gmail.com', 59.5),

    (1, 1, 2, 4, 2, 'judge_1@gmail.com', 60), (1, 1, 2, 4, 2, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 2, 'judge_3@gmail.com', 59.5),
    (1, 1, 2, 5, 2, 'judge_1@gmail.com', 60), (1, 1, 2, 5, 2, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 2, 'judge_3@gmail.com', 59.5),

    (1, 1, 2, 4, 3, 'judge_1@gmail.com', 70), (1, 1, 2, 4, 3, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 3, 'judge_3@gmail.com', 59.5),
    (1, 1, 2, 5, 3, 'judge_1@gmail.com', 70), (1, 1, 2, 5, 3, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 3, 'judge_3@gmail.com', 59.5),

    (1, 1, 2, 4, 4, 'judge_1@gmail.com', 80), (1, 1, 2, 4, 4, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 4, 'judge_3@gmail.com', 59.5),
    (1, 1, 2, 5, 4, 'judge_1@gmail.com', 80), (1, 1, 2, 5, 4, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 4, 'judge_3@gmail.com', 59.5),

    (1, 1, 2, 4, 5, 'judge_1@gmail.com', 90), (1, 1, 2, 4, 5, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 5, 'judge_3@gmail.com', 59.5),
    (1, 1, 2, 5, 5, 'judge_1@gmail.com', 90), (1, 1, 2, 5, 5, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 5, 'judge_3@gmail.com', 59.5),


    (1, 2, 3, 6, 3, 'judge_1@gmail.com', 70), (1, 2, 3, 6, 3, 'judge_2@gmail.com', 55.85), (1, 2, 3, 6, 3, 'judge_3@gmail.com', 59.5),
    (1, 2, 3, 6, 4, 'judge_1@gmail.com', 70), (1, 2, 3, 6, 4, 'judge_2@gmail.com', 55.85), (1, 2, 3, 6, 4, 'judge_3@gmail.com', 59.5),
    (1, 2, 3, 6, 5, 'judge_1@gmail.com', 70), (1, 2, 3, 6, 5, 'judge_2@gmail.com', 55.85), (1, 2, 3, 6, 5, 'judge_3@gmail.com', 59.5),

	(2, 3, 4, 7, 6, 'judge_1@gmail.com', 0), (2, 3, 4, 8, 6, 'judge_1@gmail.com', 0), (2, 3, 4, 9, 6, 'judge_1@gmail.com', 0),
    (2, 3, 4, 7, 7, 'judge_1@gmail.com', 0), (2, 3, 4, 8, 7, 'judge_1@gmail.com', 0), (2, 3, 4, 9, 7, 'judge_1@gmail.com', 0),
    (2, 3, 4, 7, 8, 'judge_1@gmail.com', 0), (2, 3, 4, 8, 8, 'judge_1@gmail.com', 0), (2, 3, 4, 9, 8, 'judge_1@gmail.com', 0);

-- SELECT contestant.id, contestant.order_number, contestant.full_name
-- FROM
-- (
-- SELECT contestant_id
-- FROM event_layout
-- INNER JOIN round_contestant
-- ON event_layout.round_id = round_contestant.round_id
-- WHERE
-- 	event_id = 2 AND
-- 	round_contestant_status_type = 'Incomplete'
-- ) data
-- INNER JOIN contestant
-- ON contestant.id = data.contestant_id
-- ORDER BY contestant.order_number ASC;

-- SELECT *
-- FROM result
-- WHERE
-- 	event_id = 2 AND
-- judge_user_email = 'judge_1@gmail.com';

-- SELECT *
-- FROM round_contestant;

-- UPDATE round_contestant SET round_contestant_status_type = 'Complete' WHERE round_id = 4 AND contestant_id = 6;
-- UPDATE result SET base_value = 56.78 WHERE criterium_id = 7 AND judge_user_email = 'judge_1@gmail.com' AND contestant_id = 6;

-- USE pageant_voting_system_development;
-- SELECT data.event_id, data.segment_id, data.round_id, criterium.id as criterium_id, name as criterium_name, base_value, maximum_value, percentage_weight
-- FROM
-- (
-- SELECT event_id, segment_id, round_id
-- FROM event_layout
-- WHERE
-- 	event_id = 2 AND
--     event_layout_status_type = 'Ongoing'
-- ) data
-- INNER JOIN result
-- INNER JOIN criterium
-- ON result.round_id = data.round_id AND
-- 	result.criterium_id = criterium.id
-- WHERE
-- 	contestant_id = 6 AND
-- 	judge_user_email = 'judge_1@gmail.com'
-- ORDER BY criterium_id ASC;


-- event_id, contestant_id, judge_email

-- Criterium
-- 	 id
--   name
--   maximum_value

-- Result
--   event_id
--   segment_id
--   round_id
--   criterium_id  
--   base_value



-- SELECT event_id, segment_id, round_id
-- FROM event_layout
-- WHERE
-- 	event_id = 2 AND
-- 	event_layout_status_type = 'Ongoing'



-- SELECT contestant_id, order_number, full_name
-- FROM
-- (
-- SELECT contestant_id
-- FROM event_layout
-- INNER JOIN round_contestant
-- ON event_layout.round_id = round_contestant.round_id
-- WHERE
-- event_id = 2 AND
-- round_contestant_status_type = 'Incomplete'
-- ) data
-- INNER JOIN contestant
-- ON contestant.id = data.contestant_id
-- ORDER BY order_number ASC;

-- SELECT event_id, name
-- FROM
-- (
-- SELECT event_layout.event_id
-- FROM
-- (
-- SELECT event_id
-- FROM event_judge
-- WHERE
-- judge_user_email = 'judge_1@gmail.com' AND
-- judge_status_type = 'Present'
-- ) data
-- INNER JOIN event_layout
-- ON event_layout.event_id = data.event_id
-- WHERE
-- event_layout_status_type = 'Ongoing'
-- ) data
-- INNER JOIN event
-- ON event.id = data.event_id
-- WHERE name LIKE '%Event - B%';



-- USE pageant_voting_system_development;
-- SELECT *
-- FROM event_layout
-- INNER JOIN event
-- INNER JOIN event_judge
-- ON event.id = event_layout.event_id
-- WHERE
--     event_judge.judge_user_email = 'judge_1@gmail.com' AND
--     event_layout.event_layout_status_type = 'Ongoing';
    
    

-- USE pageant_voting_system_development;
-- UPDATE result
-- SET
--     base_value = 79.23
-- WHERE
-- 	event_id = 2 AND
--     segment_id = 3 AND
--     round_id = 4 AND
--     criterium_id = 7 AND
--     contestant_id = 6 AND
-- 	judge_user_email = 'judge_1@gmail.com';
-- UPDATE result
-- SET
-- 	event_layout_status_type = 'Complete'
-- WHERE
-- 	event_id = 2 AND
--     segment_id = 3 AND
--     round_id = 4 AND
--     criterium_id = 7 AND
--     contestant_id = 6 AND
-- 	judge_user_email = 'judge_1@gmail.com';



-- SELECT
-- 	event.name as event_name,
--     segment.name as segment_name,
--     round.name as round_name
-- FROM event_layout
-- INNER JOIN event
-- INNER JOIN segment
-- INNER JOIN round
-- ON
--     event.id = event_layout.event_id AND
--     segment.id = event_layout.segment_id AND
--     round.id = event_layout.round_id
-- WHERE
--     event.id = 2 AND
--     segment.id = 3 AND
--     round.id = 4;



-- SELECT criterium.id, criterium.name, base_value
-- FROM result
-- INNER JOIN criterium
-- ON criterium.id = result.criterium_id
-- WHERE
-- 	event_id = 2 AND
--     segment_id = 3 AND
--     result.round_id = 4 AND
--     contestant_id = 6 AND
-- 	judge_user_email = 'judge_1@gmail.com' AND
--     event_layout_status_type = 'Incomplete';

-- SELECT COUNT(event_id) as count
-- FROM event_layout
-- WHERE event_layout_status_type = 'Incomplete';

-- SELECT *
-- FROM result
-- WHERE
-- 	event_id = 2 AND
--     segment_id = 3 AND
--     result.round_id = 4 AND
--     contestant_id = 6 AND
-- 	judge_user_email = 'judge_1@gmail.com' AND
--     event_layout_status_type = 'Incomplete';



