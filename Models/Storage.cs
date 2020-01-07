using AudioPlayer.AppData;
using AudioPlayer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Models
{
    static class Storage
    {
        public static ObservableCollection<Song> GetSongs()
        {
            var songs = new ObservableCollection<Song>();

            songs.Add(new Song
            {
                SongPath = $"{AppFiles.StandartSongsPath}\\Abba - Happy new year.mp3",
                Author = "Abba",
                Name = "Happy new year"
            });
            songs.Add(new Song
            {
                SongPath = $"{AppFiles.StandartSongsPath}\\Gladiator - Now We Are Free.mp3",
                Author = "Gladiator",
                Name = "Now We Are Free"
            });
            songs.Add(new Song
            {
                SongPath = $"{AppFiles.StandartSongsPath}\\Sting - Shape of my heart.mp3",
                Author = "Sting",
                Name = "Shape of my heart"
            });
            songs.Add(new Song
            {
                SongPath = $"{AppFiles.StandartSongsPath}\\Pascal Letoublon - Friendships.mp3",
                Author = "Pascal Letoublon",
                Name = "Friendships"
            });
            songs.Add(new Song
            {
                SongPath = $"{AppFiles.StandartSongsPath}\\Jingle bells.mp3",
                Name = "Jingle bells"
            });
            songs.Add(new Song
            {
                SongPath = $"{AppFiles.StandartSongsPath}\\Professional ost.mp3",
                Name = "Professional ost"
            });

            for (int i = 0; i < songs.Count; i++)
            {
                songs[i].Duration = Helper.GetSongDuration(songs[i].SongPath);
            }

            return songs;
        }
    }
}
