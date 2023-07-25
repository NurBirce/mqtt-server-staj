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
        internal SystemState systemState = new SystemState();

        Mqtt mqttObject;
        public FormMain()
        {
            InitializeComponent();
            mqttObject = new Mqtt(this);
        }

        FrmAnalogEkle frmAEkle;
        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAEkle = new FrmAnalogEkle(this);
            frmAEkle.ShowDialog();
        }

        FrmAnalogDuzenle frmADuzenle;
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmADuzenle = new FrmAnalogDuzenle(this);
            frmADuzenle.ShowDialog();
        }

        FrmDigitalEkle frmDEkle;
        private void ekleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDEkle = new FrmDigitalEkle(this);
            frmDEkle.ShowDialog();
        }

        FrmDigitalDuzenle frmDDuzenle;
        private void düzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDDuzenle = new FrmDigitalDuzenle(this);
            frmDDuzenle.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            initialization();
        }

        private async Task initialization()
        {
            await mqttObject.initiliaze();

            SystemState sysState = null;
            try
            {
                sysState = JsonSerializer.Deserialize<SystemState>(File.ReadAllText("KaratalDevice.json"));
            }
            catch { }

            if (sysState != null)
            {
                systemState = sysState;
                foreach (var d in systemState.analogDeviceList)
                {
                    mqttObject.subscribe("karatal2023fatmaproje/" + d.Topic);
                }
                foreach (var d in systemState.digitalDeviceList)
                {
                    mqttObject.subscribe("karatal2023fatmaproje/" + d.Topic);
                }
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

        public FormWatch frmWatch;
        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(frmWatch == null)
            {
                frmWatch = new FormWatch(this);
                //frmWatch.Owner = this;
                frmWatch.MdiParent = this;
                frmWatch.FormClosed += FrmWatch_FormClosed;
                frmWatch.Show();
            }
            else
            {
                frmWatch.Activate();
            }
        }

        private void FrmWatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmWatch?.Dispose();
            frmWatch = null;
        }
    }
}
