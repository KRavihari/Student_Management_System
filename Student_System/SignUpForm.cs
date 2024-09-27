using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_System
{
    public partial class SignUpForm : Form
    {
        public SignUpForm()
        {
            InitializeComponent();
        }

        USERS user = new USERS();

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
          
            /*
            MY_DB db = new MY_DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `username`=@usn AND `password`= @pass", db.getConnection);

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = txtUsername.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = txtPassword.Text;

            adapter.SelectCommand = command;

            adapter.Fill(table);*/

            
            string username = txtUsername.Text;
            int password = Convert.ToInt32(txtPassword.Text);

         

            if (username.Trim() == "")
            {
                MessageBox.Show("Add a Username", "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (user.checkUser(username))
            {
                if (user.insertUser(username,password))
                {
                    MessageBox.Show("Successfull!", "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Try again", "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("This username Already Uploaded", "Sign Up", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new MainForm();

            this.Close();
        }
    }
}
