using System;

namespace SpeechRecognition
{
    class WaveFile
    {
        public readonly int fmtSize;
        public readonly int channels;
        public readonly int sampleRate;
        public readonly int bitDepth;

        public readonly double[] soundDataLeft;
        public readonly double[] soundDataRight;

        public WaveFile(string filePath)
        {
            /*Reading Header Information*/
            byte[] file = System.IO.File.ReadAllBytes(filePath);

            //Get header info:
            this.fmtSize  = BitConverter.ToInt32(file, 16); //equals '16' if of PCM format. PCM = uncompressed. No extra data in PCM.
            this.channels = BitConverter.ToInt16(file, 22);
            this.sampleRate = BitConverter.ToInt32(file, 24);
            this.bitDepth = BitConverter.ToInt16(file, 34);

            //Get past all the other subchunks to get to the data subchunk:
            int pos = 12; //First subchunk ID from 12 to 16.

            //Keep iterating until we find the data chunk (i.e. 64 61 74 61... (i.e. 100 97 116 97 in decimal))
            while( !(file[pos]==100 && file[pos+1]==97 && file[pos+2]==116 && file[pos+3]==97) )
            {
                pos += 4;
                int chunkSize = file[pos] + file[pos+1]*256 + file[pos+2]*65536 + file[pos+3]*16777216;
                pos += (4+chunkSize);
            }
            pos += 8;

            //'pos' is now positioned to start of actual sound data.
            int samples = (file.Length-pos) / 2; //2 bytes per sample (16-bit mono).
            if(channels==2) samples /= 2;        //4 bytes per sample (16-bit stereo).

            //Allocate memory (soundDataRight will be null if only mono sound):
            soundDataLeft = new double[samples];
            if(channels == 2)
                soundDataRight = new double[samples];
            else soundDataRight = null;

            //Write to double array(s):
            int i = 0;
            while(pos < file.Length)
            {
                soundDataLeft[i] = bytesToDouble(file[pos], file[pos+1]);
                pos += 2;
                if(channels == 2){
                    soundDataRight[i] = bytesToDouble(file[pos], file[pos+1]);
                    pos += 2;
                }
                i++;
            }
        }

        private static double bytesToDouble(byte firstByte, byte secondByte)
        {
            //Convert two bytes to one short (little endian):
            short value = (short)((secondByte << 8) | firstByte);
            return value/32768.0; //Convert to range from -1 to (just below) 1.
        }
    }
}
