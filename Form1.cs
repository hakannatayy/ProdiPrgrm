using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;

namespace Uygulama
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void getProcesses()
        {
            Process[] processes = Process.GetProcesses();

            listBox1.Items.Clear();
            foreach (Process process in processes)
            {
                listBox1.Items.Add(process.ProcessName);
            }
        }

        private void killSelectedProcesses()
        {
            DialogResult dialogResult = MessageBox.Show("İşlemi sonlandırmak istiyor musunuz?", "Dikkat", MessageBoxButtons.YesNo);

            if (dialogResult.Equals(DialogResult.Yes))
            {
                var processName = listBox1.SelectedItem;
                Process[] processes = Process.GetProcessesByName(processName.ToString());

                foreach (Process process in processes)
                {
                    Debug.WriteLine("İşlem sonlandırıldı: " + process.ProcessName);
                    process.Kill();
                }

                MessageBox.Show("İşlem sonlandırıldı: " + processName, "Başarılı");
            }
        }

        private void killBannedProcesses()
        {
            foreach (string processName in listBox2.Items)
            {
                Process[] processes = Process.GetProcessesByName(processName);

                foreach (Process process in processes)
                {
                    Debug.WriteLine("İşlem sonlandırıldı: " + process.ProcessName);
                    process.Kill();
                }
            }
        }

        private void setButtonsEnabledState(bool state)
        {
            button1.Enabled = state;
            button2.Enabled = state;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MaterialSkinManager msm = MaterialSkinManager.Instance;
            msm.AddFormToManage(this);
            msm.Theme = MaterialSkinManager.Themes.DARK;

            this.setButtonsEnabledState(false);

            timer1.Interval = 10000;
            timer1.Start();

            this.getProcesses();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.getProcesses();
            this.killBannedProcesses();
            this.setButtonsEnabledState(false);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.killSelectedProcesses();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string processName = listBox1.SelectedItem.ToString();

            if (listBox2.Items.Contains(processName))
            {
                MessageBox.Show("Bu işlemi zaten yasakladınız.", "Hata");
                return;
            }

            listBox2.Items.Add(processName);
            this.killBannedProcesses();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Delete))
            {
                this.killSelectedProcesses();
            }
        }

        private void listBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Delete))
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setButtonsEnabledState(listBox1.Items.Count > 0);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (Form2 newTaskForm = new Form2())
            {
                newTaskForm.ShowDialog();
            }
        }
    }
}
