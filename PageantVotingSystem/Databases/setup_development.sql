
DROP DATABASE IF EXISTS pageant_voting_system_production;
CREATE DATABASE IF NOT EXISTS pageant_voting_system_production;
USE pageant_voting_system_production;

CREATE TABLE IF NOT EXISTS resource
(
    path VARCHAR(512) PRIMARY KEY,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS judge_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS contestant_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS round_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS user_role
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS marital_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS gender
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS scoring_system
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event_layout_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS round_contestant_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS judge_status
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS contestant
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    order_number INT UNSIGNED NOT NULL,
    height_in_centimeters FLOAT DEFAULT 0,
    weight_in_kilograms FLOAT DEFAULT 0,
    birth_date DATETIME,
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
    marital_status_type VARCHAR(64) DEFAULT 'Rather Not Say',
    gender_type VARCHAR(64) DEFAULT 'Rather Not Say',
    image_resource_path VARCHAR(512) DEFAULT '../../Profiles/DefaultProfile.png'
);

CREATE TABLE IF NOT EXISTS user
(
    email VARCHAR(128) PRIMARY KEY,
    full_name VARCHAR(128) NOT NULL,
    password VARCHAR(128) NOT NULL,
    description TEXT,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    user_role_type VARCHAR(64) NOT NULL,
    image_resource_path VARCHAR(512) DEFAULT '../../Profiles/DefaultProfile.png'
);

CREATE TABLE IF NOT EXISTS event
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
    host_address TEXT,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_ended TIMESTAMP,
    scoring_system_type VARCHAR(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS segment
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    event_id INT UNSIGNED NOT NULL
);

CREATE TABLE IF NOT EXISTS round
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    segment_id INT UNSIGNED NOT NULL
);

CREATE TABLE IF NOT EXISTS criterium
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    description TEXT,
    maximum_value INT UNSIGNED DEFAULT 100 NOT NULL,
    percentage_weight FLOAT DEFAULT 100 NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    round_id INT UNSIGNED NOT NULL
);

CREATE TABLE IF NOT EXISTS event_layout
(
    round_id INT UNSIGNED PRIMARY KEY,
	event_id INT UNSIGNED NOT NULL,
    segment_id INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    event_layout_status_type VARCHAR(64) NOT NULL
);

CREATE TABLE IF NOT EXISTS round_contestant
(
    round_id INT UNSIGNED NOT NULL,
    contestant_id INT UNSIGNED NOT NULL,
    judge_user_email VARCHAR(128) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    round_contestant_status_type VARCHAR(64) NOT NULL DEFAULT 'Incomplete'
);

CREATE TABLE IF NOT EXISTS event_manager
(
    event_id INT UNSIGNED PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    manager_user_email VARCHAR(128) NOT NULL
);

CREATE TABLE IF NOT EXISTS event_contestant
(
    event_id INT UNSIGNED NOT NULL,
    contestant_id INT UNSIGNED NOT NULL,
    contestant_status_type VARCHAR(64) NOT NULL DEFAULT 'Qualified',
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event_judge
(
    event_id INT UNSIGNED NOT NULL,
    judge_user_email VARCHAR(128) NOT NULL,
    order_number INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    judge_status_type VARCHAR(64) NOT NULL DEFAULT 'Present'
);

CREATE TABLE IF NOT EXISTS result
(
    event_id INT UNSIGNED NOT NULL,
    segment_id INT UNSIGNED NOT NULL,
    round_id INT UNSIGNED NOT NULL,
    criterium_id INT UNSIGNED NOT NULL,
    base_value FLOAT NOT NULL DEFAULT 0,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    contestant_id INT UNSIGNED NOT NULL,
    judge_user_email VARCHAR(128) NOT NULL
);

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
ON DELETE CASCADE;

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
ADD PRIMARY KEY(round_id, contestant_id, judge_user_email);
ALTER TABLE round_contestant
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;
ALTER TABLE round_contestant
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;
ALTER TABLE round_contestant
ADD FOREIGN KEY(judge_user_email)
REFERENCES user(email)
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
ALTER TABLE event_contestant
ADD FOREIGN KEY(contestant_status_type)
REFERENCES contestant_status(type)
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
ADD PRIMARY KEY(criterium_id, contestant_id, judge_user_email);
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
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;
ALTER TABLE result
ADD FOREIGN KEY(judge_user_email)
REFERENCES user(email)
ON DELETE CASCADE;

INSERT INTO resource (path) VALUES ('../../Profiles/DefaultProfile.png');
INSERT INTO judge_status (type) VALUES ('Present'), ('Abscent');
INSERT INTO scoring_system (type) VALUES ('Percentage');
INSERT INTO user_role (type) VALUES ('Manager'), ('Judge');
INSERT INTO round_status (type) VALUES ('Complete'), ('Incomplete');
INSERT INTO marital_status (type) VALUES ('Single'), ('Married'), ('Divorced'), ('With Significant Other'), ('Rather Not Say');
INSERT INTO gender (type) VALUES ('Male'), ('Female'), ('Non-Binary'), ('Rather Not Say');
INSERT INTO contestant_status (type) VALUES ('Qualified'), ('Disqualified');
INSERT INTO event_layout_status (type) VALUES ('Complete'), ('Incomplete'), ('Pending');
INSERT INTO round_contestant_status (type) VALUES ('Complete'), ('Incomplete'), ('Pending');
INSERT INTO user (email, full_name, password, user_role_type, description)
VALUES
    ('manager_1@gmail.com', 'John A. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Manager', 'The official profile of John A. Doe'),
    ('manager_2@gmail.com', 'Jasmine B. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Manager', 'The official profile of Jasmine B. Doe'),
    ('manager_3@gmail.com', 'Johnson C. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Manager', 'The official profile of Johnson C. Doe'),
    ('judge_1@gmail.com', 'James D. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Judge', 'The official profile of James D. Doe'),
    ('judge_2@gmail.com', 'Jessie E. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Judge', 'The official profile of Jessie E. Doe'),
    ('judge_3@gmail.com', 'Jam F. Doe', 'Clj3ixSdV3qFFp8vSu5gMNG771Udu6rIQ8L0Xv1gYuHOPAikrBMqp2FpxYLlbcuct3KPpWY9LUg5jZSkXeB2hg==', 'Judge', 'The official profile of Jam F. Doe');
