using AudioPlayer.AppData;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Helpers
{
    static class Helper
    {
        static Random rnd = new Random();

        public static TimeSpan GetSongDuration(string file)
        {
            using (var mediaReader = new MediaFoundationReader(file))
            {
                return mediaReader.TotalTime;
            }
        }
        public static bool IsAudio(string file)
        {
            using (var mediaReader = new MediaFoundationReader(file))
            {
                return mediaReader.CanRead;
            }
        }
        public static string GetRandomImage()
        {
            var images = Directory.GetFiles($"{AppFiles.ImagesPath}\\");
            return images[rnd.Next(images.Length)];
        }
    }
}
