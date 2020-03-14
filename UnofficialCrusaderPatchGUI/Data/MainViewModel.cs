using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Languages actualLanguage = Languages.English;
        public Languages ActualLanguage
        {

            get => actualLanguage;
            set
            {
                actualLanguage = value;
                this.RaisePropertyChanged("ActualLanguage");
                SelectCulture(value);
            }
        }

        private readonly Languages[] languages;
        public Languages[] LanguagesComboBox
        {
            get => languages;
        }

    }
}
