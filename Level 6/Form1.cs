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
using Newtonsoft.Json;

namespace Level_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.Visible = false;
        }

        int page = 1;     

        public class Data
        {
            public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string avatar { get; set; }
        }

        public class DataArr
        {
            public Data[] data { get; set; }
        }

        public void GetData(int page)
        {
            WebRequest request = WebRequest.Create(@"https://reqres.in/api/users?page=" + page);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            string line = "";
            line = reader.ReadToEnd();

            response.Close();

            string json = line;

            DataArr ro = JsonConvert.DeserializeObject<DataArr>(json);
            foreach (var data in ro.data)
            {
                textBox1.Text += string.Format("{0} {1}", data.first_name, data.last_name) + Environment.NewLine + Environment.NewLine;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Статус: \"Загрузка…\" ";


            textBox1.Clear();
            page++;
            GetData(page);
            button2.Visible = true;
            label1.Text = "Статус: \"Выполнено\" ";

            if (page == 4)
            {
                button1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Статус: \"Загрузка…\" ";

                textBox1.Clear();
                page--;
                GetData(page);
                button1.Visible = true;
            

            if (page == 1)
            {
                button2.Visible = false;
            }

            label1.Text = "Статус: \"Выполнено\" ";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Статус: \"Загрузка…\" ";
            GetData(1);
            label1.Text = "Статус: \"Выполнено\" ";
        }
    }
}
