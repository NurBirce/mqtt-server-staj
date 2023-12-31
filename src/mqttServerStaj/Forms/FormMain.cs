﻿using System;
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
using MqttServerStaj.Forms;
using MqttServerStaj.Models;
using MqttServerStaj.MQTT;
using MQTTnet.Server;
using MQTTnet.Client;
using System.IO.Ports;

namespace MqttServerStaj.Forms
{
    public partial class FormMain : Form
    {
        internal SystemState systemState = new SystemState();

        internal Mqtt mqttObject;
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

        FrmAnalogYonetim frmADuzenle;
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmADuzenle = new FrmAnalogYonetim(this);
            frmADuzenle.ShowDialog();
        }

        FrmDigitalEkle frmDEkle;
        private void ekleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDEkle = new FrmDigitalEkle(this);
            frmDEkle.ShowDialog();
        }

        FrmDigitalYonetim frmDDuzenle;
        private void düzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDDuzenle = new FrmDigitalYonetim(this);
            frmDDuzenle.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            initialization();

            /*
            string port = "COM5";
            int baudRate = 9600;
            serialPort1.BaudRate = baudRate;
            serialPort1.PortName = port;
            serialPort1.Open();
            */
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            /*
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            */
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
                //foreach (var d in systemState.analogDeviceList)
                //{
                //    mqttObject.subscribeAnalog(d.Topic);
                //}
                foreach (var d in systemState.digitalDeviceList)
                {
                    mqttObject.subscribeDigital(d.Topic);
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

        Random rnd = new Random(1000);
        public void timer1_Tick(object sender, EventArgs e)
        {
            SystemState sysState = null;
            try
            {
                sysState = JsonSerializer.Deserialize<SystemState>(File.ReadAllText("KaratalDevice.json"));
            }
            catch{ 
            }

            if (sysState != null)
            {
                systemState = sysState;
                int sayi;
                foreach (var d in systemState.analogDeviceList)
                {
                    sayi = rnd.Next(1, 1000);
                    mqttObject.PublishAnalog(sayi.ToString(), d.Topic);
                }
                //foreach (var d in systemState.digitalDeviceList)
                //{
                //    sayi = rnd.Next(0, 1100) % 2;
                //    mqttObject.Publish_Application_Message(sayi.ToString(), d.Topic);
                //}
            }
        }

        
    }
}
