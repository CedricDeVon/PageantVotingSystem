
USE pageant_voting_system_development;
SELECT
	RANK() OVER (ORDER BY net_value DESC) as ranking_number,
    order_number,
    full_name,
    ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,
    maximum_value
FROM
(
	SELECT contestant_id,
		ROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value,
        ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value
	FROM result
	INNER JOIN criterium
	ON id = criterium_id
	WHERE result.round_id = 1
	GROUP BY contestant_id
) data
INNER JOIN contestant
ON contestant.id = contestant_id;




USE pageant_voting_system_development;
SET @segment_id = 2;
SELECT
	RANK() OVER (ORDER BY net_value DESC) as ranking,
    contestant_id,
    full_name,
    ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,
    maximum_value
FROM
(
	SELECT contestant_id,
		ROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value,
        ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value
	FROM result
	INNER JOIN criterium
	ON id = criterium_id
	WHERE result.segment_id = @segment_id
	GROUP BY contestant_id
) data
INNER JOIN contestant
ON contestant.id = contestant_id;




USE pageant_voting_system_development;
SET @criterium_id = 1;
SELECT
	RANK() OVER (ORDER BY net_value DESC) as ranking_number,
    order_number,
    full_name,
    ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,
    maximum_value
FROM
(
	SELECT contestant_id,
		ROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value,
        ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value
	FROM result
	INNER JOIN criterium
	ON id = criterium_id
	WHERE criterium_id = @criterium_id
	GROUP BY contestant_id
) data
INNER JOIN contestant
ON contestant.id = contestant_id;




USE pageant_voting_system_development;
SET @segment_id = 2;
SELECT
	RANK() OVER (ORDER BY net_value DESC) as ranking,
    contestant_id,
    full_name,
    ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,
    maximum_value
FROM
(
	SELECT contestant_id,
		ROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value,
        ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value
	FROM result
	INNER JOIN criterium
	ON id = criterium_id
	WHERE result.segment_id = @segment_id
	GROUP BY contestant_id
) data
INNER JOIN contestant
ON contestant.id = contestant_id;



CREATE DATABASE IF NOT EXISTS pageant_voting_system_production;
USE pageant_voting_system_production;

