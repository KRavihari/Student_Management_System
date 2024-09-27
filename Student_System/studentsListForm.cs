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
using System.IO;

namespace Student_System
{
    public partial class studentsListForm : Form
    {
        public studentsListForm()
        {
            InitializeComponent();
        }

        STUDENT1 student = new STUDENT1();

        private void studentsListForm_Load(object sender, EventArgs e)
        {
            //populate the datagridview with students data
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student`");
            dataGridViewStdList.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridViewStdList.RowTemplate.Height = 50;
            dataGridViewStdList.DataSource = student.getStudents(command);
            //column 7 is the image column index
            picCol = (DataGridViewImageColumn)dataGridViewStdList.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridViewStdList.AllowUserToAddRows = false;

        }

        private void dataGridViewStdList_DoubleClick(object sender, EventArgs e)
        {
            //display the selected student in a new form to edit/remove
            UpdateDeleteStudentForm updateDeleteStdF = new UpdateDeleteStudentForm();
            updateDeleteStdF.txtID.Text = dataGridViewStdList.CurrentRow.Cells[0].Value.ToString();
            updateDeleteStdF.txtFName.Text = dataGridViewStdList.CurrentRow.Cells[1].Value.ToString();
            updateDeleteStdF.txtLName.Text = dataGridViewStdList.CurrentRow.Cells[2].Value.ToString();
            updateDeleteStdF.dateTimePicker1.Value = (DateTime)dataGridViewStdList.CurrentRow.Cells[3].Value;

            //gender
            if (dataGridViewStdList.CurrentRow.Cells[4].Value.ToString() == "Female")
            {
                updateDeleteStdF.radioButtonFemale.Checked = true;
  
            }

            updateDeleteStdF.txtPhone.Text = dataGridViewStdList.CurrentRow.Cells[5].Value.ToString();
            updateDeleteStdF.txtAddress.Text = dataGridViewStdList.CurrentRow.Cells[6].Value.ToString();

            //the image
            byte[] pic;
            pic = (byte[])dataGridViewStdList.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            updateDeleteStdF.pictureBoxStudent.Image = Image.FromStream(picture);
            updateDeleteStdF.Show();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //refresh the datagridview data
            MySqlCommand command = new MySqlCommand("SELECT * FROM `student`");
            dataGridViewStdList.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridViewStdList.RowTemplate.Height = 50;
            dataGridViewStdList.DataSource = student.getStudents(command);
            //column 7 is the image column index
            picCol = (DataGridViewImageColumn)dataGridViewStdList.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridViewStdList.AllowUserToAddRows = false;
        }

        private void dataGridViewStdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
