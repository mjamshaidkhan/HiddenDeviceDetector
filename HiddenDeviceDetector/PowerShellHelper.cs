using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections.ObjectModel;   
using System.Management.Automation;     
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
        static NLog.Logger _logger;
        public PowerShellHelper()
        {

        }
        public static Collection<Object> ExecuteString(string command)  //, IDictionary parameters
        {
            _logger = NLog.LogManager.GetLogger("");
            Collection<Object> results = null;
            string error = "";
            var ss = InitialSessionState.CreateDefault();
            using (var ps = PowerShell.Create(ss))
            {
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
                    _logger.Trace("Results found." + result.ToString());
                }
            }

            return results;
        }
    }
}