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
        public static string CurrentTrack { get; set; }
        public static bool IsOn { get; set; } = false; // האם מוזיקת רקע מתנגנת
        public static int _volume = 100; // הערך התחלתי של המוזיקה
        public static int _volume_Efects = 100;

        public static int Volume
        {
            set
            {
                _volume = value;
                _MediaPlayer.Volume = _volume / 100.0;
            }
            get
            {
                return _volume; // כאן ביטלנו את ההכפלה
            }
        }
        public static int Efects_Volume
        {
            set
            {
                _volume_Efects = value;
             
            }
            get
            {
                return _volume_Efects; // כאן ביטלנו את ההכפלה
            }
        }

        public static void Play(string Filename)
        {
            if (!IsOn || CurrentTrack != Filename)
            {
                IsOn = true;
                CurrentTrack = Filename;
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

