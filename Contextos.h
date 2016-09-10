#ifndef CONTEXTOS_H
#define CONTEXTOS_H

class Contextos
{
    struct contexto
    {
    	int pc;     //
    	int  regist[33];//Semaforo asociado al Thread
    	contexto * siguiente;
    };

public:
	
	//Constructor de la clase
	Contextos(int hilillos)
	{
	    n = 0;
	} 
	
	~Contextos(){}	//Destructor de la clase

    //Guarda el contexto
    //reg se debe recibir por referencia creo
	void Agregar(int p, int *reg)            
	{
	    
	    contexto * nueva  = new contexto;
	    nueva->pc = p;
	    for(int i = 1; i <33; ++i)
	    {
	        nueva->regist[i] = reg[i];
	    }
	    ultimo -> siguiente = nueva;
	    ultimo = nueva;
	    
	    if(n == 0)
	    {
	        primero = nueva;
	    }
	     n++;
	}
	
	//Borra el contexto 
	//estos se deben recibir por referencia &
	void Sacar(int p, int*reg)
	{
	    p = primero->pc;
	    for(int i = 1; i <33; ++i)
	    {
	        reg[i] = primero->regist[i];
	    }
	    
        primero = primero->siguiente;
	    contexto * aux = primero;
	    delete aux;
	    --n;
	}

private:

    contexto * primero;
    contexto * ultimo;
    int n;
};

#endif //CONTEXTOS_H
