using AudioPlayer.Models;
using AudioPlayer.ViewModel;
using AudioPlayer.ViewModels;
using AudioPlayer.Views;
using GalaSoft.MvvmLight.Messaging;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new AppViewModel();

            Messenger.Default.Register<NotificationMessage<Song>>(this,
                c => ReceiveNotification(c.Notification, c.Content)
                );
        }

        void ReceiveNotification(string msg, Song song)
        {
            switch (msg)
            {
                case "ShowEditSongWindow":
                    var viewModel = new EditSongViewModel(song);
                    var view = new EditSongWindow
                    {
                        DataContext = viewModel
                    };
                    view.ShowDialog();
                    break;
            }
        }
    }
}
