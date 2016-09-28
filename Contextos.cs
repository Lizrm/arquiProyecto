using System;
using System.Collections;

public class Contextos
{
    private struct Contexto // C# mantiene los struct
    {
    	int pc;    	
    	int[] regist = new int[32];
       
        public Contexto(int p, int[] reg)
        {
            pc = p;   
            for(int i = 1; i <32; ++i)
	        {
	            nueva->regist[i] = reg[i];
	        }
        }
    }

    static public void Contextos() 
    {
        Queue cola = new Queue();
    }
	
	~Contextos(){}	//Destructor de la clase

   
    //reg se debe recibir por referencia 
    static public void Guardar(int p, int[] reg)//Guarda el contexto         
	{
	    Contexto nueva  = new Contexto(p, reg);
        cola.Enqueue(nueva);
	
	}//FIN de Guardar

    //recibir todo por referencia
    static public void Sacar(int p, int[] reg) //Retorna el contexto
    {
        Contexto aux = cola.Dequeue();
        p = aux.pc;
        for (int i = 1; i < 32; ++i)
        {
            reg[i] = aux.regist[i];
        }
    }//FIN de Sacar
	
} //FIN de la clase