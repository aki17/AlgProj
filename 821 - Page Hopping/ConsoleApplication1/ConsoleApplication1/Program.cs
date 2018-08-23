using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {

        public static int avglenght()
        {
            return 0;
        }   

        public static void printPageHopTime(int[,] matrix,int caseno)
        {
            var adjmatrix = matrix;
            int n = adjmatrix.GetLength(0);
            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                            adjmatrix[i, j]  = Math.Min(adjmatrix[i, j], adjmatrix[i, k] + adjmatrix[k, j]);

            
            double pathlength = 0,totallenght = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (i != j && adjmatrix[i, j] != 10000)

                    {
                        pathlength++;
                        totallenght += adjmatrix[i, j];
                    }
            double result = totallenght / pathlength;
            Console.WriteLine("Case {0}: average length between pages = {1} clicks", caseno, result.ToString("#.000"));;
        } 

        public static int[,] bAdjMatrix(int[] line)
        {
            int n = line.Max()+1;
            int[,] adjmatrix = new int[n,n] ;
            for(int i=0;i<n;i++)
                for(int j=0;j<n;j++)
                {
                    if(i==j)
                       adjmatrix[i,j] = 0;
                    else
                       adjmatrix[i,j] = 10000;
                }

            for (int i=0;i<line.Length;i=i+2)
            {
                adjmatrix[line[i],line[ i + 1]] = 1;
            }
         
            return adjmatrix;
        }

        public static int[] getline(string[] line)
        {
            if(line[0] == "0" && line[1] == "0")
            {
                int[] a = { 0, 0 };
                return a;
            }
            int[] linev = new int[line.Length-2];
            for (int i=0;i<line.Length;i++)
            {
                if (int.Parse(line[i]) != 0)
                {
                    linev[i] = int.Parse(line[i]);
                }
            }
            return linev;
        }

        static void Main(string[] args)
        {
            // read the lines of the file
            string[] reader = File.ReadAllLines("input.txt");
            // number of lines 
            int n = reader.Length;
            int caseno = 1;
           
		    // parcurgem linie cu linie 
            for (int i=0;i<n;i++)
            {
                // get the current line and create the adjacency matrix
                var currentline = getline(reader[i].Split(' '));
                int[,] adjmatrix;
				
                // if we are not at the end of the file we call the function to calculate the click's duration
                if (currentline[0] != 0  && currentline[1] != 0 )
                {
                    // adjacency matrix
                    adjmatrix =   bAdjMatrix(currentline);
                    // functia pentru a calcula timpul necesar(trimitem ca parametri matricea de adiacenta si numarul cazului
                    printPageHopTime(adjmatrix,caseno);
                    // increment by 1 starting from 1  
                    caseno++;
                }
               
            }
            Console.ReadLine();     
        }
    }
}
