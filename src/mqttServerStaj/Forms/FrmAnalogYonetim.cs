using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MqttServerStaj.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace MqttServerStaj.Forms
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
            deviceView();
        }

        private void deviceView()
        {
            dgvAnalog.Rows.Clear();
            Object[] dizi = new object[1];

            for (int i = 0; i < frm.systemState.analogDeviceList.Count; i++)
            {
                dizi[0] = frm.systemState.analogDeviceList[i].Name;
                dgvAnalog.Rows.Add(dizi);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frm.systemState.analogDeviceList[selectedIndex].Name = txtName.Text;
            deviceView();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.systemState, options);
            File.WriteAllText(fileName, jsonString);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAnalog.SelectedRows.Count > 0)
            {
                dgvAnalog.Rows.RemoveAt(dgvAnalog.CurrentRow.Index);
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

        int selectedIndex;
        private void dgvAnalog_SelectionChanged(object sender, EventArgs e)
        {
            selectedIndex = dgvAnalog.CurrentRow.Index;
            txtName.Text = frm.systemState.analogDeviceList[selectedIndex].Name;
        }
    }
}
