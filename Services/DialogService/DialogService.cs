using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AudioPlayer.WindowServices
{
    class DialogService : IDialogService
    {
        public string Path { get; set; }

        public bool OpenFileDialog()
        {
            var dialog = new OpenFileDialog();

            if(dialog.ShowDialog().Value)
            {
                Path = dialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
