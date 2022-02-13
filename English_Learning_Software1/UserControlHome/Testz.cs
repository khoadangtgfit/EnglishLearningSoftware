using English_Learning_Software1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class Testz : UserControl
    {
        private string testz;
        private string ownerz;
        EnglishDBContext context = new EnglishDBContext();
        public Testz(string test,string owner)
        {
            InitializeComponent();
            this.testz = test;
            this.ownerz = owner;
            frmHome.Instance.PnlContainer1.Enabled = false;
        }
        int index;
        int dung;
        List<Question> listquestion;
        List<Answer> answers;
        private void Testz_Load(object sender, EventArgs e)
        {
            CreateList(testz);
            index = 0;
            dung = 0;
            Question a = listquestion[index];
            label1.Text = a.Question_Content;
            label2.Text = "Câu thứ :" + (index + 1) + "/" + listquestion.Count;
            answers = context.Answers.Where(p => p.Question_Code == a.Question_Code).ToList();
            radioButton1.Text = answers[0].Answer_Content;
            radioButton2.Text = answers[1].Answer_Content;
            radioButton3.Text = answers[2].Answer_Content;
            radioButton4.Text = answers[3].Answer_Content;
            guna2Button1.Enabled = false;
        }
        private bool DungSai(string h)
        {
            foreach(var item in answers)
            {
                if (item.Answer_Content == h && item.Correct == true)
                {
                    return true;
                }
            }
            return false;
        }
        private void RadiobuttonCheckChange(object sender, EventArgs e)
        {
            guna2Button1.Enabled = true;
        }
        private bool Checked_Radio()
        {
            if (radioButton1.Checked == true)
            {
                if (DungSai(radioButton1.Text) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (radioButton2.Checked == true)
            {
                if (DungSai(radioButton2.Text) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (radioButton3.Checked == true)
            {
                if(DungSai(radioButton3.Text) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (radioButton4.Checked == true)
            {
                if(DungSai(radioButton4.Text) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
            
        }
        private void CreateList(string tb)
        {
            try
            {
                if (tb != null)
                {
                    Test test = context.Tests.FirstOrDefault(p => p.Test_Name == tb);
                    if (test != null)
                    {
                        listquestion = context.Questions.Where(p => p.Test_Code == test.Test_Code).ToList();
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        List<int> listanswer=new List<int>();
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listquestion.Count - 1 <= index)
                {
                    if (Checked_Radio() == true)
                    {
                        dung++;
                        listanswer.Add(1);
                    }
                    else
                    {
                        listanswer.Add(0);
                    }
                    int diem = (dung * 100) / listquestion.Count;
                    Test_Detail a = new Test_Detail();
                    Test b = context.Tests.FirstOrDefault(p => p.Test_Name == testz);
                    Account account = context.Accounts.FirstOrDefault(p => p.Account_Name == ownerz);
                    Ownere c = context.Owneres.FirstOrDefault(p => p.Ownere_Code == account.Ownere_Code);
                    if (b != null && c != null)
                    {
                        a.Ownere_Code = c.Ownere_Code;
                        a.Test_Code = b.Test_Code;
                        Test_Detail testdetail = context.Test_Detail.FirstOrDefault(p => p.Ownere_Code == c.Ownere_Code && p.Test_Code == b.Test_Code);
                        if (testdetail != null && diem >= testdetail.Test_Score)
                            a.Test_Score = diem;
                        if (testdetail == null)
                            a.Test_Score = diem;
                        if (testdetail != null && diem < testdetail.Test_Score)
                            a.Test_Score = testdetail.Test_Score;
                        context.Test_Detail.AddOrUpdate(a);
                        context.SaveChanges();
                    }
                    panel1.Controls.Clear();
                    Result uc2 = new Result(b.Test_Code, c.Ownere_Code, listanswer, dung);
                    uc2.Dock = DockStyle.Fill;
                    panel1.Controls.Add(uc2);
                }
                else
                {
                    if (Checked_Radio() == true)
                    {
                        dung++;
                        listanswer.Add(1);
                    }
                    else
                    {
                        listanswer.Add(0);
                    }
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    Question a = listquestion[++index];
                    label1.Text = a.Question_Content;
                    label2.Text = "Câu thứ :" + (index + 1) + "/" + listquestion.Count;
                    guna2Button1.Enabled = false;
                    answers = null;
                    answers = context.Answers.Where(p => p.Question_Code == a.Question_Code).ToList();
                    radioButton1.Text = answers[0].Answer_Content;
                    radioButton2.Text = answers[1].Answer_Content;
                    radioButton3.Text = answers[2].Answer_Content;
                    radioButton4.Text = answers[3].Answer_Content;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn có thực sự muốn thoát?", "Câu hỏi", MessageBoxButtons.YesNo);
            if (a == DialogResult.Yes)
            {
                frmHome.Instance.PnlContainer.Controls.Clear();
                ChooseTestz uc2 = new ChooseTestz(ownerz);
                uc2.Dock = DockStyle.Fill;
                frmHome.Instance.PnlContainer.Controls.Add(uc2);
                frmHome.Instance.PnlContainer1.Enabled = true;
            }
        }
    }
}
