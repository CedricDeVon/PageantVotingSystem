CREATE DATABASE IF NOT EXISTS pageant_voting_system;

USE pageant_voting_system;

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
    data BLOB NOT NULL
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

CREATE TABLE IF NOT EXISTS segment_preliminary
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(64) NOT NULL,
    description TEXT,
    percentage_weight FLOAT NOT NULL,
    event_id INT NOT NULL
);

ALTER TABLE segment_preliminary
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS round
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(64) NOT NULL,
    description TEXT,
    percentage_weight FLOAT NOT NULL,
    segment_preliminary_id INT NOT NULL
);

ALTER TABLE round
ADD FOREIGN KEY(segment_preliminary_id)
REFERENCES segment_preliminary(id)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS criteria
(
	id INT PRIMARY KEY AUTO_INCREMENT,
    percentage_weight FLOAT NOT NULL,
    base_value FLOAT NOT NULL,
    calculated_value FLOAT NOT NULL,
    round_id INT NOT NULL,
    contestant_id INT NOT NULL,
    user_email VARCHAR(64) NOT NULL
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
ADD FOREIGN KEY(user_email)
REFERENCES user(email)
ON DELETE CASCADE;

CREATE TABLE IF NOT EXISTS contestant_result
(
	event_id INT,
    contestant_id INT,
    total FLOAT NOT NULL,
    ranking INT NOT NULL,
    remark_type VARCHAR(32) NOT NULL
);

ALTER TABLE contestant_result
ADD PRIMARY KEY(event_id, contestant_id);

ALTER TABLE contestant_result
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;

ALTER TABLE contestant_result
ADD FOREIGN KEY(contestant_id)
REFERENCES event(id)
ON DELETE CASCADE;

ALTER TABLE contestant_result
ADD FOREIGN KEY(remark_type)
REFERENCES remark(type)
ON DELETE CASCADE;
