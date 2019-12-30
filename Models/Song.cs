using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Models
{
    class Song
    {
        public string Path { get; set; }
        public string Name { get; set; } = "Unknown";
        public string Author { get; set; } = "Unknown";
        public TimeSpan Duration { get; set; }
    }
}
