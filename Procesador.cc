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
using namespace std;

int main() 
{
    
    //variables compartidas
    pthread_barrier_t barrera;  // inicializacion de la barrera

    int * memDatos;             // cache de datos (cada nucleo tiene una propia)
    int * memInstruc;           // cache de instrucciones (cada nucleo tiene una propia)
    int busDatos, busInstruc;   // para lectura y escritura deben esperar el bus
    Contextos *cola;            // para poder cambiar de contexto entre hilillos
    int total;
    
    char linea[4];
    memDatos = new int[96]; // 384/4
    memInstruc = new int [640]; // 40 bloques * 4 *4
    
    for(int i = 0; i < 96; ++i) // memoria principal inicilizada en uno
    {
        memDatos[i] = 1;
    }
    for(int i = 0; i < 640; ++i) // memoria principal inicilizada en uno
    {
        memInstruc[i] = 1;
    }
    
    cola = new Contextos();
    cout << "Direccion del archivo \n";
    //cin >> nombreArchivo;
    ifstream entrada(nombreArchivo); 
    
    int j = 0;
    while(!entrada.eof())
    {
        entrada.getline(linea, 4);
        for(int i = 0; i <4; ++i)
        {
            memInstruc[j] = linea[i] - 48;
            j++;
        }
        
    }
}

void Nucleos()
{
    int * reg = new int[33];    //Registro 0 = reg[0] (contiene un cero), registro RL = reg[32] 
    int cacheDatos[4][6];       //4 columnas 5 filas, (fila 0 p0, fila 2 p1, fila 3 etiqueta, fila 4 valides)
    int cacheInstruc[4][6];     //4 columnas 5 filas, (fila 0 p0, fila 2 p1, fila 3 etiqueta, fila 4 valides)
    int PC;                     // para el control de las instrucciones
    int dirDatos, dirInstruc;
    int cop, rf1, rf2, rd;
    //total     total???? es para el quantum?? 
    // cop codigo de operacion
    // rf1 registro fuente
    // rf2 registro fuente 2 o registro destino dependiendo de la instruccion
    // rd registro destino o inmediato dependiendo de la instruccion
    int bloque, fisica, quantum;
    // bloque es el bloque de memoria cache
    //fisica es
    //quatum es el tiempo dado por el usuario
    
    dirDatos = 0;       //indice de la cache
    dirInstruc = 0;     //indice de la cache
    reg[0] = 0;
    for(int i = 0; i < 4; ++i) //las caches se inicializadas en cero
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
    } // cuando salgo de aqui debo restar 28 al quantum
    
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
    //Detener el programa
    }
}