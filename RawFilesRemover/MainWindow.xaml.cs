using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;

namespace RawFilesRemover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileRemover Remover;
        public MainWindow()
        {
            InitializeComponent();
            Remover = new FileRemover();
        }

        private void button_startDelete_Clicked(object sender, RoutedEventArgs e)
        {
            string deleteExtension = textbox_deleteName.Text.Trim();
            string compareExtension = textbox_compareName.Text.Trim();
            List<string> deleteFiles = Remover.GetFilesToRemove(deleteExtension, compareExtension);

            string message = "Es wurden " + deleteFiles.Count + " zum Löschen gefunden. Wirklich löschen?";
            string caption = "Löschen?";
            MessageBoxResult result = MessageBox.Show(this, message, caption, MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                Remover.DeleteFiles(deleteFiles);
            }
        }
    }
}
