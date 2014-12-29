using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpeechRecognition
{
    public partial class MainForm : Form
    {
        Microphone mic;
        MFCC template;
        MFCC[] samples;
        string[][] samplePaths;

        //IU
        string recordButtonText;
        Panel[] nodes;

        public MainForm()
        {
            InitializeComponent();
            getReady();
        }

        private void getReady()
        {
            bool filesLoaded = loadFiles();
            if(filesLoaded)
            {
                btnRecord.Enabled = false;
                bool calcSuccess = calculateMFCCs();

                if(calcSuccess){
                    constructUI();
                    btnRecord.Enabled = true; //All MFCC's calculated. Ready to record.
                }
                else
                    this.tableLayoutPanel1.Controls.Clear();
            }
        }

        private bool loadFiles()
        {
            string[] names = txtFileNames.Text.Split(new string[]{", ",","}, StringSplitOptions.RemoveEmptyEntries);

            if(names.Length > 9){
                MessageBox.Show("Maximum of 9 files is currently supported.", "Too much...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            int i=0;
            samplePaths = new string[names.Length][];

            foreach(string name in names){
                samplePaths[i] = new string[]{name, "../../sound/"+name+".wav"};
                i++;
            }

            /*samplePaths = new string[][]
            {
                new string[] {"Forward", "sound/Forward.wav"},
                new string[] {"Left", "sound/Left.wav"},
                new string[] {"Stop", "sound/Stop.wav"},
                new string[] {"Right", "sound/Right.wav"},
                new string[] {"Backward", "sound/Backward.wav"},

                new string[] {"Hazem", "..//..//sound//Hazem.wav"},
                new string[] {"Hosam", "..//..//sound//Hosam.wav"},
                new string[] {"Hatem", "..//..//sound//Hatem.wav"}
            };*/

            return true;
        }

        private bool calculateMFCCs()
        {
            /*Calculating MFCC's*/
            samples = new MFCC[samplePaths.Length];

            for(int i=0; i<samplePaths.Length; i++){
                try{
                    WaveFile file = new WaveFile(samplePaths[i][1]);
                    samples[i] = new MFCC(file);
                }
                catch(ArgumentException ex){
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch(System.IO.FileNotFoundException ex){
                    MessageBox.Show(ex.Message, "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void btnRecord_Click(object sender, System.EventArgs e)
        {
            //UI:
            Button button = (sender as Button);
            button.Enabled = false;
            recordButtonText = button.Text;
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

                //UI full result:
                int min = Utilities.minIndex(distances);
                if(checkFullResult.Checked){
                    string result = string.Empty;
                    for(int i=0; i<samplePaths.Length; i++)
                        result += String.Format("{0}: {1}\n", samplePaths[i][0], distances[i]);
                    result += "\nMinimum: " + samplePaths[min][0];

                    MessageBox.Show(result, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                //UI highlight:
                highlight(min);
            }
            catch(ArgumentOutOfRangeException ex){
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                getUIBack();
                return;
            }

            //UI:
            getUIBack();
        }



        /** GUI FUNCTIONS */
        private void getUIBack(){
            btnRecord.Enabled = true;
            btnRecord.Text = recordButtonText;
            btnRecord.Focus();
        }

        private void highlight(int i){
            foreach(Panel panel in nodes)
                (panel.Controls[1] as RadioButton).Checked = false;

            (nodes[i].Controls[1] as RadioButton).Checked = true;
        }

        private void constructUI()
        {
            this.tableLayoutPanel1.Controls.Clear();

            nodes = new Panel[samplePaths.Length];
            int[] col, row;
            if(samplePaths.Length <= 5){
                col = new int[] {1, 0,1,2, 1};
                row = new int[] {0, 1,1,1, 2};
            }
            else{
                col = new int[]{0,1,2, 0,1,2, 0,1,2, 0,1,2};
                row = new int[]{0,0,0, 1,1,1, 2,2,2, 3,3,3};
            }

            int i=0;
            foreach(string[] path in samplePaths){
                nodes[i] = createNode(path[0]);
                this.tableLayoutPanel1.Controls.Add(nodes[i], col[i], row[i]);
                i++;
            }
        }

        private Panel createNode(string text)
        {
            PictureRadioButton radioButton = new PictureRadioButton();
            Panel panel = new Panel();
            Label label = new Label();

            //The box:
            panel.SuspendLayout();

            panel.Controls.Add(label);
            panel.Controls.Add(radioButton);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            panel.Margin = new Padding(0);
            panel.Padding = new Padding(0);
            panel.Size = new Size(123, 85);

            //The radio button:
            panel.Controls.Add(radioButton);

            radioButton.Anchor = AnchorStyles.None;
            radioButton.AutoSize = true;
            radioButton.Enabled = false;
            radioButton.Location = new Point(53, 29);
            radioButton.Size = new Size(14, 25);
            radioButton.TextAlign = ContentAlignment.BottomCenter;
            radioButton.UseVisualStyleBackColor = true;

            //The label:
            label.Anchor = AnchorStyles.None;
            label.AutoSize = true;
            label.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)0));
            label.Location = new Point(10, 45);
            label.Margin = new Padding(0);
            label.MinimumSize = new Size(100, 0);
            label.Size = new Size(100, 13);
            label.Text = text;
            label.TextAlign = ContentAlignment.MiddleCenter;

            return panel;
        }

        private void btnReload_Click(object sender, EventArgs e){
            getReady();
        }
    }
}
