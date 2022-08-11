using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace HiddenDeviceDetector
{

    public partial class frmhiddendevices : Form
    {

        Logger _logger;
        public frmhiddendevices()
        {
            InitializeComponent();
            _logger = LogManager.GetLogger("");
        }
        private void btnRunDetector_Click(object sender, EventArgs e)
        {
            lstDevices.Clear();
            SearchHiddenDevices();

        }


        public void SearchHiddenDevices()
        {
            _logger.Info("geting hidden devices..");
            Collection<Object> hiddenDevices = PowerShellHelper.ExecuteString("Get-PnpDevice -class 'Keyboard'");
            foreach (PSObject obj in hiddenDevices)
            {
                var status = obj.Properties["Status"].Value;
                var name = Convert.ToString(obj.Properties["Name"].Value);
                if (status.ToString().ToLower().Contains("unknown"))
                {
                    lstDevices.Items.Add(name);
                }
            }
            lblcount.Text = "(" + lstDevices.Items.Count + ")";

            MessageBox.Show("All hidden devices has been loaded", "Hidden Devices Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to uninstall all hidden devices ??", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _logger.Info("Uninstall started...");
                // foreach (ListViewItem device in lstDevices.Items)
                {
                    string selectedDevice = lstDevices.SelectedItems[0].Text;
                    string d = @"foreach ($dev in (Get-PnpDevice | Where-Object{$_.Name  -eq '" + selectedDevice + "'})) { &'pnputil' /remove-device $dev.InstanceId }";
                    PowerShellHelper.ExecuteString(d);
                }
                _logger.Info("Uninstall success...");
                MessageBox.Show("All hidden devices has been unistalled", "Hidden Devices Uninstalled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void lstDevices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
