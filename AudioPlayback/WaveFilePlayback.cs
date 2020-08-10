using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.AudioPlayback
{
    public class WaveFilePlayback : IAudioPlayback
    {
        private string waveFile;

        public WaveFilePlayback(string filePath)
        {
            this.waveFile = filePath;
        }
        
        



        public void Play()
        {
            if (waveFile == null) throw new FileNotFoundException("Could not find wave file");

            byte[] header = new byte[4];
            byte[] body;

            using(BinaryReader reader = new BinaryReader(File.OpenRead(waveFile)))
            {
                //Skip unimportant parts
                reader.ReadBytes(4);
                //Read the chunk size
                reader.Read(header, 0, header.Length);


                //Get the chunksize - The number of bytes in the rest of file.
                //Fix endianess
                int chunkSize = 0;
                chunkSize = BitConverter.ToInt32(header, 0);
                Console.WriteLine($"Chunk size is {chunkSize}");


                body = new byte[chunkSize - 4];
                reader.ReadBytes(4);
                reader.Read(body, 0, body.Length);

                //Fix endianess
                int formatChunkStart = 0;
                int formatChunkEnd = 0;
                formatChunkEnd = BitConverter.ToInt32(body, 4);
                Console.WriteLine($"Format chunk size is {formatChunkEnd}");

                //Process format header

                short audioFormat = BitConverter.ToInt16(body, 8);
                short numChannels = BitConverter.ToInt16(body, 10);
                int sampleRate = BitConverter.ToInt32(body, 12);
                int byteRate = BitConverter.ToInt32(body, 16);
                int bitsPerSample = (byteRate * 8) / (numChannels * sampleRate);

                


            }


        }

        public void Play(AudioPlaybackOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
