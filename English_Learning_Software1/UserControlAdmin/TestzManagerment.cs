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

namespace English_Learning_Software1.UserControlAdmin
{
    public partial class TestzManagerment : UserControl
    {
        EnglishDBContext context = new EnglishDBContext();
        public TestzManagerment()
        {
            InitializeComponent();
            HienThiLenListView(context.Questions.ToList());
            List<Test> tests = context.Tests.ToList();
            HienThiLenCombobox(tests);
            guna2Button2.Enabled = false;
            guna2Button3.Enabled = false;
            guna2ComboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            guna2Button4.Enabled = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Question question = context.Questions.FirstOrDefault(p => string.Compare(p.Question_Content, guna2TextBox1.Text, true) == 0);
                if (question != null)
                {
                    MessageBox.Show("Câu hỏi này đã tồn tại!");
                }
                else
                {
                    Question a = new Question();
                    a.Question_Content = guna2TextBox1.Text;
                    Test test = context.Tests.FirstOrDefault(p => p.Test_Name == guna2ComboBox1.Text);
                    if (test != null)
                        a.Test_Code = test.Test_Code;
                    List<Question> questions = context.Questions.Where(p => p.Test_Code == test.Test_Code).ToList();
                    if (questions.Count < 10)
                    {
                        context.Questions.AddOrUpdate(a);
                        context.SaveChanges();
                        ThemAnwers(guna2TextBox3.Text, guna2TextBox4.Text, guna2TextBox5.Text, guna2TextBox6.Text, a.Question_Code);
                        HienThiLenListView(context.Questions.ToList());
                        MessageBox.Show("Thêm Thành Công");
                        LamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Số câu hỏi đã vượt quá 10, không thể thêm!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void HienThiLenListView(List<Question> questions)
        {
            listView1.Items.Clear();
            int index = 0;
            foreach (Question item in questions)
            {
                ListViewItem lvi = new ListViewItem((index + 1).ToString());
                lvi.SubItems.Add(item.Question_Content);
                List<Answer> answers1 = context.Answers.Where(p => p.Question_Code == item.Question_Code).ToList();
                foreach (Answer item1 in answers1)
                {
                    lvi.SubItems.Add(item1.Answer_Content);
                }
                Test a = context.Tests.FirstOrDefault(p => p.Test_Code == item.Test_Code);
                lvi.SubItems.Add(a.Test_Name);
                listView1.Items.Add(lvi);
                index++;
            }
        }
        private void HienThiLenCombobox(List<Test> listtest)
        {
            this.guna2ComboBox1.DataSource = listtest;
            this.guna2ComboBox1.DisplayMember = "Test_Name";
            this.guna2ComboBox1.ValueMember = "Test_Code";
        }
        private void ThemAnwers(string da1,string da2,string da3,string da4,int questioncode)
        {
            try
            {
                Answer a = new Answer();
                a.Answer_Content = da1;
                a.Question_Code = questioncode;
                if (radioButton1.Checked == true)
                {
                    a.Correct = true;
                }
                else
                {
                    a.Correct = false;
                }
                context.Answers.AddOrUpdate(a);
                context.SaveChanges();
                Answer b = new Answer();
                b.Answer_Content = da2;
                b.Question_Code = questioncode;
                if (radioButton4.Checked == true)
                {
                    b.Correct = true;
                }
                else
                {
                    b.Correct = false;
                }
                context.Answers.AddOrUpdate(b);
                context.SaveChanges();
                Answer c = new Answer();
                c.Answer_Content = da3;
                c.Question_Code = questioncode;
                if (radioButton2.Checked == true)
                {
                    c.Correct = true;
                }
                else
                {
                    c.Correct = false;
                }
                context.Answers.AddOrUpdate(c);
                context.SaveChanges();
                Answer d = new Answer();
                d.Answer_Content = da4;
                d.Question_Code = questioncode;
                if (radioButton3.Checked == true)
                {
                    d.Correct = true;
                }
                else
                {
                    d.Correct = false;
                }
                context.Answers.AddOrUpdate(d);
                context.SaveChanges();
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
        }
        private void SuaAnwers(Answer da1, Answer da2, Answer da3, Answer da4)
        {
            try
            {
                da1.Answer_Content = guna2TextBox3.Text;
                da1.Question_Code = questionthis.Question_Code;
                if (radioButton1.Checked == true)
                {
                    da1.Correct = true;
                }
                else
                {
                    da1.Correct = false;
                }
                context.Answers.AddOrUpdate(da1);
                context.SaveChanges();
                da2.Answer_Content = guna2TextBox4.Text;
                da2.Question_Code = questionthis.Question_Code;
                if (radioButton4.Checked == true)
                {
                    da2.Correct = true;
                }
                else
                {
                    da2.Correct = false;
                }
                context.Answers.AddOrUpdate(da2);
                context.SaveChanges();
                da3.Answer_Content = guna2TextBox5.Text;
                da3.Question_Code = questionthis.Question_Code;
                if (radioButton2.Checked == true)
                {
                    da3.Correct = true;
                }
                else
                {
                    da3.Correct = false;
                }
                context.Answers.AddOrUpdate(da3);
                context.SaveChanges();
                da4.Answer_Content = guna2TextBox6.Text;
                da4.Question_Code = questionthis.Question_Code;
                if (radioButton3.Checked == true)
                {
                    da4.Correct = true;
                }
                else
                {
                    da4.Correct = false;
                }
                context.Answers.AddOrUpdate(da4);
                context.SaveChanges();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2TextBox2.Text == "")
                {
                    MessageBox.Show("Yêu cầu nhập!");
                }
                else
                {
                    Test a = context.Tests.FirstOrDefault(p => string.Compare(p.Test_Name, guna2TextBox2.Text, true) == 0);
                    if (a == null)
                    {
                        Test b = new Test();
                        b.Test_Name = guna2TextBox2.Text;
                        context.Tests.AddOrUpdate(b);
                        context.SaveChanges();
                        HienThiLenListView(context.Questions.ToList());
                        List<Test> tests = context.Tests.ToList();
                        HienThiLenCombobox(tests);
                        MessageBox.Show("Thêm thành công!");
                        LamMoi();
                    }
                    else
                    {
                        MessageBox.Show("Tên bài kiểm tra đã tồn tại!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Changged(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "" || guna2TextBox3.Text == "" || guna2TextBox4.Text == "" || guna2TextBox5.Text == "" || guna2TextBox6.Text == "")
            {
                guna2Button2.Enabled = false;
                guna2Button3.Enabled = false;
                guna2Button4.Enabled = false;
            }
            else
            {
                guna2Button2.Enabled = true;
                guna2Button3.Enabled = true;
                guna2Button4.Enabled = true;
            }
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (questionthis == null)
                {
                    MessageBox.Show("Vui lòng chọn câu hỏi cần sửa!");
                }
                else
                {
                    List<Answer> answers = context.Answers.Where(p => p.Question_Code == questionthis.Question_Code).ToList();
                    SuaAnwers(answers[0],answers[1],answers[2],answers[3]);
                    questionthis.Question_Content = guna2TextBox1.Text;
                    Test test = context.Tests.FirstOrDefault(p => p.Test_Name == guna2ComboBox1.Text);
                    List<Question> questions = context.Questions.Where(p => p.Test_Code == test.Test_Code).ToList();
                    if (questions.Count < 10)
                    {
                        questionthis.Test_Code = test.Test_Code;
                        context.Questions.AddOrUpdate(questionthis);
                        context.SaveChanges();
                        MessageBox.Show("Sửa thành công!");
                        HienThiLenListView(context.Questions.ToList());
                        LamMoi();
                    }
                    else
                        MessageBox.Show("Test " + test.Test_Name + " đã đủ 10 câu hỏi, bạn không thể thêm!");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DapAnDung(string tb)
        {
            if (radioButton1.Name == tb)
            {
                radioButton1.Checked = true;
            }
            else if (radioButton2.Name == tb)
            {
                radioButton2.Checked = true;
            }
            else if (radioButton3.Name == tb)
            {
                radioButton3.Checked = true;
            }
            else if(radioButton4.Name == tb)
            {
                radioButton4.Checked = true;
            }
        }
        Question questionthis;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lvi = listView1.SelectedItems[0];
                guna2TextBox1.Text = lvi.SubItems[1].Text;
                string cauhoi = lvi.SubItems[1].Text;
                Question a = context.Questions.FirstOrDefault(p => p.Question_Content ==cauhoi);
                Answer b = context.Answers.FirstOrDefault(p => p.Question_Code == a.Question_Code && p.Correct == true);
                guna2TextBox3.Text = lvi.SubItems[2].Text;
                radioButton1.Name = lvi.SubItems[2].Text;
                radioButton4.Name = lvi.SubItems[3].Text;
                radioButton2.Name = lvi.SubItems[4].Text;
                radioButton3.Name = lvi.SubItems[5].Text;
                DapAnDung(b.Answer_Content);
                guna2TextBox4.Text = lvi.SubItems[3].Text;
                guna2TextBox5.Text = lvi.SubItems[4].Text;
                guna2TextBox6.Text = lvi.SubItems[5].Text;
                guna2ComboBox1.Text = lvi.SubItems[6].Text;
                questionthis = a;
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (guna2TextBox1.Text != null)
                {
                    Question a = context.Questions.FirstOrDefault(p => string.Compare(p.Question_Content, guna2TextBox1.Text, true) == 0);
                    if (a == null)
                    {
                        MessageBox.Show("Không tìm thấy câu hỏi này!");
                    }
                    else
                    {
                        List<Answer> list = context.Answers.Where(p => p.Question_Code == a.Question_Code).ToList();
                        context.Answers.RemoveRange(list);
                        context.Questions.Remove(a);
                        context.SaveChanges();
                        HienThiLenListView(context.Questions.ToList());
                        MessageBox.Show("Xóa thành công!");
                        LamMoi();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập câu hỏi cần xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void LamMoi()
        {
            guna2TextBox1.Text = "";
            guna2TextBox1.Focus();
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
            guna2TextBox7.Text = "";
            guna2ComboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            questionthis = null;
        }
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void guna2TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (guna2TextBox7.Text == string.Empty) HienThiLenListView(context.Questions.ToList());
            else
            {
                List<Question> questions = context.Questions.Where(p => p.Question_Content.Trim().ToLower().Contains(guna2TextBox7.Text.Trim().ToLower())).ToList();
                HienThiLenListView(questions);
            }
        }
    }
}
