using MqttServerStaj.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.IO.Ports;

namespace MqttServerStaj.Forms
{
    public partial class FormWatch : Form
    {
        FormMain fm;
        Dictionary<string, DataGridViewRow> topicDgvrDictionary;

        public FormWatch(FormMain frm)
        {
            InitializeComponent();
            topicDgvrDictionary = new Dictionary<string, DataGridViewRow>();
            fm = frm;
        }

        private void FormWatch_Load(object sender, EventArgs e)
        {
            displayDevices();
            
        }

        void displayDevices()
        {
            dgvAnalog.Rows.Clear();
            dgvDigital.Rows.Clear();
            topicDgvrDictionary.Clear();
            object[] objArr = new object[2];
            int nRowIndex;
            foreach (var ad in fm.systemState.analogDeviceList)
            {
                objArr[0] = ad.Name;
                objArr[1] = ad.Value;
                dgvAnalog.Rows.Add(objArr);

                nRowIndex = dgvAnalog.Rows.Count - 1;
                topicDgvrDictionary.Add(ad.Topic.ToLower(), dgvAnalog.Rows[nRowIndex]);
            }
            foreach (var dd in fm.systemState.digitalDeviceList)
            {
                objArr[0] = dd.Name;
                objArr[1] = dd.Value ? "ON" : "OFF";
                dgvDigital.Rows.Add(objArr);
                nRowIndex = dgvDigital.Rows.Count - 1;
                dgvDigital.Rows[nRowIndex].Tag = dd;
                topicDgvrDictionary.Add(dd.Topic.ToLower(), dgvDigital.Rows[nRowIndex]);
            }
        }
        
        public void updateAnalogDevice(string topic , string value)
        {
            fm.Invoke(new Action(() =>
            {
                DataGridViewRow dgvR;
                if (topicDgvrDictionary.TryGetValue(topic, out dgvR))
                {
                    dgvR.Cells[1].Value = float.Parse(value);
                }
            }));
        }
        public void updateDigitalDevice(string topic, string value)
        {
            DataGridViewRow dgvR;
            if (topicDgvrDictionary.TryGetValue(topic, out dgvR))
            {
                var d = (Device<bool>)dgvR.Tag;
                d.Value = value == "1" ? true : false;
                dgvR.Cells[1].Value = d.Value ? "ON" : "OFF";
            }
        }

        public void updateOnlyDigitalDevice(string topic, string value)
        {
            DataGridViewRow dgvR;
            if (topicDgvrDictionary.TryGetValue(topic, out dgvR))
            {
                var d = (Device<bool>)dgvR.Tag;
                d.Value = value == "1" ? true : false;
            }
        }

        private void dgvDigital_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == dgvDigital.Columns["clmBtn"].Index)
            {
                var device = ((Device<bool>)dgvDigital.CurrentRow.Tag);
                device.Value = !device.Value;
                sendDigitalsState();
            }
        }

        public void sendDigitalsState()
        {
            byte[] digitalValues = new byte[] {
                fm.systemState.digitalDeviceList[0].Value ? Convert.ToByte('1') : Convert.ToByte('0'),
                fm.systemState.digitalDeviceList[1].Value ? Convert.ToByte('1') : Convert.ToByte('0'),
                fm.systemState.digitalDeviceList[2].Value ? Convert.ToByte('1') : Convert.ToByte('0')
            };

            fm.serialPort1.Write(System.Text.Encoding.ASCII.GetString(digitalValues));
        }
    } 
}
