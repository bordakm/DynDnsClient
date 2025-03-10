using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynDns_Client
{
    static class RegistryUtilities
    {
        public const string TimerOn = "timerOn";
        public const string Domain = "domain";
        public const string Password = "password";
        public const string SavePassword = "savePassword";
        public const string Interval = "interval";
        public const string StartWithWindows = "startWithWindows";
        public static void WriteRegistry(string key, string value)
        {
            //RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\DynDnsClient", true);
            RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DynDnsClient", true);
            if(registryKey == null)
            {
                registryKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\DynDnsClient", true);
            }
            //key.SetValue("Start", 2, RegistryValueKind.DWord);
            registryKey.SetValue(key, value);
        }

        public static string? ReadRegistry(string key)
        {
            //object result = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\DynDnsClient", key, null);
            object? result = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\DynDnsClient", key, null);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result.ToString();
            }
        }
    }
}
