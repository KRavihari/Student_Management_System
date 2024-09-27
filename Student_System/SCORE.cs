using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Student_System
{
    class SCORE
    {

        MY_DB mydb = new MY_DB();

        //create a function to insert a new score
        public bool insertScore(int studentId, int courseId, double score, string description)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`studentId`, `courseId`, `score`, `description`) VALUES (@sid,@cid,@scr,@dscr)",mydb.getConnection);

            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;
            command.Parameters.Add("@scr", MySqlDbType.Double).Value = score;
            command.Parameters.Add("@dscr", MySqlDbType.VarChar).Value = description;

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

        //create a function to check if a score is already asigned to this student in this course
        public bool studentScoreExists(int studentId, int courseId)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `score` WHERE `studentId`=@sid AND`courseId`=@cid",mydb.getConnection);

            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {               
                return false;
            }
            else
            {               
                return true;
            }
        }

        //create function to get students score
        public DataTable getStudentsScore()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = mydb.getConnection; 
               command.CommandText=("SELECT score.studentId,student.first_name,student.last_name,score.courseId,course.label,score.score FROM student INNER JOIN score ON student.id = score.studentId INNER JOIN course ON score.courseId = course.id");

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;

        }

        //function to remove course by student and course id
        public bool deleteCourse(int studentId, int courseId)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `studentId`=@sid AND `courseId`=@cid", mydb.getConnection);

            command.Parameters.Add("@sid", MySqlDbType.Int32).Value = studentId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;

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

        //create a function to get the average score by course 
        public DataTable AvgScoreByScore()
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = mydb.getConnection;
            command.CommandText = ("SELECT course.label, avg(score.score) as 'Average Score' FROM course, score WHERE course.id=score.courseId GROUP BY course.label");

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
    }
}
