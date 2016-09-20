#ifndef CONTEXTOS_H
#define CONTEXTOS_H

class Contextos
{
    struct Contexto
    {
    	int pc;    
    	int  regist[32];
    	contexto * siguiente;
    	int RL;
    };
    
    struct EstadoFinal
    {
    	int pc;    
    	int  regist[32];
    	int RL;
    	EstadoFinal* next;
    	int cD[6][4];	
    	int cI[6][16];
    };

public:
	
	//Constructor de la clase
	Contextos()
	{
	    n = 0;
	} 
	
	~Contextos(){}	//Destructor de la clase

    //Guarda el contexto
    //reg se debe recibir por referencia creo
	void Agregar(int p, int *reg, int rl)            
	{
	   
	    Contexto * nueva  = new contexto;
	    nueva->pc = p;
	    nueva->RL = rl;
	    
	    for(int i = 1; i <32; ++i)
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
	void Sacar(int p, int*reg, int rl)
	{
	    p = primero->pc;
	    rl = primero->RL;
	    for(int i = 1; i <33; ++i)
	    {
	        reg[i] = primero->regist[i];
	    }
	    
        primero = primero->siguiente;
	    Contexto * aux = primero;
	    delete aux;
	    --n;
	}

	void Encolar(int pc)
	{
		contexto * nueva  = new contexto;
		nueva ->PC = pc;
		
		ultimo -> siguiente = nueva;
	    ultimo = nueva;
}
private:

    Contexto * primero;
    Contexto * ultimo;
    int n;
    
    EstadoFinal * inicio;
   
};

#endif //CONTEXTOS_H
