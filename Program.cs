using MediaPlayer.AudioPlayback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            WaveFilePlayback waveFilePlayback = new WaveFilePlayback(@"file_example_WAV_2MG.wav");
            waveFilePlayback.Play();


        }
    }
}
