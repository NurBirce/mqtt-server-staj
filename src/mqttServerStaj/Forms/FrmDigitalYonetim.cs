using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MqttServerStaj.Forms;
using MqttServerStaj.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace MqttServerStaj.Forms
{
    public partial class FrmDigitalYonetim : Form
    {
        FormMain frm;
        public FrmDigitalYonetim(FormMain formMain)
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
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.systemState, options);
            File.WriteAllText(fileName, jsonString);
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
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            selectedIndex = dataGridView1.CurrentRow.Index;

            txtName.Text = frm.systemState.digitalDeviceList[selectedIndex].Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                frm.systemState.digitalDeviceList.Remove(frm.systemState.digitalDeviceList[selectedIndex]);
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
