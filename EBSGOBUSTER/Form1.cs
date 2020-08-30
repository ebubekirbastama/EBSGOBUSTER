using EbubekirBastamatxtokuma;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace EBSGOBUSTER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        Thread th; OpenFileDialog op; string[] dz;
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(bsl); th.Start();
        }

        private void bsl()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            for (int i = 0; i < dz.Length; i++)
            {
                label3.Text = i.ToString();
                WebRequest request = WebRequest.Create(textBox1.Text);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                try
                {
                    listBox1.Items.Add(((HttpWebResponse)response).StatusDescription + ":" + dz[i].ToString());

                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        richTextBox1.AppendText("---------------------------------------");
                        richTextBox1.AppendText(reader.ReadToEnd().ToString() + "\r");
                        richTextBox1.AppendText("---------------------------------------");
                    }
                    response.Close();

                }
                catch (Exception ex)
                {
                    listBox1.Items.Add(ex.Message);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog()==DialogResult.OK) //EBS Coding...
            {
                th = new Thread(vroku);th.Start();
            }
        }
        private void vroku()
        {
            dz =BekraTxtOkuma.TxtimportDizi(op.FileName,false);
            MessageBox.Show("Aktarım Tamamlandı...","EBS Securty");
        }
    }
}
