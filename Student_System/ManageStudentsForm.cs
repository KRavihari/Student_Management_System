using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using System.Drawing.Imaging;

namespace Student_System
{
    public partial class ManageStudentsForm : Form
    {
        public ManageStudentsForm()
        {
            InitializeComponent();
        }

        STUDENT1 student = new STUDENT1();

        private void ManageStudentsForm_Load(object sender, EventArgs e)
        {
            //populate the datagridview with students data
            fillGrid(new MySqlCommand("SELECT * FROM `student`"));
        }

        //create a function to populate the datagridview
        public void fillGrid(MySqlCommand command)
        {
            
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 50;
            dataGridView1.DataSource = student.getStudents(command);

            //column 7 is the image column index
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AllowUserToAddRows = false;

            //show the total students depending on dgv rows
            labelTotalStudents.Text = "Total Students : " + dataGridView1.Rows.Count;
        }

        //display student data on datagridview click
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtFName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtLName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker2.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female")
            {
                radioButtonFemale.Checked = true;
            }
            else
            {
                radioButtonMale.Checked = true;
            }

            txtPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            pictureBoxStudent.Image = Image.FromStream(picture);
        }

        //clear all fields 
        private void buttonReset_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtLName.Text = "";
            txtFName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            radioButtonMale.Checked = true;
            dateTimePicker2.Value = DateTime.Now;
            pictureBoxStudent.Image = null;

        }

        //search and display students in datagridview 
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM `student` WHERE CONCAT(`first_name`,`last_name`,`address`)LIKE'%" + txtSearch.Text + "%'";
            MySqlCommand command = new MySqlCommand(query);
            fillGrid(command);
        }

        //browse and display image from your computer to the picturebox
        private void btnUploadMng_Click(object sender, EventArgs e)
        {
            //browse image from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif;*.jpeg)|*.jpg;*.png;*.gif;*.jpeg";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image = Image.FromFile(opf.FileName);
            }
        }

        //save the image in your computer
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            //set the file name
            svf.FileName = "Student_" + txtID.Text;

            //check if the picturebox is empty
            if (pictureBoxStudent.Image == null)
            {
                MessageBox.Show("No Image In The Picturebox");
            }
            else if (svf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image.Save(svf.FileName + ("." + ImageFormat.Jpeg.ToString()));
            }
        }

        //add a new student
        private void btnAdd_Click(object sender, EventArgs e)
        {
            STUDENT1 student = new STUDENT1();
            string fname = txtFName.Text;
            string lname = txtLName.Text;
            DateTime bdate = dateTimePicker2.Value;
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
            int born_year = dateTimePicker2.Value.Year;
            int this_year = DateTime.Now.Year;

            if (((this_year - born_year) < 10 || ((this_year - born_year) > 100)))
            {
                MessageBox.Show("The student age must be in between 10 and 100", "Invalid Birth Date ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                pictureBoxStudent.Image.Save(pic, pictureBoxStudent.Image.RawFormat);

                if (student.insertStudent(fname, lname, bdate, phone, gender, address, pic))
                {
                    MessageBox.Show("New student added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillGrid(new MySqlCommand("SELECT * FROM `student`"));
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //edit the selected student
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text);
                string fname = txtFName.Text;
                string lname = txtLName.Text;
                DateTime bdate = dateTimePicker2.Value;
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
                int born_year = dateTimePicker2.Value.Year;
                int this_year = DateTime.Now.Year;

                if (((this_year - born_year) < 10 || ((this_year - born_year) > 100)))
                {
                    MessageBox.Show("The student age must be in between 10 and 100", "Invalid Birth Date ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (verif())
                {
                    pictureBoxStudent.Image.Save(pic, pictureBoxStudent.Image.RawFormat);

                    if (student.updateStudent(id, fname, lname, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Student information updates", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(new MySqlCommand("SELECT * FROM `student`"));
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
                MessageBox.Show("Enter a valid Student Id", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //remove the selected student
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtID.Text);
                //show a confirmation message before deleting the student
                if (MessageBox.Show("Are You Sure You Want To Delete This Student", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {
                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid(new MySqlCommand("SELECT * FROM `student`"));
                        //clear fields
                        txtID.Text = "";
                        txtFName.Text = "";
                        txtLName.Text = "";
                        txtPhone.Text = "";
                        txtAddress.Text = "";
                        dateTimePicker2.Value = DateTime.Now;
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
        }

        //create a function to verify data
        bool verif()
        {
            if ((txtFName.Text.Trim() == "") ||
                (txtLName.Text.Trim() == "") ||
                (txtPhone.Text.Trim() == "") ||
                (txtAddress.Text.Trim() == "") ||
               (pictureBoxStudent.Image == null))
            {
                return false;
            }
            else
            {
                return true;
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
