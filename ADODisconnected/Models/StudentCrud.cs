using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ADODisconnected.Models
{
    public class StudentCrud
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public StudentCrud()
        {
            string connstr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(connstr);
        }

        public DataSet GetStudents()//for show btn
        {
            string qry = "select * from Student";
            da = new SqlDataAdapter(qry, con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds,"Student");
            return ds;
        }

        public DataTable GetAllCourses()
        {
            string qry = "select * from Courses";
            da = new SqlDataAdapter(qry, con);
            ds = new DataSet();
            da.Fill(ds, "Courses");
            return ds.Tables["Courses"];
        }
        
        public int AddStudents(Student stu)
        {
            ds = GetStudents();
            DataRow row = ds.Tables["Student"].NewRow();
            row["Sname"] = stu.Sname;
            row["Rollno"] = stu.Rollno;
            row["Cid"] = stu.Cid;
            ds.Tables["Student"].Rows.Add(row);
            int res = da.Update(ds.Tables["Student"]);
            return res;
        }

        public int UpdateStudent(Student stu)
        {
            ds = GetStudents();
            int res = 0;
            // step1 find the row to modify
            DataRow row = ds.Tables["Student"].Rows.Find(stu.Sid);
            if (row != null)
            {
                // step2 - modify record to the row
                row["Sname"] = stu.Sname;
                row["Rollno"] = stu.Rollno;
                row["Cid"] = stu.Cid;

                // step3 update the changes to DB
                res = da.Update(ds.Tables["Student"]);
            }
            return res;
        }

        public int DeleteStudent(int Sid)
        {
            ds = GetStudents();
            int res = 0;
            // step1 find the row to modify
            DataRow row = ds.Tables["Student"].Rows.Find(Sid);
            if (row != null)
            {
                // row will be deleted from the DataSet
                row.Delete();
                // step3 update the changes to DB
                res = da.Update(ds.Tables["Student"]);
            }
            return res;
        }

        public Student GetStudentById(int Sid)
        {
            ds = GetStudents();
            DataRow row = ds.Tables["Student"].Rows.Find(Sid);
            Student stu = new Student();
            stu.Sid= Convert.ToInt32(row["Sid"]);
            stu.Sname = row["Sname"].ToString();
            stu.Rollno = Convert.ToInt32(row["Rollno"]);
            stu.Cid = Convert.ToInt32(row["Cid"]);
            return stu;
        }
    }
}
