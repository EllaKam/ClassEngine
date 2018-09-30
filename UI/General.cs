using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;

namespace UI
{
    public partial class General : Form
    {
        #region Members
        private CancellationTokenSource tokenSource;
        private string directoryPath = Path.Combine(System.Environment.CurrentDirectory, Constants.PATH_DIRECTORY);
        #endregion
        public General()
        {
            InitializeComponent();
        }

        private void General_Load(object sender, EventArgs e)
        {
            GenerateProcessManager.GetManager().Status += details => Invoke((Action)(() => txtResult.Text += $"{details}\r\n")); ;
        }
        private async void btnRun_Click(object sender, EventArgs e)
        {
            btnCreater.Enabled = false;
            btnRun.Enabled = false;
            txtResult.Text = "";
            lblTime.Text = "";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Task> tasks = new List<Task>();

            foreach (string filePath in Directory.GetFiles(directoryPath))
            {
                var worker = new Worker();
                worker.ProcessResult += details => Invoke((Action)(() => txtResult.Text += $"{details}\r\n"));
                Task task = Task.Factory.StartNew(() => worker.LoadClass(filePath));
                tasks.Add(task);
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (AggregateException ex)
            {
                stopwatch.Stop();
                ex.Flatten().Handle(exc =>
                {
                    MessageBox.Show(ex.Message);
                    return true;
                });

            }
            stopwatch.Stop();
            lblTime.Text = $"Time elapsed: {stopwatch.Elapsed.ToString("hh\\:mm\\:ss\\:fff")}";
            btnCreater.Enabled = true;
            btnRun.Enabled = true;
     
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            GenerateProcessManager.GetManager().LoadClassData(Constants.CLASS_DATA_XML);
            GenerateProcessManager.GetManager().LoadRules(Constants.RULES_XML);
            grCreate.Enabled = true;
            btnLoad.Enabled = false;
        }

        private async void btnCreater_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            btnCreater.Enabled = false;
            grImport.Enabled = false;
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    GenerateProcessManager.GetManager().ClassesCreater(Convert.ToInt32(nmCreater.Value), directoryPath);
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            btnCreater.Enabled = true;
            grImport.Enabled = true;
        }
       
    }
}
