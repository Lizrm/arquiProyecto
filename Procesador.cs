//Programa en C# los que estaba aqui esta en el archivo comentariosUtiles

using System.Threading; //System.Threading.Mutex;
using System; //Uso de la pantalla para impimir // igual al using namespace std

class Procesador

   static void Main()
   {
      Console.WriteLine("Hello World!"); //Escribir en consola
      
      string path = @"c:\temp\MyTest.txt";
      
      using (StreamReader sr = new StreamReader("TestFile.txt")); // objeto que lee de archivo
      sr.Read(Char[],Int32,Int32) ;     //metodo que Lee un máximo especificado de caracteres de la secuencia actual en un búfer, comenzando en el índice especificado
      String line = sr.ReadLine()	//Lee una línea de caracteres de la secuencia actual y devuelve los datos como una cadena.
      sr.Read(); //Lee el siguiente carácter de la secuencia de entrada y hace avanzar la posición de los caracteres en un carácter
   
      //VARIABLE EN C#
   
       int[] memDatos;             // cache de datos (cada nucleo tiene una propia)
       int[] memInstruc;           // cache de instrucciones (cada nucleo tiene una propia)
       Contextos cola;            // para poder cambiar de contexto entre hilillos
       Contextos finalizados;    //Guarda el estado de los registros y las cache en la que termino el hilillo
       int total;                  //Total de hilillos
       int quantumTotal;           //Variable compartida, solo de lectura, no debe ser modificada por los hilos
       char[] linea;
       int[][] cacheDatos3;      //4 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
       int[][] cacheDatos2;      //4 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
       int[][] cacheDatos1;      //4 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
       int[] RL;                 //Registros RL
       mutex md1, md2, md3;        // Los mutex para las 3 memorias de datos
       mutex busD, busI;           // Mutex para los 2 buses
       mutex mCola, listos;         //Mutex para las 2 estructuras auxiliares
       private static Mutex md1 = new Mutex();
       private static Mutex md2 = new Mutex();
       private static Mutex md3 = new Mutex();
       private static Mutex busD = new Mutex();
       private static Mutex busI = new Mutex();
       private static Mutex mCola = new Mutex();
       private static Mutex mListos = new Mutex();
       private const int numThreads = 3;  //Emulacion de los 3 nucleos
      
      //**Bloque de creacion**//
      
       memDatos = new int[96]; // 384/4
       memInstruc = new int [640]; // 40 bloques * 4 *4
       cola = new Contextos();    
       finalizados = new Contextos();  
       RL = new int[3];      
       cacheDatos = new int[6][4];     
       //*******************Fin de Bloque***********************//
       
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
       
       for(int i = 0; i < 4; ++i) //las caches se inicializadas en cero
       {
           for(int j = 0; j < 4; ++j)
           {
               cacheDatos3[i][j] = 0;
               cacheDatos2[i][j] = 0;
               cacheDatos1[i][j] = 0;
           }
       }
       for(int i = 4; i < 6; ++i) //las caches se inicializadas en cero
       {
           for(int j = 0; j < 4; ++j)
           {
               cacheDatos3[i][j] = -1;
               cacheDatos2[i][j] = -1;
               cacheDatos1[i][j] = -1;
           }
       }
       //*****************Fin de Bloque*************************//
      
        Console.Write("Ingrese el quantum \n");
        quantumTotal = int.Parse(Console.ReadLine());
      
        Console.Write("\nIngrese el numero de hilillos Totales \n");
        total = int.Parse(Console.ReadLine());
      
        int indice = 0;
        for(int j = 0; j < total; ++j)
        {
            //obtener archivo
            cola.Encolar(indice); //Solo agrega el PC para iniciar las intrucciones
            
            while(!entrada.eof()) // Cambiar por instruccion de c#
            {
                entrada.getline(linea, 7);// Cambiar por instruccion de c#
                int i = 0;
                while(i<7)
                {
                    memInstruc[indice] = (linea[i] - 48); //conversion a int // Cambiar por instruccion de c#
                    indice++;
                    i += 2;
                }
            }
    }
      
       for(int i = 1; i <= numThreads; i++)//Creacion de 3 threads
      {
         Thread myThread = new Thread(new ThreadStart(MyThreadProc));
         myThread.Name = String.Format("Nucleo{0}", i);
         myThread.Start();
      }
   }
   
   private void Nucleos(int q) //quatum
   {
      /**Bloque de Declaracion**/
       int[][] reg;              //Registro 0 = reg[0] (contiene un cero), 
       int[][] cacheInstruc;     //16 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
       int PC;                   //Para el control de las instrucciones
       int dirDatos, dirInstruc;
       int cop, rf1, rf2, rd;
       // cop codigo de operacion
       // rf1 registro fuente
       // rf2 registro fuente 2 o registro destino dependiendo de la instruccion
       // rd registro destino o inmediato dependiendo de la instruccion
       int bloque, posicion, palabra, iterador,quantum;
       // bloque es el bloque de memoria cache
       //fisica es
       //quatum es el tiempo dado por el usuario
       
       /**Bloque de Creacion**/
       reg = new int[32];    
       cacheInstruc = new int[0][16];     
       cacheInstruc = new int[1][16];
       cacheInstruc = new int[2][16];     
       cacheInstruc = new int[3][16];     
       cacheInstruc = new int[4][4];     
       cacheInstruc = new int[5][4];     
       
       /**Bloque inicializacion**//
       dirDatos = -1;       //indice de la cache
       dirInstruc = -1;     //indice de la cache
       reg[0] = 0;         //no debe ser modificado nunca
       for(int i = 0; i < 4; ++i) //las caches se inicializadas en cero
       {
           for(int j = 0; j < 16; ++j)
           {
               cacheInstruc[i][j] = 0; 
           }
       }
       for(int i = 4; i < 6; ++i) //las caches se inicializadas en cero
       {
           for(int j = 0; j < 4; ++j)
           {
               cacheInstruc[i][j] = -1; 
           }
       }
       
       while(true)//while que no deja que los hilos mueran
       {
           while(!Monitor.TryEnter(cola))
           {
               TickReloj();
           }
           while(!Monitor.TryEnter(RL))
           {
               TickReloj();     //preguntarle a la profe si es necesario dar tickde reloj y si debo soltar la cola
           }
           
           Contextos.Sacar(); // debo verificar el paso de variables por referencia
           Monitor.Exit(cola);
           Monitor.Exit(RL);
           /**************************/
           bloque = PC/16;  //calculo el bloque
           posicion = bloque % 4;    
           palabra = (PC%16) / 4;
           iterador = posicion*4;
           /*************************/
           if(!(cacheInstruc[4][posicion] == bloque) && !(cacheInstruc[4][posicion] == 1)) //1 valido
           {
               // fallo de cahce
                while(!Monitor.TryEnter(busI))
                {
                   TickReloj();     //preguntarle a la profe si es necesario dar tickde reloj y si debo soltar la cola
                }
                int it = PC;
                for(int i = 0; i < 4; ++i)
                {
                    for(int j = 0; j < 4; ++j)
                    {
                        cacheInstruc[i][j] = memInstruc[it];
                        ++it;
                    }
                }
                cacheInstruc[4][posicion] = bloque;
                cacheInstruc[5][posicion] = 1;
                this.FallodeCache(28);
                Monitor.Exit(busI);
           }
           iterador = posicion*4;
           cop = cacheInstruc[palabra][iterador];
           rf1 = cacheInstruc[palabra][iterador+1];
           rf2 = cacheInstruc[palabra][iterador+2];
           rd = cacheInstruc[palabra][iterador+3];
           PC += 4;
       }
       
           
   
           
       dirDatos =  (dirDatos + 1)%4;
       dirInstruc = (dirInstruc + 1)%4;
   //    quantum--; //lo resto al finalizar una instruccion
       }
   }
   
   private void FallodeCache(int ciclos) //Se encicla los tick de reloj dependiendo que la instruccion ejecutando
   {
       for(int i = 0; i < ciclos; ++i) //Simulacion de que un fallo de cache
       {
           TickReloj();       //tic de reloj
       }
   }
   
   private void TickReloj()
   {
      //Barrera de sincronizacion
   }
   