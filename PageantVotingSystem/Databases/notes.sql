
-- INSERT INTO result (event_id, segment_id, round_id, criterium_id, contestant_id, judge_user_email, base_value)
-- VALUES
--       (1, 1, 1, 1, 1, 'judge_1@gmail.com', 10), (1, 1, 1, 1, 1, 'judge_2@gmail.com', 25),
-- 	  (1, 1, 1, 2, 1, 'judge_1@gmail.com', 10), (1, 1, 1, 2, 1, 'judge_2@gmail.com', 25),

-- 	  (1, 1, 1, 1, 2, 'judge_1@gmail.com', 7), (1, 1, 1, 1, 2, 'judge_2@gmail.com', 14),
--       (1, 1, 1, 2, 2, 'judge_1@gmail.com', 7), (1, 1, 1, 2, 2, 'judge_2@gmail.com', 14),

--       (1, 1, 1, 1, 3, 'judge_1@gmail.com', 25), (1, 1, 1, 1, 3, 'judge_2@gmail.com', 25),
--       (1, 1, 1, 2, 3, 'judge_1@gmail.com', 20), (1, 1, 1, 2, 3, 'judge_2@gmail.com', 20);

SELECT contestant.id, order_number, full_name
FROM
(
SELECT contestant_id
FROM round_contestant
WHERE round_contestant_status_type = 'Pending'  AND round_id = 1
GROUP BY contestant_id
) data
INNER JOIN contestant
ON contestant.id = data.contestant_id
ORDER BY order_number DESC;

USE pageant_voting_system_development;
SELECT contestant.id, order_number, full_name
FROM
(
	SELECT contestant_id
	FROM round_contestant
    WHERE
		round_id = 1 AND
        judge_user_email = 'judge_1@gmail.com' AND
        round_contestant_status_type = 'Incomplete'
	GROUP BY contestant_id
) data
INNER JOIN contestant
ON contestant.id = data.contestant_id
ORDER BY order_number DESC;

SELECT event_layout.event_id, event.name, event_layout.segment_id, event_layout.round_id
FROM
(
SELECT event_id
FROM event_judge
WHERE judge_user_email = 'judge_1@gmail.com'
 ) data
INNER JOIN event_layout
INNER JOIN event
ON event_layout.event_id = data.event_id AND event.id = data.event_id
WHERE event_layout_status_type = 'Ongoing'
ORDER BY name DESC;
 

-- CreateNewRoundContestant
-- INSERT INTO round_contestant (round_id, contestant_id, judge_user_email) VALUES ()

-- ReadNextEventLayout
SELECT event_id, segment_id, round_id, event_layout_status_type FROM event_layout WHERE event_layout_status_type = 'Incomplete' AND event_id = 1 LIMIT 1;

-- SetEventLayoutToComplete
-- UPDATE event_layout SET event_layout_status_type = 'Complete' WHERE event_layout_status_type = 'Ongoing' AND event_id = 1;

-- SetEventLayoutToCurrent
-- UPDATE event_layout SET event_layout_status_type = 'Ongoing' WHERE round_id = 1;

-- SetEventLayoutToIncomplete
-- UPDATE event_layout SET event_layout_status_type = 'Incomplete' WHERE round_id = 1;

-- IsIncompleteRoundContestantFound
-- SELECT round_id FROM round_contestant WHERE round_contestant_status_type = 'Incomplete' AND round_id = 1;

-- IsIncompleteEventLayoutFound
-- SELECT event_id FROM event_layout WHERE event_id = 1 AND event_layout_status_type = 'Incomplete';

-- Read Qualified Segment Contestants
-- SELECT
-- 	RANK() OVER (ORDER BY net_value DESC) as ranking_number, contestant_id
-- FROM
-- (
-- 	SELECT contestant_id, ROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value, ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value
--     FROM result
--     INNER JOIN criterium
--     ON id = criterium_id
--     WHERE result.segment_id = 1
--     GROUP BY contestant_id
-- ) data
-- INNER JOIN contestant
-- ON contestant.id = contestant_id
-- LIMIT 2;

