using System;
using System.Windows.Forms;

namespace SpeechRecognition
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());


            /*TESTING RECOGNITION*/
            WaveFile fileOne = new WaveFile("..//..//sound//Hazem.wav");
            WaveFile fileTwo = new WaveFile("..//..//sound//Hazem_2.wav");
            WaveFile fileThree = new WaveFile("..//..//sound//Hosam.wav");
            WaveFile fileFour = new WaveFile("..//..//sound//Hatem.wav");

            if(!isPCM(fileOne) || !isPCM(fileTwo))
                Console.WriteLine("Non-PCM files are not supported.");

            MFCC mfccOne = new MFCC(fileOne);
            MFCC mfccTwo = new MFCC(fileTwo);
            MFCC mfccThree = new MFCC(fileThree);
            MFCC mfccFour = new MFCC(fileFour);

            double distance1, distance2, distance3;
            try{
                distance1 = new DynamicTimeWarping(mfccOne, mfccTwo).distance;
                distance2 = new DynamicTimeWarping(mfccOne, mfccThree).distance;
                distance3 = new DynamicTimeWarping(mfccOne, mfccFour).distance;
            }catch(ArgumentOutOfRangeException ex){
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }

        private static bool isPCM(WaveFile file){
            if(file.fmtSize!=16) /*Indicates non-PCM formats. Compressed formats are not supported.*/
                return false;
            else return true;
        }
    }
}
