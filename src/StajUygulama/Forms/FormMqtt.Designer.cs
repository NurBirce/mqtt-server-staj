
namespace StajUygulama.Forms
{
    partial class FormMqtt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSubscribe = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSubscribe
            // 
            this.txtSubscribe.Location = new System.Drawing.Point(39, 34);
            this.txtSubscribe.Name = "txtSubscribe";
            this.txtSubscribe.Size = new System.Drawing.Size(700, 20);
            this.txtSubscribe.TabIndex = 0;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSendMessage.Location = new System.Drawing.Point(580, 82);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(159, 38);
            this.btnSendMessage.TabIndex = 1;
            this.btnSendMessage.Text = "Subscribe";
            this.btnSendMessage.UseVisualStyleBackColor = false;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // FormMqtt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtSubscribe);
            this.Name = "FormMqtt";
            this.Text = "FormMqtt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSubscribe;
        private System.Windows.Forms.Button btnSendMessage;
    }
}