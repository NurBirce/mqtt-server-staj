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
using MqttServerStaj.Forms;
using System.Text.Json;
using System.IO;

namespace MqttServerStaj.Forms
{
    public partial class FrmDigitalEkle : Form
    {
        FormMain frm;
        public FrmDigitalEkle(FormMain formMain)
        {
            InitializeComponent();
            frm = formMain;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm.systemState.digitalDeviceList.Add(new Device<bool>(txtName.Text, frm.systemState.getLastTopicId().ToString()));
            deviceGoruntule();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.systemState, options);
            File.WriteAllText(fileName, jsonString);
        }

        private void FrmDigitalEkle_Load(object sender, EventArgs e)
        {
            deviceGoruntule();
        }

        
        private void deviceGoruntule()
        {
            Object[] dizi = new object[3];
            for (int i = 0; i < frm.systemState.digitalDeviceList.Count; i++)
            {
                dizi[0] = frm.systemState.digitalDeviceList[i].Name;
                dataGridView1.Rows.Add(dizi);
            }
        }
    }
}
