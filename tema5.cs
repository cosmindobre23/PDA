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
            using ( new MPI.Environment(ref args))
            {
                Random rand = new Random();
                int number = rand.Next(100, 1000);
                int pnr = Communicator.world.Size; //nr procese
                var com = Communicator.world; 
                int rnr = com.Rank; //proc rank
                if (rnr != 0)
                {
                    com.Send(number, 0, rnr);
                }
                else
                {
                    int max = number, rnk = 0;
                    int value = 0;
                    for (int i = 1; i < pnr; i++)
                    {
                        value = com.Receive<int>(i, i);
                        if (max < value)
                        {
                            max = value;
                            rnk = i;
                        }
                        else if (max == value)
                        {
                            rnk = i;
                        }
                    }
                    Console.WriteLine("Master este procesul nr {0} cu numarul {1}", rnk, max);
                }
            }
        }
    }
}
