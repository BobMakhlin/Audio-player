using AudioPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace AudioPlayer.ViewModels
{
    class EditSongViewModel
    {
        public Song Song { get; set; }

        public EditSongViewModel(Song song)
        {
            Song = song;
        }
    }
}
