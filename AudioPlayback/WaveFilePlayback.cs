using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.AudioPlayback
{
    //http://soundfile.sapp.org/doc/WaveFormat/
    //Uncompressed formats have lossless audio
    public class WaveFilePlayback : IAudioPlayback
    {
        private string waveFile;
        private AudioPlaybackOptions playbackOptions;
        private byte[] allAudioData;

        public WaveFilePlayback(string filePath)
        {
            this.waveFile = filePath;
            readFile();
        }
        
        //Read the WAV file and populate the playback options
        private void readFile()
        {
            if (waveFile == null) throw new FileNotFoundException("Could not find wave file");

            byte[] header = new byte[4];
            byte[] body;

            playbackOptions = new AudioPlaybackOptions();

            using (BinaryReader reader = new BinaryReader(File.OpenRead(waveFile)))
            {
                //Skip unimportant parts
                reader.ReadBytes(4);
                //Read the chunk size
                reader.Read(header, 0, header.Length);


                //Get the chunksize - The number of bytes in the rest of file.
                //Fix endianess
                int chunkSize = 0;
                chunkSize = BitConverter.ToInt32(header, 0);


                body = new byte[chunkSize - 4];
                reader.ReadBytes(4);
                reader.Read(body, 0, body.Length);

                //Fix endianess
                int formatChunkEnd = 0;
                formatChunkEnd = BitConverter.ToInt32(body, 4) + 4;

                //Process format header


                //Tells the length of the format subchunk (eg 16 most commonly) Fix for different formats later
                short audioFormat = BitConverter.ToInt16(body, 8);

                //Mono/stereo
                playbackOptions.NumChannels = BitConverter.ToInt16(body, 10);

                //Number of samples (ie audio frames) per second
                playbackOptions.SampleRate = BitConverter.ToInt32(body, 12);

                //Number of bits per sample for each sample for each channel
                playbackOptions.BitRate = BitConverter.ToInt32(body, 16) * 8;
                
                //Number of bits per each sample
                playbackOptions.BitsPerSample = BitConverter.ToInt16(body, 20);

                //Find start and end position of actual audio data
                int dataChunkStart = formatChunkEnd + 8;
                int dataChunkEnd = 0;
                dataChunkEnd = BitConverter.ToInt32(body, dataChunkStart);

                playbackOptions.AudioStart = dataChunkStart;
                playbackOptions.AudioEnd = dataChunkEnd;

                //Now we have the format information and the audio data
                //Need to play possibly using c or c++


            }

            int n = 5;
            
        }


        public void Play()
        {
            //Use WAVEOUT api and playback options
            //https://docs.microsoft.com/en-us/windows/win32/api/mmeapi/nf-mmeapi-waveoutopen


        }

        public void Play(AudioPlaybackOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
