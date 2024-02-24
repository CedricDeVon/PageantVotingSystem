




-- SELECT contestant_id, FLOOR(SUM(base_value * percentage_weight)) as total_value FROM 
-- (
-- 	SELECT base_value, maximum_value, criteria.percentage_weight, event.id as event_id, segment.id as segment_id, round_id, contestant_id, judge_user_email FROM event
-- 	INNER JOIN segment
-- 	ON event.id = segment.event_id
-- 	INNER JOIN round
-- 	ON segment.id = round.segment_id
-- 	JOIN criteria
-- 	ON round.id = criteria.round_id
-- ) data
-- WHERE event_id = 1 AND segment_id = 2
-- GROUP BY contestant_id;


-- Scoring

-- SELECT *, RANK() OVER (ORDER BY total_value DESC) as ranking
-- FROM
-- (
-- 	SELECT contestant_id, SUM(base_value * percentage_weight) as total_value FROM 
-- 	(
-- 		SELECT base_value, maximum_value, criteria.percentage_weight, event.id as event_id, segment.id as segment_id, round_id, contestant_id, judge_user_email FROM event
-- 		INNER JOIN segment
-- 		ON event.id = segment.event_id
-- 		INNER JOIN round
-- 		ON segment.id = round.segment_id
-- 		JOIN criteria
-- 		ON round.id = criteria.round_id
-- 	) data
-- 	WHERE event_id = 1 AND segment_id = 2
-- 	GROUP BY contestant_id
-- ) data;

-- SELECT contestant_id, total_value, RANK() OVER (ORDER BY total_value) as ranking
-- FROM 
-- 	(
--     SELECT contestant_id, AVG(base_value * percentage_weight) AS total_value
-- 	FROM (
-- 		SELECT base_value, maximum_value, criteria.percentage_weight, round_id, contestant_id, judge_user_email FROM segment
-- 		INNER JOIN round
-- 		ON segment.id = round.segment_id
-- 		INNER JOIN criteria
-- 		WHERE event_id = 1 AND segment_id = 1) event_segment_criteria_data
--     WHERE round_id >= 1 AND round_id <= 5
--     GROUP BY (contestant_id)
--     ORDER BY total_value ASC) ranking;

-- SELECT contestant_id, total_value, RANK() OVER (ORDER BY total_value) as ranking
-- FROM 
-- 	(SELECT contestant_id, base_value * percentage_weight AS total_value
-- 	FROM criteria
--     WHERE round_id = 6
--     GROUP BY (contestant_id)
--     ORDER BY total_value ASC) ranking
-- INNER JOIN
-- (SELECT contestant_id, total_value, RANK() OVER (ORDER BY total_value) as ranking
-- FROM 
-- 	(SELECT contestant_id, base_value * percentage_weight AS total_value
-- 	FROM criteria
--     WHERE round_id = 7
--     GROUP BY (contestant_id)
--     ORDER BY total_value ASC) ranking);


-- SELECT contestant_id, total_value, RANK() OVER (ORDER BY total_value) as ranking, CASE WHEN total_value < 6 THEN "TOP 5" ELSE null END as remark
-- FROM 
-- 	(SELECT contestant_id, AVG(base_value) AS total_value
-- 	FROM criteria
-- 	GROUP BY (contestant_id)
-- 	ORDER BY total_value ASC) results;
