using System;

namespace University
{
    public class Menu
    {
        public void MainMenu()
        {
            Console.WriteLine("__________________________________\n" +
                              "|       University Manager       |\n" +
                              "|                                |\n" +
                              "|   1. Add student               |\n" +
                              "|   2. Edit student              |\n" +
                              "|   3. Remove student            |\n" +
                              "|                                |\n" +
                              "|   4. Add course                |\n" +
                              "|   5. Remove course             |\n" +
                              "|   6. Assign / Unassign grades  |\n" +
                              "|                                |\n" +
                              "|   7. Read all students         |\n" +
                              "|   8. Search students           |\n" +
                              "|                                |\n" +
                              "|   9. Exit                      |\n" +
                              "|________________________________|\n");
            
            Console.WriteLine("Enter your selection.");
            Sql s = new Sql();
            int id;
            switch (Console.ReadLine())
            {
                // Add Student
                case "1":
                    Console.WriteLine("Enter the student's first name:");
                    string addFirstName = Console.ReadLine();
                    
                    Console.WriteLine("Enter the student's last name:");
                    string addLastName = Console.ReadLine();
                    
                    Console.WriteLine("Enter the student's email:");
                    string addEmail = Console.ReadLine();
                    
                    Console.WriteLine("Enter the student's birth year:");
                    int addBirthYear = Convert.ToInt32(Console.ReadLine());

                    
                    s.AddStudent(addFirstName, addLastName, addEmail, addBirthYear);
                    break;
                // Edit Student
                case "2":
                    id = s.SelectStudent();
                    if (id != 0)
                    {
                        Console.WriteLine("Enter the student's first name:");
                        string editFirstName = Console.ReadLine();
                    
                        Console.WriteLine("Enter the student's last name:");
                        string editLastName = Console.ReadLine();
                    
                        Console.WriteLine("Enter the student's email:");
                        string editEmail = Console.ReadLine();
                    
                        Console.WriteLine("Enter the student's birth year:");
                        int editBirthYear = Convert.ToInt32(Console.ReadLine());
                        s.EditStudent(id, editFirstName, editLastName, editEmail, editBirthYear);
                    }
                    break;
                // Remove student
                case "3":
                    // Gets ID from selection
                    id = s.SelectStudent();
                    // Goes back to main menu
                    if (id == 0)
                    {
                        break;
                    }
                    s.DeleteStudent(id);
                    break;
                // Add Courses
                case "4":
                    Console.WriteLine("Type in the new course name.");
                    string courseName = Console.ReadLine();
                    s.AddCourse(courseName);
                    break;
                // Remove Course
                case "5":
                    // Gets ID from selection
                    id = s.SelectCourse();
                    // Goes back to main menu
                    if (id == 0)
                    {
                        break;
                    }
                    s.RemoveCourse(id);
                    break;
                //Assign grades
                case "6":
                    Console.WriteLine("Do you wish to assign or unassign?");
                    Console.WriteLine("1. Assign");
                    Console.WriteLine("2. Unassign");
                    Console.WriteLine("3. Go back.");
                    int studentId;
                    int courseId;
                    switch (Console.ReadLine())
                    {
                        // Assign grade
                        case "1":
                            // Gets student id
                            studentId = s.SelectStudent();
                    
                            if (studentId == 0)
                            {
                                break;
                            }
                            
                            // Gets course id
                            courseId = s.SelectCourse();
                    
                            if (courseId == 0)
                            {
                                break;
                            }
                            
                            Console.WriteLine("What grade do you wish to assign? (A - F)");
                            string grade = Console.ReadLine();
                            
                            s.AssignGrade(studentId, courseId, grade);
                            break;
                        
                        // Unassign grade
                        case "2":
                            // Gets student id
                            studentId = s.SelectStudent();
                            if (studentId == 0)
                            {
                                break;
                            }
                            
                            s.UnassignGrade(studentId);
                            break;
                        
                        // Go back
                        case "3:":
                            break;
                    }
                    
                    
                    
                    break;
                // Read All Students
                case "7":
                    s.PrintAllStudents();
                    break;
                    
                // Search students
                case "8":
                    s.SearchStudent();
                    break;
                // Exit    
                case "9":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}