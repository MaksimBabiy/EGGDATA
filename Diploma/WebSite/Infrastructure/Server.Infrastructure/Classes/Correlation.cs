namespace Server.Infrastructure.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MathNet.Numerics.Statistics;

    public static class Correlation
    {

        //алгоритм пирсона (один из вариантов алгоритма корреляции)
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

        //алгоритм корреляции
        public static List<double> CorrelationPoints(List<string> allPoints, List<string> rPeaks)
        {

            List<double> firstSegment = new List<double>();
            List<double> secondSegment = new List<double>();
            List<double> corrPoints = new List<double>();
            for (int i = 0; i < rPeaks.Count - 2; i++)
            {
                int j = i + 1;
                for (int g = j; g < rPeaks.Count - 1; g++)
                {
                    int m = g + 1;
                    try
                    {
                        double difference = 0;

                        for (int n = Convert.ToInt32(rPeaks[i]); n <= Convert.ToInt32(rPeaks[j]); n++)
                        {
                            firstSegment.Add(Convert.ToDouble(allPoints[n]));
                        }

                        for (int n = Convert.ToInt32(rPeaks[g]); n <= Convert.ToInt32(rPeaks[m]); n++)
                        {
                            secondSegment.Add(Convert.ToDouble(allPoints[n]));
                        }

                        if (firstSegment.Count != secondSegment.Count)
                        {

                            int numberOfPoint = 0;

                            if (firstSegment.Count > secondSegment.Count) // Проверка какой список доминирует
                            {
                                int startCount = secondSegment.Count;

                                while (secondSegment.Count != firstSegment.Count) // Создание новых точек между существующими
                                {
                                    double newBetweenPoint = (secondSegment[numberOfPoint] + secondSegment[numberOfPoint + 1]) / 2;
                                    secondSegment.Insert(numberOfPoint + 1, newBetweenPoint);
                                    numberOfPoint += 2;

                                    if (numberOfPoint > secondSegment.Count - 3) // Если список точек подходит к концу, а операция не закончена номер точки обнуляется
                                    {
                                        numberOfPoint = 0;
                                    }
                                }

                                int endCount = secondSegment.Count;

                                difference = (endCount - startCount) / ((endCount + startCount) / 2); // Переменная разности показывает насколько график был увеличен
                            }
                            else if (firstSegment.Count < secondSegment.Count) // Проверка какой список доминирует
                            {
                                int startCount = firstSegment.Count;

                                while (firstSegment.Count != secondSegment.Count) // Создание новых точек между существующими
                                {
                                    double newBetweenPoint = (firstSegment[numberOfPoint] + firstSegment[numberOfPoint + 1]) / 2;
                                    firstSegment.Insert(numberOfPoint + 1, newBetweenPoint);
                                    numberOfPoint += 2;

                                    if (numberOfPoint > firstSegment.Count - 3) // Если список точек подходит к концу, а операция не закончена номер точки обнуляется
                                    {
                                        numberOfPoint = 0;
                                    }
                                }

                                int endCount = firstSegment.Count;

                                difference = (endCount - startCount) / ((endCount + startCount) / 2);
                            }
                        } // Проверка на одинаковое кол-во элементов

                        double corrCoefPoint = Correlation.Pearson(firstSegment, secondSegment);

                        if (corrCoefPoint > 0)
                        {
                            corrCoefPoint -= difference;
                            if (corrCoefPoint < 0)
                            {
                                corrCoefPoint = 0;
                            }
                        }
                        else if (corrCoefPoint < 0)
                        {
                            corrCoefPoint += difference;
                            if (corrCoefPoint > 0)
                            {
                                corrCoefPoint = 0;
                            }
                        }

                        corrCoefPoint = Math.Abs(corrCoefPoint);

                        corrPoints.Add(corrCoefPoint);
                        firstSegment.Clear();
                        secondSegment.Clear();
                    }
                    catch
                    {
                        // break;
                    }
                }
            }

            return corrPoints;
        }

    }
}
