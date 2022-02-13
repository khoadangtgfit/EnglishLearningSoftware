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
    public partial class VideoManagerment : UserControl
    {
        public VideoManagerment()
        {
            InitializeComponent();
            guna2Button2.Enabled = false;
            guna2Button4.Enabled = false;
            guna2Button3.Enabled = false;
            List<Video> listvideo = context.Videos.ToList();
            HienThiLenListView(listvideo);
        }
        EnglishDBContext context = new EnglishDBContext();
        private void Newword_TextChange(object sender, EventArgs e)
        {
            if (gunaTextBox1.Text != "" && gunaTextBox2.Text != "")
            {
                guna2Button2.Enabled = true;
                guna2Button4.Enabled = true;
                guna2Button3.Enabled = true;
            }
            else
            {
                guna2Button2.Enabled = false;
                guna2Button4.Enabled = false;
                guna2Button3.Enabled = false;
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Video video = context.Videos.FirstOrDefault(p => string.Compare(p.Video_Name, gunaTextBox1.Text, true) == 0);
                if (video != null)
                {
                    MessageBox.Show("Video này đã tồn tại!");
                }
                else
                {
                    Video a = new Video();
                    a.Video_Name = gunaTextBox1.Text;
                    a.Video_Path = gunaTextBox2.Text;
                    context.Videos.AddOrUpdate(a);
                    context.SaveChanges();
                    List<Video> listvideo = context.Videos.ToList();
                    HienThiLenListView(listvideo);
                    MessageBox.Show("Thêm Thành Công");
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        Video videothis;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem lvi = listView1.SelectedItems[0];
                gunaTextBox1.Text = lvi.SubItems[1].Text;
                gunaTextBox2.Text = lvi.SubItems[2].Text;
                Video c = context.Videos.ToList().FirstOrDefault(p => p.Video_Name == lvi.SubItems[1].Text);
                if (c != null)
                {
                    videothis = c;
                }
            }
        }

        private void HienThiLenListView(List<Video> listvideo)
        {
            listView1.Items.Clear();
            int index = 0;
            foreach (var item in listvideo)
            {
                ListViewItem listviewitem = new ListViewItem((index + 1).ToString());
                listviewitem.SubItems.Add(item.Video_Name);
                listviewitem.SubItems.Add(item.Video_Path);
                listView1.Items.Add(listviewitem);
                index++;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opfile = new OpenFileDialog();
            opfile.Filter = "Video files |*.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; *.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                          " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; *.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm; *.dat; ";
            if (opfile.ShowDialog() == DialogResult.OK)
            {
                gunaTextBox2.Text = opfile.SafeFileName;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (gunaTextBox1.Text != null)
                {
                    Video a = context.Videos.FirstOrDefault(p => string.Compare(p.Video_Name, gunaTextBox1.Text, true) == 0);
                    if (a == null)
                    {
                        MessageBox.Show("Không tìm thấy video này!");
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            context.Videos.Remove(a);
                            context.SaveChanges();
                            List<Video> listvideo = context.Videos.ToList();
                            HienThiLenListView(listvideo);
                            MessageBox.Show("Xóa thành công!");
                            LamMoi();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn video cần xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (videothis == null)
                {
                    MessageBox.Show("Vui lòng chọn câu chuyện cần sửa!");
                }
                else
                {
                    videothis.Video_Name = gunaTextBox1.Text;
                    videothis.Video_Path = gunaTextBox2.Text;
                    context.Videos.AddOrUpdate(videothis);
                    context.SaveChanges();
                    List<Video> listvideo = context.Videos.ToList();
                    HienThiLenListView(listvideo);
                    MessageBox.Show("Sửa thành công!");
                    LamMoi();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void LamMoi()
        {
            gunaTextBox1.Text = "";
            gunaTextBox2.Text = "";
            gunaTextBox3.Text = "";
            videothis = null;
        }
        private void gunaTextBox3_TextChanged(object sender, EventArgs e)
        {
            if (gunaTextBox3.Text == string.Empty) HienThiLenListView(context.Videos.ToList());
            else
            {
                List<Video> videos = context.Videos.Where(p => p.Video_Name.Trim().ToLower().Contains(gunaTextBox3.Text.Trim().ToLower())).ToList();
                HienThiLenListView(videos);
            }
        }
    }
}
