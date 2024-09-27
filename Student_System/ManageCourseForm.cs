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
    public partial class ManageCourseForm : Form
    {
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        COURSE course = new COURSE();
        int pos;

        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            reloadListBoxData();
        }

        //create a function to load the listbox with courses
        public void reloadListBoxData()
        {
            listBoxCourses.DataSource = course.getAllCourses();
            listBoxCourses.ValueMember = "id";
            listBoxCourses.DisplayMember = "label";

            //unselect the item from listbox
            listBoxCourses.SelectedItem = null;

            //display the total courses
            labelTotCourses.Text = "Total Courses: " + course.totalCourses();
        }

        //create a function to display course data depending on index
        void showData(int index)
        {
            DataRow dr = course.getAllCourses().Rows[index];
            listBoxCourses.SelectedIndex = index;
            textBoxID.Text = dr.ItemArray[0].ToString();
            textBoxLabel.Text = dr.ItemArray[1].ToString();
            numericUpDownHours.Value = int.Parse(dr.ItemArray[2].ToString());
            textBoxDescription.Text = dr.ItemArray[3].ToString();
        }


        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            //display the selected course data
            pos = listBoxCourses.SelectedIndex;
            showData(pos);
        }

        //button first
        private void buttonFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            showData(0);
        }

        //button next
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (pos < (course.getAllCourses().Rows.Count - 1))
            {
                pos = pos + 1;
                showData(pos);
            }
            
        }

        //button previous
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                pos = pos - 1;
                showData(pos);
            }
            
        }


        //button last
        private void buttonLast_Click(object sender, EventArgs e)
        {
            pos = course.getAllCourses().Rows.Count - 1;
            showData(pos);
        }

        //button add course
        private void buttonAddCourse_Click(object sender, EventArgs e)
        {
            string courseLabel = textBoxLabel.Text;
            int hours = (int)numericUpDownHours.Value;
            string description = textBoxDescription.Text;

            COURSE course = new COURSE();

            if (courseLabel.Trim() == "")
            {
                MessageBox.Show("Add a Course Name", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (course.checkCourseName(courseLabel))
            {
                if (course.insertCourse(courseLabel, hours, description))
                {
                    MessageBox.Show("New Course Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadListBoxData();
                }
                else
                {
                    MessageBox.Show("Course Not Inserted", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("This Course Name Already Uploaded", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //button edit course
       /* private void buttonEditCourses_Click(object sender, EventArgs e)
        {
            try
            {
                //update the selected course
                string name = textBoxLabel.Text;
                int hrs = (int)numericUpDownHours.Value;
                string descr = textBoxDescription.Text;
                int id = int.Parse(textBoxID.Text);

                if (name.Trim() != "")
                {
                    //check if this course name already exists and its not the current course using the id
                    if (!course.checkCourseName(name, id))
                    {
                        MessageBox.Show("This course name already exists", "Edit course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if(course.update)
                }
            }
        }*/

        //button remove course
        private void buttonRemoveCourses_Click(object sender, EventArgs e)
        {
            try
            {
                int courseId = Convert.ToInt32(textBoxID.Text);

                COURSE course = new COURSE();

                if (MessageBox.Show("Are you sure you want to remove this course", "Delete course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.deleteCourse(courseId))
                    {
                        MessageBox.Show("Course Deleted", "Remove Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reloadListBoxData();

                        //clear fields
                        textBoxID.Text = "";
                        numericUpDownHours.Value = 10;
                        textBoxLabel.Text = "";
                        textBoxDescription.Text = "";

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

            pos = 0;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new MainForm();

            this.Close();
        }



        /*
         * you can see this but i will use the click event instead
        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //display the selected course data
                pos = listBoxCourses.SelectedIndex;
                showData(pos);
            }
            catch
            {

            }

            
        }*/
    }
}
