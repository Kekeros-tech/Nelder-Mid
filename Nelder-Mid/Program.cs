using System;
using System.Collections.Generic;

namespace Nelder_Mid
{
    delegate double FunctionOfAlgo(Point currentPoint);

    class Program
    {
        public static double rosenbrockFunction(Point currentPoint)
        {
            double x1 = currentPoint.getValueByIndex(0);
            double x2 = currentPoint.getValueByIndex(1);
            return 100 * Math.Pow((x2 - Math.Pow(x1, 2)), 2) + Math.Pow((1 - x1), 2);
        }

        public static double secondFunction(Point currentPoint)
        {
            double x1 = currentPoint.getValueByIndex(0);
            double x2 = currentPoint.getValueByIndex(1);
            return 100 * Math.Pow(x2 - Math.Pow(x1, 3), 2) + Math.Pow(1 - x2, 2);
        }

        public static double fourthFunction(Point currentPoint)
        {
            double x1 = currentPoint.getValueByIndex(0);
            double x2 = currentPoint.getValueByIndex(1);
            double x3 = currentPoint.getValueByIndex(2);
            double x4 = currentPoint.getValueByIndex(3);
            return Math.Pow(x1 + 10 * x2, 2) + 5 * Math.Pow(x3 - x4, 2) + Math.Pow(x2 - 2 * x3, 4) + 10 * Math.Pow(x1 - x4, 4);
        }

        public static double bootFunction(Point currentPoint)
        {
            double x1 = currentPoint.getValueByIndex(0);
            double x2 = currentPoint.getValueByIndex(1);
            return Math.Pow(x1 + 2 * x2 - 7, 2) + Math.Pow(2 * x1 + x2 - 5, 2);
        }

        static void Main(string[] args)
        {
            ControlParametrs parametrs = null;
            //Code for demonstrating the results of the algorithm on tests.
            switch (3)
            {
                case 1:
                    {
                        double[] valuesOfStartPoint = { -1.2f, 1 };
                        parametrs = new ControlParametrs(2, new FunctionOfAlgo(rosenbrockFunction), 1, 2, 0.5f, 0.00001f, 2, 
                            valuesOfStartPoint, null);
                        break;
                    }
                case 2:
                    {
                        double[] valuesOfStartPoint = { -1.2f, 1 };
                        parametrs = new ControlParametrs(2, new FunctionOfAlgo(secondFunction), 1, 2, 0.5f, 0.00001f, 
                            2, valuesOfStartPoint, null);
                        break;
                    }
                case 3:
                    {
                        double[] valuesOfStartPoint = { -1.2f, 1 };
                        double[] bottomLine = { -1.2f, 1 };
                        double[] topLine = { 1, 1 };
                        OptimizationBoundary boundary = new OptimizationBoundary(bottomLine, topLine);
                        parametrs = new ControlParametrs(2, new FunctionOfAlgo(secondFunction), 1, 2, 0.5f, 0.00001, 
                            2, valuesOfStartPoint, boundary);
                        break;
                    }
                case 4:
                    {
                        double[] valuesOfStartPoint = { 3, -1, 0, 1 };
                        parametrs = new ControlParametrs(4, new FunctionOfAlgo(fourthFunction), 1, 2, 0.5f, 0.00001f,
                            2, valuesOfStartPoint, null);
                        break;
                    }
            }
            Nelder_Mid nelder_Mid = new Nelder_Mid(2, parametrs);

            Console.WriteLine("Лучшее значение функции полученное алгоритмом: " + nelder_Mid.runAlgorithmWithGivenParameters());

            //Hooke-Jeeves
            //double[] startingValues = { -1.2f, 1 };
            //double[] deltaValues = { 0.0004f, 0.0004f };
            /*double[] startingValues = { 3, -1, 0, 1 };
            double[] deltaValues = { 0.0004f, 0.0004f, 0.0004f, 0.0004f };
            double stepValue = 2.8f;
            double accuracyForHookeJeeves = 0.7f;
            ControlParametrsForHookeJeeves controlParametrs = new ControlParametrsForHookeJeeves(deltaValues, stepValue, accuracyForHookeJeeves, function);

            Hooke_Jeeves hooke_Jeeves = new Hooke_Jeeves(startingValues, controlParametrs);

            Console.WriteLine("Лучшее решение полученное алгоритмом: " + hooke_Jeeves.runAlgorithmWithGivenParameters());*/
        }
    }
}
