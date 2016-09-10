// Compilar usando g++ -pthread filename1 [filename2...] [-o outputFileName]
/**
 * Tarea Programada: simulacion de MIPS 3 nucleos
 * Adriana Mora Calvo        B24385
 * Lisbeth Rojas Montero     B15745
 * */
#include <iostream>
#include <pthread.h> //Biblioteca para uso de pthreads
#include "Contextos.h"
using namespace std;

int main() 
{
    
    //variables compartidas
    pthread_barrier_t barrera;  // inicializacion de la barrera

    int * memDatos;             // cache de datos (cada nucleo tiene una propia)
    int * memInstruc;           // cache de instrucciones (cada nucleo tiene una propia)
    int busDatos, busInstruc;   // para lectura y escritura deben esperar el bus
    Contextos *cola;            // para poder cambiar de contexto entre hilillos
    int total;                  //PARA QUE SIRVE ESTO???? es para el quantum??
    
    memDatos = new int[96];
    memInstruc = new int [640];
    
    for(int i = 0; i < 96; ++i)
    {
        memDatos[i] = 1;
    }
    for(int i = 0; i < 640; ++i)
    {
        memInstruc[i] = 1;
    }
    
    cola = new Contextos();
}

void Nucleos()
{
    int * reg = new int[33];    //Registro 0 = reg[0] (contiene un cero), registro RL = reg[32]         LOS PROCESOS COMPARTEN LOS REGISTROS ASI QUE NO SE SI ESTO DEBE DE IR ARRIBA
    int cacheDatos[4][6];       //4 columnas 5 filas, (fila 0 p0, fila 2 p1, fila 3 etiqueta, fila 4 valides)
    int cacheInstruc[4][6];     //4 columnas 5 filas, (fila 0 p0, fila 2 p1, fila 3 etiqueta, fila 4 valides)
    int PC;                     // para el control de las instrucciones
    int dirDatos, dirInstruc;
    
    int bloque, fisica, quantum;
    
    dirDatos = 0;       //indice de la cache
    dirInstruc = 0;     //indice de la cache
    reg[0] = 0;
    for(int i = 0; i < 4; ++i)
    {
        for(int j = 0; j < 6; ++j)
        {
            cacheDatos[i][j] = 0;
            cacheInstruc[i][j] = 0; 
        }
    }

    bool continuar;    
    
    // Modificación del quatum y ciclos de reloj 
    // Realizado por el hilo 0 que se encarga de la coordinación de los demás hilos
    while(continuar)
    {
        //cargar PC;
        PC +=4;
        reg[31] = PC;
        
        //bloque = ; 
        
        
        dirDatos =  (dirDatos + 1)%4;
        dirInstruc = (dirInstruc + 1)%4;
        quantum--;
    }
}

//Calcular direccion fisica de datos
int CalcularFisica(int direccion)
{
    if(direccion >= 384) //DE DONDE SE SACO ESTE 384?????
    {
        return (direccion - 384);
    }
    return (direccion / 4);
}

//Fallo de Cache
// Cuando ocurre fallo de cache debo de traer el bloque completo de donde esta lo que quiero modificar
void SubirBLoqueDatos(int columna, int posicion, bool datos)
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
    int pasado = 0;
    while(pasado < 28) //Simulacion de que un fallo de cache dura 28 ciclos de reloj
    {
        //tic de reloj
        ++pasado;
    }
    
}