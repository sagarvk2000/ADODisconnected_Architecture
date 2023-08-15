using ADODisconnected.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ADODisconnected
{
    public partial class Form1 : Form
    {
        StudentCrud crud;
        DataTable table;
        public Form1()
        {
            InitializeComponent();
            crud = new StudentCrud();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable table = crud.GetAllCourses();
            cmbcname.DataSource = table;
            cmbcname.DisplayMember="Cname";
            cmbcname.ValueMember = "Cid";
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                Student stu = crud.GetStudentById(Convert.ToInt32(txtsid.Text));
                if (stu.Sid > 0)
                {
                    List<Courses> list = new List<Courses>();
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        Courses c = new Courses();
                        c.Cid = Convert.ToInt32(table.Rows[i]["Cid"]);
                        c.Cname = table.Rows[i]["Cname"].ToString();
                        list.Add(c);
                    }
                    foreach (Courses item in list)
                    {
                        if (item.Cid == stu.Cid)
                        {
                            cmbcname.Text = item.Cname;
                            break;
                        }
                    }
                    txtname.Text = stu.Sname;
                    txtrollno.Text = stu.Rollno.ToString();

                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)



            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                Student p = new Student();
                p.Sname = txtname.Text;
                p.Rollno = Convert.ToInt32(txtrollno.Text);
                p.Cid = Convert.ToInt32(cmbcname.SelectedValue);
                int res = crud.AddStudents(p);
                if (res > 0)
                {
                    MessageBox.Show("Record inserted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                Student p = new Student();
                p.Sid = Convert.ToInt32(txtsid.Text);
                p.Sname = txtname.Text;
                p.Rollno = Convert.ToInt32(txtrollno.Text);
                p.Cid = Convert.ToInt32(cmbcname.SelectedValue);
                int res = crud.UpdateStudent(p);
                if (res > 0)
                {
                    MessageBox.Show("Record updated..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {

                int res = crud.DeleteStudent(Convert.ToInt32(txtsid.Text));
                if (res > 0)
                {
                    MessageBox.Show("Record deleted..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            DataSet ds = crud.GetStudents();
            dataGridView1.DataSource = ds.Tables["Student"];
        }
    }
}
