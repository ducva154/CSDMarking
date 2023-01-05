using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAO;
using DTO;

namespace BUS
{
    public class BUSUser
    {
        public void processBtnOpenFile(string path, Dictionary<string, Student> students)
        {
            try
            {
                string[] pathArr = path.Trim().Split('_');
                string id = pathArr[0];
                string name = pathArr[1];
                string className = pathArr[2];

                //Xử lý tên sinh viên
                List<string> nameArr = new List<string>();
                int startIndex = 0;
                for (int i = 1; i < name.Length; i++)
                {
                    if (Char.IsUpper(name[i]))
                    {
                        nameArr.Add(name.Substring(startIndex, i - startIndex));
                        startIndex = i;
                    }
                }
                nameArr.Add(name.Substring(startIndex, name.Length - startIndex));
                string fullName = "";
                for (int i = 0; i < nameArr.Count - 1; i++)
                {
                    fullName = fullName + nameArr[i] + " ";
                }
                fullName += nameArr[nameArr.Count - 1];

                Student st = new Student();
                st.ID = id;
                st.Name = fullName;
                st.Class = className;
                st.FolderName = path;
                students.Add(id, st);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool checkSolutionFolder(string path)
        {
            bool check = true;
            DirectoryInfo d = new DirectoryInfo(@path);
            DirectoryInfo[] directories = d.GetDirectories();
            int i = 1;
            foreach (DirectoryInfo directory in directories)
            {
                if (!directory.Name.Equals("Q" + i))
                {
                    check = false;
                }
                else
                {
                    int j = 1;
                    FileInfo[] files = directory.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        if (!file.Name.Equals("f" + j + ".txt"))
                        {
                            check = false;
                        }
                        j++;
                    }
                }
                i++;
            }
            return check;
        }

        public void addStudent(string id, string name, string className, string folderName, double mark, string note, string mentor)
        {
            DAOUser daoUser = new DAOUser();
            DataTable dt = daoUser.getStudentByID(id);
            if (dt.Rows.Count > 0)
            {
                if (MessageBox.Show("Bài thi đã được chấm. Bạn có muốn chấm lại?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    daoUser.updateStudent(id, mark, note, mentor);
                }
            }
            else
            {
                (new DAOUser()).addStudent(id, name, className, folderName, mark, note, mentor);
            }
        }

        public Mark getPoint(string solutionPath, string answerPath, double point)
        {
            Mark mark = new Mark();
            if (File.Exists(answerPath))
            {
                if (FileEquals(solutionPath, answerPath))
                {
                    mark.Point = point;
                    mark.Note = "OK\n";
                }
                else
                {
                    mark.Note = "FAIL\n";
                }
            }
            else
            {
                mark.Note = "N/A\n";
            }

            return mark;
        }

        public static bool FileEquals(string path1, string path2)
        {
            byte[] file1 = File.ReadAllBytes(path1);
            byte[] file2 = File.ReadAllBytes(path2);
            if (file1.Length == file2.Length)
            {
                for (int i = 0; i < file1.Length; i++)
                {
                    if (file1[i] != file2[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }

        public DataTable login(string username, string password)
        {
            return (new DAOUser()).getMentor(username, password);
        }

    }
}
