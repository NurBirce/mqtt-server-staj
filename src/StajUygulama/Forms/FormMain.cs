using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using StajUygulama.Forms;
using StajUygulama.Models;
using StajUygulama.MQTT;

namespace StajUygulama.Forms
{
    public partial class FormMain : Form
    {
        internal List<Device<float>> deviceList = new List<Device<float>>();
        internal List<Device<bool>> deviceList2 = new List<Device<bool>>();
        Mqtt mqttObject = new Mqtt();
        public FormMain()
        {
            InitializeComponent();
        }

        FrmAnalogEkle frmAEkle;
        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAEkle = new FrmAnalogEkle(this);
            frmAEkle.Show();
        }

        FrmAnalogDuzenle frmADuzenle;
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmADuzenle = new FrmAnalogDuzenle(this);
            frmADuzenle.Show();
        }

        FrmDigitalEkle frmDEkle;
        private void ekleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDEkle = new FrmDigitalEkle(this);
            frmDEkle.Show();
        }

        FrmDigitalDuzenle frmDDuzenle;
        private void düzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDDuzenle = new FrmDigitalDuzenle(this);
            frmDDuzenle.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            mqttObject.initiliaze();

            var dList =
               JsonSerializer.Deserialize<List<Device<float>>>(File.ReadAllText("KaratalDevice.json"));
            if(dList != null)
            {
                deviceList = dList;
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mqttObject.initiliaze();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mqttObject.disconnect();
        }

        FormMqtt formMqtt;
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formMqtt = new FormMqtt(this);
            formMqtt.Show();
        }
    }
}
