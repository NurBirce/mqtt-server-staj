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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm.deviceList2[selectedIndex].Name = txtName.Text;
            frm.deviceList2[selectedIndex].Topic = txtTopic.Text;
            deviceGoruntule();
        }

        private void deviceGoruntule()
        {
            dataGridView1.Rows.Clear();
            Object[] dizi = new object[3];
            for(int i = 0; i < frm.deviceList2.Count; i++)
            {
                dizi[0] = frm.deviceList2[i].Name;
                dizi[1] = frm.deviceList2[i].Topic;
                dataGridView1.Rows.Add(dizi);
            }
        }

        int selectedIndex;
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedIndex = dataGridView1.CurrentRow.Index;

            txtName.Text = frm.deviceList2[selectedIndex].Name;
            txtTopic.Text = frm.deviceList2[selectedIndex].Topic;
        }
    }
}
