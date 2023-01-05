using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class Mark
    {
        private double _point;

        public double Point
        {
            get { return _point; }
            set { _point = value; }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        public Mark() { }

        public Mark(double point, string note)
        {
            Point = point;
            Note = note;
        }
    }
}
