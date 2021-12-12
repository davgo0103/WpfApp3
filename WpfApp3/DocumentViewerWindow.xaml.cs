using Microsoft.Win32;
using System;
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

namespace WpfApp3
{
    /// <summary>
    /// DocumentViewerWindow.xaml 的互動邏輯
    /// </summary>
    public partial class DocumentViewerWindow : Window
    {
        public DocumentViewerWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 32, 64, 72, 96 };
        }

        private void Open_Executed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text Format|*.rtf|All Files|*.*";
            if(openFileDialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf);
            }
        }

        private void Save_Executed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Rich Text Format|*.rtf|All Files|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Rtf);
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFontFamily.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontFamilyProperty,cmbFontFamily.SelectedItem.ToString());
            }
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbFontSize.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.SelectedItem.ToString());
            }
        }

        private void Open_New(object sender, RoutedEventArgs e)
        {
            TextBOX.Text = "";
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
