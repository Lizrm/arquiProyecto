// Compilar usando g++ -pthread filename1 [filename2...] [-o outputFileName]
/**
 * Tarea Programada: simulacion de MIPS 3 nucleos
 * Adriana Mora Calvo        B24385
 * Lisbeth Rojas Montero     B15745
 * */
#include <iostream>
#include <fstream>
#include <pthread.h> //Biblioteca para uso de pthreads
#include "Contextos.h"
#include <mutex>
using namespace std;

int main() 
{
    
    //variables compartidas
    //**Bloque de Declaracion**//
    pthread_barrier_t barrera;  // inicializacion de la barrera

    int * memDatos;             // cache de datos (cada nucleo tiene una propia)
    int * memInstruc;           // cache de instrucciones (cada nucleo tiene una propia)
    Contextos * cola;            // para poder cambiar de contexto entre hilillos
    Contextos * finalizados;    //Guarda el estado de los registros y las cache en la que termino el hilillo
    int total;                  //Total de hilillos
    int quantumTotal;           //Variable compartida, solo de lectura, no debe ser modificada por los hilos
    char linea[4];
    int cacheDatos[6][4];       //4 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
    int * RL;
    mutex md1, md2, md3;        // Los mutex para las 3 memorias de datos
    mutex busD, busI;           // Mutex para los 2 buses
    mutex mCola, listos;         //Mutex para las 2 estructuras auxiliares
    
    //**Bloque de creacion**//
    memDatos = new int[96]; // 384/4
    memInstruc = new int [640]; // 40 bloques * 4 *4
    cola = new Contextos();     //Guarda el contexto de los hillos que no han finalizado
    finalizados = new Contextos();  //Guarda el contexto de los hillos finalizados y el estado de las caches
    RL = new int[3];      //Registros RL
    
    
    //**Bloque de inicializacion**//
    for(int i = 0; i < 96; ++i) // memoria principal inicilizada en uno
    {
        memDatos[i] = 1;
        memInstruc[i] = 1;
    }
    for(int i = 96; i < 640; ++i) // memoria principal inicilizada en uno
    {
        memInstruc[i] = 1;
    }
    
    for(int i = 0; i < 6; ++i) //las caches se inicializadas en cero
    {
        for(int j = 0; j < 4; ++j)
        {
            cacheDatos[i][j] = 0;
        }
    }

    
    cout << "Ingrese el quantum para los hilillos \n" << endl;
    cin >> quantumTotal;
    
    cout << "Ingrese el número total de hilillos \n" << endl;
    cin >> total;
    int indice = 0;
    for(int j = 0; j < total; ++j)
    {
        //obtener archivo
        cola->Encolar(indice); //Solo agrega el PC para inciciar las intrucciones
        
        while(!entrada.eof()) // leer todo lo que esta en el archivo
        {
            entrada.getline(linea, 7);
            int i = 0;
            while(i<7)
            {
                memInstruc[indice] = (linea[i] - 48); //conversion a int
                indice++;
                i += 2;
            }
        }
    }
}

void Nucleos()
{
    int * reg = new int[32];    //Registro 0 = reg[0] (contiene un cero), 
    int cacheInstruc[6][16];     //16 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
    int PC;                     // para el control de las instrucciones
    int dirDatos, dirInstruc;
    int cop, rf1, rf2, rd;
    // cop codigo de operacion
    // rf1 registro fuente
    // rf2 registro fuente 2 o registro destino dependiendo de la instruccion
    // rd registro destino o inmediato dependiendo de la instruccion
    int bloque, fisica, quantum;
    // bloque es el bloque de memoria cache
    //fisica es
    //quatum es el tiempo dado por el usuario
    
    dirDatos = -1;       //indice de la cache
    dirInstruc = -1;     //indice de la cache
    reg[0] = 0;         //no debe ser modificado nunca
    for(int i = 0; i < 6; ++i) //las caches se inicializadas en cero
    {
        for(int j = 0; j < 16; ++j)
        {
            cacheInstruc[i][j] = 0; 
        }
    }
    
    for(int i = 0; i < 33; ++i)
    {
        reg[i] = 0;
    }

        //cargar PC;
    PC +=4;
    reg[31] = PC;
        
        //bloque = ; 

        
    dirDatos =  (dirDatos + 1)%4;
    dirInstruc = (dirInstruc + 1)%4;
//    quantum--; //lo resto al finalizar una instruccion
    }
}

