USE pageant_voting_system_test;

INSERT INTO gender (type)
VALUES ("Male"), ("Female"), ("Non-Binary");

INSERT INTO marital_status (type)
VALUES ("Single"), ("Married"), ("Other");

INSERT INTO scoring_system (type)
VALUES ("Ranking"), ("Grading");

INSERT INTO remark (type)
VALUES ("Winner"), ("1st Runner-Up"), ("2st Runner-Up");

INSERT INTO role (type)
VALUES ("Manager"), ("Judge");

INSERT INTO user (email, full_name, password, role_type)
VALUES
    ("manager_1@gmail.com", "Manager 1", "password", "Manager");

INSERT INTO user (email, full_name, password, role_type)
VALUES
	("judge_1@gmail.com", "Judge 1", "password", "Judge"),
    ("judge_2@gmail.com", "Judge 2", "password", "Judge"),
    ("judge_3@gmail.com", "Judge 3", "password", "Judge"),
    ("judge_4@gmail.com", "Judge 3", "password", "Judge"),
    ("judge_5@gmail.com", "Judge 3", "password", "Judge");

INSERT INTO event (name, description, address, date_scheduled, scoring_system_type)
VALUES
	("Event 1", "Description...", "Address...", Date(now()), "Ranking");

INSERT INTO event_manager (event_id, user_email)
VALUES
	(1, "manager_1@gmail.com");

INSERT INTO event_judge (event_id, user_email)
VALUES
	(1, "judge_1@gmail.com"),
    (1, "judge_2@gmail.com"),
    (1, "judge_3@gmail.com"),
    (1, "judge_4@gmail.com"),
    (1, "judge_5@gmail.com");
    
INSERT INTO contestant (full_name, birth_date)
VALUES
	("Contestant 1", Date("2000-01-01")),
    ("Contestant 2", Date("2000-01-02")),
    ("Contestant 3", Date("2000-01-03")),
    ("Contestant 4", Date("2000-01-04")),
    ("Contestant 5", Date("2000-01-05")),
    ("Contestant 6", Date("2000-01-06")),
    ("Contestant 7", Date("2000-01-07")),
    ("Contestant 8", Date("2000-01-07"));

-- INSERT INTO image_resource(data) VALUES(LOAD_FILE('E:\Users\Dell\Pictures\contestant.png'));

INSERT INTO contestant_event_data
(
	event_id,
	contestant_id,
	height,
	weight,
	motto,
	home_address,
	talents_and_skills,
	hobbies, languages,
	job_experiences,
	education,
	gender_type,
	marital_status_type,
	image_resource_id
)
VALUES
(1, 1, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null),
(1, 2, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null),
(1, 3, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null),
(1, 4, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null),
(1, 5, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null),
(1, 6, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null),
(1, 7, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null),
(1, 8, 180.5, 64.5, "Motto...", "Address...", "Talent, ..., Skill, ...", "Hobby, ...", "Language, ...", "Job Experience, ...", "Education, ...", "Female", "Single", null);

INSERT INTO segment (name, description, maximum_contestant_count, event_id)
VALUES
	("Coronation", "Description...", 0, 1),
    ("Semi-Finals", "Description...", 5, 1),
    ("Finals", "Description...", 3, 1);

INSERT INTO round (name, description, segment_id)
VALUES
	("Prod", "Description...", 1),
    ("Swimsuit", "Description...", 1),
    ("Talent", "Description...", 1),
    ("QnA", "Description...", 1),
    ("Gown", "Description...", 1),
	("QnA", "Description...", 2),
    ("Beauty", "Description...", 2),
    ("QnA", "Description...", 3);

INSERT INTO event_segment_contestant (event_id, segment_id, contestant_id)
VALUES
	(1, 1, 1),
    (1, 1, 2),
    (1, 1, 3),
    (1, 1, 4),
    (1, 1, 5),
    (1, 1, 6),
    (1, 1, 7),
    (1, 1, 8);
    
