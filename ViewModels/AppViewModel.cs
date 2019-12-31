using AudioPlayer.AppData;
using AudioPlayer.Helpers;
using AudioPlayer.Models;
using AudioPlayer.WindowServices;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GongSolutions.Wpf.DragDrop;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AudioPlayer.ViewModel
{
    class AppViewModel : INotifyPropertyChanged, IDropTarget
    {
        WaveOutEvent wave;
        MediaFoundationReader mediaReader;
        public MediaFoundationReader MediaReader => mediaReader;

        static DispatcherTimer timer;

        ObservableCollection<Song> songs;

        public Song CurrentSong { get; set; }
        public ObservableCollection<Song> Songs { get => songs; set => songs = value; }

        const string Filename = "songs.bin";

        IWindowService editSong;

        private Visibility buttonPlayVisiblity = Visibility.Visible;
        public Visibility ButtonPlayVisiblity
        {
            get => buttonPlayVisiblity;
            set
            {
                buttonPlayVisiblity = value;
                INotifyPropertyChanged();
            }
        }

        private Visibility buttonPauseVisibility = Visibility.Hidden;
        public Visibility ButtonPauseVisibility
        {
            get => buttonPauseVisibility;
            set
            {
                buttonPauseVisibility = value;
                INotifyPropertyChanged();
            }
        }

        public ICommand CommandPlay { get; private set; }
        public ICommand CommandPause { get; private set; }
        public ICommand CommandChangeSong { get; private set; }
        public ICommand CommandDelete { get; private set; }
        public ICommand CommandEdit { get; private set; }
        public ICommand ProgramClosing { get; private set; }

        static AppViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        public AppViewModel(IWindowService editSongService)
        {
            try
            {
                songs = AppDataManager.LoadSongs(Filename);
            }
            catch (Exception)
            {
                songs = Storage.GetSongs();
            }

            if (songs.Count > 0)
            {
                CurrentSong = songs[0];
            }

            wave = new WaveOutEvent();

            CommandPlay = new RelayCommand(PlayMusic);
            CommandPause = new RelayCommand(PauseMusic);
            CommandChangeSong = new RelayCommand(ChangeSong);
            CommandDelete = new RelayCommand<Song>(DeleteSong);
            CommandEdit = new RelayCommand<Song>(EditSong);
            ProgramClosing = new RelayCommand(OnProgramClosing);

            timer.Tick += (s, e) => INotifyPropertyChanged("MediaReader");

            editSong = editSongService;
        }

        private void PlayMusic()
        {
            if (CurrentSong == null)
                return;

            // If music wasn't paused.
            if (mediaReader == null || mediaReader.Position == 0)
            {
                mediaReader = new MediaFoundationReader(CurrentSong.Path);
                wave.Init(mediaReader);
            }

            wave.Play();
            timer.Start();

            ShowPauseButton();
        }

        private void PauseMusic()
        {
            wave.Pause();
            timer.Stop();

            ShowPlayButton();
        }

        void ChangeSong()
        {
            mediaReader = null;
            wave.Stop();
            timer.Stop();
            INotifyPropertyChanged("MediaReader");

            ShowPlayButton();
        }

        void DeleteSong(Song song)
        {
            songs.Remove(song);
        }

        void EditSong(Song song)
        {
            editSong.ShowDialog(song);
        }

        void OnProgramClosing()
        {
            AppDataManager.SaveSongs(Filename, songs);
        }

        /// <summary>
        /// Make button play visible and button pause hidden.
        /// </summary>
        void ShowPlayButton()
        {
            ButtonPauseVisibility = Visibility.Hidden;
            ButtonPlayVisiblity = Visibility.Visible;
        }
        /// <summary>
        /// Make button pause visible and button play hidden.
        /// </summary>
        void ShowPauseButton()
        {
            ButtonPlayVisiblity = Visibility.Hidden;
            ButtonPauseVisibility = Visibility.Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void INotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            dropInfo.Effects = DragDropEffects.Move;
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data is DataObject obj)
            {
                var files = obj.GetFileDropList();
                foreach (var file in files)
                {
                    if (Helper.IsAudio(file))
                    {
                        var filename = Path.GetFileNameWithoutExtension(file);
                        var extension = Path.GetExtension(file);
                        var resultFile = $"Songs\\{filename}-{Guid.NewGuid()}{extension}";
                        File.Copy(file, resultFile);

                        var song = new Song()
                        {
                            Path = resultFile
                        };
                        song.Duration = Helper.GetSongDuration(resultFile);
                        songs.Add(song);
                    }
                }
            }
        }
    }
}
