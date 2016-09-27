using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static int reloj = 0;    //debe ser estatico para que la barrera lo pueda modificar
        static int []vector;
        static int quantum;

        static Barrier barrier = new Barrier(3, (bar) =>  //Pueda que tenga que cambiarlo al inicio del programa
        {
            reloj++;        // solo se ejecuta una vez
           // Console.WriteLine(reloj); //Escribir en consola
           // Console.ReadLine();        // para esperar y visualizar los datos
        });

        static void Main(string[] args)
        {
            
            Console.WriteLine("Quantum"); //Escribir en consola
            quantum = int.Parse(Console.ReadLine());
            vector = new int[4];
            //int numThreads = 3;

          

            Thread my1 = new Thread(() => Nucleos(quantum));
            my1.Name = String.Format("{0}", 1);
            my1.Start();

            Thread my2 = new Thread(() => Nucleos(quantum));
            my2.Name = String.Format("{0}", 2);
            my2.Start();

            Thread my3 = new Thread(() => Nucleos(quantum));
            my3.Name = String.Format("{0}", 3);
            my3.Start();
        }

        static void Nucleos(int q) //recibe un int // debe ser estatic
        {
            //TicdeReloj();
            if(Monitor.TryEnter(quantum))
            {              
                Console.WriteLine("primero");
                Console.WriteLine(Thread.CurrentThread.Name); //Escribir en consola             
                // Ensure that the lock is released.
                Monitor.Exit(quantum);                
            }
            else 
            {
                if (Monitor.TryEnter(vector[1]))
                {
                    Console.WriteLine("segundo");
                    Console.WriteLine(Thread.CurrentThread.Name); //Escribir en consola      
                }
            }
            TicdeReloj();
            //Console.ReadLine();        // para esperar y visualizar los datos
            //Thread.CurrentThread.Name.Equals("2")

        }

        static void TicdeReloj()
        {
            barrier.SignalAndWait();
        }
    }
}