using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTaskDemo
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient = new HttpClient();


        private readonly IHttpClientFactory gIHttpClientFactory;

        public Form1()
        {
            InitializeComponent();

            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            gIHttpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // This line will yield control to the UI as the request
            // from the web service is happening.
            //
            // The UI thread is now free to perform other work.
            var stringData = await _httpClient.GetStringAsync(@"https://www.google.com");
            //MessageBox.Show(stringData);
            richTextBox1.Text = stringData;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string result = await DoButton2();

            richTextBox2.Text = result;
        }

        private async Task<string> DoButton2()
        {
            await Task.Delay(10000);

            return "DoButton2";
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var damageResult = await Task.Run(() => DoButton3());

            richTextBox3.Text = damageResult;

        }


        private async Task<string> DoButton3()
        {
            for(int i = 0; i < 100000; i++)
            {
                for(int j = 0; j < 100000; j++)
                {

                }
            }

            await Task.Delay(1);

            return "DoButton3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoButton4();
        }

        private string DoButton4()
        {
            for (int i = 0; i < 100000; i++)
            {
                for (int j = 0; j < 100000; j++)
                {

                }
            }

            return "DoButton4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //var client = gIHttpClientFactory.CreateClient();
            //var stringData = await client.GetStringAsync(@"https://www.google.com");
            ////MessageBox.Show(stringData);
            //richTextBox1.Text = stringData;



            string result = string.Empty;
            result = DoButton5();

            //MessageBox.Show(stringData);
            richTextBox1.Text = result;
        }

        private string DoButton5()
        {
            string rt = string.Empty;

            try
            {
                

                var client = gIHttpClientFactory.CreateClient();
                string stringData = string.Empty;

                //var stringData = await client.GetStringAsync(@"https://www.google.com");
                var response = client.GetStringAsync(@"https://www.google.com");
                //MessageBox.Show(stringData);
                if(response.IsCompleted == true)
                {
                    stringData = response.Result;
                }
                else
                {
                    stringData = "Not Complete";
                }

                rt = stringData;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return rt;
        }
    }
}
