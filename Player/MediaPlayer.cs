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
            get => mediaReader.Position;
            set => mediaReader.Position = value;
        }
        public long Length
        {
            get => mediaReader.Length;
        }

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
