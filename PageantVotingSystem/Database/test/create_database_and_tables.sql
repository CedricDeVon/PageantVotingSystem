CREATE DATABASE IF NOT EXISTS pageant_voting_system_test;

USE pageant_voting_system_test;

CREATE TABLE IF NOT EXISTS contestant
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    birth_date DATE NOT NULL
);

CREATE TABLE IF NOT EXISTS gender
(
	type VARCHAR(32) PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS marital_status
(
	type VARCHAR(32) PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS image_resource
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    data BLOB
);

CREATE TABLE IF NOT EXISTS role
(
	type VARCHAR(32) PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS remark
(
	type VARCHAR(32) PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS scoring_system
(
	type VARCHAR(32) PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS user
(
	email VARCHAR(64) PRIMARY KEY,
    full_name VARCHAR(128) NOT NULL,
    password VARCHAR(32) NOT NULL,
    role_type VARCHAR(32) NOT NULL
);

CREATE TABLE IF NOT EXISTS event
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(64) NOT NULL,
    description TEXT,
    address TEXT NOT NULL,
    date_scheduled DATETIME,
    scoring_system_type VARCHAR(32) NOT NULL
);

ALTER TABLE event
ADD FOREIGN KEY(scoring_system_type)
REFERENCES scoring_system(type)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS contestant_event_data
(
    event_id INT,
    contestant_id INT,
    height FLOAT,
    weight FLOAT,
    motto TEXT NOT NULL,
    home_address TEXT NOT NULL,
    talents_and_skills TEXT NOT NULL,
    hobbies TEXT NOT NULL,
    languages TEXT NOT NULL,
    job_experiences TEXT NOT NULL,
    education TEXT NOT NULL,
    gender_type VARCHAR(32) NOT NULL,
    marital_status_type VARCHAR(32) NOT NULL,
    image_resource_id INT
);

ALTER TABLE contestant_event_data
ADD PRIMARY KEY(event_id, contestant_id);

ALTER TABLE contestant_event_data
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

ALTER TABLE contestant_event_data
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;

ALTER TABLE contestant_event_data
ADD FOREIGN KEY(gender_type)
REFERENCES gender(type)
ON DELETE CASCADE;

ALTER TABLE contestant_event_data
ADD FOREIGN KEY(marital_status_type)
REFERENCES marital_status(type)
ON DELETE CASCADE;

ALTER TABLE contestant_event_data
ADD FOREIGN KEY(image_resource_id)
REFERENCES image_resource(id)
ON DELETE SET NULL;

CREATE TABLE IF NOT EXISTS segment
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(64) NOT NULL,
    description TEXT,
    maximum_contestant_count INT NOT NULL,
    event_id INT NOT NULL
);

ALTER TABLE segment
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS round
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(64) NOT NULL,
    description TEXT,
    segment_id INT NOT NULL
);

ALTER TABLE round
ADD FOREIGN KEY(segment_id)
REFERENCES segment(id)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS criteria
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(32),
    base_value FLOAT NOT NULL,
    maximum_value FLOAT NOT NULL,
	percentage_weight FLOAT NOT NULL,
    round_id INT NOT NULL,
    contestant_id INT NOT NULL,
    judge_user_email VARCHAR(64) NOT NULL
);

ALTER TABLE criteria
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;

ALTER TABLE criteria
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;

ALTER TABLE criteria
ADD FOREIGN KEY(judge_user_email)
REFERENCES user(email)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS event_manager
(
	event_id INT,
    user_email VARCHAR(64)
);

ALTER TABLE event_manager
ADD PRIMARY KEY(event_id, user_email);

ALTER TABLE event_manager
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

ALTER TABLE event_manager
ADD FOREIGN KEY(user_email)
REFERENCES user(email)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS event_judge
(
	event_id INT,
    user_email VARCHAR(64)
);

ALTER TABLE event_judge
ADD PRIMARY KEY(event_id, user_email);

ALTER TABLE event_judge
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

ALTER TABLE event_judge
ADD FOREIGN KEY(user_email)
REFERENCES user(email)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS event_segment_contestant
(
	event_id INT,
    segment_id INT,
    contestant_id INT
);

ALTER TABLE event_segment_contestant
ADD PRIMARY KEY(event_id, segment_id, contestant_id);

ALTER TABLE event_segment_contestant
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

ALTER TABLE event_segment_contestant
ADD FOREIGN KEY(segment_id)
REFERENCES segment(id)
ON DELETE CASCADE;

ALTER TABLE event_segment_contestant
ADD FOREIGN KEY(contestant_id)
REFERENCES contestant(id)
ON DELETE CASCADE;

