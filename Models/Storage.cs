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
                Path = "Songs\\Abba - Happy new year.mp3",
                Name = "Happy new year"
            });
            songs.Add(new Song
            {
                Path = "Songs\\Gladiator - Now We Are Free.mp3",
                Name = "Now We Are Free"
            });
            songs.Add(new Song
            {
                Path = "Songs\\Sting - Shape of my heart.mp3",
                Name = "Shape of my heart"
            });
            songs.Add(new Song
            {
                Path = "Songs\\Pascal Letoublon - Friendships.mp3",
                Name = "Friendships"
            });
            songs.Add(new Song
            {
                Path = "Songs\\Jingle bells.mp3",
                Name = "Jingle bells"
            });
            songs.Add(new Song
            {
                Path = "Songs\\Professional ost.mp3",
                Name = "Professional ost"
            });

            for (int i = 0; i < songs.Count; i++)
            {
                songs[i].Duration = Helper.GetSongDuration(songs[i].Path);
            }

            return songs;
        }
    }
}