INSERT INTO criteria (base_value, maximum_value, percentage_weight, contestant_id, round_id, judge_user_email)
VALUES
	(0, 10, 1, 1, 1, "judge_1@gmail.com"), (0, 10, 1, 1, 1, "judge_2@gmail.com"), (0, 10, 1, 1, 1, "judge_3@gmail.com"), (0, 10, 1, 1, 1, "judge_4@gmail.com"), (0, 10, 1, 1, 1, "judge_5@gmail.com"),
    (0, 10, 1, 2, 1, "judge_1@gmail.com"), (0, 10, 1, 2, 1, "judge_2@gmail.com"), (0, 10, 1, 2, 1, "judge_3@gmail.com"), (0, 10, 1, 2, 1, "judge_4@gmail.com"), (0, 10, 1, 2, 1, "judge_5@gmail.com"),
    (0, 10, 1, 3, 1, "judge_1@gmail.com"), (0, 10, 1, 3, 1, "judge_2@gmail.com"), (0, 10, 1, 3, 1, "judge_3@gmail.com"), (0, 10, 1, 3, 1, "judge_4@gmail.com"), (0, 10, 1, 3, 1, "judge_5@gmail.com"),
    (0, 10, 1, 4, 1, "judge_1@gmail.com"), (0, 10, 1, 4, 1, "judge_2@gmail.com"), (0, 10, 1, 4, 1, "judge_3@gmail.com"), (0, 10, 1, 4, 1, "judge_4@gmail.com"), (0, 10, 1, 4, 1, "judge_5@gmail.com"),
    (0, 10, 1, 5, 1, "judge_1@gmail.com"), (0, 10, 1, 5, 1, "judge_2@gmail.com"), (0, 10, 1, 5, 1, "judge_3@gmail.com"), (0, 10, 1, 5, 1, "judge_4@gmail.com"), (0, 10, 1, 5, 1, "judge_5@gmail.com"),
    (0, 10, 1, 6, 1, "judge_1@gmail.com"), (0, 10, 1, 6, 1, "judge_2@gmail.com"), (0, 10, 1, 6, 1, "judge_3@gmail.com"), (0, 10, 1, 6, 1, "judge_4@gmail.com"), (0, 10, 1, 6, 1, "judge_5@gmail.com"),
    (0, 10, 1, 7, 1, "judge_1@gmail.com"), (0, 10, 1, 7, 1, "judge_2@gmail.com"), (0, 10, 1, 7, 1, "judge_3@gmail.com"), (0, 10, 1, 7, 1, "judge_4@gmail.com"), (0, 10, 1, 7, 1, "judge_5@gmail.com"),
    (0, 10, 1, 8, 1, "judge_1@gmail.com"), (0, 10, 1, 8, 1, "judge_2@gmail.com"), (0, 10, 1, 8, 1, "judge_3@gmail.com"), (0, 10, 1, 8, 1, "judge_4@gmail.com"), (0, 10, 1, 8, 1, "judge_5@gmail.com"),
    
    (0, 10, 1, 1, 2, "judge_1@gmail.com"), (0, 10, 1, 1, 2, "judge_2@gmail.com"), (0, 10, 1, 1, 2, "judge_3@gmail.com"), (0, 10, 1, 1, 2, "judge_4@gmail.com"), (0, 10, 1, 1, 2, "judge_5@gmail.com"),
    (0, 10, 1, 2, 2, "judge_1@gmail.com"), (0, 10, 1, 2, 2, "judge_2@gmail.com"), (0, 10, 1, 2, 2, "judge_3@gmail.com"), (0, 10, 1, 2, 2, "judge_4@gmail.com"), (0, 10, 1, 2, 2, "judge_5@gmail.com"),
    (0, 10, 1, 3, 2, "judge_1@gmail.com"), (0, 10, 1, 3, 2, "judge_2@gmail.com"), (0, 10, 1, 3, 2, "judge_3@gmail.com"), (0, 10, 1, 3, 2, "judge_4@gmail.com"), (0, 10, 1, 3, 2, "judge_5@gmail.com"),
    (0, 10, 1, 4, 2, "judge_1@gmail.com"), (0, 10, 1, 4, 2, "judge_2@gmail.com"), (0, 10, 1, 4, 2, "judge_3@gmail.com"), (0, 10, 1, 4, 2, "judge_4@gmail.com"), (0, 10, 1, 4, 2, "judge_5@gmail.com"),
    (0, 10, 1, 5, 2, "judge_1@gmail.com"), (0, 10, 1, 5, 2, "judge_2@gmail.com"), (0, 10, 1, 5, 2, "judge_3@gmail.com"), (0, 10, 1, 5, 2, "judge_4@gmail.com"), (0, 10, 1, 5, 2, "judge_5@gmail.com"),
    (0, 10, 1, 6, 2, "judge_1@gmail.com"), (0, 10, 1, 6, 2, "judge_2@gmail.com"), (0, 10, 1, 6, 2, "judge_3@gmail.com"), (0, 10, 1, 6, 2, "judge_4@gmail.com"), (0, 10, 1, 6, 2, "judge_5@gmail.com"),
    (0, 10, 1, 7, 2, "judge_1@gmail.com"), (0, 10, 1, 7, 2, "judge_2@gmail.com"), (0, 10, 1, 7, 2, "judge_3@gmail.com"), (0, 10, 1, 7, 2, "judge_4@gmail.com"), (0, 10, 1, 7, 2, "judge_5@gmail.com"),
    (0, 10, 1, 8, 2, "judge_1@gmail.com"), (0, 10, 1, 8, 2, "judge_2@gmail.com"), (0, 10, 1, 8, 2, "judge_3@gmail.com"), (0, 10, 1, 8, 2, "judge_4@gmail.com"), (0, 10, 1, 8, 2, "judge_5@gmail.com"),
    
    (0, 10, 1, 1, 3, "judge_1@gmail.com"), (0, 10, 1, 1, 3, "judge_2@gmail.com"), (0, 10, 1, 1, 3, "judge_3@gmail.com"), (0, 10, 1, 1, 3, "judge_4@gmail.com"), (0, 10, 1, 1, 3, "judge_5@gmail.com"),
    (0, 10, 1, 2, 3, "judge_1@gmail.com"), (0, 10, 1, 2, 3, "judge_2@gmail.com"), (0, 10, 1, 2, 3, "judge_3@gmail.com"), (0, 10, 1, 2, 3, "judge_4@gmail.com"), (0, 10, 1, 2, 3, "judge_5@gmail.com"),
    (0, 10, 1, 3, 3, "judge_1@gmail.com"), (0, 10, 1, 3, 3, "judge_2@gmail.com"), (0, 10, 1, 3, 3, "judge_3@gmail.com"), (0, 10, 1, 3, 3, "judge_4@gmail.com"), (0, 10, 1, 3, 3, "judge_5@gmail.com"),
    (0, 10, 1, 4, 3, "judge_1@gmail.com"), (0, 10, 1, 4, 3, "judge_2@gmail.com"), (0, 10, 1, 4, 3, "judge_3@gmail.com"), (0, 10, 1, 4, 3, "judge_4@gmail.com"), (0, 10, 1, 4, 3, "judge_5@gmail.com"),
    (0, 10, 1, 5, 3, "judge_1@gmail.com"), (0, 10, 1, 5, 3, "judge_2@gmail.com"), (0, 10, 1, 5, 3, "judge_3@gmail.com"), (0, 10, 1, 5, 3, "judge_4@gmail.com"), (0, 10, 1, 5, 3, "judge_5@gmail.com"),
    (0, 10, 1, 6, 3, "judge_1@gmail.com"), (0, 10, 1, 6, 3, "judge_2@gmail.com"), (0, 10, 1, 6, 3, "judge_3@gmail.com"), (0, 10, 1, 6, 3, "judge_4@gmail.com"), (0, 10, 1, 6, 3, "judge_5@gmail.com"),
    (0, 10, 1, 7, 3, "judge_1@gmail.com"), (0, 10, 1, 7, 3, "judge_2@gmail.com"), (0, 10, 1, 7, 3, "judge_3@gmail.com"), (0, 10, 1, 7, 3, "judge_4@gmail.com"), (0, 10, 1, 7, 3, "judge_5@gmail.com"),
    (0, 10, 1, 8, 3, "judge_1@gmail.com"), (0, 10, 1, 8, 3, "judge_2@gmail.com"), (0, 10, 1, 8, 3, "judge_3@gmail.com"), (0, 10, 1, 8, 3, "judge_4@gmail.com"), (0, 10, 1, 8, 3, "judge_5@gmail.com"),
    
    (0, 10, 1, 1, 4, "judge_1@gmail.com"), (0, 10, 1, 1, 4, "judge_2@gmail.com"), (0, 10, 1, 1, 4, "judge_3@gmail.com"), (0, 10, 1, 1, 4, "judge_4@gmail.com"), (0, 10, 1, 1, 4, "judge_5@gmail.com"),
    (0, 10, 1, 2, 4, "judge_1@gmail.com"), (0, 10, 1, 2, 4, "judge_2@gmail.com"), (0, 10, 1, 2, 4, "judge_3@gmail.com"), (0, 10, 1, 2, 4, "judge_4@gmail.com"), (0, 10, 1, 2, 4, "judge_5@gmail.com"),
    (0, 10, 1, 3, 4, "judge_1@gmail.com"), (0, 10, 1, 3, 4, "judge_2@gmail.com"), (0, 10, 1, 3, 4, "judge_3@gmail.com"), (0, 10, 1, 3, 4, "judge_4@gmail.com"), (0, 10, 1, 3, 4, "judge_5@gmail.com"),
    (0, 10, 1, 4, 4, "judge_1@gmail.com"), (0, 10, 1, 4, 4, "judge_2@gmail.com"), (0, 10, 1, 4, 4, "judge_3@gmail.com"), (0, 10, 1, 4, 4, "judge_4@gmail.com"), (0, 10, 1, 4, 4, "judge_5@gmail.com"),
    (0, 10, 1, 5, 4, "judge_1@gmail.com"), (0, 10, 1, 5, 4, "judge_2@gmail.com"), (0, 10, 1, 5, 4, "judge_3@gmail.com"), (0, 10, 1, 5, 4, "judge_4@gmail.com"), (0, 10, 1, 5, 4, "judge_5@gmail.com"),
    (0, 10, 1, 6, 4, "judge_1@gmail.com"), (0, 10, 1, 6, 4, "judge_2@gmail.com"), (0, 10, 1, 6, 4, "judge_3@gmail.com"), (0, 10, 1, 6, 4, "judge_4@gmail.com"), (0, 10, 1, 6, 4, "judge_5@gmail.com"),
    (0, 10, 1, 7, 4, "judge_1@gmail.com"), (0, 10, 1, 7, 4, "judge_2@gmail.com"), (0, 10, 1, 7, 4, "judge_3@gmail.com"), (0, 10, 1, 7, 4, "judge_4@gmail.com"), (0, 10, 1, 7, 4, "judge_5@gmail.com"),
    (0, 10, 1, 8, 4, "judge_1@gmail.com"), (0, 10, 1, 8, 4, "judge_2@gmail.com"), (0, 10, 1, 8, 4, "judge_3@gmail.com"), (0, 10, 1, 8, 4, "judge_4@gmail.com"), (0, 10, 1, 8, 4, "judge_5@gmail.com"),
    
    (0, 10, 1, 1, 5, "judge_1@gmail.com"), (0, 10, 1, 1, 5, "judge_2@gmail.com"), (0, 10, 1, 1, 5, "judge_3@gmail.com"), (0, 10, 1, 1, 5, "judge_4@gmail.com"), (0, 10, 1, 1, 5, "judge_5@gmail.com"),
    (0, 10, 1, 2, 5, "judge_1@gmail.com"), (0, 10, 1, 2, 5, "judge_2@gmail.com"), (0, 10, 1, 2, 5, "judge_3@gmail.com"), (0, 10, 1, 2, 5, "judge_4@gmail.com"), (0, 10, 1, 2, 5, "judge_5@gmail.com"),
    (0, 10, 1, 3, 5, "judge_1@gmail.com"), (0, 10, 1, 3, 5, "judge_2@gmail.com"), (0, 10, 1, 3, 5, "judge_3@gmail.com"), (0, 10, 1, 3, 5, "judge_4@gmail.com"), (0, 10, 1, 3, 5, "judge_5@gmail.com"),
    (0, 10, 1, 4, 5, "judge_1@gmail.com"), (0, 10, 1, 4, 5, "judge_2@gmail.com"), (0, 10, 1, 4, 5, "judge_3@gmail.com"), (0, 10, 1, 4, 5, "judge_4@gmail.com"), (0, 10, 1, 4, 5, "judge_5@gmail.com"),
    (0, 10, 1, 5, 5, "judge_1@gmail.com"), (0, 10, 1, 5, 5, "judge_2@gmail.com"), (0, 10, 1, 5, 5, "judge_3@gmail.com"), (0, 10, 1, 5, 5, "judge_4@gmail.com"), (0, 10, 1, 5, 5, "judge_5@gmail.com"),
    (0, 10, 1, 6, 5, "judge_1@gmail.com"), (0, 10, 1, 6, 5, "judge_2@gmail.com"), (0, 10, 1, 6, 5, "judge_3@gmail.com"), (0, 10, 1, 6, 5, "judge_4@gmail.com"), (0, 10, 1, 6, 5, "judge_5@gmail.com"),
    (0, 10, 1, 7, 5, "judge_1@gmail.com"), (0, 10, 1, 7, 5, "judge_2@gmail.com"), (0, 10, 1, 7, 5, "judge_3@gmail.com"), (0, 10, 1, 7, 5, "judge_4@gmail.com"), (0, 10, 1, 7, 5, "judge_5@gmail.com"),
    (0, 10, 1, 8, 5, "judge_1@gmail.com"), (0, 10, 1, 8, 5, "judge_2@gmail.com"), (0, 10, 1, 8, 5, "judge_3@gmail.com"), (0, 10, 1, 8, 5, "judge_4@gmail.com"), (0, 10, 1, 8, 5, "judge_5@gmail.com");
    
