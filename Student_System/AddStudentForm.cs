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

namespace Student_System
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //browse image from your computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.jpeg;*.gif;)|*.jpg;*.png;*.jpeg;*.gif;";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudent.Image = Image.FromFile(opf.FileName);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //add new student
            STUDENT1 student = new STUDENT1();
            string fname = textBox1.Text;
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

            if (((this_year - born_year) < 10 )|| ((this_year - born_year) > 100))
            {
                MessageBox.Show("The student age must be in between 10 and 100", "Invalid Birth Date ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                pictureBoxStudent.Image.Save(pic, pictureBoxStudent.Image.RawFormat);

                if (student.insertStudent(fname, lname, bdate, phone, gender, address, pic))
                {
                    MessageBox.Show("New student added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //create a function to verify data
        bool verif()
        {
            if ((textBox1.Text.Trim() == "" )||(txtLName.Text.Trim() == "") ||(txtPhone.Text.Trim() == "" )||(txtAddress.Text.Trim() == "") || ( pictureBoxStudent.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new MainForm();

            this.Close();
        }
    }
}
