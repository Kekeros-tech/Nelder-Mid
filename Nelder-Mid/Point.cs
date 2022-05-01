using System;
using System.Collections.Generic;
using System.Text;

namespace Nelder_Mid
{
    class Point
    {
        private List<double> valueVector;
        private double functionValue;

        public Point(double[] currentValues, FunctionOfAlgo function)
        {
            valueVector = new List<double>(currentValues);
            functionValue = function(this);
        }

        public Point(Point currentPoint, FunctionOfAlgo function)
        {
            valueVector = new List<double>(currentPoint.valueVector);
            functionValue = function(this); 
        }

        public Point(Point currentPoint)
        {
            valueVector = new List<double>(currentPoint.valueVector);
            functionValue = double.MaxValue;
        }

        public Point(double[] currentValues)
        {
            valueVector = new List<double>(currentValues);
        }

        public double FunctionValue
        {
            get => functionValue;
            set => functionValue = value;
        }

        public double[] ValueVectorToArray
        {
            get => valueVector.ToArray();
        }

        public void setFunctionValue(FunctionOfAlgo function)
        {
            functionValue = function(this);
        }

        public Point(Point[] points, int count)
        {
            valueVector = new List<double>();
            for(int i = 0; i < points[0].size(); i++)
            {
                double sum = 0;
                for (int j = 0; j < count; j++)
                {
                    sum += points[j].getValueByIndex(i);
                }
                valueVector.Add(sum);
            }
        }

        public void addValue(double value)
        {
            valueVector.Add(value);
        }

        public static bool twoPointsMatch(Point point1, Point point2)
        {
            bool result = true;
            if(point1.functionValue == point2.functionValue)
            {
                for(int i = 0; i < point1.size(); i++)
                {
                    if(point1.valueVector[i] != point2.valueVector[i])
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                return false;
            }
            return result;
        }

        public double getValueByIndex(int index)
        {
            return valueVector[index];
        }

        public int size()
        {
            return valueVector.Count;
        }

        public Point getNewPointDelta(double delta, int index, FunctionOfAlgo function)
        {
            double[] buffer = valueVector.ToArray();
            buffer[index] += delta;
            return new Point(buffer, function);
        }

        public static Point operator +(Point point1, Point point2)
        {
            List<double> buffer = new List<double>(point1.valueVector);
            for (int i = 0; i < point2.size(); i++)
            {
                buffer[i] += point2.getValueByIndex(i);
            }
            return new Point(buffer.ToArray());
        }

        public static Point operator *(Point point1, double stepValue)
        {
            List<double> buffer = new List<double>(point1.valueVector);
            for (int i = 0; i < point1.size(); i++)
            {
                buffer[i] *= stepValue;
            }
            return new Point(buffer.ToArray());
        }


        public static bool operator <=(Point point1, Point point2)
        {
            return point1.functionValue <= point2.functionValue;
        }

        public static bool operator >=(Point point1, Point point2)
        {
            return point1.functionValue >= point2.functionValue;
        }

        public static bool operator <(Point point1, Point point2)
        {
            return point1.functionValue < point2.functionValue;
        }

        public static bool operator >(Point point1, Point point2)
        {
            return point1.functionValue > point2.functionValue;
        }

        public static Point operator /(Point point1, double valueOfDivision)
        {
            List<double> buffer = new List<double>(point1.valueVector);
            for (int i = 0; i < point1.size(); i++)
            {
                buffer[i] /= valueOfDivision;
            }
            return new Point(buffer.ToArray());
        }

        public static Point operator -(Point point1, Point point2)
        {
            List<double> buffer = new List<double>(point1.valueVector);
            for (int i = 0; i < point1.size(); i++)
            {
                buffer[i] -= point2.getValueByIndex(i);
            }
            return new Point(buffer.ToArray());
        }

        public static double operator *(Point point1, Point point2)
        {
            double buffer = 0;
            for (int i = 0; i < point1.size(); i++)
            {
                buffer += point1.getValueByIndex(i) * point2.getValueByIndex(i);
            }
            return buffer;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("F(");
            /*foreach (float value in valueVector)
            {
                sb.Append(value);
                sb.Append(", ");
            }*/
            for(int i=0; i < valueVector.Count - 1; i++)
            {
                sb.Append(valueVector[i]);
                sb.Append(", ");
            }
            sb.Append(valueVector[valueVector.Count - 1]);
            sb.Append(") = ");
            sb.Append(functionValue);

            return sb.ToString();
        }

        public static Point unitPointWithPosition(int position, int pointSize)
        {
            double[] buffer = new double[pointSize];
            buffer[position] = 1;
            return new Point(buffer);
        }
    }
}
