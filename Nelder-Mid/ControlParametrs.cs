using System;
using System.Collections.Generic;
using System.Text;

namespace Nelder_Mid
{
    class OptimizationBoundary
    {
        public double[] bottomLine;
        public double[] topLine;

        public OptimizationBoundary(double[] bottomLine, double[] topLine)
        {
            this.bottomLine = bottomLine;
            this.topLine = topLine;
        }
    }

    class ControlParametrs
    {
        private int dimension;
        private FunctionOfAlgo function;
        private double reflection;
        private double stretching;
        private double compression;
        private double accuracy;
        private double constriction;
        public double[] valuesOfStartPoint;
        public OptimizationBoundary optimizationBoundary;
        

        public int Dimension
        {
            get => dimension;
            set => dimension = value;
        }

        public double Accuracy
        {
            get => accuracy;
            set => accuracy = value;
        }

        public double Constriction
        {
            get => constriction;
            set => constriction = value;
        }

        public double Reflection
        {
            get => reflection;
            set => reflection = value;
        }

        public double Stretching
        {
            get => stretching;
            set => stretching = value;
        }

        public double Compression
        {
            get => compression;
            set => compression = value;
        }

        public FunctionOfAlgo Function
        {
            get => function;
            set => function = value;
        }

        public ControlParametrs(int dimension, FunctionOfAlgo function,double reflection,
            double stretching, double compression, double accuracy, double constriction, 
            double[] valuesOfStartPoint, OptimizationBoundary optimizationBoundary)
        {
            this.dimension = dimension;
            this.function = function;
            this.reflection = reflection;
            this.stretching = stretching;
            this.compression = compression;
            this.accuracy = accuracy;
            this.constriction = constriction;
            this.valuesOfStartPoint = valuesOfStartPoint;
            this.optimizationBoundary = optimizationBoundary;
        }
    }
}
