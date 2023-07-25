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
            
            frm.deviceList[selectedIndex].Name = txtName.Text;
            frm.deviceList[selectedIndex].Topic = txtTopic.Text;
            deviceGoruntule();
        }

        private void deviceGoruntule()
        {
            dataGridView1.Rows.Clear();
            Object[] dizi = new object[3];

            for (int i = 0; i < frm.deviceList.Count; i++)
            {
                dizi[0] = frm.deviceList[i].Name;
                dizi[1] = frm.deviceList[i].Topic;
                dataGridView1.Rows.Add(dizi);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        int selectedIndex;
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            selectedIndex = dataGridView1.CurrentRow.Index;

            txtName.Text = frm.deviceList[selectedIndex].Name;
            txtTopic.Text = frm.deviceList[selectedIndex].Topic;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
