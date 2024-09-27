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
    public partial class ManageScoresForm : Form
    {
        public ManageScoresForm()
        {
            InitializeComponent();
        }

        SCORE score = new SCORE();
        STUDENT1 student = new STUDENT1();
        COURSE course = new COURSE();
        string data = "score";

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ManageScoresForm_Load(object sender, EventArgs e)
        {
            //populate the combibox with courses
            comboBox1.DataSource = course.getAllCourses();
            comboBox1.DisplayMember = "label";
            comboBox1.ValueMember = "id";

            //populate the datagridview with students score
            dataGridView1.DataSource = score.getStudentsScore();

        }

        //display students data on datagrid
        private void buttonShowStds_Click(object sender, EventArgs e)
        {
            data = "student";
            MySqlCommand command = new MySqlCommand("SELECT `id`, `first_name`, `last_name`, `birthday`FROM `student`");
            dataGridView1.DataSource = student.getStudents(command);
        }

        //display scores data on datagrid
        private void buttonShowScores_Click(object sender, EventArgs e)
        {
            data = "score";
            dataGridView1.DataSource = score.getStudentsScore();
        }

        //get data from datagridview
        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        //create a function to get data from datagridview
        public void getDataFromDatagridview()
        {
            //if the user select to show student data then we will show only the student id
            if (data == "student")
            {
                textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                
            }

            //if the user select to show student data then we will show the student id 
            //+select the course from the combobox
            else if (data == "score")
            {        
                textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                comboBox1.SelectedValue = dataGridView1.CurrentRow.Cells[3].Value;
            }
            
        }

        //button add new score
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int studentId = Convert.ToInt32(textBoxID.Text);
                int courseId = Convert.ToInt32(comboBox1.SelectedValue);
                double scoreValue = Convert.ToDouble(textBoxScore.Text);
                string description = textBoxDescription.Text;

                //check if a score is already asigned to this course
                if (!score.studentScoreExists(studentId, courseId))
                {
                    if(score.insertScore(studentId, courseId, scoreValue, description))
                    {
                        MessageBox.Show("Student Score Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Student Score Not Inserted", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("The Score For This Course Are Already Set", "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //button remove
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int studentId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            int courseId = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());

            if (MessageBox.Show("Do you want to delete this score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (score.deleteCourse(studentId, courseId))
                {
                    MessageBox.Show("Score Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = score.getStudentsScore();
                }
                else
                {
                    MessageBox.Show("Score Not Deleted", "Remove Score", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        //show a new form with the average score by course
        private void buttonAvgScore_Click(object sender, EventArgs e)
        {
            AvgScoreByCourseForm avgScrByCrsF = new AvgScoreByCourseForm();
            avgScrByCrsF.Show(this);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new MainForm();

            this.Close();
        }
    }
}
