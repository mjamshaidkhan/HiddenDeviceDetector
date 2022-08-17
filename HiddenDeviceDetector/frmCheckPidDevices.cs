using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiddenDeviceDetector
{


    public partial class frmCheckPidDevices : Form
    {

        private List<string> lstDevices = new List<string>();

        public frmCheckPidDevices()
        {
            InitializeComponent();

        }
        private void btnCheck_Click(object sender, EventArgs e)
        {
            SearchPIDDevices();
        }
        private void bwHiddenDeviceChecker_DoWork(object sender, DoWorkEventArgs e)
        {

            UnInstallHiddenDevices();

        }
        private void bwHiddenDeviceChecker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbChecking.Invoke(new MethodInvoker(() =>
            {
                pbChecking.Style = ProgressBarStyle.Continuous;
                pbChecking.MarqueeAnimationSpeed = 0;
                pbChecking.Visible = false;
            }));

            MessageBox.Show("All hidden devices has been unistalled", "Hidden Devices Uninstalled", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }

        private async void frmCheckPidDevices_Load(object sender, EventArgs e)
        {
            txtpdiamount.Text = Convert.ToString(ConfigurationManager.AppSettings["DefaultPIDAmountToCheck"]);
            // Start Searching hidden ports
            lblmessage.Text = ConstantUtil.SearchingMessage;
            await Task.Run(() => { return CountHiddenDevices(); });
            if (lstDevices.Count > Convert.ToInt32(ConfigurationManager.AppSettings["HiddenDeviceDeleteCount"]))
            {
                pbChecking.Visible = true;
                StartWorker(true);
            }
        }


        #region Private Functions

        private void StartWorker(object arg)
        {
            new Thread(delegate ()
            {
                pbChecking.Invoke(new MethodInvoker(() =>
                {
                    pbChecking.Style = ProgressBarStyle.Marquee;
                    pbChecking.MarqueeAnimationSpeed = 30;
                }));
                bwHiddenDeviceChecker.RunWorkerAsync(arg);
            }).Start();
        }
        private void SearchPIDDevices()
        {

            lblStatus.Invoke(new MethodInvoker(() =>
            {
                this.lblStatus.ForeColor = Color.Black;
                this.lblStatus.Text = "PENDING";
            }));

            new Thread(delegate ()
            {
                try
                {

                    Thread.Sleep(ConstantUtil.ThreadSleepInterval);
                    List<string> list = new List<string>();
                    int pidDevicesCount = 0;

                    string pid = ConfigurationManager.AppSettings["PID"];
                    long pidAmountToCheck = string.IsNullOrEmpty(txtpdiamount.Text) ? Convert.ToInt64(ConfigurationManager.AppSettings["DefaultPIDAmountToCheck"]) : Convert.ToInt64(txtpdiamount.Text);


                    foreach (ManagementObject obj2 in new ManagementClass("Win32_USBHub").GetInstances())
                    {
                        string str = obj2["deviceid"].ToString();
                        int index = str.IndexOf("PID_");
                        string item = str.Substring(index + 4).Substring(0, 4);
                        if (index > 0)
                            list.Add(item);
                        if (pid == item)
                        {
                            pidDevicesCount++;
                        }
                    }
                    if (pidDevicesCount >= pidAmountToCheck)
                    {
                        lblStatus.Invoke(new MethodInvoker(() => { this.lblStatus.ForeColor = Color.Green; }));
                        lblStatus.Invoke(new MethodInvoker(() => { this.lblStatus.Text = "PASSED"; }));

                    }
                    else
                    {

                        lblStatus.Invoke(new MethodInvoker(() => { this.lblStatus.ForeColor = Color.Red; }));
                        lblStatus.Invoke(new MethodInvoker(() => { this.lblStatus.Text = "FAIL"; }));
                    }
                    lblpidcount.Invoke(new MethodInvoker(() => { this.lblpidcount.Text = "(" + pidDevicesCount + ")"; }));
                }
                catch (Exception ex)
                {

                }
            }).Start();
        }
        private async Task CountHiddenDevices()
        {


            Collection<Object> hiddenDevices = PowerShellHelper.ExecuteString("Get-PnpDevice -class 'Ports'");
            foreach (PSObject obj in hiddenDevices)
            {

                var status = obj.Properties["Status"].Value;
                var name = Convert.ToString(obj.Properties["Name"].Value);
                if (status.ToString().ToLower().Contains("unknown"))
                {
                    lstDevices.Add(name);
                }
            }

            lblhiddendevicescount.Invoke(new MethodInvoker(() =>
            {
                lblhiddendevicescount.Text = "(" + lstDevices.Count + ")";
                lblhiddendevicescount.Invoke(new MethodInvoker(() => { lblhiddendevicescount.Visible = true; }));
            }));
            lblmessage.Invoke(new MethodInvoker(() =>
            {
                lblmessage.Text = ConstantUtil.TotalDevicesFoundMessage;
            }));



            await Task.Run(() => { return true; });


        }


        private void UnInstallHiddenDevices()
        {
            foreach (string device in lstDevices)
            {
                string d = @"foreach ($dev in (Get-PnpDevice | Where-Object{$_.Name  -eq '" + device + "'})) { &'pnputil' /remove-device $dev.InstanceId }";
                PowerShellHelper.ExecuteString(d);
            }
        }


        #endregion

        private void txtpdiamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }


    public class ConstantUtil
    {
        public static string SearchingMessage { get; } = "Searching hidden devices..";
        public static string TotalDevicesFoundMessage { get; } = "Total hidden devices detected:";
        public static int ThreadSleepInterval { get; } = 1000;

    }


}
