using System;
using System.Collections.Generic;
using System.Text;

namespace Nelder_Mid
{
    class PointComparer: IComparer<Point>
    { 
        public int Compare(Point a, Point b)
        {
            
            if(a.FunctionValue > b.FunctionValue)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
