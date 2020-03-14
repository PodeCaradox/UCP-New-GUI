using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UCP.Patching;

namespace UCP
{
    public static class Utility
    {
        public enum Languages { English, Deutsch, Русский, Polski };
        public static void SelectCulture(Languages language)
        {
            var dictionaryList = Application.Current.Resources.MergedDictionaries;

            object dummy;
            var resourceDictionary = dictionaryList.FirstOrDefault(d => d.Contains(language.ToString()) == true);
            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }

        public static String GetText(String key)
        {
            var dummy = Application.Current.Resources[key];
            if (dummy == null) return "";
            return dummy.ToString();
        }

        public static String CheckCrusaderPath()
        {
            if (!Directory.Exists(Configuration.Path))
            {
                // check if we can already find the steam path
                const string key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 40970";
                RegistryKey myKey = Registry.LocalMachine.OpenSubKey(key, false);
                if (myKey != null && myKey.GetValue("InstallLocation") is string path
                    && !string.IsNullOrWhiteSpace(path) && Patcher.CrusaderExists(path))
                {
                    return path;
                }
                else if (Patcher.CrusaderExists(Environment.CurrentDirectory))
                {
                    return Environment.CurrentDirectory;
                }
            }

            return Configuration.Path;

        }
    }


}
