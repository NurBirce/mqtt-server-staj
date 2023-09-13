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
            deviceView();
        }

        private void deviceView()
        {
            dgvDigital.Rows.Clear();
            object[] dizi = new object[1];
            for(int i = 0; i < frm.systemState.digitalDeviceList.Count; i++)
            {
                dizi[0] = frm.systemState.digitalDeviceList[i].Name;
                dgvDigital.Rows.Add(dizi);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frm.systemState.digitalDeviceList[selectedIndex].Name = txtName.Text;
            deviceView();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.systemState, options);
            File.WriteAllText(fileName, jsonString);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDigital.SelectedRows.Count > 0)
            {
                dgvDigital.Rows.RemoveAt(dgvDigital.CurrentRow.Index);
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
        int selectedIndex;
        private void dgvDigital_SelectionChanged(object sender, EventArgs e)
        {
            selectedIndex = dgvDigital.CurrentRow.Index;
            txtName.Text = frm.systemState.digitalDeviceList[selectedIndex].Name;
        }
    }
}
