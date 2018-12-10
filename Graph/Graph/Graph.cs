using System;
using System.Collections.Generic;
using System.IO;

namespace Graph
{
    public class Graph
    {
        /// <summary>
        /// 4 attributes
        /// A list of vertices (to store node information for each index such as name/text)
        /// a 2D array - our adjacency matrix, stores edges between vertices
        /// a graphSize integer
        /// a StreamReader, to read in graph data to create the data structure
        /// </summary>
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

        public void findPath(double xOrigin, double yOrigin, double xDestiny, double yDestiny)
        {
            int[] resltInd = new int[2];

            resltInd[0] = vertices.FindIndex(node => node.DataX1 == xOrigin && node.DataY1 == yOrigin);
            resltInd[1] = vertices.FindIndex(node => node.DataX1 == xDestiny && node.DataY1 == yDestiny);

            if (resltInd[0] == -1 || resltInd[1] == -1)
            {
                Console.WriteLine("Algunos de los datos solicitados no existe.");
                return;
            }

            runDijkstra(resltInd);
        }

        void sortPathList()
        {
            
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