using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayer.Models;
using AudioPlayer.ViewModels;
using AudioPlayer.Views;

namespace AudioPlayer.WindowServices
{
    class SongWindowService : ISongWindowService
    {
        public Song Song { get; set; }

        public void ShowWindow()
        {
            if (Song == null)
            {
                throw new ArgumentNullException("Song");
            }

            var viewModel = new EditSongViewModel(Song);
            var view = new EditSongWindow
            {
                DataContext = viewModel
            };
            view.ShowDialog();
        }
    }
}