UPDATE criteria
SET base_value = 4
WHERE contestant_id = 1;

UPDATE criteria
SET base_value = 5
WHERE contestant_id = 2;

UPDATE criteria
SET base_value = 8
WHERE contestant_id = 3;

UPDATE criteria
SET base_value = 6
WHERE contestant_id = 4;

UPDATE criteria
SET base_value = 1
WHERE contestant_id = 5;

UPDATE criteria
SET base_value = 7
WHERE contestant_id = 6;

UPDATE criteria
SET base_value = 1
WHERE contestant_id = 7;

UPDATE criteria
SET base_value = 3
WHERE contestant_id = 8;

INSERT INTO event_segment_contestant (event_id, segment_id, contestant_id)
VALUES
	(1, 2, 3),
    (1, 2, 6),
    (1, 2, 4),
    (1, 2, 2),
    (1, 2, 1);

INSERT INTO criteria (base_value, maximum_value, percentage_weight, contestant_id, round_id, judge_user_email)
VALUES
	(0, 10, 0.6, 3, 6, "judge_1@gmail.com"), (0, 10, 0.6, 3, 6, "judge_2@gmail.com"), (0, 10, 0.6, 3, 6, "judge_3@gmail.com"), (0, 10, 0.6, 3, 6, "judge_4@gmail.com"), (0, 10, 0.6, 3, 6, "judge_5@gmail.com"),
    (0, 10, 0.4, 3, 7, "judge_1@gmail.com"), (0, 10, 0.4, 3, 7, "judge_2@gmail.com"), (0, 10, 0.4, 3, 7, "judge_3@gmail.com"), (0, 10, 0.4, 3, 7, "judge_4@gmail.com"), (0, 10, 0.4, 3, 7, "judge_5@gmail.com"),

	(0, 10, 0.6, 6, 6, "judge_1@gmail.com"), (0, 10, 0.6, 6, 6, "judge_2@gmail.com"), (0, 10, 0.6, 6, 6, "judge_3@gmail.com"), (0, 10, 0.6, 6, 6, "judge_4@gmail.com"), (0, 10, 0.6, 6, 6, "judge_5@gmail.com"),
    (0, 10, 0.4, 6, 7, "judge_1@gmail.com"), (0, 10, 0.4, 6, 7, "judge_2@gmail.com"), (0, 10, 0.4, 6, 7, "judge_3@gmail.com"), (0, 10, 0.4, 6, 7, "judge_4@gmail.com"), (0, 10, 0.4, 6, 7, "judge_5@gmail.com"),

	(0, 10, 0.6, 4, 6, "judge_1@gmail.com"), (0, 10, 0.6, 4, 6, "judge_2@gmail.com"), (0, 10, 0.6, 4, 6, "judge_3@gmail.com"), (0, 10, 0.6, 4, 6, "judge_4@gmail.com"), (0, 10, 0.6, 4, 6, "judge_5@gmail.com"),
    (0, 10, 0.4, 4, 7, "judge_1@gmail.com"), (0, 10, 0.4, 4, 7, "judge_2@gmail.com"), (0, 10, 0.4, 4, 7, "judge_3@gmail.com"), (0, 10, 0.4, 4, 7, "judge_4@gmail.com"), (0, 10, 0.4, 4, 7, "judge_5@gmail.com"),

	(0, 10, 0.6, 2, 6, "judge_1@gmail.com"), (0, 10, 0.6, 2, 6, "judge_2@gmail.com"), (0, 10, 0.6, 2, 6, "judge_3@gmail.com"), (0, 10, 0.6, 2, 6, "judge_4@gmail.com"), (0, 10, 0.6, 2, 6, "judge_5@gmail.com"),
    (0, 10, 0.4, 2, 7, "judge_1@gmail.com"), (0, 10, 0.4, 2, 7, "judge_2@gmail.com"), (0, 10, 0.4, 2, 7, "judge_3@gmail.com"), (0, 10, 0.4, 2, 7, "judge_4@gmail.com"), (0, 10, 0.4, 2, 7, "judge_5@gmail.com"),

	(0, 10, 0.6, 1, 6, "judge_1@gmail.com"), (0, 10, 0.6, 1, 6, "judge_2@gmail.com"), (0, 10, 0.6, 1, 6, "judge_3@gmail.com"), (0, 10, 0.6, 1, 6, "judge_4@gmail.com"), (0, 10, 0.6, 1, 6, "judge_5@gmail.com"),
    (0, 10, 0.4, 1, 7, "judge_1@gmail.com"), (0, 10, 0.4, 1, 7, "judge_2@gmail.com"), (0, 10, 0.4, 1, 7, "judge_3@gmail.com"), (0, 10, 0.4, 1, 7, "judge_4@gmail.com"), (0, 10, 0.4, 1, 7, "judge_5@gmail.com");

