using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;
using System.Xml.Linq;
using System.Web.Script;

using System.Web.Script.Serialization;

using System.Runtime.Serialization;

namespace api_call
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        WebRequest req;


        private void Form1_Load(object sender, EventArgs e)
        {
            
            

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string site = string.Format("https://api.monkeylearn.com/v3/classifiers/cl_pi3C7JiL/classify/");
            req = WebRequest.Create(site);
            req.ContentType = "application/json";
            req.Method = "POST";
            req.Headers.Add("Authorization: Token 4ce100a3cd7d8e87a9f9b41f4fd1a168c373b9a5");





            string postdata = "{\"data\": [\" " +richTextBox2.Text + "\"]}";

           
            
            string txt = "";

            using (var str = new StreamWriter(req.GetRequestStream()))
            {
                str.Write(postdata);
                str.Flush();
                str.Close();
                
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                using (Stream st = resp.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(st);
                    txt = sr.ReadToEnd();
                }
            }

            //       [{"text":" sal","external_id":null,"error":false,"classifications":[{"tag_name":"Neutral","tag_id":60333050,"confidence":0.681}]}]

            //parsare string din json in normal;
            txt = txt.Substring(txt.IndexOf("\"tag_name\""));
            txt= txt.Replace(']', ' ');
            txt= txt.Replace('}',' ');

            string[] auxtext = txt.Split(':', ',');


            //   string convert = 
            richTextBox1.Text = auxtext[1].Trim('\"')+"\n"+"procent: "+ (Double.Parse(auxtext[5].Trim('\"'))*100).ToString() +" %";
            
        }
    }
}
