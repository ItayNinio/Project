using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GameEngine.GameServices
{
    public static class Music
    {
        public static MediaPlayer _MediaPlayer = new MediaPlayer(); // הנגן
        public static bool _Flag { get; set; } = false;
        public static bool IsOn { get; set; } = false; // האם מוזיקת רקע מתנגנת
        public static int _volume = 100; // הערך התחלתי של המוזיקה

        public static int Volume
        {
            set
            {
                _volume = value;
                _MediaPlayer.Volume = _volume/100.0;
            }
            get
            {
                return _volume*100;
            }
        }

        public static void Play(string Filename)
        {
            if (!IsOn)
            {
                IsOn = true;
                _MediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{Filename}"));
                _MediaPlayer.IsLoopingEnabled = true;
                _MediaPlayer.Play();
            }
        }

        public static void Pause()
        {
            if (IsOn)
            {
                IsOn = false;
                _MediaPlayer.Pause();
            }
        }
    }
}
