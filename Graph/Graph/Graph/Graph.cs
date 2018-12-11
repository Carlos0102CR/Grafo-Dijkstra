using System;
using System.Collections.Generic;
using System.IO;

namespace Graph
{
    public class Graph
    {
        private static List<Node> vertices;

        private static int[,] adjMatrix;
        private int graphSize = 25;
        private static int[,] pathList;
        private StreamReader srNodes;
        private StreamReader srMatrix;
        private const int infinity = 9999;

        public Graph()
        {
            vertices = new List<Node>(graphSize);
            srNodes = new StreamReader("nodes.txt");
            srMatrix = new StreamReader("matrix.txt");
            createGraph();
        }

        private void createGraph()
        {
            adjMatrix = new int[graphSize, graphSize];
            fillNodes();
            fillMatrix();
        }

        private void fillNodes()
        {
            using (srNodes)
            {
                string line;
                string[] splitLine;

                for (int i = 0; i < graphSize; i++)
                {
                    line = srNodes.ReadLine();
                    splitLine = line.Split(' ');
                    vertices.Add(new Node(float.Parse(splitLine[0]), float.Parse(splitLine[1])));
                }
            }
        }

        private void fillMatrix()
        {
            using (srMatrix)
            {
                string line;
                string[] splitLine;

                for (int i = 0; i < graphSize; i++)
                {
                    line = srMatrix.ReadLine();
                    splitLine = line.Split(' ');
                    for (int j = 0; j < graphSize; j++)
                    {
                        adjMatrix[i, j] = Int32.Parse(splitLine[j]);
                    }
                }
            }
        }

        public double[,] findPath(double xOrigin, double yOrigin, double xDestiny, double yDestiny)
        {
            int[] resltInd = new int[2];
            bool end = false;
            string rute = "";

            resltInd[0] = vertices.FindIndex(node => node.DataX1.Equals(xOrigin) && node.DataY1.Equals(yOrigin));
            resltInd[1] = vertices.FindIndex(node => node.DataX1.Equals(xDestiny) && node.DataY1.Equals(yDestiny));

            if (resltInd[0] == -1 || resltInd[1] == -1)
            {
                Console.WriteLine("Algunos de los datos solicitados no existen.");
                return null;
            }

            runDijkstra(resltInd);
            double[,] nodePaths = getPathFromList(resltInd);
            

            return nodePaths;//Return a Matrix with the xData/yData/weight of each of the Nodes in te path
        }

        private double[,] getPathFromList(int[] resltInd)
        {
            int actNode = resltInd[1];
            int size = 0;
            double[,] nodePaths = new double[graphSize, 3];
            double[,] sortedNodePaths;
            int j = 0;
            
            for (int i = 0; i < graphSize; i++)
            {
                nodePaths[i, 0] = vertices[actNode].DataX1;
                nodePaths[i, 1] = vertices[actNode].DataX1;
                nodePaths[i, 2] = pathList[actNode,1];
                if (actNode == resltInd[0])
                {
                    size = i;
                    break;
                }

                actNode = pathList[actNode, 0];
            }

            sortedNodePaths = new double[size+1,3];
            
            for (int i = size; i >= 0; i--)
            {
                sortedNodePaths[j,0] = nodePaths[i,0];
                sortedNodePaths[j,1] = nodePaths[i,1];
                sortedNodePaths[j,2] = nodePaths[i,2];
                j++;
            }
            
            return sortedNodePaths;
        }

        public double[,] getAdyacentNodes(double x, double y)
        {
            int index;
            int size = 0;
            double[,] adyacentNodes = new double[graphSize, 3];
            double[,] fixedAdyacentNodes;

            index = vertices.FindIndex(node => node.DataX1.Equals(x) && node.DataY1.Equals(y));
            for (int i = 0; i < graphSize; i++)
            {
                if (adjMatrix[index, i] != 0)
                {
                    adyacentNodes[size, 0] = vertices[i].DataX1;
                    adyacentNodes[size, 1] = vertices[i].DataY1;
                    adyacentNodes[size, 2] = adjMatrix[index, i];
                    size++;
                }
            }
            fixedAdyacentNodes = new double[size,3];
            for (int i = 0; i < size; i++)
            {
                fixedAdyacentNodes[i, 0] = adyacentNodes[i, 0];
                fixedAdyacentNodes[i, 1] = adyacentNodes[i, 1];
                fixedAdyacentNodes[i, 2] = adyacentNodes[i, 2];
            }

            return fixedAdyacentNodes;
        }

        private void runDijkstra(int[] resltInd) //Runs Algorithm on the adjacency matrix
        {
            pathList = new int[graphSize, 3]; //Save the shortest path of each node/ index,previousIndex,weight
            int actInd = resltInd[0];
            int nextInd = 0;
            int actWeight = 0;
            int weight = 0;
            int nextWeight = 0;
            bool end = false;

            for (int i = 0; i < graphSize; i++)
            {
                pathList[i, 1] = infinity;
            }

            do
            {
                pathList[actInd, 2] = 1;
                for (int i = 0; i < graphSize; i++)
                {
                    if (i == resltInd[0])
                    {
                        pathList[i, 1] = 0;
                        i++;
                    }

                    if (adjMatrix[actInd, i] != 0)
                    {
                        weight = actWeight + adjMatrix[actInd, i];
                        if (pathList[i, 1] > weight)
                        {
                            pathList[i, 0] = actInd;
                            pathList[i, 1] = weight;
                        }
                    }
                }

                nextWeight = infinity;
                for (int i = 0; i < graphSize; i++)
                {
                    if (pathList[i, 2] == 0)
                    {
                        if (pathList[i, 1] > 0 && pathList[i, 1] < infinity && nextWeight == 0)
                        {
                            nextWeight = pathList[i, 1];
                            nextInd = i;
                        }
                        else if (pathList[i, 1] < nextWeight || pathList[i, 1] == nextWeight)
                        {
                            nextWeight = pathList[i, 1];
                            nextInd = i;
                        }
                    }
                }

                if (actInd == resltInd[1])
                {
                    end = true;
                }

                actWeight = nextWeight;
                actInd = nextInd;
            } while (!end);
        }
    }
}