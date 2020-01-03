using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Helpers
{
    class FileFormat
    {
        public static bool IsAudio(string file)
        {
            using (var mediaReader = new MediaFoundationReader(file))
            {
                return mediaReader.CanRead;
            }
        }
        public static bool IsImage(string file)
        {
            try
            {
                using (var img = new Bitmap(file)) { }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
