using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections.ObjectModel;   //Used for CollectionSystem.Collections.ObjectModel.Collection
using System.Management.Automation;     //Used for all automation related functionality. Also has System.Management.Automation.PSObject.
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Xml.Linq;

namespace HiddenDeviceDetector
{
    public enum ResultType
    {
        PSObjectCollection = 0,
        DataTable = 1
    }

    public class PowerShellHelper
    {
     static   NLog.Logger _logger;
        public PowerShellHelper()
        {
           
        }

        //This works but only returns standard output as text and not an object but will still work to invoke full-fledged scripts
        //Object invokedResults = PowerShellHelper.InvokePowerShellScript(@"C:\MyDir\TestPoShScript.ps1");
        public static Object InvokePowerShellScript(string scriptPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process process = new Process();
            Object returnValue = null;

            startInfo.FileName = @"powershell.exe";
            startInfo.Arguments = (@"& 'PATH'").Replace("PATH", scriptPath);
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            process = new Process { StartInfo = startInfo, EnableRaisingEvents = true };

            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate (object sender, DataReceivedEventArgs e)
                {
                    //For some e.Data always has an empty string
                    returnValue = e.Data;
                    //using (StreamReader output = process.StandardOutput)
                    //{
                    //    standardOutput = output.ReadToEnd();
                    //}
                }
            );
            process.Start();
            //process.BeginOutputReadLine();  //This is starts reading the return value by invoking OutputDataReceived event handler
            process.WaitForExit();

            Object standardOutput = process.StandardOutput.ReadToEnd();
            //Assert.IsTrue(output.Contains("StringToBeVerifiedInAUnitTest"));

            String errors = process.StandardError.ReadToEnd();
            //Assert.IsTrue(string.IsNullOrEmpty(errors));

            process.Close();

            //For some reason returnValue does not have the object type output
            //return returnValue;
            return standardOutput;
        }

        //IDictionary parameters = new Dictionary<String, String>();
        //parameters.AddUser("Identity", "My-AD-Group-Name");
        //Collection<Object> results = PowerShellHelper.Execute(textBoxCommand.Text);
        //DataTable dataTable = PowerShellHelper.ToDataTable(results);
        public static Collection<Object> ExecuteString(string command)  //, IDictionary parameters
        {
            _logger = NLog.LogManager.GetLogger("");
            Collection<Object> results = null;
            string error = "";
            var ss = InitialSessionState.CreateDefault();
            //ss.ImportPSModulesFromPath("C:\\WINDOWS\\system32\\WindowsPowerShell\\v1.0\\Modules\\ActiveDirectory\\ActiveDirectory.psd1");
            //ss.ImportPSModulesFromPath(@"C:\WINDOWS\system32\WindowsPowerShell\v1.0\Modules\dbatools\dbatools.psm1");
            //ss.ImportPSModule(new[] { "ActiveDirectory", "dbatools" });

            using (var ps = PowerShell.Create(ss))
            {

                //http://www.agilepointnxblog.com/powershell-error-there-is-no-runspace-available-to-run-scripts-in-this-thread/
                // Exception getting "Path": "There is no Runspace available to run scripts in this thread. You can provide one in the DefaultRunspace property of the System.Management.Automation.Runspaces.Runspace type. The script block you attempted to invoke was: $this.Mainmodule.FileName"
                //
                //ps.AddCommand("[System.Net.ServicePointManager]::ServerCertificateValidationCallback = $null").Invoke();
                //var rslt = ps.AddCommand("Import-Module").AddParameter("Name", "ActiveDirectory").Invoke();
                //rslt = ps.AddCommand("Import-Module").AddParameter("Name", "dbatools").Invoke();
                //rslt = ps.AddCommand("Import-Module").AddParameter("Name", "sqlps").Invoke();

                ps.AddCommand("Set-ExecutionPolicy")
          .AddParameter("Scope", "Process")
          .AddParameter("ExecutionPolicy", "Bypass")
          .Invoke();



                if (ps.HadErrors)
                {
                    _logger.Error("Command has invoked error");
                    System.Collections.ArrayList errorList = (System.Collections.ArrayList)ps.Runspace.SessionStateProxy.PSVariable.GetValue("Error");
                    error = string.Join("\n", errorList.ToArray());
                    throw new Exception(error);
                }

                ps.Commands.Clear();

                PSInvocationSettings settings = new PSInvocationSettings();
                settings.ErrorActionPreference = ActionPreference.Stop;


                //results = ps.AddCommand(command).AddParameters(parameters).Invoke<PSObject>();
                //results = ps.AddCommand(command).AddParameters(parameters).Invoke<Object>();
                results = ps.AddScript(command).Invoke<Object>();

                if (ps.HadErrors == true)
                {
                    _logger.Error("Command has invoked error 2");

                    System.Collections.ArrayList errorList = (System.Collections.ArrayList)ps.Runspace.SessionStateProxy.PSVariable.GetValue("Error");
                    error = string.Join("\n", errorList.ToArray());
                    throw new Exception(error);
                }

                foreach (var result in results)
                {
                    //Debug.WriteLine(result.ToString());
                    _logger.Trace("Results found." + result.ToString());
                }
            }

            return results;
        }


        public static DataTable ToDataTable(Collection<object> data)
        {
            DataTable table = new DataTable();

            if ((data != null) && (data.Count > 0))
            {
                PSObject psObject = (PSObject)data[0];

                foreach (var property in psObject.Properties)
                {
                    System.Diagnostics.Debug.Print(property.Name + " - " + property.TypeNameOfValue);
                }

                foreach (var property in psObject.Properties)
                {
                    System.Diagnostics.Debug.Print(property.Name);

                    //If these are types unknown (beyond basic types or referenced classes), then create a String type
                    if ((Type.GetType(property.TypeNameOfValue) != null) &&
                            (!property.TypeNameOfValue.StartsWith("System.Nullable")))
                    {
                        table.Columns.Add(property.Name, Type.GetType(property.TypeNameOfValue));
                    }
                    else
                    {
                        table.Columns.Add(property.Name, Type.GetType("System.String"));
                    }
                }

                object[] values = new object[table.Columns.Count];
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(data[0]);

                foreach (Object item in data)
                {
                    DataRow row = table.NewRow();

                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item);
                    }

                    table.Rows.Add(row);
                }
            }

            return table;
        }


    }
}