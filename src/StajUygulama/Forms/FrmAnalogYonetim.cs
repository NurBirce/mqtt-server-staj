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
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace StajUygulama.Forms
{
    public partial class FrmAnalogYonetim : Form
    {
        FormMain frm;
        
        public FrmAnalogYonetim(FormMain formMain)
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
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.systemState, options);
            File.WriteAllText(fileName, jsonString);
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


        int selectedIndex;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            selectedIndex = dataGridView1.CurrentRow.Index;

            txtName.Text = frm.systemState.analogDeviceList[selectedIndex].Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                frm.systemState.analogDeviceList.Remove(frm.systemState.analogDeviceList[selectedIndex]);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string fileName = "KaratalDevice.json";
                string jsonString = JsonSerializer.Serialize(frm.systemState, options);
                File.WriteAllText(fileName, jsonString);
            }
            else
            {
                MessageBox.Show("Silinecek satırı seçin..");
            }
        }
    }
}
