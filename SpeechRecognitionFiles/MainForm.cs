using System;
using System.Windows.Forms;

namespace SpeechRecognition
{
    public partial class MainForm : Form
    {
        Microphone mic;
        MFCC template;
        MFCC[] samples;
        string[][] samplePaths;

        string buttonText;

        public MainForm()
        {
            InitializeComponent();

            samplePaths = new string[][]
            {
                new string[] {"Up", "..//..//sound//Up.wav"},
                new string[] {"Down", "..//..//sound//Down.wav"},
                new string[] {"Right", "..//..//sound//Right.wav"},
                new string[] {"Left", "..//..//sound//Left.wav"}
                //new string[] {"Hazem", "..//..//sound//Hazem.wav"},
                //new string[] {"Hosam", "..//..//sound//Hosam.wav"},
                //new string[] {"Hatem", "..//..//sound//Hatem.wav"}
            };
            test(samplePaths);
        }

        private void test(string[][] samplePaths)
        {
            /*TESTING RECOGNITION*/
            samples = new MFCC[samplePaths.Length];

            for(int i=0; i<samplePaths.Length; i++){
                try{
                    WaveFile file = new WaveFile(samplePaths[i][1]);
                    samples[i] = new MFCC(file);
                }
                catch(ArgumentException ex){
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            btnRecord.Visible = true; //All MFCC's calculated. Ready to record.
        }

        private void btnRecord_Click(object sender, System.EventArgs e)
        {
            //UI:
            Button button = (sender as Button);
            button.Enabled = false;
            buttonText = button.Text;
            button.Text = "Recording...";
            
            //Real work:
            int duration = (int)numDuration.Value;
            bool echo = checkEcho.Checked;
            mic = new Microphone(Microphone.DEF_SAMPLE_RATE, Microphone.DEF_CHANNELS, duration, echo);
            mic.RecordingStopped += recordingStopped;
            mic.startRecording();
        }

        //Event handler to get the audio buffer form mic when it's ready.
        private void recordingStopped(object sender, System.EventArgs e)
        {
            double[] audioBuffer = (sender as Microphone).getBuffer(); //Safely get the full buffer.

            /*
             * TESTING CONTINUE
             */
            WaveFile fileRecorded = new WaveFile(audioBuffer, null, Microphone.DEF_SAMPLE_RATE, Microphone.DEF_CHANNELS);
            template = new MFCC(fileRecorded);

            double[] distances = new double[samples.Length];
            try{
                for(int i=0; i<samples.Length; i++){
                    distances[i] = new DynamicTimeWarping(samples[i], template).distance;
                    distances[i] = Math.Round(distances[i], 2);
                }

                //Final result:
                string result = string.Empty;
                for(int i=0; i<samplePaths.Length; i++)
                    result += String.Format("{0}: {1}\n", samplePaths[i][0], distances[i]);
                MessageBox.Show(result, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(ArgumentOutOfRangeException ex){
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                getUIBack();
                return;
            }

            //UI:
            getUIBack();
        }

        private void getUIBack(){
            btnRecord.Enabled = true;
            btnRecord.Text = buttonText;
            btnRecord.Focus();
        }
    }
}
