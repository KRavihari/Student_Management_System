using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Student_System
{
    class USERS
    {
        MY_DB mydb = new MY_DB();
        public bool insertUser(string username, int password)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `users`( `username`, `password`) VALUES (@uname,@pw)", mydb.getConnection);

            //@uid,@uname,@pw
            
            command.Parameters.Add("@uname", MySqlDbType.Text).Value = username;
            command.Parameters.Add("@pw", MySqlDbType.Text).Value = password;

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
        public bool checkUser(string username)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `username` = @uName", mydb.getConnection); 

            command.Parameters.Add("@uName", MySqlDbType.VarChar).Value = username; 

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
            //double check the whole code

        }
    }
}
