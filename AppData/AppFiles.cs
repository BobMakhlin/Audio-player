using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.AppData
{
    static class AppFiles
    {
        public static string Filename => "AppData\\songs.bin";

        public static string StandartSongsPath => "AppData\\Songs\\Standart";
        public static string CustomSongsPath => "AppData\\Songs\\Custom";

        public static string StandartImagesPath => "AppData\\Images\\Standart";
        public static string CustomImagesPath => "AppData\\Images\\Custom";
    }
}
