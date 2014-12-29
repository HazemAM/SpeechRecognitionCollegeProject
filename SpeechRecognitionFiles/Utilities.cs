using System;

namespace SpeechRecognition
{
    class Utilities
    {
        public static double bytesToDouble(byte firstByte, byte secondByte){
            //Convert two bytes to one short (little endian):
            short value = (short)((secondByte << 8) | firstByte);
            return value/32768.0; //Convert to range from -1 to (just below) 1.
        }

        public static int minIndex(double[] array){
            if(array.Length == 0)
                throw new ArgumentNullException("Argument doesn't contain any elements.");
            
            int min = 0;
            for(int i=1; i<array.Length; i++)
                if(array[i] < array[min])
                    min = i;

            return min;
        }
    }
}