UPDATE criteria
SET base_value = 10
WHERE contestant_id = 3 AND round_id = 6;
UPDATE criteria
SET base_value = 5
WHERE contestant_id = 3 AND round_id = 7;

UPDATE criteria
SET base_value = 7.5
WHERE contestant_id = 6 AND round_id = 6;
UPDATE criteria
SET base_value = 7.5
WHERE contestant_id = 6 AND round_id = 7;

UPDATE criteria
SET base_value = 5
WHERE contestant_id = 4 AND round_id = 6;
UPDATE criteria
SET base_value = 9
WHERE contestant_id = 4 AND round_id = 7;

UPDATE criteria
SET base_value = 8
WHERE contestant_id = 2 AND round_id = 6;
UPDATE criteria
SET base_value = 3
WHERE contestant_id = 2 AND round_id = 7;

UPDATE criteria
SET base_value = 7
WHERE contestant_id = 1 AND round_id = 6;
UPDATE criteria
SET base_value = 7
WHERE contestant_id = 1 AND round_id = 7;

INSERT INTO event_segment_contestant (event_id, segment_id, contestant_id)
VALUES
	(1, 3, 3),
    (1, 3, 6),
    (1, 3, 1);

INSERT INTO criteria (base_value, maximum_value, percentage_weight, contestant_id, round_id, judge_user_email)
VALUES
	(0, 10, 1, 3, 8, "judge_1@gmail.com"), (0, 10, 1, 3, 8, "judge_2@gmail.com"), (0, 10, 1, 3, 8, "judge_3@gmail.com"), (0, 10, 1, 3, 8, "judge_4@gmail.com"), (0, 10, 1, 3, 8, "judge_5@gmail.com"),
    (0, 10, 1, 6, 8, "judge_1@gmail.com"), (0, 10, 1, 6, 8, "judge_2@gmail.com"), (0, 10, 1, 6, 8, "judge_3@gmail.com"), (0, 10, 1, 6, 8, "judge_4@gmail.com"), (0, 10, 1, 6, 8, "judge_5@gmail.com"),
    (0, 10, 1, 1, 8, "judge_1@gmail.com"), (0, 10, 1, 1, 8, "judge_2@gmail.com"), (0, 10, 1, 1, 8, "judge_3@gmail.com"), (0, 10, 1, 1, 8, "judge_4@gmail.com"), (0, 10, 1, 1, 8, "judge_5@gmail.com");

UPDATE criteria
SET base_value = 8.5
WHERE contestant_id = 3 AND round_id = 8;

UPDATE criteria
SET base_value = 8.9
WHERE contestant_id = 6 AND round_id = 8;

UPDATE criteria
SET base_value = 8.7
WHERE contestant_id = 1 AND round_id = 8;
