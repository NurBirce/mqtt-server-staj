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
        public string data;

        public FormMain()
        {
            InitializeComponent();
            mqttObject = new Mqtt(this);
        }

        FrmAnalogEkle frmAAdd;
        private void ekleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAAdd = new FrmAnalogEkle(this);
            frmAAdd.ShowDialog();
        }

        FrmAnalogYonetim frmAEdit;
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAEdit = new FrmAnalogYonetim(this);
            frmAEdit.ShowDialog();
        }

        FrmDigitalEkle frmDAdd;
        private void ekleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDAdd = new FrmDigitalEkle(this);
            frmDAdd.ShowDialog();
        }

        FrmDigitalYonetim frmDEdit;
        private void düzenleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDEdit = new FrmDigitalYonetim(this);
            frmDEdit.ShowDialog();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            await initialization();

            string port = "COM6";
            int baudRate = 9600;
            serialPort1.BaudRate = Convert.ToInt32(baudRate);
            serialPort1.PortName = port;
            serialPort1.Open();
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

        public void timer1_Tick(object sender, EventArgs e)
        {
            if (systemState != null)
            {
                foreach (var d in systemState.analogDeviceList)
                {
                    mqttObject.PublishAnalog(d.Value.ToString(), d.Topic);
                }
                foreach (var d in systemState.digitalDeviceList)
                {
                    mqttObject.PublishDigital(d.Value ? "1" : "0", d.Topic);
                }
            }
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (systemState == null) return;

            data = null;
            data = serialPort1.ReadLine();
            if (data == null) return;
            string[] bothData = data.Split('D');
            if (bothData?.Length < 2) return;

            string[] analogData = bothData[0].Split('_');
            string[] digitalData = bothData[1].Split('_');

            for (int i = 0; i < analogData.Length; i++)
            {
                Console.WriteLine("a: {0}", analogData[i]);
                systemState.analogDeviceList[i].Value = float.Parse(analogData[i]);
                frmWatch?.updateAnalogDevice(systemState.analogDeviceList[i].Topic, analogData[i]);

            }
            for (int i = 0; i < digitalData.Length; i++)
            {
                Console.WriteLine("d: {0}", digitalData[i]);
                systemState.digitalDeviceList[i].Value =digitalData[i] == "1" ? true : false;
                frmWatch?.updateDigitalDevice(systemState.digitalDeviceList[i].Topic, digitalData[i]);
            }

        }
    }
}
