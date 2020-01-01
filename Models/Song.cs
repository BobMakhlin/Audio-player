using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Models
{
    [Serializable]
    class Song
    {
        public string Path { get; set; }
        public string Name { get; set; } = "Unknown";
        public string Author { get; set; } = "Unknown";
        public TimeSpan Duration { get; set; }
    }
}
