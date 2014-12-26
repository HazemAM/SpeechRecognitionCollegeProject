using System;

namespace SpeechRecognition
{
    class DynamicTimeWarping
    {
        public readonly double distance;

        public DynamicTimeWarping(MFCC sample, MFCC template)
        {
            double[][] mfccSample = sample.MFCCS;
            double[][] mfccTemplate = template.MFCCS;

            int rows = mfccSample[0].Length;
            int cols = mfccTemplate[0].Length;

            if(mfccSample.Length > 2*mfccTemplate.Length || mfccTemplate.Length > 2*mfccSample.Length)
                throw new ArgumentOutOfRangeException("Too big length difference between both files.");

            //Initializations:
            double[,] table = new double[rows,cols];
            for(int i=0; i<rows; i++)
                for(int j=0; j<cols; j++)
                    table[i,j] = double.PositiveInfinity;

            table[0,0] = euclideanDistance(mfccSample, mfccTemplate, 0, 0); //First cell.

            for(int i=1; i<cols; i++){ //First column.
                double cost = euclideanDistance(mfccSample, mfccTemplate, 0, i);
                table[0,i] = cost + table[0,i-1];
            }

            for(int i=1; i<rows; i++){ //First row.
                double cost = euclideanDistance(mfccSample, mfccTemplate, i, 0);
                table[i,0] = cost + table[i-1,0];
            }

            for(int i=1; i<cols; i++){ //Second column.
                double cost = euclideanDistance(mfccSample, mfccTemplate, 1, i);
                table[1,i] = cost + Math.Min(table[1,i-1], table[0,i-1]);
            }

            //REAL WORK:
            for(int i=2; i<rows; i++)
                for(int j=1; j<cols; j++){
                    double tempMin;
                    double cost = euclideanDistance(mfccSample, mfccTemplate, i, j);
                    tempMin = Math.Min(table[i,j-1], table[i-1,j-1]);
                    table[i,j] = cost + Math.Min(tempMin, table[i-2,j-1]);
                }

            this.distance = table[rows-1,cols-1];
        }

        private static double euclideanDistance(double[][] arrOne, double[][] arrTwo, int indexOne, int indexTwo){
            double sum = 0;
            int length = Math.Max(arrOne[0].Length, arrTwo[0].Length);

            double t1, t2;
            for(int j=0; j<length; j++){
                t1 = j<arrOne.Length ? arrOne[j][indexOne] : 0;
                t2 = j<arrTwo.Length ? arrTwo[j][indexTwo] : 0;
                sum += Math.Pow(t1-t2, 2);
            }

            return Math.Sqrt(sum);
        }
    }
}
