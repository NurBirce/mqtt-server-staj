using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StajUygulama.Forms;
using StajUygulama.Models;

namespace StajUygulama.Forms
{
    public partial class FrmDigitalDuzenle : Form
    {
        FormMain frm;
        public FrmDigitalDuzenle(FormMain formMain)
        {
            InitializeComponent();
            frm = formMain;
        }

        private void FrmDigitalDuzenle_Load(object sender, EventArgs e)
        {
            deviceGoruntule();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm.systemState.digitalDeviceList[selectedIndex].Name = txtName.Text;
            deviceGoruntule();
        }

        private void deviceGoruntule()
        {
            dataGridView1.Rows.Clear();
            object[] dizi = new object[1];
            for(int i = 0; i < frm.systemState.digitalDeviceList.Count; i++)
            {
                dizi[0] = frm.systemState.digitalDeviceList[i].Name;
                dataGridView1.Rows.Add(dizi);
            }
        }

        int selectedIndex;
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedIndex = dataGridView1.CurrentRow.Index;

            txtName.Text = frm.systemState.digitalDeviceList[selectedIndex].Name;
        }
    }
}
