﻿using System;
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
using MqttServerStaj.Models;

namespace MqttServerStaj.Forms
{
    public partial class FrmAnalogEkle : Form
    {
        FormMain frm;
        public FrmAnalogEkle(FormMain formMain)
        {
            InitializeComponent();
            frm = formMain;
            
        }

        private void FrmAnalogEkle_Load(object sender, EventArgs e)
        {
            deviceView();
        }

        private void deviceView()
        {
            dgvAnalog.Rows.Clear();
            Object[] dizi = new object[3];

            for(int i = 0; i < frm.systemState.analogDeviceList.Count; i++)
            {
                    dizi[0] = frm.systemState.analogDeviceList[i].Name;
                    dgvAnalog.Rows.Add(dizi);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frm.systemState.analogDeviceList.Add(new Device<float>(txtName.Text, frm.systemState.getLastTopicId().ToString()));
            deviceView();
            var options = new JsonSerializerOptions { WriteIndented = true };
            string fileName = "KaratalDevice.json";
            string jsonString = JsonSerializer.Serialize(frm.systemState, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
