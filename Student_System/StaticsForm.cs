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
    public partial class StaticsForm : Form
    {
        public StaticsForm()
        {
            InitializeComponent();
        }

        //color variables
        Color panTotalColor;
        Color panMaleColor;
        Color panFemaleColor;


        private void StaticsForm_Load(object sender, EventArgs e)
        {
            //get the panels color
            panTotalColor = panelTotal.BackColor;
            panMaleColor = panelMale.BackColor;
            panFemaleColor = panelFemale.BackColor;

            //display the values
            STUDENT1 student = new STUDENT1();
            double totalStudents = Convert.ToDouble(student.totalStudent());
            double totalMaleStudent = Convert.ToDouble(student.totalMaleStudent());
            double totalFemaleStudent = Convert.ToDouble(student.totalFemaleStudent());

            //count the %
            double malePercentage = totalMaleStudent * 100 / totalStudents;
            double femalePercentage = totalFemaleStudent * 100 / totalStudents;

            labelTotal.Text = "Total Students: " + totalStudents.ToString();
            labelMale.Text = "Male: " + malePercentage.ToString("0.00")+ "%";
            labelFemale.Text = "Female: " + femalePercentage.ToString("0.00" ) + "%";
        }

        private void labelTotal_Click(object sender, EventArgs e)
        {

        }

        private void StaticsForm_MouseEnter(object sender, EventArgs e)
        {

        }

        private void panelTotal_MouseEnter(object sender, EventArgs e)
        {

        }

        private void labelTotal_MouseEnter(object sender, EventArgs e)
        {
            panelTotal.BackColor = Color.PaleGreen;
            labelTotal.ForeColor = panTotalColor;
        }

        private void labelTotal_MouseLeave(object sender, EventArgs e)
        {
            panelTotal.BackColor = panTotalColor;
            labelTotal.ForeColor = Color.PaleGreen;
        }


        private void panelTotal_MouseLeave(object sender, EventArgs e)
        {

        }

        private void panelMale_MouseEnter(object sender, EventArgs e)
        {

        }

        private void labelMale_MouseEnter(object sender, EventArgs e)
        {
            panelMale.BackColor = Color.LightSkyBlue;
            labelMale.ForeColor = panMaleColor;
        }

        private void labelMale_MouseLeave(object sender, EventArgs e)
        {
            panelMale.BackColor = panMaleColor;
            labelMale.ForeColor = Color.LightSkyBlue;
        }

        private void panelFemale_MouseEnter(object sender, EventArgs e)
        {

        }

        private void labelFemale_MouseEnter(object sender, EventArgs e)
        {
            panelFemale.BackColor = Color.LightPink;
            labelFemale.ForeColor = panFemaleColor;
        }

        private void labelFemale_MouseLeave(object sender, EventArgs e)
        {
            panelFemale.BackColor = panFemaleColor;
            labelFemale.ForeColor = Color.LightPink;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new MainForm();

            this.Close();
        }
    }
}
