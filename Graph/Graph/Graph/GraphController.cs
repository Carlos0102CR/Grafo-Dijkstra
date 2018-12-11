namespace Graph
{
    public class GraphController
    {
        private static Graph graph;

        public GraphController()
        {
            graph = new Graph();
        }

        public double[,] getAdyacentNodes(double x, double y)
        {
            return graph.getAdyacentNodes(x,y);
        }

        public double[,] findPath(double xOrigin, double yOrigin, double xDestiny, double yDestiny)
        {
            return graph.findPath(xOrigin, yOrigin, xDestiny, yDestiny);
        }
    }
}