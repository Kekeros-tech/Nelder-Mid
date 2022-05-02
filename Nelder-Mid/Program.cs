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

        private static Point matrixMultiply(double[,] matrix, Point point)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < point.size(); i++)
            {
                double buffer = 0;
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                {
                    buffer += point.getValueByIndex(i) * matrix[i, j];
                }
                result.Add(buffer);
            }
            return new Point(result.ToArray());
        }

        public static double fifthFunction(Point currentPoint)
        {
            double[,] matrixA = { 
                { 2, 0 }, 
                { 0, 4 } 
            };
            double[] vectorG = { 1, 2 };
            return calculateResultOfMatrixFunctions(matrixA, vectorG, currentPoint);
        }

        public static double calculateResultOfMatrixFunctions(double[,] matrix, double[] g, Point currentPoint)
        {
            Point G = new Point(g);
            return matrixMultiply(matrix, currentPoint) * currentPoint - G * currentPoint;
        }

        public static double sixthFunction(Point currentPoint)
        {
            double[,] matrixA1 =
            {
                { 10, 0 },
                { 0, 7 }
            };
            double[] vectorG1 = { 100, 4 };
            double[,] matrixA2 =
            {
                { 10, 0 },
                { 0, 1 }
            };
            double[] vectorG2 = { 2, 5 };
            double result1 = calculateResultOfMatrixFunctions(matrixA1, vectorG1, currentPoint);
            double result2 = calculateResultOfMatrixFunctions(matrixA2, vectorG2, currentPoint);
            return Math.Min(result1, result2);
        }

        public static double bootFunction(Point currentPoint)
        {
            double x1 = currentPoint.getValueByIndex(0);
            double x2 = currentPoint.getValueByIndex(1);
            return Math.Pow(x1 + 2 * x2 - 7, 2) + Math.Pow(2 * x1 + x2 - 5, 2);
        }

