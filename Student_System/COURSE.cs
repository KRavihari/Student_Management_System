using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Student_System
{
    class COURSE
    {

        MY_DB mydb = new MY_DB();

        //create aa function to insert course
        public bool insertCourse(string courseName, int hoursNumber,string description)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `course`( `label`, `hours_number`, `description`) VALUES (@name,@hrs,@dscr)", mydb.getConnection);

            //@name,@hrs,@dscr
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", MySqlDbType.Int32).Value = hoursNumber;
            command.Parameters.Add("@dscr", MySqlDbType.Text).Value = description;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        //create a function to check if the course name already exists in the database
        public bool checkCourseName(string courseName)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course` WHERE `label` = @cName",mydb.getConnection);

            command.Parameters.Add("@cName", MySqlDbType.VarChar).Value = courseName;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                //this course name already exsits
                return false;
            }
            else
            {
                return true; 
            }

        }

        //create a function to execute the count queries
        public string execCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, mydb.getConnection);

            mydb.openConnection();
            string count = command.ExecuteScalar().ToString();
            mydb.closeConnection();

            return count;
        }

        //get the total courses
        public string totalCourses()
        {
            return execCount("SELECT COUNT(*) FROM `course`");
        }

        //create a function to get all courses
        public DataTable getAllCourses()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `course`", mydb.getConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //function to remove course by id
        public bool deleteCourse(int courseId)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `course` WHERE `id`=@CID", mydb.getConnection);
            command.Parameters.Add("@CID", MySqlDbType.Int32).Value = courseId;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
