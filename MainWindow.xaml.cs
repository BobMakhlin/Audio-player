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
        WaveOutEvent wave;
        MediaFoundationReader mediaReader;
        public MainWindow()
        {
            InitializeComponent();

            wave = new WaveOutEvent();
            mediaReader = new MediaFoundationReader("music.mp3");
            wave.Init(mediaReader);
        }

        private void OnPlayMouseDown(object sender, MouseButtonEventArgs e)
        {
            wave.Play();
            iPlay.Visibility = Visibility.Hidden;
            iPause.Visibility = Visibility.Visible;
        }

        private void OnPauseMouseDown(object sender, MouseButtonEventArgs e)
        {
            wave.Stop();
            iPause.Visibility = Visibility.Hidden;
            iPlay.Visibility = Visibility.Visible;
        }
    }
}
