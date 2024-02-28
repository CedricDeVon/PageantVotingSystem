-- USE pageant_voting_system_development;
-- USE pageant_voting_system_testing;
-- USE pageant_voting_system_production;

-- USE pageant_voting_system_development_backup;
-- USE pageant_voting_system_testing_backup;
-- USE pageant_voting_system_production_backup;

SELECT * FROM demo;

SELECT * FROM gender;
SELECT * FROM marital_status;
SELECT * FROM remark;
SELECT * FROM role;
SELECT * FROM image_resource;
SELECT * FROM user;
SELECT * FROM contestant;
SELECT * FROM event;
SELECT * FROM event_manager;
SELECT * FROM event_judge;
SELECT * FROM contestant_event_data;
SELECT * FROM segment;
SELECT * FROM round;
SELECT * FROM criteria;

-- Get All Contestants
SELECT contestant_event_data.contestant_id as contestant_id, height, weight, motto, home_address, talents_and_skills, hobbies, languages, job_experiences, education, gender_type, marital_status_type, image_resource_id
FROM contestant
INNER JOIN contestant_event_data
ON contestant_event_data.contestant_id = contestant.id
GROUP BY contestant.id;

-- Get One Contestant At A Specific Event
SET @target_event_id = 1;
SET @target_contestant_id = 1;
SELECT contestant_event_data.contestant_id as contestant_id, height, weight, motto, home_address, talents_and_skills, hobbies, languages, job_experiences, education, gender_type, marital_status_type, image_resource_id
FROM event_segment_contestant
INNER JOIN contestant_event_data
ON contestant_event_data.contestant_id = event_segment_contestant.contestant_id
WHERE event_segment_contestant.event_id = @target_event_id AND event_segment_contestant.contestant_id = @target_contestant_id
GROUP BY event_segment_contestant.contestant_id;

-- Get All Contestants At A Specific Event
SET @target_event_id = 1;
SELECT contestant_event_data.contestant_id as contestant_id, height, weight, motto, home_address, talents_and_skills, hobbies, languages, job_experiences, education, gender_type, marital_status_type, image_resource_id
FROM event_segment_contestant
INNER JOIN contestant_event_data
ON contestant_event_data.contestant_id = event_segment_contestant.contestant_id
WHERE event_segment_contestant.event_id = @target_event_id
GROUP BY event_segment_contestant.contestant_id;

-- Get One Contestant At A Specific Event and Segment
SET @target_event_id = 1;
SET @target_segment_id = 1;
SET @target_contestant_id = 3;
SELECT contestant_event_data.contestant_id as contestant_id, height, weight, motto, home_address, talents_and_skills, hobbies, languages, job_experiences, education, gender_type, marital_status_type, image_resource_id
FROM event_segment_contestant
INNER JOIN contestant_event_data
ON contestant_event_data.contestant_id = event_segment_contestant.contestant_id
WHERE event_segment_contestant.event_id = @target_event_id AND event_segment_contestant.segment_id = @target_segment_id AND event_segment_contestant.contestant_id = @target_contestant_id
GROUP BY event_segment_contestant.contestant_id;

-- Get All Contestants At A Specific Event And Segment
SET @target_event_id = 1;
SET @target_segment_id = 3;
SELECT contestant_event_data.contestant_id as contestant_id, height, weight, motto, home_address, talents_and_skills, hobbies, languages, job_experiences, education, gender_type, marital_status_type, image_resource_id
FROM event_segment_contestant
INNER JOIN contestant_event_data
ON contestant_event_data.contestant_id = event_segment_contestant.contestant_id
WHERE event_segment_contestant.event_id = @target_event_id AND event_segment_contestant.segment_id = @target_segment_id
GROUP BY event_segment_contestant.contestant_id;

-- Get Managers At A Specific Event
SET @target_event_id = 1;
SELECT email as manager_email, full_name
FROM event_manager
INNER JOIN user
ON user.email = event_manager.user_email
WHERE event_manager.event_id = @target_event_id
GROUP BY event_manager.user_email;

-- Get Judges At A Specific Event
SET @target_event_id = 1;
SELECT email as judge_email, full_name
FROM event_judge
INNER JOIN user
ON user.email = event_judge.user_email
WHERE event_judge.event_id = @target_event_id
GROUP BY event_judge.user_email;

-- Get Calculated Results At A Specific Event And Segment
SET @event_id = 1;
SET @segment_id = 1;
SELECT
	contestant_id, RANK() OVER (ORDER BY total_value DESC) as ranking, total_value FROM
(
	SELECT contestant_id, SUM(base_value * percentage_weight) as total_value FROM
	(
		SELECT base_value, criteria.percentage_weight, event.id as event_id, segment.id as segment_id, contestant_id FROM event
		INNER JOIN segment
		ON event.id = segment.event_id
		INNER JOIN round
		ON segment.id = round.segment_id
		JOIN criteria
		ON round.id = criteria.round_id
	) data
	WHERE event_id = @event_id AND segment_id = @segment_id
    GROUP BY contestant_id
) data;

-- Get Calculated Results At A Specific Event, Segment And Round
SET @event_id = 1;
SET @segment_id = 1;
SET @round_id = 1;
SELECT contestant_id, RANK() OVER (ORDER BY total_value DESC) as ranking, total_value FROM
(
	SELECT contestant_id, SUM(base_value * percentage_weight) as total_value FROM
	(
		SELECT base_value, criteria.percentage_weight, event.id as event_id, segment.id as segment_id, round.id as round_id, contestant_id FROM event
		INNER JOIN segment
		ON event.id = segment.event_id
		INNER JOIN round
		ON segment.id = round.segment_id
		JOIN criteria
		ON round.id = criteria.round_id
	) data
	WHERE event_id = @event_id AND segment_id = @segment_id AND round_id = @round_id
    GROUP BY contestant_id
) data;
