#ifndef CONTEXTOS_H
#define CONTEXTOS_H

class Contextos
{

    struct contexto
    {
    	int pc;     //
    	int  regist[33];//Semaforo asociado al Thread
    	contexto * sig;
    };

public:
	
	Contextos(int hilillos){} //Constructor de la clase
	
	~Contextos(){}	//Destructor de la clase

	void Agregar(int p, int *reg)             //Guarda el contexto
	{
	    
	}
	bool Sacar();		//Borra el contexto 

private:

    contexto * primero;
    contexto * ultimo;
};

#endif //CONTEXTOS_H
