using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_System
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudentForm addStdF = new AddStudentForm();
            addStdF.Show(this);
            
        }

        private void studentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            studentsListForm stdListF = new studentsListForm();
            stdListF.Show(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void editRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm upDeStdF = new UpdateDeleteStudentForm();
            upDeStdF.Show(this);
        }

        private void staticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticsForm stcF = new StaticsForm();
            stcF.Show(this);
        }

        private void manageStudentsFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageStudentsForm mngStdF = new ManageStudentsForm();
            mngStdF.Show(this);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStudentForm prStdF = new PrintStudentForm();
            prStdF.Show();
        }

        private void addCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCourseForm addCrsF = new AddCourseForm();
            addCrsF.Show(this);
        }

        private void removeCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCourseForm rmvCrsF = new RemoveCourseForm();
            rmvCrsF.Show(this);
        }

        private void manageCoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCourseForm mngCrsF = new ManageCourseForm();
            mngCrsF.Show(this);
        }

        private void manageScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageScoresForm mngScrF = new ManageScoresForm();
            mngScrF.Show(this);
        }

        private void avgScoreByCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AvgScoreByCourseForm avgScrByCrsF = new AvgScoreByCourseForm();
            avgScrByCrsF.Show(this);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
