using AudioPlayer.AppData;
using AudioPlayer.DropHandlers;
using AudioPlayer.Helpers;
using AudioPlayer.Models;
using AudioPlayer.Player;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace AudioPlayer.ViewModel
{
    class AppViewModel : INotifyPropertyChanged
    {
        MediaPlayer player = new MediaPlayer();
        public MediaPlayer Player => player;

        static DispatcherTimer timer;

        ObservableCollection<Song> songs;

        public bool Cycle { get; set; } = false;

        ISongWindowService editSongService;
        IDialogService dialogService;

        Song currentSong;
        public Song CurrentSong
        {
            get => currentSong;
            set
            {
                currentSong = value;
                INotifyPropertyChanged();
            }
        }

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
        public ICommand CommandDelete { get; private set; }
        public ICommand CommandEdit { get; private set; }
        public ICommand ProgramClosing { get; private set; }
        public ICommand CommandOpenImage { get; private set; }
        public ICommand CommandPlayNextSong { get; private set; }
        public ICommand CommandPlayPrevSong { get; private set; }
        public ICommand CommandCycleSong { get; private set; }

        public IDropTarget SongDropHandler { get; private set; }
        public IDropTarget ImageDropHandler { get; private set; }

        static AppViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
        }

        public AppViewModel(ISongWindowService songService, IDialogService dlgService)
        {
            try
            {
                songs = AppDataManager.LoadSongs(AppFiles.Filename);
            }
            catch (Exception)
            {
                songs = Storage.GetSongs();
            }

            if (songs.Count > 0)
            {
                CurrentSong = songs[0];
            }

            InitCommands();
            SongDropHandler = new SongDropHandler(this);
            ImageDropHandler = new ImageDropHandler(this);

            timer.Tick += TimerTick;

            editSongService = songService;
            dialogService = dlgService;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            INotifyPropertyChanged("Player");

            if (player.IsSongFinished())
            {
                if (!Cycle)
                {
                    PlayNextSong();
                    PlayMusic();
                }
                else
                {
                    player.Position = 0;
                }
            }
        }

        void InitCommands()
        {
            CommandPlay = new RelayCommand(PlayMusic);
            CommandPause = new RelayCommand(PauseMusic);
            CommandChangeSong = new RelayCommand(ChangeSong);
            CommandDelete = new RelayCommand<Song>(DeleteSong);
            CommandEdit = new RelayCommand<Song>(EditSong);
            ProgramClosing = new RelayCommand(OnProgramClosing);
            CommandOpenImage = new RelayCommand(OpenSongImage);
            CommandPlayNextSong = new RelayCommand(PlayNextSong);
            CommandPlayPrevSong = new RelayCommand(PlayPrevSong);
            CommandCycleSong = new RelayCommand(CycleSong);
        }

        private void PlayMusic()
        {
            if (CurrentSong == null)
                return;

            player.Play(CurrentSong.SongPath);
            timer.Start();

            ShowPauseButton();
        }

        private void PauseMusic()
        {
            player.Pause();
            timer.Stop();

            ShowPlayButton();
        }

        void ChangeSong()
        {
            player.Stop();
            timer.Stop();
            INotifyPropertyChanged("Player");

            ShowPlayButton();
        }

        void DeleteSong(Song song)
        {
            songs.Remove(song);
        }

        void EditSong(Song song)
        {
            editSongService.Song = song;
            editSongService.ShowWindow();
        }

        void OnProgramClosing()
        {
            AppDataManager.SaveSongs(AppFiles.Filename, songs);
        }

        void OpenSongImage()
        {
            if (dialogService.OpenFileDialog())
            {
                var file = dialogService.Path;
                if (FileFormat.IsImage(file))
                {
                    var resultFile = Helper.CopyToImagesDir(file);
                    CurrentSong.ImagePath = resultFile;
                }
            }
        }

        void PlayNextSong()
        {
            int pos = Songs.IndexOf(CurrentSong);

            if (pos != -1)
            {
                if (pos < Songs.Count - 1)
                {
                    CurrentSong = Songs[pos + 1];
                }
                else
                {
                    CurrentSong = Songs[0];
                }
            }
        }

        void PlayPrevSong()
        {
            int pos = Songs.IndexOf(CurrentSong);

            if (pos != -1)
            {
                if (pos > 0)
                {
                    CurrentSong = Songs[pos - 1];
                }
                else
                {
                    CurrentSong = Songs[Songs.Count - 1];
                }
            }
        }

        void CycleSong()
        {
            Cycle = !Cycle;
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
    }
}
