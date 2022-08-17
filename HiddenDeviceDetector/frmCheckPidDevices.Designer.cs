
namespace HiddenDeviceDetector
{
    partial class frmCheckPidDevices
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
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtpdiamount = new System.Windows.Forms.TextBox();
            this.lblpidamoutmessage = new System.Windows.Forms.Label();
            this.lblpidcounttext = new System.Windows.Forms.Label();
            this.lblpidcount = new System.Windows.Forms.Label();
            this.lbldivider = new System.Windows.Forms.Label();
            this.lblmessage = new System.Windows.Forms.Label();
            this.lblhiddendevicescount = new System.Windows.Forms.Label();
            this.bwHiddenDeviceChecker = new System.ComponentModel.BackgroundWorker();
            this.pbChecking = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // btnCheck
            // 
            this.btnCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Location = new System.Drawing.Point(458, 142);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(113, 22);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(245, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(326, 97);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtpdiamount
            // 
            this.txtpdiamount.Location = new System.Drawing.Point(245, 142);
            this.txtpdiamount.MaxLength = 15;
            this.txtpdiamount.Name = "txtpdiamount";
            this.txtpdiamount.Size = new System.Drawing.Size(197, 20);
            this.txtpdiamount.TabIndex = 2;
            this.txtpdiamount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpdiamount_KeyPress);
            // 
            // lblpidamoutmessage
            // 
            this.lblpidamoutmessage.AutoSize = true;
            this.lblpidamoutmessage.Location = new System.Drawing.Point(242, 116);
            this.lblpidamoutmessage.Name = "lblpidamoutmessage";
            this.lblpidamoutmessage.Size = new System.Drawing.Size(260, 13);
            this.lblpidamoutmessage.TabIndex = 3;
            this.lblpidamoutmessage.Text = "Enter an amount of PID (6001) you want to check for:";
            // 
            // lblpidcounttext
            // 
            this.lblpidcounttext.AutoSize = true;
            this.lblpidcounttext.Location = new System.Drawing.Point(242, 180);
            this.lblpidcounttext.Name = "lblpidcounttext";
            this.lblpidcounttext.Size = new System.Drawing.Size(112, 13);
            this.lblpidcounttext.TabIndex = 4;
            this.lblpidcounttext.Text = "Total PID 6001 found:";
            // 
            // lblpidcount
            // 
            this.lblpidcount.AutoSize = true;
            this.lblpidcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpidcount.Location = new System.Drawing.Point(367, 180);
            this.lblpidcount.Name = "lblpidcount";
            this.lblpidcount.Size = new System.Drawing.Size(22, 13);
            this.lblpidcount.TabIndex = 5;
            this.lblpidcount.Text = "(0)";
            // 
            // lbldivider
            // 
            this.lbldivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbldivider.Location = new System.Drawing.Point(-3, 232);
            this.lbldivider.Name = "lbldivider";
            this.lbldivider.Size = new System.Drawing.Size(811, 10);
            this.lbldivider.TabIndex = 6;
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.Location = new System.Drawing.Point(12, 259);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(0, 13);
            this.lblmessage.TabIndex = 7;
            // 
            // lblhiddendevicescount
            // 
            this.lblhiddendevicescount.AutoSize = true;
            this.lblhiddendevicescount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhiddendevicescount.Location = new System.Drawing.Point(169, 259);
            this.lblhiddendevicescount.Name = "lblhiddendevicescount";
            this.lblhiddendevicescount.Size = new System.Drawing.Size(22, 13);
            this.lblhiddendevicescount.TabIndex = 8;
            this.lblhiddendevicescount.Text = "(0)";
            this.lblhiddendevicescount.Visible = false;
            // 
            // bwHiddenDeviceChecker
            // 
            this.bwHiddenDeviceChecker.WorkerReportsProgress = true;
            this.bwHiddenDeviceChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwHiddenDeviceChecker_DoWork);
            this.bwHiddenDeviceChecker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwHiddenDeviceChecker_RunWorkerCompleted);
            // 
            // pbChecking
            // 
            this.pbChecking.Location = new System.Drawing.Point(15, 287);
            this.pbChecking.Name = "pbChecking";
            this.pbChecking.Size = new System.Drawing.Size(773, 23);
            this.pbChecking.TabIndex = 9;
            this.pbChecking.Visible = false;
            // 
            // frmCheckPidDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbChecking);
            this.Controls.Add(this.lblhiddendevicescount);
            this.Controls.Add(this.lblmessage);
            this.Controls.Add(this.lbldivider);
            this.Controls.Add(this.lblpidcount);
            this.Controls.Add(this.lblpidcounttext);
            this.Controls.Add(this.lblpidamoutmessage);
            this.Controls.Add(this.txtpdiamount);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCheck);
            this.MaximizeBox = false;
            this.Name = "frmCheckPidDevices";
            this.Text = "Check PID Devices";
            this.Load += new System.EventHandler(this.frmCheckPidDevices_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtpdiamount;
        private System.Windows.Forms.Label lblpidamoutmessage;
        private System.Windows.Forms.Label lblpidcounttext;
        private System.Windows.Forms.Label lblpidcount;
        private System.Windows.Forms.Label lbldivider;
        private System.Windows.Forms.Label lblmessage;
        private System.Windows.Forms.Label lblhiddendevicescount;
        private System.ComponentModel.BackgroundWorker bwHiddenDeviceChecker;
        private System.Windows.Forms.ProgressBar pbChecking;
    }
}