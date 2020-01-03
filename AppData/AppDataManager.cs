using AudioPlayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AudioPlayer.AppData
{
    static class AppDataManager
    {
        public static void SaveSongs(string path, ObservableCollection<Song> songs)
        {
            using (var fs = File.Create(path))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, songs);
            }
        }
        public static ObservableCollection<Song> LoadSongs(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var bf = new BinaryFormatter();
                return (ObservableCollection<Song>)bf.Deserialize(fs);
            }
        }
    }
}
