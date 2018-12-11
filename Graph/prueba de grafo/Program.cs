using System;
using Graph;

namespace prueba_de_grafo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GraphController grafito = new GraphController();
            grafito.findPath(10.010000228881836,10.010000228881836,70.069999694824219,70.069999694824219);
            grafito.getAdyacentNodes(10.010000228881836,10.010000228881836);
           
        }
    }
} 