-- SELECT
-- 	contestant_id, RANK() OVER (ORDER BY net_value DESC) as ranking_number, order_number, full_name, ROUND(net_value / maximum_value * 100, 2) as net_percentage, net_value,  maximum_value
-- FROM
-- (
-- 	SELECT contestant_id, ROUND(SUM(base_value * (percentage_weight / 100)), 2) as net_value, ROUND(SUM(maximum_value * (percentage_weight / 100)), 2) as maximum_value
--     FROM result
--     INNER JOIN criterium
--     ON id = criterium_id
--     WHERE result.segment_id = 1
--     GROUP BY contestant_id
-- ) data
-- INNER JOIN contestant
-- ON contestant.id = contestant_id;



-- If complete round contestants AND complete event layouts, THEN go to dashboard
-- If complete round contestants, THEN create new rounds and filter accepted contestants
-- If incomplete round contestants found, THEN go to Administer Event Form




-- SELECT * FROM round_contestant;

-- UPDATE round_contestant
-- SET round_contestant_status_type = 'Complete'
-- WHERE round_id = 1 AND contestant_id = 1 AND judge_user_email = 'judge_2@gmail.com';

-- SELECT order_number, full_name, round_contestant_status_type
-- FROM round_contestant
-- INNER JOIN user
-- INNER JOIN event_judge
-- ON
-- 	user.email = round_contestant.judge_user_email AND
--     event_judge.judge_user_email = round_contestant.judge_user_email
-- WHERE round_id = 1 AND contestant_id = 1;

-- SELECT *
-- FROM round_contestant
-- WHERE round_id = 1 AND contestant_id = 1;

-- UPDATE round_contestant
-- SET round_contestant_status_type = 'Pending'
-- WHERE round_id = 1 AND contestant_id = 1;


-- GET Ongoing Events
SELECT event.id, event.name
FROM event_manager
INNER JOIN event_layout
INNER JOIN event
ON
	event_layout.event_id = event_manager.event_id AND
    event.id = event_manager.event_id
WHERE
	manager_user_email = 'manager_1@gmail.com' AND
    event_layout_status_type = 'Ongoing';
    
-- Is Ongoing Event Found
SELECT COUNT(event_manager.event_id) as count
FROM event_manager
INNER JOIN event_layout
ON event_layout.event_id = event_manager.event_id
WHERE
	manager_user_email = 'manager_1@gmail.com' AND
    event_layout_status_type = 'Ongoing';

-- GET Last Ids
SELECT id FROM event ORDER BY id DESC LIMIT 1;
SELECT id FROM segment ORDER BY id DESC LIMIT 1;
SELECT id FROM round ORDER BY id DESC LIMIT 1;
SELECT id FROM criterium ORDER BY id DESC LIMIT 1;

-- GET Event, Segment, Round Layout
USE pageant_voting_system_development;
SELECT event_manager.event_id, segment_id, round_id
FROM event_manager
INNER JOIN event_layout
ON event_manager.event_id = event_layout.event_id
WHERE
	manager_user_email = 'manager_1@gmail.com' AND
    event_layout_status_type = 'Ongoing';

-- GET Judges
SELECT order_number, judge_user_email, judge_status_type
FROM event_judge
WHERE event_id = 1
ORDER BY order_number DESC;

-- GET Contestants
-- SELECT contestant_id as id, order_number, full_name, contestant_status_type
-- contestant.id, order_number, full_name, round_contestant_status_type

-- GET Event Segment Rounds
SELECT round_id as id, name, event_layout_status_type
FROM event_layout
INNER JOIN round
ON round.id = event_layout.round_id
WHERE event_layout.segment_id = 1
ORDER BY round_id DESC;

-- GET Judge Status
SELECT order_number, full_name, round_contestant_status_type
FROM round_contestant
INNER JOIN event_judge
INNER JOIN user
ON
	event_judge.judge_user_email = round_contestant.judge_user_email AND
    user.email = round_contestant.judge_user_email
