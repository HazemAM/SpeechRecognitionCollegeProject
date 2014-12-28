using NAudio.Wave;
using System;

namespace SpeechRecognition
{
    class Microphone
    {
        /** ATTRIBUTES */
        //Recording handler:
        public event EventHandler RecordingStopped;

        //Constants/defaults:
        public const int DEF_SAMPLE_RATE = 44100;
        public const int DEF_CHANNELS = 1;
        public const int DEF_DURATION = 1000;

        //Variables:
        private double[] audioBuffer; //The party star.

        private int deviceIndex = 0;
        private WaveIn waveIn;
        private WaveOut waveOut;
        private int sampleRate, channels, duration;
        private bool echo;
        private System.Windows.Forms.Timer durationTimer;



        /** CONSTRUCTOR */
        public Microphone(int sampleRate, int channels, int duration, bool echo){
            setupAttribute(ref this.sampleRate, sampleRate);
            setupAttribute(ref this.channels, channels);
            setupAttribute(ref this.duration, duration);
            setupAttribute(ref this.echo, echo);

            initialize();
        }

        //Secondary
        public Microphone(){ //With default values.
            setupAttribute(ref this.sampleRate, DEF_SAMPLE_RATE);
            setupAttribute(ref this.channels, DEF_CHANNELS);
            setupAttribute(ref this.duration, DEF_DURATION);
            setupAttribute(ref this.echo, false);

            initialize();
        }

        private void setupAttribute<Type>(ref Type att, Type value){
            att = value;
        }



        /** MAIN METHODS */
        private void initialize()
        {
            //Inits:
            durationTimer = new System.Windows.Forms.Timer();
            durationTimer.Interval = this.duration; //in milliseconds.
            durationTimer.Tick += new EventHandler(stopRecording);

            //Detecting:
            Console.WriteLine(WaveIn.GetCapabilities(deviceIndex).ProductName);
        }

        public void startRecording()
        {
            waveIn = new WaveIn();
            waveIn.DeviceNumber = deviceIndex;            //Microphone device index.
            waveIn.DataAvailable += waveIn_DataAvailable; //Assign data listener.
            waveIn.RecordingStopped += recordingStopped;
            waveIn.WaveFormat = new WaveFormat(this.sampleRate, this.channels);

            durationTimer.Start();
            waveIn.StartRecording();

            //Play to speakers:
            if(echo){
                waveOut = new WaveOut();
                waveOut.Init( new WaveInProvider(waveIn) );
                waveOut.Play();
            }
        }

        private void stopRecording(object sender, EventArgs e)
        {
            waveIn.StopRecording();
            waveIn.Dispose();
            waveIn = null;

            if(waveOut != null){
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }

            durationTimer.Enabled = false; //Disable the timer.
        }

        //Data receive event handler:
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            double[] newBuffer = toDoubleArray(e.Buffer);

            if(audioBuffer!=null){          //If audioBuffer is not empty, enlarge it and append new data.
                int currLength = audioBuffer.Length;
                double[] temp = new double[newBuffer.Length + currLength];

                Array.Copy(audioBuffer, temp, currLength);
                Array.Copy(newBuffer, 0, temp, currLength-1, newBuffer.Length);
                audioBuffer = temp;
            }
            else audioBuffer = newBuffer;   //Else, initialize audioBuffer.
        }

        //The handler that fire the event (event-handler inception).
        private void recordingStopped(object sender, StoppedEventArgs e)
        {
            if(RecordingStopped != null)
                RecordingStopped(this, new EventArgs());
        }



        /** HELPERS/GETTERS/SETTERS */
        private double[] toDoubleArray(byte[] array){
            double[] doubleBuffer = new double[array.Length/2];
            for(int i=0, j=0; j<array.Length; i++, j+=2)
                doubleBuffer[i] = Utilities.bytesToDouble(array[j], array[j+1]);
            
            return doubleBuffer;
        }

        public double[] getBuffer(){
            return audioBuffer;
        }
    }
}