//Calcular direccion fisica de datos
int CalcularFisica(int direccion)
{
    if(direccion >= 384)
    {
        return (direccion - 384);
    }
    return (direccion / 4);
}

//Fallo de Cache
// Cuando ocurre fallo de cache debo de traer el bloque completo de donde esta lo que quiero modificar
void SubirBLoqueDatos(int direccion)
{
    if(datos)
    {
        //pedir memoria de datos
        for(int i = 0; i < 4; ++i)
        {
            cacheDatos[columna][i] = memDatos[posicion];     
        }
        cacheDatos[columna][4] = posicion;
        cacheDatos[columna][5] = 1;
    }
    else
    {
        //pedir memoria de instrucciones
        for(int i = 0; i < 4; ++i)
        {
            cacheInstruc[columna][i] = memInstruc[posicion];     
        }
        cacheInstruc[columna][4] = posicion;
        cacheInstruc[columna][5] = 1;
    }
}

void Encicliarse(int ciclos)
{
    for(int i = 0; i < ciclos; ++i) //Simulacion de que un fallo de cache
    {
        TickReloj();       //tic de reloj
    }
}

void SinHilillos()
{
    bool noHilillo = true;
    while(noHilillo)
    {
        TickReloj();
        if(tri_lock(mCola))
        {
            if(!cola->Vacia)
            {
                //cola->Sacar();
                unlock(mCola);
                noHilillo = false;
            }
        }    
    }
}
void TickReloj()
{
    // aqui se debe poner la barrera de sincronizacion
}

void Ejecutar(int cop)//recibe el codigo de operacion para saber que hacer con los operandos
{ 
    
    //Codificacion de las instrucciones recibidas
    switch(cop) //cop es el codigo de operacion que tenemos que definir como se lee la instruccion para meter eso en una variable entera
    { 
        case 8 : //DADDI 
    //      rf1 <------- rf2 + inm
        reg[rf1] =  reg[rf2] + rd;
        
        case 32 : //DADD
    //      rd <------ rf1 + rf2
        reg[rd] = reg[rf1] + reg[rf2];
        
        case 34 : //DSUB
    //     rd <------- rf1 - rf2
        reg[rd] = reg[rf1] - reg[rf2];
        
        case 12: //DMUL
    //      rd <------ rf1 * rf2
        reg[rd] = reg[rf1] * reg[rf2];
        
        case 14: //DIV
    //      rd <------ rf1 * rf2
        reg[rd] = reg[rf1] / reg[rf2];
        
        case 4 : //BEZ
    // si rf = 0 entonces SALTA
        if(reg[rf1] == reg[0])
        {
            //que hago con la n que recibo en rd
        }
        
        case 5 : //BNEZ
    // si rf z 0 o rf > 0 entonces SALTA
        if(reg[rf1] < reg[0] || reg[rf1] > reg[0])
        {
             //que hago con la n que recibo en rd
        }
        
        case 3 : //JAL
    //   reg 31 = PC
        reg[31] = PC;
    //  PC = PC + inm;
        PC = PC + rd;
        
        case 2 :  //JR
    //  PC = rf1
        PC = reg[rf1];
        
        case 50 : //LL
        //Se implementará en la tercera entrega
        
        case 51 : //SC
        //Se implementará en la tercera entrega
        
        case 35 : //LW
        //  ESTO DEBEMOS DE VER COMO LO VAMOS A PROGRAMAR
        //PORQUE HAY QUE VER LO DE LA TRADUCCION A BLOQUE Y PALABRA EN CACHE
        // Y FALLOS DE CACHE
        //USANDO EL BUS DE DATOS 
        //PROGRAMAR LO DE LOS SEMAFOROS PARA EL USO DE BUSES
        case 43: //SW
        //ESTE SE DEBE DE PROGRAMAR ESCRIBIENDO EN MI CACHE Y EN LA MEMORIA PRINCIPAL
        //USANDO EL BUS DE DATOS 
        //PROGRAMAR LO DE LOS SEMAFOROS PARA EL USO DE BUSES
        case 63 : //FIN
    //Detener el hilillo
    }
}