WHERE
	round_id = 1 AND
    contestant_id = 1
ORDER BY order_number DESC;

    
-- CREATE TABLE IF NOT EXISTS application_save_state
-- (
--     name VARCHAR(128) PRIMARY KEY,
--     value VARCHAR(128),
-- 	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
--     timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
-- );

-- CREATE TABLE IF NOT EXISTS judge_save_state
-- (
--     judge_user_email VARCHAR(128) PRIMARY KEY,
--     round_id INT UNSIGNED,
-- 	timestamp_created TIMESTAMP DEFAULT NOW() NOT NULL,
--     timestamp_modified TIMESTAMP DEFAULT NOW() NOT NULL
-- );

-- ALTER TABLE judge_save_state
-- ADD FOREIGN KEY(judge_user_email)
-- REFERENCES user(email)
-- ON DELETE CASCADE;
-- ALTER TABLE judge_save_state
-- ADD FOREIGN KEY(round_id)
-- REFERENCES round(id)
-- ON DELETE CASCADE;



-- INSERT INTO result (event_id, segment_id, round_id, criterium_id, contestant_id, judge_user_email, base_value)
-- VALUES
--     (1, 1, 1, 1, 1, 'judge_1@gmail.com', 50), (1, 1, 1, 1, 1, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 1, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 2, 1, 'judge_1@gmail.com', 50), (1, 1, 1, 2, 1, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 1, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 3, 1, 'judge_1@gmail.com', 50), (1, 1, 1, 3, 1, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 1, 'judge_3@gmail.com', 59.5),

--     (1, 1, 1, 1, 2, 'judge_1@gmail.com', 60), (1, 1, 1, 1, 2, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 2, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 2, 2, 'judge_1@gmail.com', 60), (1, 1, 1, 2, 2, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 2, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 3, 2, 'judge_1@gmail.com', 60), (1, 1, 1, 3, 2, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 2, 'judge_3@gmail.com', 59.5),

--     (1, 1, 1, 1, 3, 'judge_1@gmail.com', 70), (1, 1, 1, 1, 3, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 3, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 2, 3, 'judge_1@gmail.com', 70), (1, 1, 1, 2, 3, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 3, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 3, 3, 'judge_1@gmail.com', 70), (1, 1, 1, 3, 3, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 3, 'judge_3@gmail.com', 59.5),

--     (1, 1, 1, 1, 4, 'judge_1@gmail.com', 80), (1, 1, 1, 1, 4, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 4, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 2, 4, 'judge_1@gmail.com', 80), (1, 1, 1, 2, 4, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 4, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 3, 4, 'judge_1@gmail.com', 80), (1, 1, 1, 3, 4, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 4, 'judge_3@gmail.com', 59.5),

--     (1, 1, 1, 1, 5, 'judge_1@gmail.com', 90), (1, 1, 1, 1, 5, 'judge_2@gmail.com', 55.85), (1, 1, 1, 1, 5, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 2, 5, 'judge_1@gmail.com', 90), (1, 1, 1, 2, 5, 'judge_2@gmail.com', 55.85), (1, 1, 1, 2, 5, 'judge_3@gmail.com', 59.5),
--     (1, 1, 1, 3, 5, 'judge_1@gmail.com', 90), (1, 1, 1, 3, 5, 'judge_2@gmail.com', 55.85), (1, 1, 1, 3, 5, 'judge_3@gmail.com', 59.5),



--     (1, 1, 2, 4, 1, 'judge_1@gmail.com', 50), (1, 1, 2, 4, 1, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 1, 'judge_3@gmail.com', 59.5),
--     (1, 1, 2, 5, 1, 'judge_1@gmail.com', 50), (1, 1, 2, 5, 1, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 1, 'judge_3@gmail.com', 59.5),

--     (1, 1, 2, 4, 2, 'judge_1@gmail.com', 60), (1, 1, 2, 4, 2, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 2, 'judge_3@gmail.com', 59.5),
--     (1, 1, 2, 5, 2, 'judge_1@gmail.com', 60), (1, 1, 2, 5, 2, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 2, 'judge_3@gmail.com', 59.5),

