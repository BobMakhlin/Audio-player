using AudioPlayer.AppData;
using AudioPlayer.Helpers;
using AudioPlayer.Models;
using AudioPlayer.ViewModel;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AudioPlayer.DropHandlers
{
    class SongDropHandler : IDropTarget
    {
        AppViewModel viewModel; 

        public SongDropHandler(AppViewModel vm)
        {
            viewModel = vm;
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject obj)
            {
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject obj)
            {
                var files = obj.GetFileDropList();
                foreach (var file in files)
                {
                    if (FileFormat.IsAudio(file))
                    {
                        var filename = Path.GetFileNameWithoutExtension(file);
                        var extension = Path.GetExtension(file);
                        var resultFile = $"{AppFiles.SongsPath}\\{filename}-{Guid.NewGuid()}{extension}";
                        File.Copy(file, resultFile);

                        var song = new Song()
                        {
                            SongPath = resultFile
                        };
                        song.Duration = Helper.GetSongDuration(resultFile);
                        viewModel.Songs.Add(song);
                    }
                }
            }
        }
    }
}
