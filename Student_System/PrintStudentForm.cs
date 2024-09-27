using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Student_System
{
    public partial class PrintStudentForm : Form
    {
        public PrintStudentForm()
        {
            InitializeComponent();
        }

        STUDENT1 student = new STUDENT1();
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PrintStudentForm_Load(object sender, EventArgs e)
        {
            fillGrid(new MySqlCommand("SELECT * FROM `student` "));

            if (radioButtonNO.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
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
        }

        private void radioButtonNO_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButtonYES_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            //display data on the datagridview depending on what the user have selected

            MySqlCommand command;
            string query;

            //check if the radiobutton yes is checked
            //that mean the user want to use a data range
            if (radioButtonYES.Checked)
            {
                //get the date values
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE `birthday`BETWEEN '"+date1+"' AND '"+date2+"' AND gender='Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE `birthday`BETWEEN '" + date1 + "' AND '" + date2 + "' AND gender='Female'";
                }
                else
                {
                    query = "SELECT * FROM `student` WHERE `birthday`BETWEEN '" + date1 + "' AND '" + date2 + "'";
                }

                command = new MySqlCommand(query);
                fillGrid(command);
            }

            else //display data without a birthdate range
            {
                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE  gender='Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM `student` WHERE gender='Female'";
                }
                else
                {
                    query = "SELECT * FROM `student`";
                }

                command = new MySqlCommand(query);
                fillGrid(command);

            }

        }

        //print data from datagridview to text file
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //our file path
            //the file name=students_list.text
            //location=in the desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +@"\students_list.txt";

            using(var writer=new StreamWriter(path))
            {
                //check if the files exists
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                DateTime bdate;

                //rows
                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //columns
                    for(int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        //the birthdate column
                        if (j == 3)
                        {
                            bdate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());
                            writer.Write("\t" + bdate.ToString("yyyy-MM-dd") + "\t" + "|");
                        }
                        //the last column
                        else if(j== dataGridView1.Columns.Count - 2)
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                        }
                        else
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                        }                        
                    }
                    //make a new line
                    writer.WriteLine("");
                    //make a separation
                    writer.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------");
                }


                writer.Close();
                MessageBox.Show("Data Exported");
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
