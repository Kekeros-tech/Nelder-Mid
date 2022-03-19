using System;
using System.Collections.Generic;

namespace Nelder_Mid
{
    class Program
    {
        public static double function(Point currentPoint)
        {
            float x1 = currentPoint.getValueByIndex(0);
            float x2 = currentPoint.getValueByIndex(1);
            return 100 * Math.Pow((x2 - Math.Pow(x1, 2)), 2) + Math.Pow((1 - x1), 2);
        }

        static Point[] setiInitialVectors(Point startPoint, float l, int dimension)
        {
            List<Point> currentArray = new List<Point>();
            currentArray.Add(startPoint);
            for(int i = 0; i < dimension; i++)
            {
                Point nextPoint = startPoint + Point.unitPointWithPosition(i, startPoint.size()) * l;
                currentArray.Add(nextPoint);
            }
            return currentArray.ToArray();
        }

        static bool targetAccuracyReached(float accuracy, Point[] pointArray)
        {
            double sum = 0;
            for(int i = 1; i < pointArray.Length; i++)
            {
                double a = function(pointArray[i]);
                double b = function(pointArray[0]);
                sum += Math.Pow(a - b , 2);
            }
            double result = Math.Sqrt(sum / (pointArray.Length - 1));
            return (result - accuracy) < 0;
        }

        static void Main(string[] args)
        {
            int dimension = 2;
            float[] valuesOfStartPoint = { -1.2f, 1 };
            float scalar = 2;
            Point startPoint = new Point(valuesOfStartPoint);
            Point[] pointArray = setiInitialVectors(startPoint, scalar, dimension);
            Array.Sort(pointArray, new PointComparer());

            float reflection = 1; //коэф отражения
            float stretching = 2; //коэф растяжения
            float compression = 0.5f; //коэф сжатия
            float accuracy = 0.0000001f; //заданная точность
            ControlParametrs controlParametrs = new ControlParametrs(reflection, stretching, compression);

            while (!targetAccuracyReached(accuracy, pointArray))
            {
                Point centerOfGravity = new Point(pointArray, dimension) / dimension;
                Point pointOfReflection = centerOfGravity + (centerOfGravity - pointArray[dimension]) * controlParametrs.Reflection;

                if (function(pointArray[0]) <= function(pointOfReflection)
                    && function(pointOfReflection) <= function(pointArray[dimension - 1]))
                {
                    pointArray[dimension] = pointOfReflection;
                    continue;
                }
                else if (function(pointOfReflection) < function(pointArray[0]))
                {
                    Point pointOfStretching = centerOfGravity + (pointOfReflection - centerOfGravity) * controlParametrs.Stretching;
                    if (function(pointOfStretching) < function(pointOfReflection))
                    {
                        pointArray[dimension] = pointOfStretching;
                    }
                    else
                    {
                        pointArray[dimension] = pointOfReflection;
                    }
                }
                else if (function(pointOfReflection) > function(pointArray[dimension - 1]))
                {
                    Point pointOfCompression = centerOfGravity + (pointArray[dimension] - centerOfGravity) * controlParametrs.Compression;
                    if(function(pointOfReflection) < function(pointArray[dimension]))
                    {
                        pointOfCompression = centerOfGravity + (pointOfReflection - centerOfGravity) * controlParametrs.Compression;
                    }

                    if(function(pointOfCompression) < Math.Min(function(pointArray[dimension]), function(pointOfReflection)))
                    {
                        pointArray[dimension] = pointOfCompression;
                    }
                    else
                    {
                        for(int i = 1; i < pointArray.Length; i++)
                        {
                            pointArray[i] = (pointArray[0] + pointArray[i]) / 2; 
                        }
                    }
                }
                Array.Sort(pointArray, new PointComparer());
                Console.WriteLine("---");
                foreach (Point point in pointArray)
                {
                    Console.WriteLine(point);
                    Console.WriteLine(function(point));
                }
            }
            Console.WriteLine();
            Console.WriteLine("Лучший результат" + pointArray[0]);
            Console.WriteLine("Значение в этой точке: " + function(pointArray[0]));
        }
    }
}
