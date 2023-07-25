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
using StajUygulama.Forms;
using System.Text.Json;
using System.IO;

namespace StajUygulama.Forms
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
            frm.deviceList2.Add(new Device<bool>(txtName.Text, txtTopic.Text));
            deviceGoruntule();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.deviceList2, options);
            File.WriteAllText(fileName, jsonString);
        }

        private void FrmDigitalEkle_Load(object sender, EventArgs e)
        {
            deviceGoruntule();
        }

        
        private void deviceGoruntule()
        {
            Object[] dizi = new object[3];
            for (int i = 0; i < frm.deviceList2.Count; i++)
            {
                dizi[0] = frm.deviceList2[i].Name;
                dizi[1] = frm.deviceList2[i].Topic;
                dataGridView1.Rows.Add(dizi);
            }
        }
    }
}
