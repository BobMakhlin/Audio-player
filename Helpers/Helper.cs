﻿using AudioPlayer.AppData;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public static string GetRandomImage()
        {
            var images = Directory.GetFiles($"{AppFiles.ImagesPath}\\");
            return images[rnd.Next(images.Length)];
        }
        public static string CopyToImagesDir(string file)
        {
            var filename = Path.GetFileNameWithoutExtension(file);
            var extension = Path.GetExtension(file);
            var resultFile = $"{AppFiles.ImagesPath}\\{filename}-{Guid.NewGuid()}{extension}";
            File.Copy(file, resultFile);

            return resultFile;
        }
        public static string CopyToSongsDir(string file)
        {
            var filename = Path.GetFileNameWithoutExtension(file);
            var extension = Path.GetExtension(file);
            var resultFile = $"{AppFiles.SongsPath}\\{filename}-{Guid.NewGuid()}{extension}";
            File.Copy(file, resultFile);

            return resultFile;
        }
    }
}
