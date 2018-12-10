using System.Collections.Generic;

namespace Graph
{
    public class Node
    {
        private double DataX;
        private double DataY;

        public Node()
        {
        }

        public Node(double x, double y)
        {
            DataX = x;
            DataY = y;

        }

        public double DataX1
        {
            get => DataX;
            set => DataX = value;
        }

        public double DataY1
        {
            get => DataY;
            set => DataY = value;
        }
    }
}