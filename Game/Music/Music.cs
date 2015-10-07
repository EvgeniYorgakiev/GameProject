using System;
using System.Media;

namespace Musics
{
    public class Music
    {
        private SoundPlayer playSong = new SoundPlayer();
        public string path = @"song.wav";

        public void stopSong()
        {
            playSong.Stop();
        }
        public void startSong()
        {
            playSong.SoundLocation = path;
            try
            {
                playSong.PlayLooping();
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Wrong path");
            }
        }
    }
}