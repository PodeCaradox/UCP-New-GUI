using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UCP.Patching;
using UCP.Structs;
using static UCP.Utility;

namespace UCP.Data
{
    public partial class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            languages = Enum.GetValues(typeof(Languages)).Cast<Languages>().ToArray();
        }

        public void LoadUCPData()
        {

        }

        private Dictionary<string, object> _xamlObjects = new Dictionary<string, object>();

        private Languages actualLanguage = Languages.English;
        public Languages ActualLanguage
        {

            get => actualLanguage;
            set
            {
                actualLanguage = value;
                this.RaisePropertyChanged("ActualLanguage");
                SelectCulture(value);
                Configuration.Language = (int)value;
                Configuration.Save("Language");

                //update Lists
                this.RaisePropertyChanged("ImageResourceKey");
            }
        }

        internal void AddXamlObjects(StackPanel mainStackPanel)
        {
            foreach (var item in mainStackPanel.Children)
            {
                if (item is TreeViewItem)
                {
                    var treeViewItem = item as TreeViewItem;
                    var frameworkElement = treeViewItem.Header as FrameworkElement;
                    if(frameworkElement != null &&!String.IsNullOrEmpty( frameworkElement.Name))
                    {
                        _xamlObjects.Add((treeViewItem.Header as FrameworkElement).Name, treeViewItem.Header);
                        if (treeViewItem.Items.Count > 1)
                        {
                            foreach (var treeViewChild in treeViewItem.Items)
                            {
                                if (treeViewChild.GetType() == typeof(Slider))
                                {
                                    Slider slider = treeViewChild as Slider;
                                    _xamlObjects.Add(slider.Name, slider);
                                    break;
                                }
                                //ColorBrush todo
                                //else if (treeViewChild.GetType() == typeof(Slider))
                                //{

                                //}
                            }
                        }
                    }
                    else
                    {
                        Debug.Error("Framework element not found, check if names are Set");
                    }
                }
            }
        }

        private readonly Languages[] languages;

        internal void LoadConfigData()
        {
            var config = SaveConfig();
            for (int i = 0; i < config.Configs.Length; i++)
            {
                object xamlObject;
                if (_xamlObjects.TryGetValue(config.Configs[i].XamlObjectName, out xamlObject)) 
                {
                    if (config.Configs[i].ObjectType == typeof(CheckBox))
                    {
                        var checkbox = xamlObject as CheckBox;
                        checkbox.IsChecked = (bool)config.Configs[i].ObjectValue;
                    }
                    else if (config.Configs[i].ObjectType == typeof(Slider))
                    {
                        var slider = xamlObject as Slider;
                        slider.Value = (double)config.Configs[i].ObjectValue;
                    }//todo ColorBlock
                    else if (config.Configs[i].ObjectType == typeof(TextBlock))
                    {

                    }

                }

            }
        }

        #region Debug
        private ConfigFile SaveConfig()
        {
            ConfigFile config = new ConfigFile();
            config.Language = ActualLanguage;
            config.StrongholdPath = StrongholdPath;
            config.Configs = new SavedConfig[2];
            config.Configs[0] = new SavedConfig() { XamlObjectName = "Dummy", ObjectValue = true, ObjectType = typeof(CheckBox) };
            config.Configs[1] = new SavedConfig() { XamlObjectName = "DummySlider", ObjectValue = 50d, ObjectType = typeof(Slider) };
            return config;
        }
        #endregion


        public Languages[] LanguagesComboBox
        {
            get => languages;
        }
        

        private String imageResourceKey = "ui_Bugfix";
        public String ImageResourceKey
        {

            get => imageResourceKey;
            set
            {
                imageResourceKey = value;
                this.RaisePropertyChanged("ImageResourceKey");
            }
        }
        private String strongholdPath;
        public String StrongholdPath
        {

            get => strongholdPath;
            set
            {
                strongholdPath = value;
                this.RaisePropertyChanged("StrongholdPath");
            }
        }

    }
}
