using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BUS;
using DTO;

namespace Presentation
{
    public partial class FrmCSDMarking : Form
    {
        public FrmCSDMarking()
        {
            InitializeComponent();
        }
        public FrmCSDMarking(string mentor)
        {
            InitializeComponent();
            txtAcount.Text = mentor;
        }

        Dictionary<string, Student> students = new Dictionary<string, Student>();
        private void btnAnswer_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
                if (directchoosedlg.ShowDialog() == DialogResult.OK)
                {
                    students.Clear();
                    txtAnswerPath.Text = directchoosedlg.SelectedPath;
                    DirectoryInfo d = new DirectoryInfo(@directchoosedlg.SelectedPath);
                    DirectoryInfo[] Directories = d.GetDirectories();
                    foreach (DirectoryInfo directory in Directories)
                    {
                        (new BUSUser()).processBtnOpenFile(directory.Name, students);
                    }

                    cbxAnswer.DataSource = new BindingSource(students, null);
                    cbxAnswer.DisplayMember = "Key";
                    cbxAnswer.ValueMember = "Value";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Vui lòng chọn thư mục chứa bài làm của sinh viên!", "Thông báo");
            }
        }

        private void btnSolution_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog solution = new FolderBrowserDialog();
            if (solution.ShowDialog() == DialogResult.OK)
            {
                txtSolutionPath.Text = solution.SelectedPath;
                if (!(new BUSUser()).checkSolutionFolder(solution.SelectedPath))
                {
                    MessageBox.Show("Vui lòng chọn thư mục chứa đáp án bài thi!", "Thông báo");
                }
            }
        }

        private void cbxAnswer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Student st = ((KeyValuePair<string, Student>)cbxAnswer.SelectedItem).Value;
            txtID.Text = st.ID;
            txtName.Text = st.Name;
            txtClass.Text = st.Class;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (txtAnswerPath.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn thư mục chứa bài làm của sinh viên!", "Thông báo");
            }
            else if (txtSolutionPath.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng chọn thư mục chứa đáp án bài thi!", "Thông báo");
            }
            else if (!(new BUSUser()).checkSolutionFolder(txtSolutionPath.Text))
            {
                MessageBox.Show("Vui lòng chọn thư mục chứa đáp án bài thi!", "Thông báo");
            }
            else if (cbxAnswer.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn thư mục chứa bài làm của sinh viên!", "Thông báo");
            }
            else
            {
                //Get all solution path
                string[] solutionFolder = { txtSolutionPath.Text + "\\Q1", txtSolutionPath.Text + "\\Q2", txtSolutionPath.Text + "\\Q3" };
                string[] solutionQ1 = { solutionFolder[0] + "\\f1.txt", solutionFolder[0] + "\\f2.txt", solutionFolder[0] + "\\f3.txt", solutionFolder[0] + "\\f4.txt" };
                string[] solutionQ2 = { solutionFolder[1] + "\\f1.txt", solutionFolder[1] + "\\f2.txt", solutionFolder[1] + "\\f3.txt", solutionFolder[1] + "\\f4.txt" };
                string[] solutionQ3 = { solutionFolder[2] + "\\f1.txt", solutionFolder[2] + "\\f2.txt", solutionFolder[2] + "\\f3.txt", solutionFolder[2] + "\\f4.txt" };
                //Get all answer path
                Student st = ((KeyValuePair<string, Student>)cbxAnswer.SelectedItem).Value;     
                string[] answerFolder = { txtAnswerPath.Text + "\\" + st.FolderName + "\\Q1", txtAnswerPath.Text + "\\" + st.FolderName + "\\Q2", txtAnswerPath.Text + "\\" + st.FolderName + "\\Q3" };
                string[] answerQ1 = { answerFolder[0] + "\\f1.txt", answerFolder[0] + "\\f2.txt", answerFolder[0] + "\\f3.txt", answerFolder[0] + "\\f4.txt" };
                string[] answerQ2 = { answerFolder[1] + "\\f1.txt", answerFolder[1] + "\\f2.txt", answerFolder[1] + "\\f3.txt", answerFolder[1] + "\\f4.txt" };
                string[] answerQ3 = { answerFolder[2] + "\\f1.txt", answerFolder[2] + "\\f2.txt", answerFolder[2] + "\\f3.txt", answerFolder[2] + "\\f4.txt" };

                BUSUser busUser = new BUSUser();
                Mark mark = new Mark();
                double totalMark = 0;
                string note = "";
                //Get mark
                note += "Q1:\n";
                for (int i = 0; i < solutionQ1.Length; i++)
                {
                    note += "\t" + (i+1) + ": ";
                    mark = busUser.getPoint(solutionQ1[i], answerQ1[i], 1);
                    totalMark += mark.Point;
                    note += mark.Note;
                }
                note += "Q2:\n";
                for (int i = 0; i < solutionQ2.Length; i++)
                {
                    note += "\t" + (i + 1) + ": ";
                    mark = busUser.getPoint(solutionQ2[i], answerQ2[i], 1);
                    totalMark += mark.Point;
                    note += mark.Note;
                }
                note += "Q3:\n";
                for (int i = 0; i < solutionQ3.Length; i++)
                {
                    note += "\t" + (i + 1) + ": ";
                    mark = busUser.getPoint(solutionQ3[i], answerQ3[i], 0.5);
                    totalMark += mark.Point;
                    note += mark.Note;
                }

                (new BUSUser()).addStudent(st.ID, st.Name, st.Class, st.FolderName, totalMark, note, txtAcount.Text);
                lblMark.Text = totalMark + "";
                lblNote.Text = note;
            }
        }

        
    }
}
