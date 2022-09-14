using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace CopiaLogsPDV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox2.ScrollBars = ScrollBars.Vertical;
        }

        string[] relacaoIps;
        string data = "";
        private void button1_Click(object sender, EventArgs e)
        {
            data = textBox1.Text;
            File.WriteAllText("relacaoIps.txt", textBox2.Text);
            relacaoIps = System.IO.File.ReadAllLines("relacaoIps.txt");
            for (int i = 0; i < relacaoIps.Length; i++)
            {
                IPHostEntry hostInfo = Dns.Resolve(relacaoIps[i]);
                StreamWriter logCopia = new StreamWriter("log.txt", true);
                logCopia.WriteLine(DateTime.Now.ToString() + " >> Copiando >> " + hostInfo.HostName.Substring(0, 10) + "_" + relacaoIps[i] + "_log_erros_"+data+".log");
                try
                {
                    File.Copy(@"\\" + relacaoIps[i] + @"\c$\proton\pdv-client\log\erros\log_erros_" + data + ".log", @"D:\logPDV\" + 
                                       hostInfo.HostName.Substring(0, 10)+ "_" + relacaoIps[i] + "_log_erros_" + data + ".log", true);
                }
                
                catch(Exception ex)
                {
                    logCopia.WriteLine(DateTime.Now.ToString() + "Erro: " + hostInfo.HostName.Substring(0, 10) + "_" + relacaoIps[i] + " >> " + ex.Message);
                    logCopia.Close();
                }
                logCopia.Close();
            }
        }
    }
}
