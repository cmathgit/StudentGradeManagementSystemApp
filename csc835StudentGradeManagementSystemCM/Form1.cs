using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace csc835StudentGradeManagementSystemCM
{
    public partial class Form1 : Form
    {
        //declar class members
        char buttCheck = 'Z';
        double GPA = 0.0;
        double totalCredits_CM = 0.0;
        double totalWeightedGradePoints_CM = 0.0;

        // declare class methods
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        //click Add Record button
        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = true; // make Add Grade Record Form visible
            label9.Visible = false; // make instruction non visible
            label8.Visible = false; // make title non visible

            //disable other buttons
            button8.BackColor = Color.LightSkyBlue;
            button8.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            //clear the text boxes
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        // Cancel button for Add Grade Record form
        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false; // make Add Grade Record Form non visible
            label8.Visible = true;  // make title visible
            label9.Visible = true; // make instruction visible

            // enable other buttons
            button8.BackColor = Color.WhiteSmoke;
            button8.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        // click Modify Record button
        private void button3_Click_1(object sender, EventArgs e)
        {
            buttCheck = 'M'; // Mark the button clicked flag for the Modify Record Button
            panel4.Visible = true; //make Search for Grade Record form visible
            label9.Visible = false; // make instruction non visible
            label8.Visible = false; // make title non visible

            // disable other buttons
            button3.BackColor = Color.LightSkyBlue;
            button3.Enabled = false;
            button8.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
            button4.Enabled = false;

            //clear the text boxes
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox7.Clear(); //Student ID
            textBox8.Clear(); //Course Prefix
            textBox9.Clear(); //Course Number
            textBox10.Clear(); //Grade
            textBox11.Clear(); //Year
            textBox12.Clear(); //Semester
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        // Cancel button for Modify Grade Record form
        private void button9_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false; //make Modify Grade record non visible
            label8.Visible = true;  // make title visible
            label9.Visible = true; // make instruction visible

            // enable other buttons
            button3.BackColor = Color.WhiteSmoke;
            button3.Enabled = true;
            button8.Enabled = true;
            button2.Enabled = true;
            button1.Enabled = true;
            button4.Enabled = true;
        }

        // Cancel button for Search for Grade Record form
        private void button11_Click(object sender, EventArgs e)
        {
            if (buttCheck == 'M')
            {
                panel4.Visible = false; // make Search for Grade form non visible
                label8.Visible = true;  // make title visible
                label9.Visible = true; // make instruction visible

                // enable other buttons
                button3.BackColor = Color.WhiteSmoke;
                button3.Enabled = true;
                button8.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
                button4.Enabled = true;
            }
            else if (buttCheck == 'D')
            {
                panel4.Visible = false; // make Search for Grade form non visible
                label8.Visible = true;  // make title visible
                label9.Visible = true; // make instruction visible

                // enable other buttons
                button2.BackColor = Color.WhiteSmoke;
                button2.Enabled = true;
                button8.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = true;
                button4.Enabled = true;
            }
        }

        // search button for Search for Grade Record form
        private void button10_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the Search for Grade Record form
            bool recordFound = false;
            foreach (Control ctrl in panel4.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true)
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {
                /*
                 * Student ID:    textBox13.Text
                 * Course Prefix: textBox14.Text
                 * Course Number: textBox15.Text
                 * Year:          textBox16.Text
                 * Semester:      textBox17.Text
                 */

				//assuming a MariaDB or MySQL RDBMS  implementation exists (connections can be tested with a third party app, e.g., HeidiSQL), replace connection string parameters with your database server, database name, user name, and password. 
                string cnnStr = "Server=dummymariadb.abc.edu;Database=sample_db_name;Uid=dummy_user;Pwd=FakePassword1234;";

                using (MySqlConnection connection = new MySqlConnection(cnnStr))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Connected to the MariaDB database.");

                        // Select data using a SELECT statement
                        string retStudentID = textBox13.Text;
                        string retCoursePrefix = textBox14.Text;
                        string retCourseNum = textBox15.Text;
                        string retYear = textBox16.Text;
                        string retSemester = textBox17.Text;

                        string selectQuery = "SELECT StudentID, CoursePrefix, CourseNum, Year, Semester FROM csc835_cruzmacias_student_grades WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester;";

                        using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                        {
                            command.Parameters.AddWithValue("@StudentID", retStudentID);
                            command.Parameters.AddWithValue("@CoursePrefix", retCoursePrefix);
                            command.Parameters.AddWithValue("@CourseNum", retCourseNum);
                            command.Parameters.AddWithValue("@Year", retYear);
                            command.Parameters.AddWithValue("@Semester", retSemester);


                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string studentID = reader["StudentID"].ToString();
                                    string coursePrefix = reader["CoursePrefix"].ToString();
                                    string courseNum = reader["CourseNum"].ToString();
                                    string year = reader["Year"].ToString();
                                    string semester = reader["Semester"].ToString();

                                    Console.WriteLine($"Student Grade Record Found: {studentID} {coursePrefix} {courseNum} {year} {semester}");
                                    MessageBox.Show($"Searching for Student Grade Record: \nStudentID: {studentID}\nCoursePrefix: {coursePrefix}\nCourseNum: {courseNum}\nYear: {year}\nSemester: {semester}");
                                    recordFound = true;
                                }
                                else
                                {
                                    MessageBox.Show("No student grade record found for: \n" + "Student ID: " + textBox13.Text + "\nCourse Prefix: " + textBox14.Text + "\nCourse Number: " + textBox15.Text + "\nYear: " + textBox16.Text + "\nSemester:" + textBox17.Text); // display a message to the user and return to main menu    
                                    Console.WriteLine("No student grade record found for the specified student grade record: \n" + "Student ID: " + textBox13.Text + "\nCourse Prefix: " + textBox14.Text + "\nCourse Number: " + textBox15.Text + "\nYear: " + textBox16.Text + "\nSemester: " + textBox17.Text);
                                }
                            }
                        }

                        connection.Close();
                        Console.WriteLine("Connection closed.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

                if (recordFound)
                {
                    MessageBox.Show("Student grade record found: \n" + "Student ID: " + textBox13.Text + "\nCourse Prefix: " + textBox14.Text + "\nCourse Number: " + textBox15.Text + "\nYear: " + textBox16.Text + "\nSemester:" + textBox17.Text); // display a message to the user and return to main menu    

                    if (buttCheck == 'M')
                    {
                        panel3.Visible = true; //make Modify Grade Record form visible
                        panel4.Visible = false;  // make Search for Grade Record Form non visible

                        /* Modify Record Form
                         * Student ID:    textBox7.Text
                         * Course Prefix: textBox8.Text
                         * Course Number: textBox9.Text
                         * Grade:         textBox10.Text
                         * Year:          textBox11.Text
                         * Semester:      textBox12.Text
                         */

                        /*Search for Grade Record Form
                         * Student ID:    textBox13.Text
                         * Course Prefix: textBox14.Text
                         * Course Number: textBox15.Text
                         * Year:          textBox16.Text
                         * Semester:      textBox17.Text
                         */

                        //populate the text boxes
                        textBox7.Text = textBox13.Text; //Student ID
                        textBox8.Text = textBox14.Text; //Course Prefix
                        textBox9.Text = textBox15.Text; //Course Number
                        textBox10.Clear(); //Grade
                        textBox11.Text = textBox16.Text; //Year
                        textBox12.Text = textBox17.Text; //Semester
                    }
                    else if (buttCheck == 'D')
                    {
                        panel5.Visible = true; //make Delete Grade Record form visible
                        panel4.Visible = false;  // make Search for Grade Record Form non visible

                        /* Delete Grade Record Form
                         * Student ID:    textBox23.Text
                         * Course Prefix: textBox22.Text
                         * Course Number: textBox21.Text
                         * Year:          textBox19.Text
                         * Semester:      textBox18.Text
                         */

                        /*Search for Grade Record Form
                         * Student ID:    textBox13.Text
                         * Course Prefix: textBox14.Text
                         * Course Number: textBox15.Text
                         * Year:          textBox16.Text
                         * Semester:      textBox17.Text
                         */

                        //populate the text boxes
                        textBox18.Text = textBox17.Text; //Semester
                        textBox19.Text = textBox16.Text; //Year
                        textBox21.Text = textBox15.Text; //Course Number
                        textBox22.Text = textBox14.Text; //Course Prefix
                        textBox23.Text = textBox13.Text; //Student ID
                    }
                }
                else
                {
                    if (buttCheck == 'M')
                    {
                        panel4.Visible = false; // make Search for Grade form non visible
                        label8.Visible = true;  // make title visible
                        label9.Visible = true; // make instruction visible

                        // enable other buttons
                        button3.BackColor = Color.WhiteSmoke;
                        button3.Enabled = true;
                        button8.Enabled = true;
                        button2.Enabled = true;
                        button1.Enabled = true;
                        button4.Enabled = true;

                        //clear the textboxes
                        textBox7.Clear(); //Student ID
                        textBox8.Clear(); //Course Prefix
                        textBox9.Clear(); //Course Number
                        textBox10.Clear(); //Grade
                        textBox11.Clear(); //Year
                        textBox12.Clear(); //Semester
                    }
                    else if (buttCheck == 'D')
                    {
                        panel4.Visible = false; // make Search for Grade form non visible
                        label8.Visible = true;  // make title visible
                        label9.Visible = true; // make instruction visible

                        // enable other buttons
                        button2.BackColor = Color.WhiteSmoke;
                        button2.Enabled = true;
                        button8.Enabled = true;
                        button3.Enabled = true;
                        button1.Enabled = true;
                        button4.Enabled = true;

                        //clear the textboxes
                        textBox18.Clear(); //Semester
                        textBox19.Clear(); //Year
                        textBox21.Clear(); //Course Number
                        textBox22.Clear(); //Course Prefix
                        textBox23.Clear(); //Student ID
                    }
                }

            }
        }

        // submit button for New Grade Record form
        private void button7_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the New Grade Record form
            bool exceptionThrown = true;
            foreach (Control ctrl in panel2.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true)
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {
                /*
                 * Student ID:    textBox1.Text
                 * Course Prefix: textBox2.Text
                 * Course Number: textBox3.Text
                 * Grade:         textBox4.Text
                 * Year:          textBox5.Text
                 * Semester:      textBox6.Text
                 */

				//assuming a MariaDB or MySQL RDBMS  implementation exists (connections can be tested with a third party app, e.g., HeidiSQL), replace connection string parameters with your database server, database name, user name, and password. 
                string cnnStr = "Server=dummymariadb.abc.edu;Database=sample_db_name;Uid=dummy_user;Pwd=FakePassword1234;";

                using (MySqlConnection conn = new MySqlConnection(cnnStr))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connected to MariaDB database.");

                        //invoke a stored procedure to calculate GPA
                        //MessageBox.Show("Connection Established.");
                        //insert record into the database
                        // Insert data using an INSERT statement
                        string newStudentID = textBox1.Text;
                        string newCoursePrefix = textBox2.Text;
                        string newCourseNum = textBox3.Text;
                        string newGrade = textBox4.Text;
                        string newYear = textBox5.Text;
                        string newSemester = textBox6.Text;

                        string insert = "INSERT INTO csc835_cruzmacias_student_grades (StudentID, CoursePrefix, CourseNum, Grade, Year, Semester) VALUES (@StudentID, @CoursePrefix, @CourseNum, @Grade, @Year, @Semester);";

                        using (MySqlCommand command = new MySqlCommand(insert, conn))
                        {
                            command.Parameters.AddWithValue("@StudentID", newStudentID);
                            command.Parameters.AddWithValue("@CoursePrefix", newCoursePrefix);
                            command.Parameters.AddWithValue("@CourseNum", newCourseNum);
                            command.Parameters.AddWithValue("@Grade", newGrade);
                            command.Parameters.AddWithValue("@Year", newYear);
                            command.Parameters.AddWithValue("@Semester", newSemester);

                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"Rows affected: {rowsAffected}");
                        }

                        conn.Close();
                        //MessageBox.Show("Connection Terminated.");
                        Console.WriteLine("Connection closed.");
                        exceptionThrown = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        MessageBox.Show("Error: " + ex.Message);

                    }
                }

                if (!exceptionThrown)
                {
                    MessageBox.Show("New grade has been added successfully: \n" + "Student ID: " + textBox1.Text + "\nCourse Prefix: " + textBox2.Text + "\nCourse Number: " + textBox3.Text + "\nGrade: " + textBox4.Text + "\nYear: " + textBox5.Text + "\nSemester:" + textBox6.Text); // display a message to the user and return to main menu    
                    MessageBox.Show("Calculating new Overall GPA."); // display a message to the user and return to main menu    
                    //Update OverallGPA for imapcted student
                    /*
                     * Student ID:    textBox1.Text
                     * Course Prefix: textBox2.Text
                     * Course Number: textBox3.Text
                     * Grade:         textBox4.Text
                     * Year:          textBox5.Text
                     * Semester:      textBox6.Text
                     */

                    //reset GPA member to 0
                    GPA = 0.0;

                    using (MySqlConnection conn = new MySqlConnection(cnnStr))
                    {
                        try
                        {
                            conn.Open();
                            Console.WriteLine("Connected to MariaDB database.");

                            //invoke a stored procedure to calculate GPA
                            string StudentID = textBox1.Text;
                            string CoursePrefix = textBox2.Text;
                            string CourseNum = textBox3.Text;
                            string Grade = textBox4.Text;
                            string Year = textBox5.Text;
                            string Semester = textBox6.Text;

                            string selectTotalCredits = "SELECT SUM(A.Hours) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                            using (MySqlCommand command1 = new MySqlCommand(selectTotalCredits, conn))
                            {
                                command1.Parameters.AddWithValue("@StudentID", StudentID);

                                object queryResult = command1.ExecuteScalar(); // Retrieve the single value

                                if (queryResult != null && queryResult != DBNull.Value)
                                {
                                    double totalCredits = Convert.ToDouble(queryResult);
                                    totalCredits_CM = totalCredits;
                                    Console.WriteLine($"Total Credits: {totalCredits}");
                                }
                                else
                                {
                                    Console.WriteLine("No data found.");
                                }

                                int rowsAffected = command1.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            string selectTotalWeightedGradePoints = "SELECT SUM(A.Hours * CASE WHEN B.Grade = 'A' THEN 4.00 WHEN B.Grade = 'B' THEN 3.00 WHEN B.Grade = 'C' THEN 2.00 WHEN B.Grade = 'D' THEN 1.00 WHEN B.Grade = 'F' THEN 0.00 ELSE 0.00 END) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                            using (MySqlCommand command2 = new MySqlCommand(selectTotalWeightedGradePoints, conn))
                            {
                                command2.Parameters.AddWithValue("@StudentID", StudentID);

                                object queryResult1 = command2.ExecuteScalar(); // Retrieve the single value

                                if (queryResult1 != null && queryResult1 != DBNull.Value)
                                {
                                    double totalWeightedGradePoints = Convert.ToDouble(queryResult1);
                                    totalWeightedGradePoints_CM = totalWeightedGradePoints;
                                    Console.WriteLine($"Total Weighted Grade Points: {totalWeightedGradePoints}");
                                }
                                else
                                {
                                    Console.WriteLine("No data found.");
                                }

                                int rowsAffected = command2.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            //calculate the new Overall GPA
                            if (totalCredits_CM > 0)
                            {
                                GPA = totalWeightedGradePoints_CM / totalCredits_CM;
                            }

                            //reformat the GPA value
                            GPA = Convert.ToDouble(GPA.ToString("0.0"));

                            Console.WriteLine("GPA: " + GPA);

                            //update the student info table
                            string updateOverallGPA = "UPDATE csc835_cruzmacias_student_info SET OverallGPA = @GPA WHERE StudentID = @StudentID;";

                            using (MySqlCommand command3 = new MySqlCommand(updateOverallGPA, conn))
                            {
                                command3.Parameters.AddWithValue("@GPA", GPA);
                                command3.Parameters.AddWithValue("@StudentID", StudentID);

                                int rowsAffected = command3.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            conn.Close();
                            Console.WriteLine("Connection closed.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            MessageBox.Show("Error: " + ex.Message);

                        }
                    }

                }
                panel2.Visible = false;  // make Add Grade Record Form non visible
                label8.Visible = true; // make title visible
                label9.Visible = true; // make instruction visible

                // enable other buttons
                button8.BackColor = Color.WhiteSmoke;  // make Add Grade Record Form non visible
                button8.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }
        // Submit button for Modify Grade Record form
        private void button5_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the Modify Grade Record form
            bool exceptionThrown = true;
            foreach (Control ctrl in panel3.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true)
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {

                /*
                 * Student ID:    textBox7.Text
                 * Course Prefix: textBox8.Text
                 * Course Number: textBox9.Text
                 * Grade:         textBox10.Text
                 * Year:          textBox11.Text
                 * Semester:      textBox12.Text
                 */

				//assuming a MariaDB or MySQL RDBMS  implementation exists (connections can be tested with a third party app, e.g., HeidiSQL), replace connection string parameters with your database server, database name, user name, and password. 
                string cnnStr = "Server=dummymariadb.abc.edu;Database=sample_db_name;Uid=dummy_user;Pwd=FakePassword1234;";

                using (MySqlConnection conn = new MySqlConnection(cnnStr))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connected to MariaDB database.");

                        //update record in the database
                        string updStudentID = textBox7.Text;
                        string updCoursePrefix = textBox8.Text;
                        string updCourseNum = textBox9.Text;
                        string newGrade = textBox10.Text;
                        string updYear = textBox11.Text;
                        string updSemester = textBox12.Text;

                        string update = "UPDATE csc835_cruzmacias_student_grades SET Grade = @NewGrade WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester;";

                        using (MySqlCommand command = new MySqlCommand(update, conn))
                        {
                            command.Parameters.AddWithValue("@NewGrade", newGrade);
                            command.Parameters.AddWithValue("@StudentID", updStudentID);
                            command.Parameters.AddWithValue("@CoursePrefix", updCoursePrefix);
                            command.Parameters.AddWithValue("@CourseNum", updCourseNum);
                            command.Parameters.AddWithValue("@Year", updYear);
                            command.Parameters.AddWithValue("@Semester", updSemester);
                        
                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"Rows affected: {rowsAffected}");
                        }

                        conn.Close();
                        Console.WriteLine("Connection closed.");
                        exceptionThrown = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        MessageBox.Show("Error: " + ex.Message);

                    }
                }

                if (!exceptionThrown)
                { 
                    MessageBox.Show("Existing grade has been modified successfully: \n" + "New Grade: " + textBox12.Text + "\nStudent ID: " + textBox7.Text + "\nCourse Prefix: " + textBox8.Text + "\nCourse Number: " + textBox9.Text + "\nYear: " + textBox10.Text + "\nSemester:" + textBox11.Text); // display a message to the user and return to main menu    
                    MessageBox.Show("Calculating new Overall GPA."); // display a message to the user and return to main menu    
                    //Update OverallGPA for imapcted student
                    /*
                     * Student ID:    textBox7.Text
                     * Course Prefix: textBox8.Text
                     * Course Number: textBox9.Text
                     * Grade:         textBox10.Text
                     * Year:          textBox11.Text
                     * Semester:      textBox12.Text
                     */

                    //reset GPA member to 0
                    GPA = 0.0;

                    using (MySqlConnection conn = new MySqlConnection(cnnStr))
                    {
                        try
                        {
                            conn.Open();
                            Console.WriteLine("Connected to MariaDB database.");

                            //invoke a stored procedure to calculate GPA
                            string StudentID = textBox7.Text;
                            string CoursePrefix = textBox8.Text;
                            string CourseNum = textBox9.Text;
                            string Grade = textBox10.Text;
                            string Year = textBox11.Text;
                            string Semester = textBox12.Text;

                            string selectTotalCredits = "SELECT SUM(A.Hours) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                            using (MySqlCommand command1 = new MySqlCommand(selectTotalCredits, conn))
                            {
                                command1.Parameters.AddWithValue("@StudentID", StudentID);

                                object queryResult = command1.ExecuteScalar(); // Retrieve the single value

                                if (queryResult != null && queryResult != DBNull.Value)
                                {
                                    double totalCredits = Convert.ToDouble(queryResult);
                                    totalCredits_CM = totalCredits;
                                    Console.WriteLine($"Total Credits: {totalCredits}");
                                }
                                else
                                {
                                    Console.WriteLine("No data found.");
                                }

                                int rowsAffected = command1.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            string selectTotalWeightedGradePoints = "SELECT SUM(A.Hours * CASE WHEN B.Grade = 'A' THEN 4.00 WHEN B.Grade = 'B' THEN 3.00 WHEN B.Grade = 'C' THEN 2.00 WHEN B.Grade = 'D' THEN 1.00 WHEN B.Grade = 'F' THEN 0.00 ELSE 0.00 END) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                            using (MySqlCommand command2 = new MySqlCommand(selectTotalWeightedGradePoints, conn))
                            {
                                command2.Parameters.AddWithValue("@StudentID", StudentID);

                                object queryResult1 = command2.ExecuteScalar(); // Retrieve the single value

                                if (queryResult1 != null && queryResult1 != DBNull.Value)
                                {
                                    double totalWeightedGradePoints = Convert.ToDouble(queryResult1);
                                    totalWeightedGradePoints_CM = totalWeightedGradePoints;
                                    Console.WriteLine($"Total Weighted Grade Points: {totalWeightedGradePoints}");
                                }
                                else
                                {
                                    Console.WriteLine("No data found.");
                                }

                                int rowsAffected = command2.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            //calculate the new Overall GPA
                            if (totalCredits_CM > 0)
                            {
                                GPA = totalWeightedGradePoints_CM / totalCredits_CM;
                            }

                            //reformat the GPA value
                            GPA = Convert.ToDouble(GPA.ToString("0.0"));

                            Console.WriteLine("GPA: " + GPA);

                            //update the student info table
                            string updateOverallGPA = "UPDATE csc835_cruzmacias_student_info SET OverallGPA = @GPA WHERE StudentID = @StudentID;";

                            using (MySqlCommand command3 = new MySqlCommand(updateOverallGPA, conn))
                            {
                                command3.Parameters.AddWithValue("@GPA", GPA);
                                command3.Parameters.AddWithValue("@StudentID", StudentID);

                                int rowsAffected = command3.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            conn.Close();
                            Console.WriteLine("Connection closed.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            MessageBox.Show("Error: " + ex.Message);

                        }
                    }

                }


                panel3.Visible = false;  // make Add Grade Record Form non visible
                label8.Visible = true; // make title visible
                label9.Visible = true; // make instruction visible

                // enable other buttons
                button3.BackColor = Color.WhiteSmoke;  // make Add Grade Record Form non visible
                button3.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = true;
                button8.Enabled = true;
                button4.Enabled = true;
            }
        }

        // click Delete Record button
        private void button2_Click(object sender, EventArgs e)
        {
            buttCheck = 'D'; // Mark the button clicked flag for the Modify Record Button
            panel4.Visible = true; //make Search for Grade Record form visible
            label9.Visible = false; // make instruction non visible
            label8.Visible = false; // make title non visible

            // disable other buttons
            button2.BackColor = Color.LightSkyBlue;
            button2.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = false;
            button4.Enabled = false;

            //clear the text boxes
            textBox13.Clear();
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            textBox18.Clear(); //Semester
            textBox19.Clear(); //Year
            textBox21.Clear(); //Course Number
            textBox22.Clear(); //Course Prefix
            textBox23.Clear(); //Student ID
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        // submit button for Delete Grade Record form
        private void button12_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the Delete Grade Record form
            bool exceptionThrown = true;
            foreach (Control ctrl in panel5.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true)
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {
                /*
                 * Student ID:    textBox23.Text
                 * Course Prefix: textBox22.Text
                 * Course Number: textBox21.Text
                 * Year:          textBox19.Text
                 * Semester:      textBox18.Text
                 */

				//assuming a MariaDB or MySQL RDBMS  implementation exists (connections can be tested with a third party app, e.g., HeidiSQL), replace connection string parameters with your database server, database name, user name, and password. 
                string cnnStr = "Server=dummymariadb.abc.edu;Database=sample_db_name;Uid=dummy_user;Pwd=FakePassword1234;";

                using (MySqlConnection conn = new MySqlConnection(cnnStr))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connected to MariaDB database.");

                        //delete record from the database
                        string delStudentID = textBox23.Text;
                        string delCoursePrefix = textBox22.Text;
                        string delCourseNum = textBox21.Text;
                        string delYear = textBox19.Text;
                        string delSemester = textBox18.Text;

                        string delete = "DELETE FROM csc835_cruzmacias_student_grades WHERE StudentID = @StudentID AND CoursePrefix = @CoursePrefix AND CourseNum = @CourseNum AND Year = @Year AND Semester = @Semester;";

                       using (MySqlCommand command = new MySqlCommand(delete, conn))
                        {
                            command.Parameters.AddWithValue("@StudentID", delStudentID);
                            command.Parameters.AddWithValue("@CoursePrefix", delCoursePrefix);
                            command.Parameters.AddWithValue("@CourseNum", delCourseNum);
                            command.Parameters.AddWithValue("@Year", delYear);
                            command.Parameters.AddWithValue("@Semester", delSemester);

                            int rowsAffected = command.ExecuteNonQuery();
                            Console.WriteLine($"Rows affected: {rowsAffected}");
                        }

                        conn.Close();
                        Console.WriteLine("Connection closed.");
                        exceptionThrown = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        MessageBox.Show("Error: " + ex.Message);

                    }
                }

                if (!exceptionThrown)
                { 
                    MessageBox.Show("Existing grade has been deleted successfully: \n" + "Student ID: " + textBox23.Text + "\nCourse Prefix: " + textBox22.Text + "\nCourse Number: " + textBox21.Text +  "\nYear: " + textBox19.Text + "\nSemester:" + textBox18.Text); // display a message to the user and return to main menu    
                    MessageBox.Show("Calculating new Overall GPA."); // display a message to the user and return to main menu    
                    //Update OverallGPA for imapcted student
                    /*
                     * Student ID:    textBox23.Text
                     * Course Prefix: textBox22.Text
                     * Course Number: textBox21.Text
                     * Year:          textBox19.Text
                     * Semester:      textBox18.Text
                     */

                    //reset GPA member to 0
                    GPA = 0.0;

                    using (MySqlConnection conn = new MySqlConnection(cnnStr))
                    {
                        try
                        {
                            conn.Open();
                            Console.WriteLine("Connected to MariaDB database.");

                            //invoke a stored procedure to calculate GPA
                            string StudentID = textBox23.Text;
                            string CoursePrefix = textBox22.Text;
                            string CourseNum = textBox21.Text;
                            string Year = textBox19.Text;
                            string Semester = textBox18.Text;

                            string selectTotalCredits = "SELECT SUM(A.Hours) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                            using (MySqlCommand command1 = new MySqlCommand(selectTotalCredits, conn))
                            {
                                command1.Parameters.AddWithValue("@StudentID", StudentID);

                                object queryResult = command1.ExecuteScalar(); // Retrieve the single value

                                if (queryResult != null && queryResult != DBNull.Value)
                                {
                                    double totalCredits = Convert.ToDouble(queryResult);
                                    totalCredits_CM = totalCredits;
                                    Console.WriteLine($"Total Credits: {totalCredits}");
                                }
                                else
                                {
                                    Console.WriteLine("No data found.");
                                }

                                int rowsAffected = command1.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            string selectTotalWeightedGradePoints = "SELECT SUM(A.Hours * CASE WHEN B.Grade = 'A' THEN 4.00 WHEN B.Grade = 'B' THEN 3.00 WHEN B.Grade = 'C' THEN 2.00 WHEN B.Grade = 'D' THEN 1.00 WHEN B.Grade = 'F' THEN 0.00 ELSE 0.00 END) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                            using (MySqlCommand command2 = new MySqlCommand(selectTotalWeightedGradePoints, conn))
                            {
                                command2.Parameters.AddWithValue("@StudentID", StudentID);

                                object queryResult1 = command2.ExecuteScalar(); // Retrieve the single value

                                if (queryResult1 != null && queryResult1 != DBNull.Value)
                                {
                                    double totalWeightedGradePoints = Convert.ToDouble(queryResult1);
                                    totalWeightedGradePoints_CM = totalWeightedGradePoints;
                                    Console.WriteLine($"Total Weighted Grade Points: {totalWeightedGradePoints}");
                                }
                                else
                                {
                                    Console.WriteLine("No data found.");
                                    totalWeightedGradePoints_CM = 0.0;
                                }

                                int rowsAffected = command2.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            //calculate the new Overall GPA
                            if (totalCredits_CM > 0)
                            {
                                GPA = totalWeightedGradePoints_CM / totalCredits_CM;
                            }

                            //reformat the GPA value
                            GPA = Convert.ToDouble(GPA.ToString("0.0"));

                            Console.WriteLine("GPA: " + GPA);

                            //update the student info table
                            string updateOverallGPA = "UPDATE csc835_cruzmacias_student_info SET OverallGPA = @GPA WHERE StudentID = @StudentID;";

                            using (MySqlCommand command3 = new MySqlCommand(updateOverallGPA, conn))
                            {
                                command3.Parameters.AddWithValue("@GPA", GPA);
                                command3.Parameters.AddWithValue("@StudentID", StudentID);

                                int rowsAffected = command3.ExecuteNonQuery();
                                Console.WriteLine($"Rows affected: {rowsAffected}");
                            }

                            conn.Close();
                            Console.WriteLine("Connection closed.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            MessageBox.Show("Error: " + ex.Message);

                        }
                    }

                }

                panel5.Visible = false;  // make Add Grade Record Form non visible
                label8.Visible = true; // make title visible
                label9.Visible = true; // make instruction visible

                // enable other buttons
                button2.BackColor = Color.WhiteSmoke;  // make Add Grade Record Form non visible
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = true;
                button8.Enabled = true;
                button4.Enabled = true;
            }
        }

        // cancel button for Delete Grade Record form
        private void button13_Click(object sender, EventArgs e)
        {
            panel5.Visible = false; //make Modify Grade record non visible
            label8.Visible = true;  // make title visible
            label9.Visible = true; // make instruction visible

            // enable other buttons
            button2.BackColor = Color.WhiteSmoke;
            button2.Enabled = true;
            button8.Enabled = true;
            button3.Enabled = true;
            button1.Enabled = true;
            button4.Enabled = true;
        }

        private void label25_Click_1(object sender, EventArgs e)
        {

        }

        // Search for Student Record form Search button
        private void button15_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the Search for Student Record form
            foreach (Control ctrl in panel6.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true)
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {
                panel7.Visible = true; //make Print Report Card and Print Transcript buttons visible
                button15.BackColor = Color.LightSkyBlue;
                button15.Enabled = false; // disable the search button
                textBox20.Enabled = false;

            }
        }

        // Search for Student Record form Cancel button
        private void button14_Click(object sender, EventArgs e)
        {
            panel6.Visible = false; //make Search for Student Record non visible
            panel7.Visible = false; //make Print Report Card and Print Transcript buttons non visible
            label8.Visible = true;  // make title visible
            label9.Visible = true; // make instruction visible

            // enable other buttons
            button15.BackColor = Color.WhiteSmoke;
            button4.BackColor = Color.WhiteSmoke;
            button15.Enabled = true;
            textBox20.Enabled = true;
            button4.Enabled = true;
            button8.Enabled = true;
            button3.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;

            //clear the text boxes
            textBox20.Clear();
        }

        // click Print Record button
        private void button4_Click(object sender, EventArgs e)
        {
            panel6.Visible = true; //make Search for Grade Record form visible
            label9.Visible = false; // make instruction non visible
            label8.Visible = false; // make title non visible

            // disable other buttons
            button4.BackColor = Color.LightSkyBlue;
            button4.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
        }

        // Print Report Card button for the Search for Student Record form
        private void button17_Click(object sender, EventArgs e)
        {
            panel8.Visible = true; //make Select Semester and Year form visible
            panel7.Visible = false; //make Print Report Card and Print Transcript buttons non visible
        }

        // Print Transcript button for the Search for Student Record form
        private void button16_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the Search for Student Record form
            foreach (Control ctrl in panel6.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true)
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {

                string searchStudentID = textBox20.Text;

				//assuming a MariaDB or MySQL RDBMS  implementation exists (connections can be tested with a third party app, e.g., HeidiSQL), replace connection string parameters with your database server, database name, user name, and password. 
                string cnnStr = "Server=dummymariadb.abc.edu;Database=sample_db_name;Uid=dummy_user;Pwd=FakePassword1234;";
                string selectStudentQuery = "SELECT A.StudentID AS StudentID, A.Name AS Name, A.OverallGPA AS OverallGPA, B.CoursePrefix AS CoursePrefix, B.CourseNum AS CourseNum, B.Grade AS Grade, B.Year AS Year, B.Semester AS Semester FROM csc835_cruzmacias_student_info A, csc835_cruzmacias_student_grades B WHERE A.StudentID = B.StudentID AND A.StudentID = @StudentID;";

                using (MySqlConnection conn = new MySqlConnection(cnnStr))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connected to MariaDB database.");

                        using (MySqlCommand command10 = new MySqlCommand(selectStudentQuery, conn))
                        {
                            command10.Parameters.AddWithValue("@StudentID", searchStudentID);
                            MySqlDataReader reader = command10.ExecuteReader();
                            if (reader.Read())
                            {
                                // Fetch student data from the database
                                string Name = reader["Name"].ToString();
                                string OverallGPA = reader["OverallGPA"].ToString();
                                string StudentID = reader["StudentID"].ToString();
                                string CoursePrefix1 = reader["CoursePrefix"].ToString();
                                string CourseNum1 = reader["CourseNum"].ToString();
                                string Grade1 = reader["Grade"].ToString();
                                string Year1 = reader["Year"].ToString();
                                string Semester1 = reader["Semester"].ToString();
                                // Fetch more data as needed


                                // Generate the PDF report card
                                Document doc = new Document();
                                PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\Public\\Downloads\\Transcript.pdf", FileMode.Create));
                                doc.Open();

                                doc.Add(new Paragraph("Student Transcript"));
                                doc.Add(new Paragraph($"Student ID: {StudentID}"));
                                doc.Add(new Paragraph($"Name: {Name}"));
                                doc.Add(new Paragraph($"Overall GPA: {OverallGPA}"));
                                doc.Add(new Paragraph($"------------------------------------------"));
                                doc.Add(new Paragraph($"Course Prefix: {CoursePrefix1}"));
                                doc.Add(new Paragraph($"Course Number: {CourseNum1}"));
                                doc.Add(new Paragraph($"Grade: {Grade1}"));
                                doc.Add(new Paragraph($"Year: {Year1}"));
                                doc.Add(new Paragraph($"Semester: {Semester1}"));

                                while (reader.Read())
                                {
                                    // Fetch student data from the database
                                    string CoursePrefix = reader["CoursePrefix"].ToString();
                                    string CourseNum = reader["CourseNum"].ToString();
                                    string Grade = reader["Grade"].ToString();
                                    string Year = reader["Year"].ToString();
                                    string Semester = reader["Semester"].ToString();
                                    // Fetch more data as needed
                                    doc.Add(new Paragraph($"------------------------------------------"));
                                    doc.Add(new Paragraph($"Course Prefix: {CoursePrefix}"));
                                    doc.Add(new Paragraph($"Course Number: {CourseNum}"));
                                    doc.Add(new Paragraph($"Grade: {Grade}"));
                                    doc.Add(new Paragraph($"Year: {Year}"));
                                    doc.Add(new Paragraph($"Semester: {Semester}"));
                                    // Add more content as needed
                                }

                                doc.Close();
                                MessageBox.Show($"Transcript generated successfully for Student ID {StudentID}!");
                            }
                            else
                            {
                                MessageBox.Show("Student grade records not found!");
                            }
                        }

                        conn.Close();
                        Console.WriteLine("Connection closed.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        MessageBox.Show("Error: " + ex.Message);

                    }
                }

                MessageBox.Show("Printing transcript to C:\\Users\\Public\\Downloads\\."); // display a message to the user
                panel6.Visible = false; //make Search for Student Record non visible
                panel7.Visible = false; //make Print Report Card and Print Transcript buttons non visible
                label8.Visible = true; // make title visible
                label9.Visible = true; // make instruction visible

                //clear the text boxes
                textBox24.Clear();
                textBox25.Clear();
                textBox20.Clear();

                // enable other buttons
                button15.BackColor = Color.WhiteSmoke;
                button4.BackColor = Color.WhiteSmoke;
                button15.Enabled = true;
                textBox20.Enabled = true;
                button4.Enabled = true;
                button8.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        // Print button for the Select Semester and Year form
        private void button19_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the Search for Student Record form
            foreach (Control ctrl in panel8.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true)
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {
                MessageBox.Show("Printing report card."); // display a message to the user
                panel8.Visible = false;  // make Select Semester and Year Form non visible
                panel6.Visible = false; //make Search for Student Record non visible
                label8.Visible = true; // make title visible
                label9.Visible = true; // make instruction visible

                //clear the text boxes
                textBox20.Clear();

                // enable other buttons
                button15.BackColor = Color.WhiteSmoke;
                button4.BackColor = Color.WhiteSmoke;
                button15.Enabled = true;
                textBox20.Enabled = true;
                button4.Enabled = true;
                button8.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        //Cancel button for the Select Semester and Year form
        private void button18_Click(object sender, EventArgs e)
        {
            panel7.Visible = true; //make Print Report Card and Print Transcript buttons visible
            panel8.Visible = false; //make Select Semester and Year form non visible
            
            //clear the text boxes
            textBox24.Clear();
            textBox25.Clear();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        //Click Import Records button
        private void button1_Click(object sender, EventArgs e)
        {
            panel9.Visible = true; //make Search for Grade Record form visible
            label9.Visible = false; // make instruction non visible
            label8.Visible = false; // make title non visible

            // disable other buttons
            button1.BackColor = Color.LightSkyBlue;
            button1.Enabled = false;
            button8.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = false;
        }

        // Cancel button of the Import Student Grade Records Form
        private void button21_Click(object sender, EventArgs e)
        {
            panel9.Visible = false; //make Search for Student Record non visible
            label8.Visible = true;  // make title visible
            label9.Visible = true; // make instruction visible

            // enable other buttons
            button1.BackColor = Color.WhiteSmoke;
            button1.Enabled = true;
            button8.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = true;

            //clear the text boxes
            textBox26.Clear();

            //repopulate the text box
            textBox26.Text = "Enter full file path, e.g., \"C:\\Users\\JohnDoe\\Desktop\\Grades 2021 Spring\\CSC 835 2021 Spring.csv\"";
        }

        //Import button of Import Student Grade Records Form
        private void button20_Click(object sender, EventArgs e)
        {
            bool emptyTextBox = false; // status of the text boxes in the Search for Student Record form
            bool exceptionThrown = true;
            foreach (Control ctrl in panel9.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox)
                {
                    if (string.IsNullOrEmpty(ctrl.Text.Trim()))
                    {
                        emptyTextBox = true; // a text box is empty
                        break;
                    }
                }
            }
            if (emptyTextBox == true || textBox26.Text == "Enter full file path, e.g., \"C:\\Users\\JohnDoe\\Desktop\\Grades 2021 Spring\\CSC 835 2021 Spring.csv\"")
                MessageBox.Show("There is some data missing in the form."); // display a message to the user
            else
            {
                string csvFilePath = textBox26.Text;
				
				//assuming a MariaDB or MySQL RDBMS  implementation exists (connections can be tested with a third party app, e.g., HeidiSQL), replace connection string parameters with your database server, database name, user name, and password. 
                string cnnStr = "Server=dummymariadb.abc.edu;Database=sample_db_name;Uid=dummy_user;Pwd=FakePassword1234;";

                using (MySqlConnection conn = new MySqlConnection(cnnStr))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connected to MariaDB database.");

                        //invoke a stored procedure to insert new student grade records
                        using (StreamReader reader = new StreamReader(csvFilePath))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] values = line.Split(',');

                                // Assuming StudentID is the first column, CoursePrefix is the second, CourseNum is the third, Grade is the fourth, Year is the fifth, Semester is the sixth
                                string StudentID = values[0];
                                string CoursePrefix = values[1];
                                string CourseNum = values[2];
                                string Grade = values[3];
                                string Year = values[4];
                                string Semester = values[5];

                                string insertRecord = "INSERT INTO csc835_cruzmacias_student_grades (StudentID, CoursePrefix, CourseNum, Grade, Year, Semester) VALUES (@StudentID, @CoursePrefix, @CourseNum, @Grade, @Year, @Semester);";

                                using (MySqlCommand command5 = new MySqlCommand(insertRecord, conn))
                                {
                                    command5.Parameters.AddWithValue("@StudentID", StudentID);
                                    command5.Parameters.AddWithValue("@CoursePrefix", CoursePrefix);
                                    command5.Parameters.AddWithValue("@CourseNum", CourseNum);
                                    command5.Parameters.AddWithValue("@Grade", Grade);
                                    command5.Parameters.AddWithValue("@Year", Year);
                                    command5.Parameters.AddWithValue("@Semester", Semester);

                                    int rowsAffected = command5.ExecuteNonQuery();
                                    Console.WriteLine($"Rows affected: {rowsAffected}");
                                }
                            }
                        }
                        conn.Close();
                        Console.WriteLine("Connection closed.");
                        exceptionThrown = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        MessageBox.Show("Error: " + ex.Message);

                    }
                }

                if (!exceptionThrown)
                {
                    MessageBox.Show("Student Grade Records have been imported."); // display a message to the user   
                    MessageBox.Show("Calculating new Overall GPAs."); // display a message to the user and return to main menu    

                    using (MySqlConnection conn = new MySqlConnection(cnnStr))
                    {
                        try
                        {
                            conn.Open();
                            Console.WriteLine("Connected to MariaDB database.");

                            //invoke a stored procedure to calculate GPA
                            using (StreamReader reader = new StreamReader(csvFilePath))
                            {
                                string line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    //reset GPA member to 0
                                    GPA = 0.0;
                                    string[] values = line.Split(',');

                                    // Assuming StudentID is the first column, CoursePrefix is the second, CourseNum is the third, Grade is the fourth, Year is the fifth, Semester is the sixth
                                    string StudentID = values[0];
                                    string CoursePrefix = values[1];
                                    string CourseNum = values[2];
                                    string Grade = values[3];
                                    string Year = values[4];
                                    string Semester = values[5];

                                    string selectTotalCredits = "SELECT SUM(A.Hours) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                                    using (MySqlCommand command6 = new MySqlCommand(selectTotalCredits, conn))
                                    {
                                        command6.Parameters.AddWithValue("@StudentID", StudentID);

                                        object queryResult = command6.ExecuteScalar(); // Retrieve the single value

                                        if (queryResult != null && queryResult != DBNull.Value)
                                        {
                                            double totalCredits = Convert.ToDouble(queryResult);
                                            totalCredits_CM = totalCredits;
                                            Console.WriteLine($"Total Credits: {totalCredits}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("No data found.");
                                        }

                                        int rowsAffected = command6.ExecuteNonQuery();
                                        Console.WriteLine($"Rows affected: {rowsAffected}");
                                    }

                                    string selectTotalWeightedGradePoints = "SELECT SUM(A.Hours * CASE WHEN B.Grade = 'A' THEN 4.00 WHEN B.Grade = 'B' THEN 3.00 WHEN B.Grade = 'C' THEN 2.00 WHEN B.Grade = 'D' THEN 1.00 WHEN B.Grade = 'F' THEN 0.00 ELSE 0.00 END) FROM csc835_cruzmacias_courses A, csc835_cruzmacias_student_grades B WHERE 1 = 1 AND A.CoursePrefix = B.CoursePrefix AND A.CourseNum = B.CourseNum AND A.Semester = B. Semester AND A.Year = B.Year AND B.StudentID = @StudentID;";

                                    using (MySqlCommand command7 = new MySqlCommand(selectTotalWeightedGradePoints, conn))
                                    {
                                        command7.Parameters.AddWithValue("@StudentID", StudentID);

                                        object queryResult1 = command7.ExecuteScalar(); // Retrieve the single value

                                        if (queryResult1 != null && queryResult1 != DBNull.Value)
                                        {
                                            double totalWeightedGradePoints = Convert.ToDouble(queryResult1);
                                            totalWeightedGradePoints_CM = totalWeightedGradePoints;
                                            Console.WriteLine($"Total Weighted Grade Points: {totalWeightedGradePoints}");
                                        }
                                        else
                                        {
                                            Console.WriteLine("No data found.");
                                        }

                                        int rowsAffected = command7.ExecuteNonQuery();
                                        Console.WriteLine($"Rows affected: {rowsAffected}");
                                    }


                                    //calculate the new Overall GPA
                                    if (totalCredits_CM > 0)
                                    {
                                        GPA = totalWeightedGradePoints_CM / totalCredits_CM;
                                    }

                                    //reformat the GPA value
                                    GPA = Convert.ToDouble(GPA.ToString("0.0"));

                                    Console.WriteLine("GPA: " + GPA);

                                    //update the student info table
                                    string updateOverallGPA = "UPDATE csc835_cruzmacias_student_info SET OverallGPA = @GPA WHERE StudentID = @StudentID;";

                                    using (MySqlCommand command8 = new MySqlCommand(updateOverallGPA, conn))
                                    {
                                        command8.Parameters.AddWithValue("@GPA", GPA);
                                        command8.Parameters.AddWithValue("@StudentID", StudentID);

                                        int rowsAffected = command8.ExecuteNonQuery();
                                        Console.WriteLine($"Rows affected: {rowsAffected}");
                                    }
                                }
                            }

                            conn.Close();
                            Console.WriteLine("Connection closed.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            MessageBox.Show("Error: " + ex.Message);

                        }
                    }
                }
                
                panel9.Visible = false;  // make Select Semester and Year Form non visible
                label8.Visible = true; // make title visible
                label9.Visible = true; // make instruction visible

                //clear the text boxes
                textBox26.Clear();

                //repopulate the text box
                textBox26.Text = "Enter full file path, e.g., \"C:\\Users\\JohnDoe\\Desktop\\Grades 2021 Spring\\CSC 835 2021 Spring.csv\"";

                // enable other buttons
                button1.BackColor = Color.WhiteSmoke;
                button1.Enabled = true;
                button8.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button2.Enabled = true;
            }
        }
    }
}
