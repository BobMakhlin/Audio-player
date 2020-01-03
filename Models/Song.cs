using AudioPlayer.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Models
{
    [Serializable]
    class Song : INotifyPropertyChanged
    {
        private string imagePath = Helper.GetRandomImage();

        public string ImagePath
        {
            get => $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\{imagePath}";
            set
            {
                imagePath = value;
                INotifyPropertyChanged();
            }
        }

        public string SongPath { get; set; }

        private string name = "Unknown";
        public string Name
        {
            get => name;
            set
            {
                name = value;
                INotifyPropertyChanged();
            }
        }

        private string author = "Unknown";
        public string Author 
        {
            get => author;
            set
            {
                author = value;
                INotifyPropertyChanged();
            }
        }

        public TimeSpan Duration { get; set; }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        void INotifyPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
