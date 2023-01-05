using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DAO
{
    public class DAOUser
    {
        public void addStudent(string id, string name, string className, string folderName, double mark, string note, string mentor)
        {
            string sql = "INSERT INTO Student VALUES ('" + id + "','" + name + "','" + className + "','" + folderName + "','" + mark + "','" + note + "','" + mentor + "')";
            (new DataProvider()).executeNonQuery(sql);
        }

        public void updateStudent(string id, double mark, string note, string mentor)
        {
            string sql = "UPDATE Student SET Mark = '" + mark + "', Note = '" + note + "', Mentor = '" + mentor + "' WHERE ID = '" + id + "'";
            (new DataProvider()).executeNonQuery(sql);
        }

        public DataTable getStudentByID(string id)
        {
            string sql = "SELECT * FROM Student WHERE ID = '" + id + "'";
            return (new DataProvider()).executeQuery(sql);
        }

        public DataTable getMentor(string user, string pass)
        {
            string sql = "SELECT * FROM Mentor WHERE UserName = '" + user + "' AND Password = '" + pass + "'";
            return (new DataProvider()).executeQuery(sql);
        }
    }
}
