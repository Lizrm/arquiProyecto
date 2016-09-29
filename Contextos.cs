
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MultiThread
{
    public class Contextos
    {
        private static Queue cola;
        private struct Contexto // C# mantiene los struct
        {
            public int pc;
            public int[] regist;

            public Contexto(int p, int[] reg)
            {
                pc = p;
                regist = new int[32];
                for (int i = 1; i < 32; ++i)
                {
                    regist[i] = reg[i];
                }
            }

            public Contexto(int p)
            {
                pc = p;
                regist = new int[32];
                for (int i = 1; i < 32; ++i)
                {
                    regist[i] = 0;
                }
            }
        }

        public Contextos()
        {
            cola = new Queue();
        }

        ~Contextos() //Destructor de la clase
        {
            //cola.Finalize();
        }


        //reg se debe recibir por referencia 
        public void Guardar(int p, ref int[] reg)//Guarda el contexto         
        {
            Contexto nueva = new Contexto(p, reg);
            cola.Enqueue(nueva);

        }//FIN de Guardar

        //recibir todo por referencia
        public void Sacar(out int p, ref int[] reg)//Retorna el contexto
        {
            Contexto aux = (Contexto)cola.Dequeue();
            for (int i = 1; i < 32; ++i)
            {
                reg[i] = aux.regist[i];
            }
            p = aux.pc;
        }//FIN de Sacar

        public void Encolar(int p)
        {
            Contexto nueva = new Contexto(p);
            cola.Enqueue(nueva);
        }//FIN de Encolar

        public int Cantidad()
        {
            return cola.Count;
        }//FIN de cantidad

    }//FIN de la clase Contextos
}//FIN del namespace
