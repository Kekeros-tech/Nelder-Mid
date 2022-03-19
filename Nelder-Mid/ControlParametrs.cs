using System;
using System.Collections.Generic;
using System.Text;

namespace Nelder_Mid
{
    class ControlParametrs
    {
        private float reflection;
        private float stretching;
        private float compression;

        public float Reflection
        {
            get => reflection;
            set => reflection = value;
        }

        public float Stretching
        {
            get => stretching;
            set => stretching = value;
        }

        public float Compression
        {
            get => compression;
            set => compression = value;
        }

        public ControlParametrs(float reflection, float stretching, float compression)
        {
            this.reflection = reflection;
            this.stretching = stretching;
            this.compression = compression;
        }
    }
}
