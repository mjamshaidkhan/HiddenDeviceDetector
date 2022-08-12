
namespace HiddenDeviceDetector
{
    partial class frmhiddendevices
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
            this.btnRun = new System.Windows.Forms.Button();
            this.lstDevices = new System.Windows.Forms.ListView();
            this.PortName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDevicesHeaderText = new System.Windows.Forms.Label();
            this.lblTotalDevices = new System.Windows.Forms.Label();
            this.lblcount = new System.Windows.Forms.Label();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(15, 51);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(132, 86);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lstDevices
            // 
            this.lstDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PortName});
            this.lstDevices.HideSelection = false;
            this.lstDevices.Location = new System.Drawing.Point(12, 212);
            this.lstDevices.Name = "lstDevices";
            this.lstDevices.Size = new System.Drawing.Size(776, 226);
            this.lstDevices.TabIndex = 1;
            this.lstDevices.UseCompatibleStateImageBehavior = false;
            this.lstDevices.View = System.Windows.Forms.View.List;
            // 
            // PortName
            // 
            this.PortName.Width = 1000;
            // 
            // lblDevicesHeaderText
            // 
            this.lblDevicesHeaderText.Location = new System.Drawing.Point(12, 186);
            this.lblDevicesHeaderText.Name = "lblDevicesHeaderText";
            this.lblDevicesHeaderText.Size = new System.Drawing.Size(254, 23);
            this.lblDevicesHeaderText.TabIndex = 2;
            this.lblDevicesHeaderText.Text = "Hidden Devices list";
            // 
            // lblTotalDevices
            // 
            this.lblTotalDevices.Location = new System.Drawing.Point(525, 186);
            this.lblTotalDevices.Name = "lblTotalDevices";
            this.lblTotalDevices.Size = new System.Drawing.Size(156, 23);
            this.lblTotalDevices.TabIndex = 3;
            this.lblTotalDevices.Text = "Total Hidden Devices Count:";
            // 
            // lblcount
            // 
            this.lblcount.Location = new System.Drawing.Point(678, 186);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(111, 23);
            this.lblcount.TabIndex = 4;
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(621, 51);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(132, 86);
            this.btnUninstall.TabIndex = 5;
            this.btnUninstall.Text = "un-install";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // frmhiddendevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.lblcount);
            this.Controls.Add(this.lblTotalDevices);
            this.Controls.Add(this.lblDevicesHeaderText);
            this.Controls.Add(this.lstDevices);
            this.Controls.Add(this.btnRun);
            this.MaximizeBox = false;
            this.Name = "frmhiddendevices";
            this.Text = "Hidden Devices Detector";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.ListView lstDevices;
        private System.Windows.Forms.Label lblDevicesHeaderText;
        private System.Windows.Forms.Label lblTotalDevices;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.ColumnHeader PortName;
        private System.Windows.Forms.Button btnUninstall;
    }
}

