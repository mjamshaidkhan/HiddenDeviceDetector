using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
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
        public void SearchHiddenDevices()
        {
            try
            {
                lstDevices.Clear();
                _logger.Info("geting hidden devices..");
                Collection<Object> hiddenDevices = PowerShellHelper.ExecuteString("Get-PnpDevice -class 'Ports'");
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
                if (lstDevices.Items.Count < 1)
                {
                    MessageBox.Show("No hidden device found.", "not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("All hidden devices has been loaded", "Hidden Devices Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                MessageBox.Show("Unable to get the hidden devices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            
            if (lstDevices.Items.Count < 1)
            {
                MessageBox.Show("There is no device found to uninstall.", "not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure to delete all hidden devices ??", "Confirm Delete!!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                btnUninstall.Enabled = false;
                _logger.Info("Uninstall started...");
                foreach (ListViewItem device in lstDevices.Items)
                {
                    string d = @"foreach ($dev in (Get-PnpDevice | Where-Object{$_.Name  -eq '" + device.Text + "'})) { &'pnputil' /remove-device $dev.InstanceId }";
                    PowerShellHelper.ExecuteString(d);
                }
                _logger.Info("Uninstall success...");
                MessageBox.Show("All hidden devices has been unistalled", "Hidden Devices Uninstalled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUninstall.Enabled = true;
                SearchHiddenDevices();
            }

        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            SearchHiddenDevices();
        }
    }
}
