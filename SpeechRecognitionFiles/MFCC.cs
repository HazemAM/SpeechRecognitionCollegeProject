using AForge.Math;
using System;

namespace SpeechRecognition
{
    class MFCC
    {
        private double windowLength;
        private int windowSize;
        private double[] window;
        private double windowStep;
        private double filtersCount;
        private double cepstCount;
        private double lifterCount;
        private double fftLength;
        private double fs2mel;
        private double[] lifter;
        private double[] freqs;
        private double[] freqMel;
        private double[][] FBC;
        private double[][] MAG;

        public readonly double[][] MFCCS;

        private double[] soundData;
        private int sampleRate;

        public MFCC(WaveFile file)
        {
            this.soundData = file.soundDataLeft;
            this.sampleRate = file.sampleRate;
            
            //Windowing:
            windowLength = 0.020 * this.sampleRate;
            windowSize = (int)Math.Round(windowLength, 0, MidpointRounding.AwayFromZero);
            window = Hamming(windowSize);
            windowStep = Math.Round(0.01*this.sampleRate, 0, MidpointRounding.AwayFromZero);

            //Liftering:
            filtersCount = 26; //no. of filters in filterbank.
            cepstCount = 12;   //no. of cepstral coefficients.
            lifterCount = 22;  //liftering coefficient.
            lifter = getLifter(cepstCount, lifterCount); //lifter coefficients.

            //Fast Fourier transform:
            fftLength = 2048; //total length of FFT.
            freqs = getFreqs(fftLength, this.sampleRate); //frequencies in half of FFT.
            freqMel = getMelFreqs(freqs) ; //f on mel scale.
            fs2mel = 2595 * Math.Log10(1 + (this.sampleRate / 2.0) / 700.0); //fs/2 on mel scale.
            FBC = getFBC(fs2mel, filtersCount, freqMel);

            MFCCS = getMfccs(this.soundData, this.soundData.Length);
        }

        private double[][] getMfccs(double[] soundData, int dataSize)
        {
            int frameind = 0;
            double[] dataWin = new double[windowSize];
            int n = 0;
            int ln = FBC[0].Length;
            double[] mag = new double[(int)filtersCount];
            double[][] mfccArray = new double[(int)cepstCount+1][];
            MAG = new double [(int)filtersCount][];

            while(n+windowSize <= dataSize)
            {
                frameind = frameind + 1;
                n = n + (int)windowStep;
            }

            for(int i = 0; i < filtersCount; i++)
            {
                if(i<cepstCount+1)
                    mfccArray[i] = new double[frameind];
                MAG[i] = new double[frameind];
            }

            frameind = 0;
            n = 0;
            while(n+windowSize <= dataSize)
            {
                for(int i=0;i<windowSize;i++)
                    dataWin[i] = soundData[i+n]*window[i];
                
                double [] DataWFFT = fft( dataWin, fftLength);
                int len=DataWFFT.Length/2+1;
                double [] dataFFT = new double[len];
                for(int i=0;i<len;i++)
                    dataFFT[i]=DataWFFT[i];
                for(int i=0;i<filtersCount;i++)
                    mag[i] = 9 + getMag(dataFFT, FBC, i);
                for(int i=0;i<filtersCount;i++)
                    MAG[i][(int)frameind] = mag[i]; //check correctness.
                
                double[] cc = DCT(mag);
                double c0 = Math.Sqrt(2)*cc[0];
                for(int i=0;i<cepstCount+1;i++)
                    cc[i] = cc[i]*lifter[i];
                for(int i=0;i<cepstCount;i++)
                    mfccArray[i][(int)frameind] = cc[i+1]; //check correctness.

                mfccArray[12][(int)frameind] = c0;
                n = n + (int)windowStep;
                frameind++;
            }

            return mfccArray;
        }

