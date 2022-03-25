using System;
using System.Collections.Generic;
using System.Text;

namespace Nelder_Mid
{
    class Nelder_Mid
    {
        private ControlParametrs parametrs;
        private Point[] pointArray;

        public Nelder_Mid(double[] valuesOfStartPoint, double scalar, ControlParametrs parametrs)
        {
            this.parametrs = parametrs;
            pointArray = setInitialSimplex(valuesOfStartPoint, scalar);
        }

        public Point[] setInitialSimplex(double[] valuesOfStartPoint, double scalar)
        {
            Point startPoint = new Point(valuesOfStartPoint, parametrs.Function);

            List<Point> currentArray = new List<Point>();
            currentArray.Add(startPoint);

            for (int i = 0; i < startPoint.size(); i++)
            {
                Point nextPoint = new Point(startPoint + Point.unitPointWithPosition(i, startPoint.size()) * scalar, parametrs.Function);
                currentArray.Add(nextPoint);
            }
            currentArray.Sort(new PointComparer());
            return currentArray.ToArray();
        }

        public bool targetAccuracyReached()
        {
            double sum = 0;
            for (int i = 1; i < pointArray.Length; i++)
            {
                double a = pointArray[i].FunctionValue;
                double b = pointArray[0].FunctionValue;
                sum += Math.Pow(a - b, 2);
            }
            double result = Math.Sqrt(sum / (pointArray.Length - 1));
            return (result - parametrs.Accuracy) < 0;
        }

        public Point getValueOfLastPointByPointOfStretching(Point pointOfReflection, Point centerOfGravity)
        {
            Point pointOfStretching = new Point(centerOfGravity + (pointOfReflection - centerOfGravity) * parametrs.Stretching, parametrs.Function);
            if (pointOfStretching < pointOfReflection)
            {
                return pointOfStretching;
            }
            return pointOfReflection;
        }

        public Point getValueOfLastPointByPointOfCompression(Point pointOfReflection, Point centerOfGravity, Point worstPoint)
        {
            Point pointOfCompression = setPointOfCompression(pointOfReflection, centerOfGravity, worstPoint);

            if (pointOfCompression.FunctionValue < Math.Min(worstPoint.FunctionValue, pointOfReflection.FunctionValue))
            {
                return pointOfCompression;
            }
            return null;
        }

        public Point setPointOfCompression(Point pointOfReflection, Point centerOfGravity, Point worstPoint)
        {
            Point pointOfCompression = new Point(centerOfGravity + (worstPoint - centerOfGravity) * parametrs.Compression, parametrs.Function);
            if (pointOfReflection < worstPoint)
            {
                pointOfCompression = new Point(centerOfGravity + (pointOfReflection - centerOfGravity) * parametrs.Compression, parametrs.Function);
            }
            return pointOfCompression;
        }

        public Point assignValueToLastPoint()
        {
            Point centerOfGravity = new Point(new Point(pointArray, parametrs.Dimension) / parametrs.Dimension, parametrs.Function);
            Point pointOfReflection = new Point(centerOfGravity + (centerOfGravity - pointArray[parametrs.Dimension]) * parametrs.Reflection, parametrs.Function);
            Point bestPoint = pointArray[0];
            Point penultimatePoint = pointArray[parametrs.Dimension - 1];
            Point worstPoint = pointArray[parametrs.Dimension];

            if (bestPoint <= pointOfReflection && pointOfReflection <= penultimatePoint)
            {
                return pointOfReflection;
            }

            if (pointOfReflection < bestPoint)
            {
                return getValueOfLastPointByPointOfStretching(pointOfReflection, centerOfGravity);
            }

            if (pointOfReflection > penultimatePoint)
            {
                return getValueOfLastPointByPointOfCompression(pointOfReflection, centerOfGravity, worstPoint);
            }

            return null;
        }

        public void reducePolygon()
        {
            for (int i = 1; i < pointArray.Length; i++)
            {
                pointArray[i] = new Point((pointArray[0] + pointArray[i]) / parametrs.Constriction, parametrs.Function);
            }
        }

        public void formNewPointArray(Point resultOfAlgo)
        {
            if (resultOfAlgo != null)
            {
                pointArray[parametrs.Dimension] = resultOfAlgo;
            }
            else
            {
                reducePolygon();
            }
            Array.Sort(pointArray, new PointComparer());
        }

        public void printCurrentPointArray()
        {
            Console.WriteLine("---");
            foreach (Point point in pointArray)
            {
                Console.WriteLine(point);
            }
        }

        public Point runAlgorithmWithGivenParameters()
        {
            while (!targetAccuracyReached())
            {
                Point resultOfAlgo = assignValueToLastPoint();

                formNewPointArray(resultOfAlgo);

                //printCurrentPointArray();
            }
            return pointArray[0];
        }
    }
}
