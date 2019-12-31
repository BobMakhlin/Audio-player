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
    class WindowService : IWindowService
    {
        public void ShowDialog(Song song)
        {
            var viewModel = new EditSongViewModel(song);
            var view = new EditSongWindow
            {
                DataContext = viewModel
            };
            view.ShowDialog();
        }
    }
}
