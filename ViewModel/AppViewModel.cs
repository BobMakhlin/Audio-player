using AudioPlayer.Helpers;
using AudioPlayer.Models;
using GalaSoft.MvvmLight.Command;
using GongSolutions.Wpf.DragDrop;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        static AppViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        public AppViewModel()
        {
            songs = Storage.GetSongs();
            CurrentSong = songs[0];

            wave = new WaveOutEvent();

            CommandPlay = new RelayCommand(PlayMusic);
            CommandPause = new RelayCommand(PauseMusic);
            CommandChangeSong = new RelayCommand(ChangeSong);

            timer.Tick += (s, e) => INotifyPropertyChanged("MediaReader");
        }

        private void PlayMusic()
        {
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
                    // If the file isn't an audio method "Helper.GetSongDuration" throws an exception.
                    // So the file will add into the collection only if it is an audio-file.
                    try
                    {
                        var song = new Song()
                        {
                            Path = file
                        };
                        song.Duration = Helper.GetSongDuration(file);
                        songs.Add(song);
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}