/*        public static double generateGrid(double[] delta, Point startPoint)
        {
            List<List<double>> result = new List<List<double>>();
            double[] valuesOfPoint = startPoint.ValueVectorToArray; 
            for(int i = 0; i < delta.Length; i++)
            {
                for(int j = -5; j < 5; j++)
                {
                    for(int k = 0; k < valuesOfPoint.Length; k++)
                    {
                        List<double> buffer = 
                    }
                }
            }

        }*/

        private static Point findMinimumPoint(List<Point> points)
        {
            Point minPoint = points[0];
            for(int i = 1; i < points.Count; i++)
            {
                if(points[i] < minPoint)
                {
                    minPoint = points[i];
                }
            }
            return minPoint;
        }

        private static Point runAlgorithmWithDeltaVector(double[] delta, ControlParametrs parametrs, Nelder_Mid nelder_Mid)
        {
            Point magnifyingVector = new Point(delta, parametrs.Function);
            parametrs.startPoint = new Point(parametrs.startPoint + magnifyingVector, sixthFunction);
            return nelder_Mid.runAlgorithmWithGivenParameters();
        }

        static void Main(string[] args)
        {
            double[] valuesOfTest = { 1, 2 };
            sixthFunction(new Point(valuesOfTest));
            //Code for demonstrating the results of the algorithm on tests.
            ControlParametrs parametrs = null;
            bool algorithmRepetition = false;
            switch (6)
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
                        double[] bottomLine = { -1.2f, -1 };
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
                case 5:
                    {
                        double[] valuesOfStartPoint = { -1.2f, 1 };
                        parametrs = new ControlParametrs(2, new FunctionOfAlgo(fifthFunction), 1, 2, 0.5f, 0.00001f,
                            2, valuesOfStartPoint, null);
                        break;
                    }
                case 6:
                    {
                        double[] valuesOfStartPoint = { -1.2f, 1 };
                        parametrs = new ControlParametrs(2, new FunctionOfAlgo(sixthFunction), 1, 2, 0.5f, 0.00001f,
                            2, valuesOfStartPoint, null);
                        algorithmRepetition = true;
                        break;

                    }
            }
            Nelder_Mid nelder_Mid = new Nelder_Mid(2, parametrs);
            Point minPoint = nelder_Mid.runAlgorithmWithGivenParameters();


            if (algorithmRepetition)
            {
                double[] delta = { 100, 100 };
                List<Point> grid = minPoint.generateGrid(delta, parametrs.Function);
                foreach(Point currentPoint in grid)
                {
                    parametrs.startPoint = currentPoint;
                    Point resultOfAlgo = nelder_Mid.runAlgorithmWithGivenParameters();
                    if(resultOfAlgo.FunctionValue < minPoint.FunctionValue)
                    {
                        Console.WriteLine(resultOfAlgo);
                        minPoint = resultOfAlgo;
                    }
                }
            }

            Console.WriteLine("Лучшее значение функции полученное алгоритмом: " + minPoint);

            ControlParametrsForHookeJeeves forHookeJeeves = null;
            algorithmRepetition = false;
            switch (6)
            {
                case 1:
                    {
                        double[] startingValues = { -1.2f, 1 };
                        double[] deltaValues = { 0.0004f, 0.0004f };
                        forHookeJeeves = new ControlParametrsForHookeJeeves(deltaValues, 2.8f, 0.7f, 
                            new FunctionOfAlgo(rosenbrockFunction), null, startingValues);
                        break;
                    }
                case 2:
                    {
                        double[] startingValues = { -1.2f, 1 };
                        double[] deltaValues = { 0.0004f, 0.0004f };
                        forHookeJeeves = new ControlParametrsForHookeJeeves(deltaValues, 2.8f, 0.7f,
                            new FunctionOfAlgo(secondFunction), null, startingValues);
                        break;
                    }
                case 3:
                    {
                        double[] startingValues = { -1.2f, 1 };
                        double[] deltaValues = { 0.0004f, 0.0004f };
                        double[] bottomLine = { -1.2f, -1 };
                        double[] topLine = { 1, 1 };
                        OptimizationBoundary boundary = new OptimizationBoundary(bottomLine, topLine);
                        forHookeJeeves = new ControlParametrsForHookeJeeves(deltaValues, 2.8f, 0.7f,
                            new FunctionOfAlgo(rosenbrockFunction), boundary, startingValues);
                        break;
                        break;
                    }
                case 4:
                    {
                        double[] startingValues = { 3, -1, 0, 1 };
                        double[] deltaValues = { 0.0004f, 0.0004f, 0.0004f, 0.00004f };
                        forHookeJeeves = new ControlParametrsForHookeJeeves(deltaValues, 2.8f, 0.7f,
                            new FunctionOfAlgo(rosenbrockFunction), null, startingValues);
                        break;
                        break;
                    }
                case 5:
                    {
                        double[] startingValues = { -1.2f, 1 };
                        double[] deltaValues = { 0.0004f, 0.0004f };
                        forHookeJeeves = new ControlParametrsForHookeJeeves(deltaValues, 2.8f, 0.000001f,
                            new FunctionOfAlgo(secondFunction), null, startingValues);
                        break;
                    }
                case 6:
                    {
                        double[] startingValues = { -1.2f, 1 };
                        double[] deltaValues = { 0.0004f, 0.0004f };
                        forHookeJeeves = new ControlParametrsForHookeJeeves(deltaValues, 2.8f, 0.000001f,
                            new FunctionOfAlgo(sixthFunction), null, startingValues);
                        algorithmRepetition = true;
                        break;
                    }
            }
            Hooke_Jeeves hooke_Jeeves = new Hooke_Jeeves(forHookeJeeves);
            Point minPointOfHooke_Jeeves = hooke_Jeeves.runAlgorithmWithGivenParameters();
            
            if (algorithmRepetition)
            {
                double[] deltaForHooke_Jeeves = { 1, 1 };
                List<Point> grid = minPointOfHooke_Jeeves.generateGrid(deltaForHooke_Jeeves, forHookeJeeves.Function);
                List<Point> arrayOfResults = new List<Point>();

                foreach (Point currentPoint in grid)
                {
                    forHookeJeeves.startPoint = currentPoint;
                    Point resultOfAlgo = hooke_Jeeves.runAlgorithmWithGivenParameters();
                    arrayOfResults.Add(resultOfAlgo);
                }

                minPointOfHooke_Jeeves = findMinimumPoint(arrayOfResults);
            }

            Console.WriteLine("Лучшее решение полученное алгоритмом: " + minPointOfHooke_Jeeves);
        }
    }
}
