using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPI;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                int sum = 0;
                int[] v= { 1,2,3,4,5,6,7,8,9,10};
                int pnr = Communicator.world.Size;
                var com = Communicator.world;
                int rnr = com.Rank;
                if (rnr != 0)
                {

                    for(int i=rnr; i<10; i = i + pnr - 1)
                    {
                        sum += v[i];
                    }
                    com.Send(sum, 0, rnr);
                }
                else
                {
                    var value = 0;
                    for (int i = 1; i < pnr; i++)
                    { 
                        value = com.Receive<int>(i, i);
                        sum += value;
                    }
                    Console.WriteLine("suma este {0}", sum);
                }
            }
        }
    }
}
