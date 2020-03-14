using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            var dummy = Application.Current.Resources["key"];
            return dummy.ToString();
        }
    }


}
