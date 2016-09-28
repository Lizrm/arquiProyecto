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
        private Queue cola;
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
        }

        Contextos()
        {
            cola = new Queue();
        }

        ~Contextos() { }    //Destructor de la clase


        //reg se debe recibir por referencia 
        public void Guardar(int p, int[] reg)//Guarda el contexto         
        {
            Contexto nueva = new Contexto(p, reg);
            cola.Enqueue(nueva);

        }//FIN de Guardar

        //recibir todo por referencia
        public void Sacar(int p, int[] reg)//Retorna el contexto
        {
            Contexto aux = (Contexto)cola.Dequeue();
            p = aux.pc;
            for (int i = 1; i < 32; ++i)
            {
                reg[i] = aux.regist[i];
            }
        }//FIN de Sacar

    }//FIN de la clase
}