--     (1, 1, 2, 4, 3, 'judge_1@gmail.com', 70), (1, 1, 2, 4, 3, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 3, 'judge_3@gmail.com', 59.5),
--     (1, 1, 2, 5, 3, 'judge_1@gmail.com', 70), (1, 1, 2, 5, 3, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 3, 'judge_3@gmail.com', 59.5),

--     (1, 1, 2, 4, 4, 'judge_1@gmail.com', 80), (1, 1, 2, 4, 4, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 4, 'judge_3@gmail.com', 59.5),
--     (1, 1, 2, 5, 4, 'judge_1@gmail.com', 80), (1, 1, 2, 5, 4, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 4, 'judge_3@gmail.com', 59.5),

--     (1, 1, 2, 4, 5, 'judge_1@gmail.com', 90), (1, 1, 2, 4, 5, 'judge_2@gmail.com', 55.85), (1, 1, 2, 4, 5, 'judge_3@gmail.com', 59.5),
--     (1, 1, 2, 5, 5, 'judge_1@gmail.com', 90), (1, 1, 2, 5, 5, 'judge_2@gmail.com', 55.85), (1, 1, 2, 5, 5, 'judge_3@gmail.com', 59.5),



--     (1, 2, 3, 6, 3, 'judge_1@gmail.com', 90), (1, 2, 3, 6, 3, 'judge_2@gmail.com', 95.85), (1, 2, 3, 6, 3, 'judge_3@gmail.com', 99.5),
--     (1, 2, 3, 6, 4, 'judge_1@gmail.com', 80), (1, 2, 3, 6, 4, 'judge_2@gmail.com', 85.85), (1, 2, 3, 6, 4, 'judge_3@gmail.com', 89.5),
--     (1, 2, 3, 6, 5, 'judge_1@gmail.com', 70), (1, 2, 3, 6, 5, 'judge_2@gmail.com', 55.85), (1, 2, 3, 6, 5, 'judge_3@gmail.com', 59.5);


-- GET Judge Status (Old)
-- SELECT
-- 	order_number,
--     data.judge_user_email,
-- 	SUM(round_contestant_status_type = 'Complete') as complete,
-- 	COUNT(round_contestant_status_type) as total
-- FROM
-- (
-- SELECT order_number, judge_user_email, judge_status_type
-- FROM event_judge
-- WHERE event_id = 1
-- ) data
-- INNER JOIN round_contestant
-- ON round_contestant.judge_user_email = data.judge_user_email
-- WHERE round_id = 1
-- GROUP BY judge_user_email
-- ORDER BY order_number ASC;
-- SELECT order_number, data.judge_user_email, round_contestant_status_type
-- FROM
-- (
-- SELECT order_number, judge_user_email, judge_status_type
-- FROM event_judge
-- WHERE event_id = 1
-- ) data
-- INNER JOIN round_contestant
-- ON round_contestant.judge_user_email = data.judge_user_email
-- WHERE
-- 	round_id = 1 AND
--     contestant_id = 1
-- GROUP BY data.judge_user_email
-- ORDER BY order_number ASC;






-- SELECT order_number, data.judge_user_email, complete, total
-- FROM
-- (
-- SELECT
-- 	round_contestant.judge_user_email,
--     SUM(round_contestant_status_type = 'Complete') as complete,
--     COUNT(round_contestant_status_type) as total
-- FROM round_contestant
-- WHERE round_id = 4
-- GROUP BY round_contestant.judge_user_email
-- ) data
-- INNER JOIN event_judge
-- ON event_judge.judge_user_email = data.judge_user_email
-- WHERE event_id = 2
-- ORDER BY order_number ASC;



-- INNER JOIN event_judge
-- INNER JOIN user
-- ON
-- 	event_judge.judge_user_email = data.judge_user_email AND
--     user.email = data.judge_user_email;


