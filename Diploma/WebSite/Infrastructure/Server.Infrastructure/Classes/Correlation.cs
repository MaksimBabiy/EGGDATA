using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.Statistics;

namespace Server.Infrastructure.Classes
{
    public static class Correlation
    {
        public static double Pearson(IEnumerable<double> dataA, IEnumerable<double> dataB)
        {
            int n = 0;
            double r = 0.0;
            double meanA = dataA.Mean();
            double meanB = dataB.Mean();
            double sdevA = dataA.StandardDeviation();
            double sdevB = dataB.StandardDeviation();

            IEnumerator<double> ieA = dataA.GetEnumerator();
            IEnumerator<double> ieB = dataB.GetEnumerator();

            while (ieA.MoveNext())
            {
                if (ieB.MoveNext() == false)
                {
                    throw new ArgumentOutOfRangeException("Datasets dataA and dataB need to have the same length.");
                }

                n++;
                r += (ieA.Current - meanA) * (ieB.Current - meanB) / (sdevA * sdevB);
            }

            if (ieB.MoveNext() == true)
            {
                throw new ArgumentOutOfRangeException("Datasets dataA and dataB need to have the same length.");
            }

            return r / (n - 1);
        }

        public static List<double> CorrelationPoints(List<string> allPoints, List<string> rPeaks)
        {
            
            List<double> FirstSegment = new List<double>();
            List<double> SecondSegment = new List<double>();
            List<double> CorrPoints = new List<double>();

            for (int i = 0; i <= rPeaks.Count; i++)
            {
                int j = i + 1;
                for(int g = 0; g <= rPeaks.Count; g++)
                {
                    int m = g + 1;

                    try
                    {
                        for (int n = Convert.ToInt32(rPeaks[i]); n <= Convert.ToInt32(rPeaks[j]); n++)
                        {
                            FirstSegment.Add(Convert.ToDouble(allPoints[n]));
                        }

                        for (int n = Convert.ToInt32(rPeaks[g]); n <= Convert.ToInt32(rPeaks[m]); n++)
                        {
                            SecondSegment.Add(Convert.ToDouble(allPoints[n]));
                        }

                        CorrPoints.Add(Correlation.Pearson(FirstSegment, SecondSegment));
                    }
                    catch { break; }
                }
            }
            return CorrPoints;
        }

    }
}