        //Discrete cousine transform:
        public static double[] DCT(double[] data)
        {
            double[] result = new double[data.Length];
            double c = Math.PI / (2.0 * data.Length);
            double scale = Math.Sqrt(2.0 / data.Length);

            for(int k=0; k<data.Length; k++)
            {
                double sum = 0;
                for (int n = 0; n < data.Length; n++)
                    sum += data[n] * Math.Cos((2.0 * n + 1.0) * k * c);
                result[k] = scale * sum;
            }

            data[0] = result[0] / Math.Sqrt(2);
            for(int i=1; i<data.Length; i++)
                data[i] = result[i];
            return data;
        }


        private static double w(int i, int l)
        {
 	        if(i==0)
                return 1/Math.Sqrt(l);
            else
                return Math.Sqrt(2.0/(double)l);
        }

        private static double getMag(double[] DataWFFT, double[][] FBC, int row)
        {
            double sum = 0;
            int len = FBC[row].Length;
            
            for(int i=0; i<len; i++)
                sum += Math.Abs(DataWFFT[i]) * FBC[row][i];
            
            return Math.Log(sum);
        }

        private static double[] fft(double[] dataWin, double fftlen)
        {
            int len = dataWin.Length;
            Complex[] dat = new Complex [(int)fftlen];
            
            if(fftlen > len)
            {
                double temp = fftlen - len;
                temp /= 2;
                int i = 0;
                for(; i<temp; i++)
                {
                    dat[i] = new Complex();
                    dat[i].Re = 0;
                }
                for(; i<len+temp; i++)
                {
                    dat[i] = new Complex();
                    dat[i].Re = dataWin[i-(int)temp];
                }
                for(; i<fftlen; i++)
                {
                    dat[i] = new Complex();
                    dat[i].Re = 0;
                }
            }
            else
            {
                for(int i=0; i<fftlen; i++)
                    dat[i] = Complex.Parse(dataWin[i].ToString());
            }
            
            FourierTransform.FFT(dat, FourierTransform.Direction.Backward);
            
            double[] res=new double[(int)fftlen];
            for(int i=0; i<fftlen; i++)
                res[i] = dat[i].Magnitude;
            return res;
        }



        private static double[][] getFBC(double fs2mel, double numchannels, double[] freqMel)
        {
            double[][] FBC = new double[(int)numchannels][];
            int size=freqMel.Length;
            for(int i=0; i<numchannels; i++)
            {
                double cdelta = 1*fs2mel/(numchannels+1);
                double cmid =   (i+1)*fs2mel/(numchannels+1);
                double[] fbc = new double[size];
                for (int j = 0; j < size; j++)
                {
                    fbc[j] = 1 - Math.Abs(freqMel[j] - cmid) / cdelta;
                    if(fbc[j]<0)
                        fbc[j] = 0;
                }
                FBC[i] = fbc;
            }
            return FBC;
        }

        private static double[] getMelFreqs(double[] freqs)
        {
            int size = freqs.Length;
            double [] mels=new double[size];
            for (int i = 0; i < size; i++)
                mels[i]=2595*Math.Log10(1+freqs[i]/700.0);
            return mels;
        }
        
        private static double[] getFreqs(double fftlen, int sampleRate)
        {
            int size=(int)fftlen/2;
            double[] freqs = new double[size+1];
            for (double i = 0; i < size+1; i++)
            {
                freqs[(int)i] = i / (double)(fftlen) * sampleRate;
            }
            return freqs;
        }

        private static double[] getLifter(double numceps, double L)
        {
            double [] liftArr=new double [(int)numceps+1];
            for (int i = 0; i < numceps+1; i++)
            {
                liftArr[i] = 1 + L / 2 * Math.Sin(Math.PI * (i) / L);
            }
            return liftArr;
        }

        private static double[] Hamming(int winlength)
        {
            double[] arr = new double[winlength];
            for (int i = 0; i < winlength; i++)
            {
                arr[i] = (float)((0.54 - (0.46 * Math.Cos(2*Math.PI * (double)i / (double)(winlength - 1)))));
            }
            return arr;
        }
    }
}
