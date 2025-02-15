﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nelder_Mid
{
    class Hooke_Jeeves
    {
        ControlParametrsForHookeJeeves parametrs;
        Point currentPoint;


        public Hooke_Jeeves(ControlParametrsForHookeJeeves parametrs)
        {
            //currentPoint = parametrs.startPoint;
            this.parametrs = parametrs;
        }

        public bool betterAccuracy()
        {
            return currentPoint.FunctionValue < parametrs.Accuracy;
        }

        public bool neighboringPointIsBetter(Point currentPoint, double delta, int index)
        {
            return currentPoint.getNewPointDelta(delta, index, parametrs.Function) < currentPoint;
        }

        public void printResultOfPoint(Point currentPoint, string pointInformation)
        {
            Console.WriteLine(pointInformation + currentPoint);
            Console.WriteLine("|Значение в этой точке: " + parametrs.Function(currentPoint));
        }

        public Point getPointAfterDelta()
        {
            Point buffer = new Point(currentPoint.ValueVectorToArray, parametrs.Function);
            for (int i = 0; i < buffer.size(); i++)
            {
                if (neighboringPointIsBetter(buffer, parametrs.getDeltaByIndex(i), i))
                {
                    buffer = new Point(buffer.getNewPointDelta(parametrs.getDeltaByIndex(i), i, parametrs.Function), parametrs.Function);
                    continue;
                }

                if (neighboringPointIsBetter(buffer, -parametrs.getDeltaByIndex(i), i))
                {
                    buffer = new Point(buffer.getNewPointDelta(-parametrs.getDeltaByIndex(i), i, parametrs.Function), parametrs.Function);
                    continue;
                }
            }
            
            return buffer;
        }

        public bool locatedWithin(Point nextPoint)
        {
            if(parametrs.optimizationBoundary != null)
            {
                for (int i = 0; i < nextPoint.size(); i++)
                {
                    if (nextPoint.getValueByIndex(i) < parametrs.optimizationBoundary.bottomLine[i]
                                || nextPoint.getValueByIndex(i) > parametrs.optimizationBoundary.topLine[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Point getNextPoint(Point pointAfterDelta)
        {
            Point nextPoint = new Point(currentPoint + (pointAfterDelta - currentPoint) * parametrs.getStepValue(), parametrs.Function);
            if(nextPoint < pointAfterDelta)
            {
                if(!locatedWithin(nextPoint))
                {
                    nextPoint = new Point(nextPoint);
                }
                return nextPoint;
            }
            return pointAfterDelta;
        }

        public Point runAlgorithmWithGivenParameters()
        {
            currentPoint = parametrs.startPoint;

            while (!betterAccuracy())
            {
                Point buffer = getPointAfterDelta();

                Point nextPoint = getNextPoint(buffer);
                
                if(Point.twoPointsMatch(nextPoint, currentPoint))
                {
                    break;
                }

                currentPoint = nextPoint;
            }
            return currentPoint;
        }
    }
}
