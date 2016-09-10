#include <iostream>
#include "Contextos.h"
using namespace std;

int main() 
{
    cout << "Hello World!" << endl;
    
    //variables compartidas
    
    int * memDatos;
    int * memInstruc;
    int busDatos, busInstruc;
    Contextos *cola;
    int total;
    
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
    int * reg = new int[33];    //Registro 0  = reg[0], registro RL = reg[32]
    int cacheDatos[4][6];       //4 columnas 5 filas, (fila 0 p0, fila 3 etiqueta, fila 4 valides)
    int cacheInstruc[4][6];     //4 columnas 5 filas, (fila 0 p0, fila 3 etiqueta, fila 4 valides)
    int PC;
    int dirDatos, dirInstruc;
    
    int bloque, fisica, quantum;
    
    dirDatos = 0;//indice de la cache
    dirInstruc = 0;//indice de la cache
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

int CalcularFisica(int direccion)
{
    if(direccion >= 384)
    {
        return (direccion - 384);
    }
    return (direccion / 4);
}

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
    while(pasado < 28)
    {
        //doy ciclo
        ++pasado;
    }
    
}