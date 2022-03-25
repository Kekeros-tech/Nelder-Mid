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

        public static double bootFunction(Point currentPoint)
        {
            double x1 = currentPoint.getValueByIndex(0);
            double x2 = currentPoint.getValueByIndex(1);
            return Math.Pow(x1 + 2 * x2 - 7, 2) + Math.Pow(2 * x1 + x2 - 5, 2);
        }

        static void Main(string[] args)
        {
            //Nelder-Mid
            int dimension = 2; // размерность
            FunctionOfAlgo function = new FunctionOfAlgo(rosenbrockFunction); // функция для оптимизации
            double reflection = 1; //коэф отражения
            double stretching = 2; //коэф растяжения
            double compression = 0.5f; //коэф сжатия
            double accuracy = 0.000000000001f; //заданная точность
            double constriction = 2; // сжатие многогранника
            ControlParametrs parametrs = new ControlParametrs(dimension,function,reflection, stretching, compression,accuracy,constriction);

            //задание начального симплекса
            double[] valuesOfStartPoint = { -1.2f, 1 };
            Nelder_Mid nelder_Mid = new Nelder_Mid(valuesOfStartPoint, 2, parametrs);

            Console.WriteLine("Лучшее значение функции полученное алгоритмом: " + nelder_Mid.runAlgorithmWithGivenParameters());

            //Hooke-Jeeves
            double[] startingValues = { -1.2f, 1 };
            double[] deltaValues = { 0.0004f, 0.0004f };
            double stepValue = 2.8f;
            double accuracyForHookeJeeves = 0.7f;
            ControlParametrsForHookeJeeves controlParametrs = new ControlParametrsForHookeJeeves(deltaValues, stepValue, accuracyForHookeJeeves, function);

            Hooke_Jeeves hooke_Jeeves = new Hooke_Jeeves(valuesOfStartPoint, controlParametrs);

            Console.WriteLine("Лучшее решение полученное алгоритмом: " + hooke_Jeeves.runAlgorithmWithGivenParameters());
        }
    }
}
