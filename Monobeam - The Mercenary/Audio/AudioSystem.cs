using System;
using System.Media;
using System.Runtime.InteropServices;
using TheMatiaz0_MonobeamTheMercenary.Serialization;

namespace TheMatiaz0_MonobeamTheMercenary.Audio
{
#pragma warning disable IDE1006
    public class AudioSystem
    {
        private static SoundPlayer soundPlayer;

        private static uint VolumeVariable;

        /// <summary>
        /// Plays audio clip.
        /// </summary>
        /// <param name="filePath">Path to audio clip.</param>
        /// <param name="audioChannel">Channel: Music/Sound</param>
        /// <param name="looped">Is it looped?</param>
        public static void Play(string filePath, AudioChannel audioChannel, bool looped = false)
        {
            // On play set volume of audio clip to the default one:
            SetVolume(audioChannel);

            soundPlayer = new SoundPlayer
            {
                SoundLocation = $"{AppDomain.CurrentDomain.BaseDirectory}\\Audio\\Content\\{filePath}"
            };

            if (looped == false)
            {
                soundPlayer.Play();
            }

            else
            {
                soundPlayer.PlayLooping();
            }
        }

        /// <summary>
        /// Stop audio clip.
        /// </summary>
        public static void Stop()
        {
            soundPlayer.Stop();
        }

        /// <summary>
        /// Set volume to the OptionsManager.Music.
        /// </summary>
        /// <param name="audioChannel">Channel: Music or Sound</param>
        public static void SetVolume(AudioChannel audioChannel)
        {
            switch (audioChannel)
            {
                case AudioChannel.Music:
                    VolumeVariable = OptionsManager.Music * 655;
                    break;

                case AudioChannel.Sound:
                    VolumeVariable = OptionsManager.Sounds * 655;
                    break;
            }

            uint vAll = VolumeVariable | (VolumeVariable << 16);

            NativeMetods.waveOutSetVolume(IntPtr.Zero, vAll);
        }
    }

    /// <summary>
    /// Get System DLL variables to set volume.
    /// </summary>
    internal static class NativeMetods
    {
        [DllImport("winmm.dll")]
        public static extern long waveOutSetVolume(IntPtr deviceID, uint Volume);
    }
}
