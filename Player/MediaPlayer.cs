using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Player
{
    class MediaPlayer
    {
        WaveOutEvent wave = new WaveOutEvent();
        MediaFoundationReader mediaReader;

        AudioState state = AudioState.Stoped;

        public long Position
        {
            get => mediaReader != null ? mediaReader.Position : 0;
            set => mediaReader.Position = value;
        }
        public long Length => mediaReader != null ? mediaReader.Length : 1;

        public void Pause()
        {
            wave.Pause();
            state = AudioState.Paused;
        }

        public void Play(string path)
        {
            if (state == AudioState.Stoped)
            {
                mediaReader = new MediaFoundationReader(path);
                wave.Init(mediaReader);
            }
            wave.Play();

            state = AudioState.Playing;
        }

        public void Stop()
        {
            wave.Stop();
            state = AudioState.Stoped;
        }

        public bool IsSongFinished()
        {
            return (mediaReader.TotalTime - mediaReader.CurrentTime) < new TimeSpan(0, 0, 1);
        }
    }
}
