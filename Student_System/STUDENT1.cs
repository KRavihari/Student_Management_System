using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Student_System
{
    class STUDENT1
    {

        MY_DB db = new MY_DB();
        //private object id;

        //create a function to add a new student to the database
        public bool insertStudent(string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `student`(`first_name`, `last_name`, `birthday`, `gender`, `phone`, `address`, `picture`) VALUES (@fn,@ln,@bdt,@gdr,@phn,@adrs,@pic)", db.getConnection);

            //@fn,@ln,@bdt,@gdr,@phn,@adrs,@pic
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

            db.openConnection();

            if (command.ExecuteNonQuery() ==1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }

        //create a function to return a table of students data
        public DataTable getStudents(MySqlCommand command)
        {

            command.Connection = db.getConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
             
        }

        //create a function to update students information
        /*
        public bool updateStudent(int id, string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("UPDATE 'student' SET 'first_name'=@fn,'last_name'=@ln,'birthday'=@bdt,'gender'=@gdr,'phone'=@phn,'address'=@adrs,'picture'=@pic WHERE 'id'=@ID", db.getConnection);

            //
            //@ID,@fn,@ln,@bdt,@gdr,@phn,@adrs,@pic
            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        } */

            public bool updateStudent(int id, string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `student` SET `first_name`= @fn,`last_name`= @ln,`birthday`= @bdt,`gender`= @gdr,`phone`= @phn,`address`= @adrs,`picture`= @pic WHERE `id`= @ID", db.getConnection);

            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", MySqlDbType.Text).Value = address;
            command.Parameters.Add("@pic", MySqlDbType.Blob).Value = picture.ToArray();

            db.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    db.closeConnection();
                    return true;
                }
            else
            {
                db.closeConnection();
                return false;
            }

            }




        //create a function to delete the selected student
        /*
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE 'id'=@studentID", db.getConnection);

            //@studentID
            command.Parameters.Add("@studentID", MySqlDbType.Int32).Value = id; //error in id was fixed as "use id in STUDENT1"
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        } */

            public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `student` WHERE `id`=@studentID", db.getConnection);

            command.Parameters.Add("@studentID", MySqlDbType.Int32).Value = id;
            

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }



        //create a function to execute the count queries
        public string execCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, db.getConnection);

            db.openConnection();
            string count = command.ExecuteScalar().ToString();
            db.closeConnection();

            return count;
        }

        //get the total students
        public string totalStudent()
        {
            return execCount("SELECT COUNT(*) FROM `student`");
        }

        //get the male students
        public string totalMaleStudent()
        {
            return execCount("SELECT COUNT(*) FROM `student` WHERE `gender`='Male'");
        }

        //get the female students
        public string totalFemaleStudent()
        {
            return execCount("SELECT COUNT(*) FROM `student` WHERE `gender`='Female'");
        }
    }
}
