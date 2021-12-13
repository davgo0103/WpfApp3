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
        Color currentStrokeColor;
        Brush currentStrokeBrush = new SolidColorBrush(Colors.Black);
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
                L1.Content = $"檔名:{fs.Name}";
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
        private Color GetDialogColor()
        {

            System.Windows.Forms.ColorDialog dlg = new System.Windows.Forms.ColorDialog();
            dlg.ShowDialog();

            System.Drawing.Color dlgColor = dlg.Color;
            return Color.FromArgb(dlgColor.A, dlgColor.R, dlgColor.G, dlgColor.B);
        }
        private void FontColor_Button_Click(object sender, RoutedEventArgs e)
        {
            currentStrokeColor = GetDialogColor();
            currentStrokeBrush = new SolidColorBrush(currentStrokeColor);
            Font_color.Background = currentStrokeBrush;
            rtbEditor.Foreground = currentStrokeBrush;
        }

        private void Font_BackGround_Click(object sender, RoutedEventArgs e)
        {
            currentStrokeColor = GetDialogColor();
            currentStrokeBrush = new SolidColorBrush(currentStrokeColor);
            Font_BackGround.Background = currentStrokeBrush;
            rtbEditor.Background = currentStrokeBrush;
        }
 
        private void TextBOX_DataContextChanged(object sender, KeyEventArgs e)
        {
            L2.Content = $"字數:{TextBOX.Text.Length}";
        }
    }
}
