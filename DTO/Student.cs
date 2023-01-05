using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Student
    {
        private string _id;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _class;

        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        private string _folderName;

        public string FolderName
        {
            get { return _folderName; }
            set { _folderName = value; }
        }

        private double _mark;

        public double Mark
        {
            get { return _mark; }
            set { _mark = value; }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        private string _mentor;

        public string Mentor
        {
            get { return _mentor; }
            set { _mentor = value; }
        }

        public Student() { }

    }
}
