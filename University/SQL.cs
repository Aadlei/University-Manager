using System;
using MySql.Data.MySqlClient;

namespace University
{
    public class Sql
    {
        // Connection to database
        private const string DBfilePath = @"server=localhost; userid=root; password=#Xq8Xr&glzH3#pCqbkA@641irO7D$xNADA6WPDT;database=university;";
        private MySqlConnection _con;

        // Code for connection, remember to close it off when using the function
        private MySqlConnection Connection()
        {
            _con = new MySqlConnection(DBfilePath);
            _con.Open();
            return _con;
        }
        
        // Functions //
        
        // STUDENT FUNCTIONS //
        // SELECT STUDENT - GET ID //
        public int SelectStudent()
        {
            _con = Connection();
            const string findStudentsQuery = "Select * FROM Students";
            var cmd = new MySqlCommand(findStudentsQuery, _con);
            MySqlDataReader finder = cmd.ExecuteReader();
            
            // Selection that allows you to go back to main menu
            Console.WriteLine(" ");
            Console.WriteLine("0. Go back.");
            while (finder.Read())
            {
                Console.WriteLine(
                    finder.GetString(0) + ". " +
                    finder.GetString(1) + " " +
                    finder.GetString(2) + ", " +
                    finder.GetString(3) + ", " +
                    finder.GetString(4));
            }
            
            // Returns the student ID for use in other functions
            Console.WriteLine(" ");
            Console.WriteLine("Enter the ID of the student you wish to modify.");
            int id = Convert.ToInt32(Console.ReadLine());
            _con.Close();
            return id;
        }
        
        // PRINT ALL STUDENTS
        public void PrintAllStudents()
        {
            _con = Connection();
            // Concatenates the firstname and lastname so it becomes a full name
            const string printStudentsQuery = "SELECT ID, CONCAT(Firstname, ' ', Lastname) AS 'Name', Email, Year AS 'Birth year' FROM Students";
            var cmd = new MySqlCommand(printStudentsQuery, _con);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    reader.GetName(0) + ": " + reader.GetString(0) + ", " +
                    reader.GetName(1) + ": " + reader.GetString(1) + ", " +
                    reader.GetName(2) + ": " + reader.GetString(2) + ", " +
                    reader.GetName(3) + ": " + reader.GetString(3));
            }

