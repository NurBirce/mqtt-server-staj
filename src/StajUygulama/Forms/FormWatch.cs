using StajUygulama.Models;
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

namespace StajUygulama.Forms
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
                objArr[1] = dd.Value;
                dgvDigital.Rows.Add(objArr);
                nRowIndex = dgvDigital.Rows.Count - 1;
                topicDgvrDictionary.Add(dd.Topic.ToLower(), dgvDigital.Rows[nRowIndex]);
            }
        }

        public void updateDeviceValue(string topic , string value)
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
    }
}
