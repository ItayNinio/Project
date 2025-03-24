using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace GameEngine.GameServices
{
    public static class SoundEffects
    {
        private static MediaPlayer _backgroundPlayer = new MediaPlayer(); // Background music player
        private static List<MediaPlayer> _effectPlayers = new List<MediaPlayer>(); // List of media players for sound effects
        public static bool IsOn { get; set; } = false; // Is background music playing
        private static int _volume = 100; // Initial volume level

        public static int Volume
        {
            set
            {
                _volume = value;
                _backgroundPlayer.Volume = _volume / 100.0;
            }
            get
            {
                return _volume;
            }
        }

        public static void PlayBackground(string filename)
        {
            if (!IsOn)
            {
                IsOn = true;
                _backgroundPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Music/{filename}"));
                _backgroundPlayer.IsLoopingEnabled = true;
                _backgroundPlayer.Play();
            }
        }

        public static void PauseBackground()
        {
            if (IsOn)
            {
                IsOn = false;
                _backgroundPlayer.Pause();
            }
        }

        public static void PlaySoundEffect(string filename)
        {
            MediaPlayer effectPlayer = new MediaPlayer();
            effectPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Sfx/{filename}"));
            effectPlayer.Volume = _volume / 100.0;
            effectPlayer.Play();

            // Remove the player after playback ends
            effectPlayer.MediaEnded += (sender, args) =>
            {
                effectPlayer.Dispose();
                _effectPlayers.Remove(effectPlayer);
            };

            _effectPlayers.Add(effectPlayer);
        }
    }
}
