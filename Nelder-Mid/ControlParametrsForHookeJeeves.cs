using System;
using System.Collections.Generic;
using System.Text;

namespace Nelder_Mid
{
    class ControlParametrsForHookeJeeves
    {
        private List<double> controlVector;
        private double stepValue;
        private double accuracy;
        private FunctionOfAlgo function;

        public double Accuracy
        {
            get => accuracy;
            set => accuracy = value;
        }

        public FunctionOfAlgo Function
        {
            get => function;
            set => function = value;
        }

        public ControlParametrsForHookeJeeves(double[] currentValues, double stepValue, double accuracy, FunctionOfAlgo function)
        {
            controlVector = new List<double>(currentValues);
            this.stepValue = stepValue;
            this.accuracy = accuracy;
            this.function = function;
        }

        public double getDeltaByIndex(int index)
        {
            return controlVector[index];
        }

        public double getStepValue()
        {
            return stepValue;
        }
    }
}
