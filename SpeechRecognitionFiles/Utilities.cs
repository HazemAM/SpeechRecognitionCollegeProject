using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecognition
{
    class Utilities
    {
        public static double bytesToDouble(byte firstByte, byte secondByte){
            //Convert two bytes to one short (little endian):
            short value = (short)((secondByte << 8) | firstByte);
            return value/32768.0; //Convert to range from -1 to (just below) 1.
        }
    }
}
