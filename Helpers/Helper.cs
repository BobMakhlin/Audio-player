using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Helpers
{
    static class Helper
    {
        public static TimeSpan GetSongDuration(string file)
        {
            using (var mediaReader = new MediaFoundationReader(file))
            {
                return mediaReader.TotalTime;
            }
        }
    }
}
