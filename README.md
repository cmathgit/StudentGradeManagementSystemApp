# StudentGradeManagementSystemApp
# Manage Student Grades stored on remote database via local GUI. Computer Science graduate course term project. Developed in Microsoft .Net and C# via VisualStudio IDE

# Business Requirements: The University's Registrar Office needs to update student course records after receiving student grades from all of the faculty at the end of each semester.  Each faculty member provides the office an Excel file with letter grades (A, B, C, D, or F) of the students who took course(s) with them.  The office is asking you to develop a system to manage the data for them.  They need you to develop a software system to do the following tasks:

# 1. Add new grades of courses for each student to a database:  The office can gather all the Excel files from the faculty, and put them in a folder.  The formats of the names for the folder and Excel files are: 

# Folder name:  “Grades [Year] [Semester]”, E.g., 
```
Grades 2023 Fall.csv
```

# File name:  “[Course Prefix] [Number] [Year] [Semester]”, E.g., 
```
MAT 234 2025 Spring.csv
```

# Dummy data has been provided in the Data folder

# 2. Edit a grade for a student:  There might be some mistakes in the Excel files provided by the faculty.  The system shall allow the office to edit the grade of a course for a student.  Editing a grade means changing the grade, deleting the grade, and/or adding a grade to the database. 

# 3. Print a report card (or transcript) for a student:  The system shall let them print a report card (or transcript) for a selected student.  The report card (or transcript) should list student’s name, id, overall gpa, and a list of courses with grades that he/she has taken before. 


# Technical Requiremnts:

# 1. We will apply structured analysis and design (not Object-oriented analysis and design) method to implement the project.  Although we are going to use C# in Visual Studio for coding, there will be no other classes to create except for the main (default) class.  All the functions in the class should be static. 

# 2. We will need to provide our own server to create and manage the database system. 

# 3. C# is very similar to Java.  It is your responsibility to learn the programming language and Visual Studio on your own. Debugging tips and basic instructions for use of the language and tool will be minimal

