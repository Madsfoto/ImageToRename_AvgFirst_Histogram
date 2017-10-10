using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageToRename_AvgFirst_Histogram
{
    class Program
    {
        public string CrushedInts(int[] averageArrayData)
        {
            int[] crushedArr = new int[256];
            for (int i = 0; i < averageArrayData.Length; i++)
            {
                crushedArr[i] = (int)((averageArrayData[i] / (254 / 89f)) + 10);    // ugly code. Should be 255/89, but then it gets trunciated from 2.876404494 to 2 
                                                                                    // and gives wrong results. 255*(255/89f) is 98 so I'm not filling the range properly.
                                                                                    // so the answer was 254/89f. Would 253/89f give better results? To be tested!
            }
            string output = "";
            for (int i = 0; i < crushedArr.Length; i++)
            {
                output += crushedArr[i].ToString() + ", ";
            }

            Console.WriteLine(output);
            return output;
        }

        public int AverageSum(int[] averageArrayData)
        {
            int average = averageArrayData.Sum() / averageArrayData.Count();


            return average;
        }

        public int WeightedSumEights(int[] averageArrayData) // Split into 1/8's, exposure is 3/8's, rest 1/8
        {
            double factor = (1d / 8);
            int max = 256;

            double blacksdbl, shadowsdbl, highlightsdbl, whihetsdbl;
            blacksdbl = shadowsdbl = highlightsdbl = whihetsdbl = factor;


            int blacksInt = (int)(factor * max);

            int shadowsInt = blacksInt + (int)(factor * max);

            int exposureInt = blacksInt + shadowsInt + blacksInt + shadowsInt;
            double exposuredbl = (1 - (blacksdbl + shadowsdbl + highlightsdbl + whihetsdbl));

            int highlightsInt = blacksInt + exposureInt;
            int whitesInt = blacksInt + highlightsInt;

            int sum = 0;

            for (int i = 0; i < averageArrayData.Length; i++)
            {
                if (averageArrayData[i] < blacksInt)
                {
                    sum += (int)(averageArrayData[i] * blacksdbl);
                }
                else if (averageArrayData[i] < shadowsInt)
                {
                    sum += (int)(averageArrayData[i] * shadowsdbl);
                }
                else if (averageArrayData[i] < exposureInt)
                {
                    sum += (int)(averageArrayData[i] * exposuredbl);
                }
                else if (averageArrayData[i] < highlightsInt)
                {
                    sum += (int)(averageArrayData[i] * highlightsdbl);
                }
                else
                {
                    sum += (int)(averageArrayData[i] * whihetsdbl);
                }


            }
            sum = sum / averageArrayData.Length;

            return sum;
        }
        
        public int GeoMean(int[] averageArrayData) // Geometric mean of the sum
        {

            double sum = 1.0;

            foreach (int value in averageArrayData)
            {
                if (value != 0)
                {
                    double valdb = value * 1.0;

                    sum = sum * Math.Pow(valdb, (1.0 / averageArrayData.Length));  // Works because of Indicies rule 4: (a*b)^n = a^n*b^n
                }
                else // meaning that if value IS 0, we can not use the information because of multiplication rules
                {

                }
            }



            return (int)sum;
        }

        int WeightedSumBridge(int[] averageArrayData) // Split into the sections that Bridge has (probably more correct)
        {
            int max = 256;

            double blacksdbl, shadowsdbl, highlightsdbl, whihtesdbl;

            int blacksInt = 26;
            blacksdbl = whihtesdbl = (blacksInt / max); // should be 0,1015625

            int shadowsInt = blacksInt + 60;
            shadowsdbl = highlightsdbl = (shadowsInt / max); // should be 0,234375

            int exposureInt = blacksInt + shadowsInt + 85;
            double exposuredbl = 1 - (blacksdbl + whihtesdbl + shadowsdbl + highlightsdbl); // should be 0,328125

            int highlightsInt = exposureInt + shadowsInt;

            int sum = 0;

            for (int i = 0; i < averageArrayData.Length; i++)
            {
                if (averageArrayData[i] < blacksInt)
                {
                    sum += (int)(averageArrayData[i] * blacksdbl);
                }
                else if (averageArrayData[i] < shadowsInt)
                {
                    sum += (int)(averageArrayData[i] * shadowsdbl);
                }
                else if (averageArrayData[i] < exposureInt)
                {
                    sum += (int)(averageArrayData[i] * exposuredbl);
                }
                else if (averageArrayData[i] < highlightsInt)
                {
                    sum += (int)(averageArrayData[i] * highlightsdbl);
                }
                else
                {
                    sum += (int)(averageArrayData[i] * whihtesdbl);
                }


            }
            sum = sum / averageArrayData.Length;

            return sum;
        }


        static void Main(string[] args)
        {
            // Import bitmap
            // resize to 49 pixels, 7x7
            // Make histogram, crushed to 2 digits all the time
            // rename the input with "average number" "-" "histogram"
            // 

        }
    }
}