-- INNER JOIN round_contestant
-- ON
-- 	round_contestant.round_id = result.round_id AND
--     round_contestant.contestant_id = result.contestant_id
-- WHERE
-- 	result.event_id = 2 AND
--     result.segment_id = 3 AND
-- 	result.round_id = 4 AND
--     result.judge_user_email = 'judge_1@gmail.com' AND
--     round_contestant_status_type = 'Incomplete';
--     

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



INSERT INTO contestant (order_number, image_resource_path, full_name, email, phone_number, home_address, birth_date, gender_type, marital_status_type, height_in_centimeters, weight_in_kilograms, talents_and_skills, hobbies, languages, work_experiences, education, motto)
VALUES
    (1, '../../Profiles/DefaultProfile.png', 'Contestant - A', 'contestant_1@gmail.com', '1111-111-111', 'Home Address - A', '2000-01-01', 'Female', 'Single', 190, 60, 'Talents and Skills - A', 'Hobbies - A', 'Languages - A', 'Work Experiences - A', 'Education - A', 'Motto - A'),
    (2, '../../Profiles/DefaultProfile.png', 'Contestant - B', 'contestant_1@gmail.com', '2222-222-222', 'Home Address - B', '2000-01-01', 'Female', 'Single', 190, 60, 'Talents and Skills - B', 'Hobbies - B', 'Languages - B', 'Work Experiences - B', 'Education - B', 'Motto - B'),
    (3, '../../Profiles/DefaultProfile.png', 'Contestant - C', 'contestant_1@gmail.com', '3333-333-333', 'Home Address - C', '2000-01-01', 'Female', 'Single', 190, 60, 'Talents and Skills - B', 'Hobbies - C', 'Languages - C', 'Work Experiences - C', 'Education - C', 'Motto - C');

INSERT INTO event (id, name, description, host_address, scoring_system_type)
VALUES
    (1, 'Event - A', 'Description - A', 'Host Address - A', 'Percentage');

INSERT INTO segment (id, name, description, maximum_contestant_count, event_id)
VALUES
    (1, 'Segment - A', 'Description - A', 3, 1),
    (2, 'Segment - B', 'Description - B', 2, 1);

INSERT INTO round (id, name, description, segment_id)
VALUES
    (1, 'Round - A', 'Description - A', 1),
    (2, 'Round - B', 'Description - B', 1),
    (3, 'Round - C', 'Description - C', 2);
    
INSERT INTO criterium (id, name, description, maximum_value, percentage_weight, round_id)
VALUES
    (1, 'Criterium - A', 'Description - A', 40, 50, 1),
    (2, 'Criterium - B', 'Description - B', 60, 30, 1),
    (3, 'Criterium - C', 'Description - C', 20, 60, 2),
    (4, 'Criterium - D', 'Description - D', 80, 40, 2),
    (5, 'Criterium - E', 'Description - E', 100, 100, 3);

INSERT INTO event_layout (event_id, segment_id, round_id, event_layout_status_type)
VALUES
	(1, 1, 1, 'Ongoing'),
    (1, 1, 2, 'Incomplete'),
    (1, 2, 3, 'Incomplete');

INSERT INTO event_manager (event_id, manager_user_email)
VALUES
    (1, 'manager_1@gmail.com');

INSERT INTO event_judge (event_id, order_number, judge_user_email)
VALUES
    (1, 1, 'judge_1@gmail.com'),
    (1, 2, 'judge_2@gmail.com');

INSERT INTO event_contestant (event_id, contestant_id)
VALUES
    (1, 1),
    (1, 2),
    (1, 3);

INSERT INTO round_contestant (round_id, contestant_id, judge_user_email, round_contestant_status_type)
VALUES
	(1, 1, 'judge_1@gmail.com', 'Incomplete'),
    (1, 2, 'judge_1@gmail.com', 'Incomplete'),
    (1, 3, 'judge_1@gmail.com', 'Incomplete'),
	(1, 1, 'judge_2@gmail.com', 'Incomplete'),
    (1, 2, 'judge_2@gmail.com', 'Incomplete'),
    (1, 3, 'judge_2@gmail.com', 'Incomplete');

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