CREATE TABLE IF NOT EXISTS image_resource
(
	id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    data BLOB,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS current_status
(
    type VARCHAR(64) PRIMARY KEY,
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

CREATE TABLE IF NOT EXISTS scoring_system
(
    type VARCHAR(64) PRIMARY KEY,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS contestant
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    full_name VARCHAR(128) NOT NULL,
    image_resource_id INT UNSIGNED,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS user
(
    email VARCHAR(128) PRIMARY KEY,
    full_name VARCHAR(128) NOT NULL,
    password VARCHAR(128) NOT NULL,
    user_role_type VARCHAR(64) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    host_address TEXT NOT NULL,
    scoring_system_type VARCHAR(64) NOT NULL,
    current_status_type VARCHAR(64) NOT NULL,
    manager_user_email VARCHAR(128) NOT NULL,
    timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS segment
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    maximum_contestant_count NUMERIC(3, 0) NOT NULL,
    event_id INT UNSIGNED NOT NULL,
    current_status_type VARCHAR(64) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS round
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    segment_id INT UNSIGNED NOT NULL,
    current_status_type VARCHAR(64) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS criteria
(
    id INT UNSIGNED PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(128) NOT NULL,
    maximum_value NUMERIC(5, 2) NOT NULL,
    percentage_weight NUMERIC(3, 2) NOT NULL,
    round_id INT UNSIGNED NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS result
(
    event_id INT UNSIGNED NOT NULL,
    segment_id INT UNSIGNED NOT NULL,
    round_id INT UNSIGNED NOT NULL,
    criteria_id INT UNSIGNED NOT NULL,
    base_value NUMERIC(5, 2) UNSIGNED NOT NULL,
    contestant_id INT UNSIGNED NOT NULL,
    result_remark_type VARCHAR(64) NOT NULL,
    judge_user_email VARCHAR(128) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event_manager
(
    event_id INT UNSIGNED PRIMARY KEY,
    manager_user_email VARCHAR(128) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event_contestant
(
    event_id INT UNSIGNED NOT NULL,
    contestant_id INT UNSIGNED NOT NULL,
    number NUMERIC(3, 0) UNSIGNED NOT NULL,
    current_status_type VARCHAR(64) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

CREATE TABLE IF NOT EXISTS event_judge
(
    event_id INT UNSIGNED NOT NULL,
    judge_user_email VARCHAR(128) NOT NULL,
    number NUMERIC(3, 0) UNSIGNED NOT NULL,
    current_status_type VARCHAR(64) NOT NULL,
	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
    timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
);

ALTER TABLE contestant
ADD FOREIGN KEY(image_resource_id)
REFERENCES image_resource(id)
ON DELETE SET NULL;

ALTER TABLE user
ADD FOREIGN KEY(user_role_type)
REFERENCES user_role(type)
ON DELETE CASCADE;

ALTER TABLE event
ADD FOREIGN KEY(scoring_system_type)
REFERENCES scoring_system(type)
ON DELETE CASCADE;
ALTER TABLE event
ADD FOREIGN KEY(current_status_type)
REFERENCES current_status(type)
ON DELETE CASCADE;
ALTER TABLE event
ADD FOREIGN KEY(manager_user_email)
REFERENCES user(email)
ON DELETE CASCADE;

ALTER TABLE segment
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;
ALTER TABLE segment
ADD FOREIGN KEY(current_status_type)
REFERENCES current_status(type)
ON DELETE CASCADE;

ALTER TABLE round
ADD FOREIGN KEY(segment_id)
REFERENCES segment(id)
ON DELETE CASCADE;
ALTER TABLE round
ADD FOREIGN KEY(current_status_type)
REFERENCES current_status(type)
ON DELETE CASCADE;

ALTER TABLE criteria
ADD FOREIGN KEY(round_id)
REFERENCES round(id)
ON DELETE CASCADE;

ALTER TABLE result
ADD PRIMARY KEY(event_id, segment_id, round_id, criteria_id);
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
ADD FOREIGN KEY(current_status_type)
REFERENCES current_status(type)
ON DELETE CASCADE;

ALTER TABLE event_judge
ADD PRIMARY KEY(event_id, judge_user_email);
ALTER TABLE event_judge
ADD FOREIGN KEY(event_id)
REFERENCES event(id)
ON DELETE CASCADE;
ALTER TABLE event_judge
ADD FOREIGN KEY(judge_user_email)
REFERENCES user(email)
ON DELETE CASCADE;
ALTER TABLE event_judge
ADD FOREIGN KEY(current_status_type)
REFERENCES current_status(type)
ON DELETE CASCADE;

INSERT INTO current_status (type) VALUES ('Qualified'), ('Disqualified'), ('Present'), ('Abscent');
INSERT INTO result_remark (type) VALUES ('Winner'), ('1st Runner-Up'), ('2nd Runner-Up');
INSERT INTO user_role (type) VALUES ('Manager'), ('Judge');
INSERT INTO scoring_system (type) VALUES ('Percentage'), ('Ranking');

-- DROP TABLE IF EXISTS result;
-- DROP TABLE IF EXISTS event_manager;
-- DROP TABLE IF EXISTS event_judge;
-- DROP TABLE IF EXISTS event_contestant;
-- DROP TABLE IF EXISTS criteria;
-- DROP TABLE IF EXISTS round;
-- DROP TABLE IF EXISTS segment;
-- DROP TABLE IF EXISTS event;
-- DROP TABLE IF EXISTS contestant;
-- DROP TABLE IF EXISTS user;
-- DROP TABLE IF EXISTS current_status;
-- DROP TABLE IF EXISTS image_resource;
-- DROP TABLE IF EXISTS scoring_system;
-- DROP TABLE IF EXISTS user_role;
-- DROP TABLE IF EXISTS result_remark;
