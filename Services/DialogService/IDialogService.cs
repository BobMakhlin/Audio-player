using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.WindowServices
{
    interface IDialogService
    {
        void ShowMessage(string message);

        string Path { get; set; }
        bool OpenFileDialog();
    }
}
