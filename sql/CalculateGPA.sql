CREATE DEFINER=`student` PROCEDURE `csc835_cruzmacias_CalculateGPA`(
	IN `Student_ID` VARCHAR(50)
)
LANGUAGE SQL
NOT DETERMINISTIC
CONTAINS SQL
SQL SECURITY DEFINER
COMMENT 'Stored procedure for calculating student GPA to be invoked by Student Grade Management System application by Cruz Macias'


BEGIN
    DECLARE total_credits DECIMAL(5, 2);
    DECLARE total_weighted_grade_points DECIMAL(8, 2);
    DECLARE gpa DECIMAL(4, 2);

    SET total_credits = 0;
    SET total_weighted_grade_points = 0;
    
    SELECT SUM(A.Hours) INTO total_credits
	 FROM csc835_cruzmacias_courses A
	 , csc835_cruzmacias_student_grades B
	 WHERE 1 = 1
	 AND A.CoursePrefix = B.CoursePrefix
	 AND A.CourseNum = B.CourseNum
	 AND A.Semester = B. Semester
	 AND A.Year = B.Year
	 AND B.StudentID = Student_ID;

    SELECT SUM(A.Hours * CASE
                            WHEN B.Grade = 'A' THEN 4.00
                            WHEN B.Grade = 'B' THEN 3.00
                            WHEN B.Grade = 'C' THEN 2.00
                            WHEN B.Grade = 'D' THEN 1.00
                            WHEN B.Grade = 'F' THEN 0.00
                            ELSE 0.00
                          END) AS total_weighted_grade_points
    FROM csc835_cruzmacias_courses A
	 , csc835_cruzmacias_student_grades B
    WHERE 1 = 1
    AND A.CoursePrefix = B.CoursePrefix
	 AND A.CourseNum = B.CourseNum
	 AND A.Semester = B. Semester
	 AND A.Year = B.Year
	 AND B.StudentID = Student_ID;

    IF total_credits > 0 THEN
        SET gpa = total_weighted_grade_points / total_credits;
    ELSE
        SET gpa = 0;
    END IF;

   -- SELECT gpa;
   UPDATE csc835_cruzmacias_student_info SET OverallGPA = gpa WHERE StudentID = Student_ID;
END;