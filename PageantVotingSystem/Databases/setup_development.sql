
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

CREATE TABLE IF NOT EXISTS contestant
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    order_number INT UNSIGNED NOT NULL,
    height_in_centimeters FLOAT,
    weight_in_kilograms FLOAT,
    motto TEXT,
    home_address TEXT,
    talents_and_skills TEXT,
    hobbies TEXT,
    languages TEXT,
    job_experiences TEXT,
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
    event_id INT UNSIGNED,
    segment_id INT UNSIGNED,
    round_id INT UNSIGNED,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL,
    event_layout_status_type VARCHAR(64) NOT NULL
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
    ('Name - A', 'Description - A', 'Host Address - A', NOW(), 'Percentage'),
    ('Name - B', 'Description - B', 'Host Address - B', NOW(), 'Percentage');

INSERT INTO segment (name, description, maximum_contestant_count, event_id)
VALUES
    ('Name - A', 'Description - A', 5, 1),
    ('Name - B', 'Description - B', 3, 1);

INSERT INTO round (name, description, segment_id)
VALUES
    ('Name - A', 'Description - A', 1),
    ('Name - B', 'Description - B', 1),
    ('Name - C', 'Description - C', 2);

INSERT INTO criterium (name, description, maximum_value, percentage_weight, round_id)
VALUES
    ('Name - A', 'Description - A', 100, 50, 1),
    ('Name - B', 'Description - B', 100, 30, 1),
    ('Name - C', 'Description - C', 100, 20, 1),
    
    ('Name - D', 'Description - D', 100, 60, 2),
    ('Name - E', 'Description - E', 100, 40, 2),
    
    ('Name - F', 'Description - F', 100, 100, 3);

INSERT INTO event_layout (event_id, segment_id, round_id, event_layout_status_type)
VALUES
    (1, 1, 1, 'Complete'),
    (1, 1, 2, 'Complete'),
    (1, 2, 3, 'Complete');

INSERT INTO contestant (full_name, order_number, contestant_status_type, marital_status_type, gender_type)
VALUES
    ('Contestant - A', 1, 'Qualified', 'Single', 'Female'),
    ('Contestant - B', 2, 'Qualified', 'Single', 'Female'),
    ('Contestant - C', 3, 'Qualified', 'Single', 'Female'),
    ('Contestant - D', 4, 'Qualified', 'Single', 'Female'),
    ('Contestant - E', 5, 'Qualified', 'Single', 'Female');

INSERT INTO event_contestant (event_id, contestant_id)
VALUES
    (1, 1),
    (1, 2),
    (1, 3),
    (1, 4),
    (1, 5);

INSERT INTO event_manager (event_id, manager_user_email)
VALUES
    (1, 'manager_1@gmail.com'),
    (2, 'manager_2@gmail.com');

INSERT INTO event_judge (event_id, order_number, judge_user_email, judge_status_type)
VALUES
    (1, 3, 'judge_1@gmail.com', 'Present'),
    (1, 2, 'judge_2@gmail.com', 'Present'),
    (1, 1, 'judge_3@gmail.com', 'Present');

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
    (1, 2, 3, 6, 5, 'judge_1@gmail.com', 70), (1, 2, 3, 6, 5, 'judge_2@gmail.com', 55.85), (1, 2, 3, 6, 5, 'judge_3@gmail.com', 59.5);

-- USE pageant_voting_system_development;
-- SELECT * FROM user;

-- SELECT * FROM event;
-- SELECT * FROM segment;
-- SELECT * FROM round;
-- SELECT * FROM criterium;
-- SELECT * FROM event_layout;
-- SELECT * FROM contestant;
-- SELECT * FROM event_contestant;
-- SELECT * FROM event_manager;
-- SELECT * FROM event_judge;
-- SELECT * FROM result;