using AudioPlayer.Models;
using GalaSoft.MvvmLight.Command;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AudioPlayer.ViewModel
{
    class AppViewModel : INotifyPropertyChanged
    {
        WaveOutEvent wave;
        MediaFoundationReader mediaReader;
        public MediaFoundationReader MediaReader => mediaReader;

        Song song = new Song
        {
            Path = "music.mp3"
        };

        Song Song { get => song; set => song = value; }

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

        public AppViewModel()
        {
            wave = new WaveOutEvent();
            mediaReader = new MediaFoundationReader(song.Path);
            wave.Init(mediaReader);

            CommandPlay = new RelayCommand(PlayMusic);
            CommandPause = new RelayCommand(PauseMusic);
        }

        private void PlayMusic()
        {
            wave.Play();

            ButtonPlayVisiblity = Visibility.Hidden;
            ButtonPauseVisibility = Visibility.Visible;
        }

        private void PauseMusic()
        {
            wave.Stop();

            ButtonPauseVisibility = Visibility.Hidden;
            ButtonPlayVisiblity = Visibility.Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void INotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
