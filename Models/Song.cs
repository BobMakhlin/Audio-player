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
    class Song
    {
        private string imagePath = Helper.GetRandomImage();
        public string ImagePath 
        { 
            get => $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\{imagePath}"; 
            set => imagePath = value; 
        }

        public string SongPath { get; set; }
        public string Name { get; set; } = "Unknown";
        public string Author { get; set; } = "Unknown";
        public TimeSpan Duration { get; set; }
    }
}
