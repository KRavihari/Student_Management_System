using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_System
{
    public partial class UpdateDeleteStudentForm : Form
    {
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }
        STUDENT1 student = new STUDENT1();

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

         }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //browse image from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif;*.jpeg)|*.jpg;*.png;*.gif;*.jpeg";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image = Image.FromFile(opf.FileName);
            }
        }


        //create a function to verify data
        bool verif()
        {
            if ((txtFName.Text.Trim() == "") ||(txtLName.Text.Trim() == "") ||(txtPhone.Text.Trim() == "") ||(txtAddress.Text.Trim() == "") ||(pictureBoxStudent.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
        }
            /*
            //update the selected student
            try
            {
                //add new student

                int id = Convert.ToInt32(txtID.Text);
                string fname = txtFName.Text;
                string lname = txtLName.Text;
                DateTime bdate = dateTimePicker1.Value;
                String phone = txtPhone.Text;
                string address = txtAddress.Text;
                string gender = "Male";

                if (radioButtonFemale.Checked)
                {
                    gender = "Female";
                }

                MemoryStream pic = new MemoryStream();

                //we need to check the age of the student 
                // the student age must be between 10 - 100 
                int born_year = dateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;

                if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
                {
                    MessageBox.Show("The Student Age Must Be Between 10 and 100 Year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verif())
                {
                    pictureBoxStudent.Image.Save(pic, pictureBoxStudent.Image.RawFormat);
                    if (student.updateStudent(id, fname, lname, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Student Information Updated", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Empty Fields", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid student ID", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }*/

        

        private void btnRemove_Click(object sender, EventArgs e)
        {

            //remove the selected student
            
           try
            {
                int id = Convert.ToInt32(txtID.Text);
                //show a confirmation message before deleting the student
                if (MessageBox.Show("Are You Sure You Want To Delete This Student", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {
                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //clear fields
                        txtID.Text = "";
                        txtFName.Text = "";
                        txtLName.Text = "";
                        txtPhone.Text = "";
                        txtAddress.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBoxStudent.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Student Not Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch 
            {
                MessageBox.Show("Enter a valid Student Id", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*
             try
            {
                int courseId = Convert.ToInt32(txtID.Text);

                COURSE course = new COURSE();

                if (MessageBox.Show("Are you sure you want to remove this course", "Delete course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.updateStudent(id))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        

                        //clear fields
                        txtID.Text = "";
                        txtFName.Text = "";
                        txtLName.Text = "";
                        txtPhone.Text = "";
                        txtAddress.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBoxStudent.Image = null;
                        

                    }
                    else
                    {
                        MessageBox.Show("Course not deleted", "Remove course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Enter a valid numeric ID", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */


        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //search student by id
            try
            {
                int id = Convert.ToInt32(txtID.Text);
                MySqlCommand command = new MySqlCommand("SELECT `id`, `first_name`, `last_name`, `birthday`, `gender`, `phone`, `address`, `picture` FROM `student` WHERE `id`=" + id);

                DataTable table = student.getStudents(command);

                if (table.Rows.Count > 0)
                {
                    txtFName.Text = table.Rows[0]["first_name"].ToString();
                    txtLName.Text = table.Rows[0]["last_name"].ToString();
                    txtPhone.Text = table.Rows[0]["phone"].ToString();
                    txtAddress.Text = table.Rows[0]["address"].ToString();

                    dateTimePicker1.Value = (DateTime)table.Rows[0]["birthday"];

                    //gender
                    if (table.Rows[0]["gender"].ToString() == "Female")
                    {
                        radioButtonFemale.Checked = true;
                    }
                    else
                    {
                        radioButtonMale.Checked = true;
                    }

                    //image
                    byte[] pic = (byte[])table.Rows[0]["picture"];
                    MemoryStream picture = new MemoryStream(pic);
                    pictureBoxStudent.Image = Image.FromStream(picture);
                }

            }
            catch
            {
                MessageBox.Show("Enter a valid Student Id", "Invalid Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //allow only numbers on key press
        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && ! char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //add new student

                int id = Convert.ToInt32(txtID.Text);
                string fname = txtFName.Text;
                string lname = txtLName.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = txtPhone.Text;
                string address = txtAddress.Text;
                string gender = "Male";

                if (radioButtonFemale.Checked)
                {
                    gender = "Female";
                }

                MemoryStream pic = new MemoryStream();

                //we need to check the age of the student
                //the student age must be between 10-100
                int born_year = dateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;

                if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
                {
                    MessageBox.Show("the Student age must be between 10 and 100", "invalid birth date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verif()) {
                    pictureBoxStudent.Image.Save(pic, pictureBoxStudent.Image.RawFormat);
                    if (student.updateStudent(id, fname, lname, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Student information updated", "edit student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("error", "edit student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } }
                else {
                    MessageBox.Show("Empty fields", "edit student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                }
            catch
            {
                MessageBox.Show("enter valid id", "edit student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //MainForm loginForm = new MainForm();
            //loginForm.Show();

            this.Hide();
            var form1 = new MainForm();
            //form1.FormClosed += (s, args) => this.Close();
            //form1.Show();
            this.Close();
        }
    }
    
}
