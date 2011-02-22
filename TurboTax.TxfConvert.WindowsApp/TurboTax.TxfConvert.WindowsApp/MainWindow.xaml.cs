using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TurboTax.TxfConvert.WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertToTXF_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextboxCsvFile.Text)
                && !string.IsNullOrEmpty(TextboxOutputDirectory.Text))
            {
                try
                {
                    TXFConverter txf = new TXFConverter(TextboxCsvFile.Text, TextboxOutputDirectory.Text);
                    txf.Execute();

                    if (!string.IsNullOrEmpty(txf.OutputFile))
                    {
                        MessageBox.Show(string.Concat("File operation complete! File saved at: ", txf.OutputFile));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
        }
    }
}
