﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UCP.Views
{
    /// <summary>
    /// Interaktionslogik für PathSelection.xaml
    /// </summary>
    public partial class PathSelection : Window
    {
        public PathSelection()
        {
            InitializeComponent();
            
        }

        void bPathSearch_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.ShowNewFolderButton = false;
                dialog.SelectedPath = Directory.Exists(pTextBoxPath.Text) ? pTextBoxPath.Text : null;
                dialog.Description = "Bitte wähle dein Stronghold Crusader - Installationsverzeichnis.";

                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                    pTextBoxPath.Text = dialog.SelectedPath;
            }
        }
    }
}
