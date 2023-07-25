using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using StajUygulama.Models;

namespace StajUygulama.Forms
{
    public partial class FrmAnalogEkle : Form
    {
        FormMain frm;
        public FrmAnalogEkle(FormMain formMain)
        {
            InitializeComponent();
            frm = formMain;
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmAnalogEkle_Load(object sender, EventArgs e)
        {
            deviceGoruntule();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            frm.deviceList.Add(new Device<float>(txtName.Text,txtTopic.Text));
            deviceGoruntule();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.deviceList,options);
            File.WriteAllText(fileName, jsonString);
        }

        private void deviceGoruntule()
        {
            dataGridView1.Rows.Clear();
            Object[] dizi = new object[3];

            for(int i = 0; i < frm.deviceList.Count; i++)
                {
                    dizi[0] = frm.deviceList[i].Name;
                    dizi[1] = frm.deviceList[i].Topic;
                    dataGridView1.Rows.Add(dizi);
                }
        }

        
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
