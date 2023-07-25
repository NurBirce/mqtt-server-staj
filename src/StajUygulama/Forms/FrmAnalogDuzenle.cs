using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StajUygulama.Models;

namespace StajUygulama.Forms
{
    public partial class FrmAnalogDuzenle : Form
    {
        FormMain frm;
        
        public FrmAnalogDuzenle(FormMain formMain)
        {
            InitializeComponent();
            frm = formMain;
        }

        private void FrmAnalogDuzenle_Load(object sender, EventArgs e)
        {
            deviceGoruntule();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm.systemState.analogDeviceList[selectedIndex].Name = txtName.Text;
            deviceGoruntule();
        }

        private void deviceGoruntule()
        {
            dataGridView1.Rows.Clear();
            Object[] dizi = new object[1];

            for (int i = 0; i < frm.systemState.analogDeviceList.Count; i++)
            {
                dizi[0] = frm.systemState.analogDeviceList[i].Name;
                dataGridView1.Rows.Add(dizi);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int selectedIndex;
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
