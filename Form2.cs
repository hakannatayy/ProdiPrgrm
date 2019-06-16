using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Diagnostics;

namespace Uygulama
{
    public partial class Form2 : MaterialForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MaterialSkinManager msm = MaterialSkinManager.Instance;
            msm.AddFormToManage(this);
            msm.Theme = MaterialSkinManager.Themes.DARK;

          
          
                Button1.Enabled = false;
            
        }

        private void startNewTask()
        {
            try
            {
                Process.Start(textBox1.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sistem belirtilen dosyayı bulamadı.", "Hata");

                Debug.WriteLine(ex.Message);
            }

            this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.startNewTask();
        }
    }
}