            _con.Close();
        }
        
        // ADD STUDENT FUNCTION //
        public void AddStudent(string firstname, string lastname, string email, int year)
        {
            _con = Connection();
            const string addStudentQuery = "INSERT INTO students(Firstname, Lastname, Email, Year) VALUES(?first, ?last, ?email, ?year)";
            var cmd = new MySqlCommand(addStudentQuery, _con);
            
            // Gets variables from Menu, binds them together into the query
            cmd.Parameters.AddWithValue("?first", firstname);
            cmd.Parameters.AddWithValue("?last", lastname);
            cmd.Parameters.AddWithValue("?email", email);
            cmd.Parameters.AddWithValue("?year", year);
            MySqlDataReader adder = cmd.ExecuteReader();
            
            if(adder.RecordsAffected > 0) 
            {
                Console.WriteLine("|//////////////|");
                Console.WriteLine("|Added Student!|");
                Console.WriteLine("|//////////////|");
            }
            else
            {
                Console.WriteLine("Failed adding student!");
            }

            _con.Close();
        }
        
        // EDIT STUDENT FUNCTION //
        public void EditStudent(int id, string firstname, string lastname, string email, int year)
        {
            _con = Connection();
            
            // Query needs student id to specify the student to modify
            const string editStudentQuery = "UPDATE Students SET Firstname = ?firstname, Lastname = ?lastname, Email = ?email, Year = ?year WHERE ID = ?ID";
            var cmd = new MySqlCommand(editStudentQuery, _con);
            cmd.Parameters.AddWithValue("?firstname", firstname);
            cmd.Parameters.AddWithValue("?lastname", lastname);
            cmd.Parameters.AddWithValue("?email", email);
            cmd.Parameters.AddWithValue("?year", year);
            cmd.Parameters.AddWithValue("?ID", id);
            
            
            MySqlDataReader finder = cmd.ExecuteReader();
            if (finder.RecordsAffected > 0)
            {
                Console.WriteLine("|////////////////|");
                Console.WriteLine("|Updated Student!|");
                Console.WriteLine("|////////////////|");
            }
            else
            {
                Console.WriteLine("Failed to edit student!");
            }
            _con.Close();
        }
        
        // REMOVE STUDENT FUNCTION //
        public void DeleteStudent(int id)
        {
            _con = Connection();
            const string deleteStudentQuery = "DELETE FROM Students WHERE ID = ?ID";
            var cmd = new MySqlCommand(deleteStudentQuery, _con);
            cmd.Parameters.AddWithValue("?ID", id);
            MySqlDataReader remover = cmd.ExecuteReader();

            if (remover.RecordsAffected > 0)
            {
                Console.WriteLine("|////////////////|");
                Console.WriteLine("|Removed Student!|");
                Console.WriteLine("|////////////////|");
            }
            else
            {
                Console.WriteLine("Failed to remove student!");
            }

            _con.Close();
        }
        
        // SEARCH STUDENT FUNCTION // 
        public void SearchStudent()
        {
            _con = Connection();
            Console.WriteLine("Type in the student's name to search.");
            // Combines wildcard with entered name
            string search =  "%" + Console.ReadLine() + "%";
            
            const string searchStudentQuery = 
                "SELECT Students.ID, CONCAT(Students.Firstname, ' ', Students.Lastname) AS 'Name', Courses.Course_Name, Grades.Grade " + 
                    "FROM Students, Courses, Grades " + 
                        "WHERE CONCAT(Students.Firstname, ' ', Students.Lastname) LIKE ?search " +
                            "AND Students.ID = Grades.Student_ID " +
                                "AND Courses.ID = Grades.Course_ID";

            var cmd = new MySqlCommand(searchStudentQuery, _con);
            cmd.Parameters.AddWithValue("?search", search);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader.GetName(0) + ": " + reader.GetString(0) + ", " +
                                  reader.GetName(1) + ": " + reader.GetString(1) + ", " +
                                  reader.GetName(2) + ": " + reader.GetString(2) + ", " +
                                  reader.GetName(3) + ": " + reader.GetString(3));
            }
            
            _con.Close();
        }
        
        // COURSE FUNCTIONS //
        // SELECT COURSE - GET ID //
        public int SelectCourse()
        {
            _con = Connection();
            const string findCourseQuery = "SELECT * FROM courses";
            var cmd = new MySqlCommand(findCourseQuery, _con);
            MySqlDataReader finder = cmd.ExecuteReader();
            Console.WriteLine(" ");
            Console.WriteLine("0. Go back.");
            while (finder.Read())
            {
                Console.WriteLine(
                    finder.GetString(0) + ". " +
                    finder.GetString(1));
            }
            
            Console.WriteLine(" ");
            Console.WriteLine("Enter the ID of the course you wish to modify.");
            int id = Convert.ToInt32(Console.ReadLine());
            _con.Close();
            return id;
        }
        
        // ADD COURSE FUNCTION //
        public void AddCourse(string courseName)
        {
            _con = Connection();
            const string addCourseQuery = "INSERT INTO Courses (Course_name) VALUES (?course_name)";
            var cmd = new MySqlCommand(addCourseQuery, _con);
            cmd.Parameters.AddWithValue("?course_name", courseName);
            MySqlDataReader adder = cmd.ExecuteReader();
            if (adder.RecordsAffected > 0)
            {
                Console.WriteLine("|/////////////|");
                Console.WriteLine("|Added Course!|");
                Console.WriteLine("|/////////////|");
            } 
            else
            {
                Console.WriteLine("Failed adding course!");
            }
            _con.Close();
        }
        // REMOVE COURSE FUNCTION //
        public void RemoveCourse(int id)
        {
            _con = Connection();
            const string deleteCourseQuery = "DELETE FROM Courses WHERE ID = ?ID";
            var cmd = new MySqlCommand(deleteCourseQuery, _con);
            cmd.Parameters.AddWithValue("?ID", id);
            MySqlDataReader remover = cmd.ExecuteReader();
            if (remover.RecordsAffected > 0)
            {
                Console.WriteLine("|///////////////|");
                Console.WriteLine("|Removed Course!|");
                Console.WriteLine("|///////////////|");
            }
            else
            {
                Console.WriteLine("Failed to remove course!");
            }
            _con.Close();
        }

        // PRINT GRADES FUNCTION
        private int ShowGrades(int studentId)
        {
            // Functions prints out selection for grades the selected student you can delete
            const string showGradesQuery = "SELECT CONCAT(Students.Firstname, ' ', Students.Lastname) AS Name, " +
                                                "Courses.ID, Courses.Course_name AS 'Course name', Grades.Grade " +
                                                    "FROM Students, Courses, Grades " +
                                                        "WHERE Students.ID = Grades.Student_ID " +
                                                            "AND Courses.ID = Grades.Course_ID " +
                                                                "AND Grades.Student_ID = ?student ";
            var cmd = new MySqlCommand(showGradesQuery, _con);
            var name = new MySqlCommand(showGradesQuery, _con);
            cmd.Parameters.AddWithValue("?student", studentId);
            name.Parameters.AddWithValue("?student", studentId);

            // This is only for getting the full name of the person
            MySqlDataReader getName = name.ExecuteReader();
            if (!getName.Read())
            {
                Console.WriteLine("No grades found!");
                getName.Close();
                _con.Close();
                return 0;
            }

            Console.WriteLine("Displaying grades for " + getName.GetString(0));
            getName.Close();
            // Then this is for getting all the grades + courses and ID
            MySqlDataReader reader = cmd.ExecuteReader();
            Console.WriteLine("0. Go back");
            while (reader.Read())
            {
                Console.WriteLine(
                    reader.GetString(1) + ". "
                                        +   reader.GetName(2) + ": " + reader.GetString(2) + ", "
                                        +   reader.GetName(3) + ": " + reader.GetString(3));
            }
            Console.WriteLine("Which grade do you wish to delete?");
            int id = Convert.ToInt32(Console.ReadLine());
            reader.Close();
            return id;
        }
        
        // ASSIGN GRADES //
        public void AssignGrade(int studentId, int courseId, string grade)
        {
            _con = Connection();
            const string addGradeQuery = "INSERT INTO Grades (Grade, Course_ID, Student_ID) VALUES (?grade, ?course, ?student)";
            var cmd = new MySqlCommand(addGradeQuery, _con);

            cmd.Parameters.AddWithValue("?grade", grade);
            cmd.Parameters.AddWithValue("?course", courseId);
            cmd.Parameters.AddWithValue("?student", studentId);
            
            MySqlDataReader adder = cmd.ExecuteReader();
            
            if (adder.RecordsAffected > 0)
            {
                Console.WriteLine("|////////////////|");
                Console.WriteLine("|Grades assigned!|");
                Console.WriteLine("|////////////////|");
            } 
            else
            {
                Console.WriteLine("Failed assigning grades!");
            }

            _con.Close();
        }   
        
        // UNASSIGN GRADES //
        public void UnassignGrade(int studentId)
        {
            _con = Connection();
            // Gets course id based on the choice done
            int courseId = ShowGrades(studentId);

            if (courseId != 0)
            {
                //The actual deletion query
                const string removeGradeQuery = "DELETE FROM Grades WHERE Student_ID = ?student AND Course_ID = ?course";
                var cmd = new MySqlCommand(removeGradeQuery, _con);
                cmd.Parameters.AddWithValue("?student", studentId);
                cmd.Parameters.AddWithValue("?course", courseId);
                MySqlDataReader remover = cmd.ExecuteReader();
                
                if (remover.RecordsAffected > 0)
                {
                    Console.WriteLine("|//////////////|");
                    Console.WriteLine("|Removed Grade!|");
                    Console.WriteLine("|//////////////|");
                }
                else
                {
                    Console.WriteLine("Failed to remove grade!");
                }
            }
            _con.Close();
        }
    }
}