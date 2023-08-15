using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODisconnected.Models
{
    public class Student
    {
        public int Sid { get; set; }
        public string Sname { get; set; }
        public int Rollno { get; set; }
        public int Cid { get; set; }
    }

    public class Courses
    {
        public int Cid { get; set; }
        public string Cname { get; set; }
    }
}
