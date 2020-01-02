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
    class ImageDropHandler : IDropTarget
    {
        AppViewModel viewModel;

        public ImageDropHandler(AppViewModel vm)
        {
            viewModel = vm;
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject obj)
            {
                if (obj.GetFileDropList().Count == 1)
                {
                    dropInfo.Effects = DragDropEffects.Move;
                }
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject obj)
            {
                if(obj.GetFileDropList().Count == 1)
                {
                    var file = obj.GetFileDropList()[0];

                    if (FileFormat.IsImage(file))
                    {
                        var filename = Path.GetFileNameWithoutExtension(file);
                        var extension = Path.GetExtension(file);
                        var resultFile = $"{AppFiles.ImagesPath}\\{filename}-{Guid.NewGuid()}{extension}";
                        File.Copy(file, resultFile);

                        viewModel.CurrentSong.ImagePath = resultFile;
                    }
                }
            }
        }
    }
}
