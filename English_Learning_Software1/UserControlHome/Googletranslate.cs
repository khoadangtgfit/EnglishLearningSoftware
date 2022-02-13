using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace English_Learning_Software1.UserControlHome
{
    public partial class Googletranslate : UserControl
    {
        public Googletranslate()
        {
            InitializeComponent();
        }
        public string TranslateText(string input)
        {
            string url;
            if (guna2ComboBox1.SelectedIndex == 0)
            {
                url = String.Format
                ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                "vi", "en", Uri.EscapeUriString(input));
            }
            else
            {
                url = String.Format
                ("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                "en", "vi", Uri.EscapeUriString(input));
            }
            HttpClient httpClient = new HttpClient();
            string result = httpClient.GetStringAsync(url).Result;
            var jsonData = new JavaScriptSerializer().Deserialize<List<dynamic>>(result);
            var translationItems = jsonData[0];
            string translation = "";
            foreach (object item in translationItems)
            {
                IEnumerable translationLineObject = item as IEnumerable;
                IEnumerator translationLineString = translationLineObject.GetEnumerator();
                translationLineString.MoveNext();
                translation += string.Format(" {0}", Convert.ToString(translationLineString.Current));
            }
            if (translation.Length > 1) { translation = translation.Substring(1); };
                return translation;
            
        }
        private void AddToCombobox()
        {
            guna2ComboBox1.Items.Add("Vietnamese");
            guna2ComboBox1.Items.Add("English");
            guna2ComboBox2.Items.Add("Vietnamese");
            guna2ComboBox2.Items.Add("English");
        }
        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox2.SelectedIndex == 0)
            {
                guna2ComboBox1.SelectedIndex = 1;
            }
            else if (guna2ComboBox2.SelectedIndex == 1)
            {
                guna2ComboBox1.SelectedIndex = 0;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2TextBox2.Text = TranslateText(guna2TextBox1.Text);
        }

        private void Googletranslate_Load(object sender, EventArgs e)
        {
            AddToCombobox();
            guna2ComboBox1.SelectedIndex = 1;
            guna2ComboBox2.SelectedIndex = 0;
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == 0)
            {
                guna2ComboBox2.SelectedIndex = 1;
            }
            else if(guna2ComboBox1.SelectedIndex==1)
            {
                guna2ComboBox2.SelectedIndex = 0;
            }
        }
        SpeechSynthesizer sp = new SpeechSynthesizer();

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedIndex == 0)
            {
                if (guna2TextBox2.Text != "")
                {
                    sp.Dispose();
                    sp = new SpeechSynthesizer();
                    sp.SelectVoice("Microsoft Zira Desktop");
                    sp.SpeakAsync(guna2TextBox2.Text);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập text!", "Thông báo");
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if(sp.State == SynthesizerState.Speaking)
            {
                sp.Pause();
            }
            
        }
    }
}
