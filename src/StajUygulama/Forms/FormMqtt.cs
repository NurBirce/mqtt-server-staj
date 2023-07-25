using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StajUygulama.MQTT;
using StajUygulama.Forms;

namespace StajUygulama.Forms
{
    public partial class FormMqtt : Form
    {
        FormMain frm;
        public FormMqtt(FormMain formMain)
        {
            InitializeComponent();
            frm = formMain;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {

        }
    }
}
