using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThread
{
    class Procesador
    {
        
        static int[] memDatos;             // cache de datos (cada nucleo tiene una propia)
        static int[] memInstruc;           // cache de instrucciones (cada nucleo tiene una propia)
        static Contextos cola;            // para poder cambiar de contexto entre hilillos
        static Contextos finalizados;    //Guarda el estado de los registros y las cache en la que termino el hilillo
        static int total;                  //Total de hilillos
        static int reloj;                   //Variable general del reloj
        static int quantumTotal;           //Variable compartida, solo de lectura, no debe ser modificada por los hilos
        static int[,] cacheDatos3;      //4 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
        static int[,] cacheDatos2;      //4 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
        static int[,] cacheDatos1;      //4 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
        static int[] RL1;
        static int[] RL2;
        static int[] RL3;
        static int[] busD;
        static int[] busI;
       
       Barrier barrier = new Barrier(3, (bar) =>  //Barrera de sincronizacion, lo que esta dentro se ejecuta una sola vez
       {
            reloj++;
       }); //FIN de la Barrera
       
       static public void TicReloj()
       {
           barrier.SignalAndWait();
       }//FIN de TicReloj
       
       static public void FallodeCache(int ciclos) //Se encicla los tick de reloj dependiendo que la instruccion ejecutando
       {
           for(int i = 0; i < ciclos; ++i) //Simulacion de que un fallo de cache
           {
               TicReloj();       //tic de reloj
           }
       }//FIN de Fallo de Cache
       
       static void Main()
       {
          Console.WriteLine("Hello World!"); //Escribir en consola
          string path = @"c:\temp\MyTest.txt";
          char[] linea;
          using (StreamReader sr = new StreamReader("TestFile.txt")); // objeto que lee de archivo
          sr.Read(Char[],Int32,Int32) ;     //metodo que Lee un máximo especificado de caracteres de la secuencia actual en un búfer, comenzando en el índice especificado
          String line = sr.ReadLine()	//Lee una línea de caracteres de la secuencia actual y devuelve los datos como una cadena.
          sr.Read(); //Lee el siguiente carácter de la secuencia de entrada y hace avanzar la posición de los caracteres en un carácter
           private const int numThreads = 3;  //Emulacion de los 3 nucleos
          
          //**Bloque de creacion**//
          
           memDatos = new int[96]; // 384/4
           memInstruc = new int [640]; // 40 bloques * 4 *4
           cola = new Contextos();    
           finalizados = new Contextos();  
          
           RL1 = new int[1];
           RL2 = new int[1];
           RL3 = new int[1];
           busD = new int[1];
           busI = new int[1];
           
           cacheDatos1 = new int[6,4]; //Preguntar si es recomendable recorrerlas por filas
           cacheDatos2 = new int[6,4]; 
           cacheDatos3 = new int[6,4];
           //*******************Fin de Bloque***********************//
           
          //*************Bloque de inicializacion******************//
           reloj = 0;
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
                   cacheDatos3[i,j] = 0;
                   cacheDatos2[i,j] = 0;
                   cacheDatos1[i,j] = 0;
               }
           }
           for(int i = 4; i < 6; ++i) //las caches se inicializadas en invalidas
           {
               for(int j = 0; j < 4; ++j)
               {
                   cacheDatos3[i,j] = -1;
                   cacheDatos2[i,j] = -1;
                   cacheDatos1[i,j] = -1;
               }
           }
           //*****************Fin de Bloque*************************//
          
            Console.Write("Ingrese el quantum \n");
            quantumTotal = int.Parse(Console.ReadLine());      
            Console.Write("\nIngrese el numero de hilillos Totales \n");
            total = int.Parse(Console.ReadLine());
            
            int indice = 0;                                     //*****************Editar aqui  lee de archivo a medias*********************////
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
                //*****************Leer archivo termina aqui*********************////
    
    
            //Creacion de los 3 hilos que emulan los nucleos
            Thread my1 = new Thread(() => Nucleos(quantumTotal));       
            Thread my2 = new Thread(() => Nucleos(quantumTotal));
            Thread my3 = new Thread(() => Nucleos(quantumTotal));
            //Se les asigna un "id" a los hilos
            my1.Name = "1";
            my2.Name = "2";
            my3.Name = "3";
            //Se inician los hilos
            my1.Start();
            my2.Start();
            my3.Start();
       }//FIN de Main
       
       private void Nucleos(int q) //quatum
       {
          /**Bloque de Declaracion**/
           int[] reg;               //Vector de registros
           int[][] cacheInstruc = new int[6][];     //16 columnas 6 filas, (fila 0 p0, fila 2 p1, fila 4 etiqueta, fila 5 valides)
           int PC;                   //Para el control de las instrucciones
           int cop, rf1, rf2, rd;//codigo de operacion, registro fuente, registro fuente2 o registro destino dependiendo de la instruccion, rd registro destino o inmediato dependiendo de la instruccion
           int bloque, posicion, palabra, iterador,quantum, inicioBloque; // bloque es el bloque de memoria cache, quatum es el tiempo dado por el usuario
       
           /**Bloque de Creacion**/
           reg = new int[32];    
           cacheInstruc[0] = new int[16];     
           cacheInstruc[1] = new int[16];
           cacheInstruc[2] = new int[16];     
           cacheInstruc[3] = new int[16];     
           cacheInstruc[4] = new int[4];     //Para no desperdiciar memoria
           cacheInstruc[5] = new int[4];   
           //*****************Fin bloque de creacion******************//
       
           //***********Bloque inicializacion***********************//

           reg[0] = 0;         //no debe ser modificado nunca

           for(int i = 0; i < 4; ++i) //las caches se inicializadas en cero
           {
               for(int j = 0; j < 16; ++j)
               {
                   cacheInstruc[i][j] = 0; 
               }
           }
           for(int i = 4; i < 6; ++i) //las caches se inicializadas en -1
           {
               for(int j = 0; j < 4; ++j)
               {
                   cacheInstruc[i][j] = -1;
               }
           }
           //**************Fin bloque inicilaizacion****************//
           
           while(true)//while que no deja que los hilos mueran
           {
               while(!Monitor.TryEnter(cola))
               {
                   TicReloj();
               }
               TickReloj();
               
               switch(int.Parse(Thread.CurrentThread.Name)) //RL
               {
                   
                   case 1:
                        while(!Monitor.TryEnter(RL1))
                        {
                           TicReloj();     
                        }
                        TicReloj();
                        RL1 = -1;
                        Monitor.Exit(RL1);
                   break;
                   
                   case 2:
                        while(!Monitor.TryEnter(RL2))
                        {
                           TicReloj();     
                        }
                        TicReloj();
                        RL2 = -1;
                        Monitor.Exit(RL2);
                   break;
                   
                   case 3:
                        while(!Monitor.TryEnter(RL3))
                        {
                           TicReloj();     
                        }
                        TicReloj();
                        RL3 = -1;
                        Monitor.Exit(RL3);
                   break;
               }
               
               Contextos.Sacar(ref PC, ref reg); // debo verificar el paso de variables por referencia
               Monitor.Exit(cola);
               quantum = q;
               
               while(quantum > 0)
               {
    		       /**************************/
    		       bloque = PC/16;  //calculo el bloque
    		       posicion = bloque % 4;    //posicion en cache
    		       palabra = (PC%16) / 4;
    		       /*************************/
    		       if(!(cacheInstruc[4][posicion] == bloque) && !(cacheInstruc[4][posicion] == 1)) //1 valido
    		       {
    		           // fallo de cache
    		            while(!Monitor.TryEnter(busI))
    		            {
    		               TicReloj(); 
    		            }
    		            TicReloj();
    		            
    		            cacheInstruc[4][posicion] = bloque;
    		            cacheInstruc[5][posicion] = 1;
    		            
    		            iterador = posicion *4;
    		            inicioBloque = ;// calcular
    		            for(int i = 0; i < 4; ++i)
    		            {
    		                for(int j = 0; j < 4; ++j)
    		                {
    		                    cacheInstruc[i][iterador] = memInstruc[inicioBloque];
    		                    ++iterador;
    		                    +inicioBloque;
    		                }
    		                iterador -= 3;
    		            }
    		            this.FallodeCache(28);
    		            Monitor.Exit(busI);
    		       } //Fin fallo de cache
    		       
    		       iterador = posicion*4;
    		       cop = cacheInstruc[palabra][iterador];
    		       rf1 = cacheInstruc[palabra][iterador+1];
    		       rf2 = cacheInstruc[palabra][iterador+2];
    		       rd = cacheInstruc[palabra][iterador+3];  //destino
    		       PC += 4;
    		       
    		       //Codificacion de las instrucciones recibidas
    				switch(cop) //cop es el codigo de operacion 		// se deben verificar que el registro destino no sea cero 
    				{ 
    					case 8 : //DADDI rf1 <------- rf2 + inm
    
    						reg[rf1] =  reg[rf2] + rd;
    					break;
    
    					case 32 : //DADD rd <------ rf1 + rf2
    
    						reg[rd] = reg[rf1] + reg[rf2];
    					break;
    
    					case 34 : //DSUB  rd <------- rf1 - rf2
    
    						reg[rd] = reg[rf1] - reg[rf2];
    					break;
    
    					case 12: //DMUL  rd <------ rf1 * rf2
    
    						reg[rd] = reg[rf1] * reg[rf2];
    					break;
    
    					case 14: //DIV  rd <------ rf1 / rf2
    
    						reg[rd] = reg[rf1] / reg[rf2];
    					break;
    
    					case 4 : //BEZ si rf = 0 entonces SALTA
    
    						if(reg[rf1] == reg[0])
    						{
    							PC += (rd*4);
    						}
    					break;
    
    					case 5 : //BNEZ si rf z 0 o rf > 0 entonces SALTA
    
    						if(reg[rf1] < reg[0] || reg[rf1] > reg[0]) //PUEDO cambiar esto por un != de cero
    						{
    							PC += (rd*4);
    						}
    					break;
    
    					case 3 : //JAL  reg 31 = PC
    
    						reg[31] = PC;
    						PC += rd;   //  PC = PC + inm;
    					
    					break;
    
    					case 2 :  //JR  PC = rf1
    
    						PC = reg[rf1];
    					break;
    
    					case 50 : //LL
    					//Se implementará en la tercera entrega
    					break;
    
    					case 51 : //SC
    					//Se implementará en la tercera entrega
    					break;
    
    					case 35 : //LW
    						//pedir memoria de datos
    						//caculo de bloque y palabra
    						switch(int.Parse(Thread.CurrentThread.Name))
                            {
                                case 1:
                                    //caculo de bloque y palabra
            						bool conseguido = false;
            						while(!conseguido)
            						{
            						    while(!Monitor.TryEnter(cacheDatos1))    //cambiar por mi cache
                    		            {
                    		               TicReloj(); 
                    		            }
                    					TickReloj();
                						if(!(bloque == cacheDatos1[4][posicion]) && !(cacheDatos1[5][posicion] == 1]))
                						{
                						    if(!Monitor.TryEnter(busD))
                        		            {
                        		                Monitor.Exit(cacheDatos1); //cambiar por mi cache de datos
                                                TicReloj();            		               
                        		            }else
                        		            {
                        		                conseguido = true;
                        		                TicReloj();
                    						    inicioBloque = ;    //inicio del bloque a copiar
                        		                
                    						    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                        						{
                        							cacheDatos1[i][posicion] = memDatos[nicioBloque];
                        							nicioBloque++;
                        						}
                        						cacheDatos1[4][posicion] = bloque;
                        						cacheDatos1[5][posicion] = 1;
                        		            }
                						}
            						}
                					this.FallodeCache(28);
                					Monitor.Exit(busD);
            						Monitor.Exit(cacheDatos1);
                					//Se le entrega el dato al registro 
                                    
                                break;
                               
                                case 2:
                                    //caculo de bloque y palabra
            						bool conseguido = false;
            						while(!conseguido)
            						{
            						    while(!Monitor.TryEnter(cacheDatos2))    //cambiar por mi cache
                    		            {
                    		               TicReloj(); 
                    		            }
                    					TickReloj();
                						if(!(bloque == cacheDatos2[4][posicion]) && !(cacheDatos2[5][posicion] == 1]))
                						{
                						    if(!Monitor.TryEnter(busD))
                        		            {
                        		                Monitor.Exit(cacheDatos2); //cambiar por mi cache de datos
                                                TicReloj();            		               
                        		            }else
                        		            {
                        		                conseguido = true;
                        		                TicReloj();
                        		                inicioBloque = ;    //inicio del bloque a copiar
                        		                
                    						    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                        						{
                        							cacheDatos2[i][posicion] = memDatos[nicioBloque];
                        							nicioBloque++;
                        						}
                        						cacheDatos2[4][posicion] = bloque;
                        						cacheDatos2[5][posicion] = 1;
                        		            }
                						}
            						}
                					this.FallodeCache(28);
                					Monitor.Exit(busD);
            						Monitor.Exit(cacheDatos2); //soltar mi cache 
                					//Se le entrega el dato al registro 
                                    
                                break;
                               
                                case 3:
                                    //caculo de bloque y palabra y posicion
            						bool conseguido = false;
            						while(!conseguido)
            						{
            						    while(!Monitor.TryEnter(cacheDatos3))
                    		            {
                    		               TicReloj(); 
                    		            }
                    					TickReloj();
                						if(!(bloque == cacheDatos3[4][posicion]) && !(cacheDatos3[5][posicion] == 1]))
                						{
                						    if(!Monitor.TryEnter(busD))
                        		            {
                        		                Monitor.Exit(cacheDatos); //cambiar por mi cache de datos
                                                TicReloj();            		               
                        		            }else
                        		            {
                        		                conseguido = true;
                        		                TicReloj();
                        		                inicioBloque = ;    //inicio del bloque a copiar
            						            
            						            for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                        						{
                        							cacheDatos3[i][posicion] = memDatos[nicioBloque];
                        							nicioBloque++;
                        						}
                        						cacheDatos3[4][posicion] = bloque;
                        						cacheDatos3[5][posicion] = 1;
                        		            }
                						}
            						}
                					this.FallodeCache(28);
                					Monitor.Exit(busD);
            						Monitor.Exit(caheDatos3); //soltar mi cache 
                					//Se le entrega el dato al registro
                                break;
                            }
    	                break;
    
    					case 43: //SW
    						switch(int.Parse(Thread.CurrentThread.Name))
                            {
                                case 1:
                                    //caculo de bloque y palabra
            						bool conseguido = false;
            						while(!conseguido)
            						{
            						    while(!Monitor.TryEnter(cacheDatos1))    
                    		            {
                    		               TicReloj(); 
                    		            }
                    					TickReloj();
                    					
                    					if(!Monitor.TryEnter(busD))
                    		            {
                    		                Monitor.Exit(cacheDatos1); //cambiar por mi cache de datos
                                            TicReloj();
                    		            	
                    		            }else
                    		            {
                    		            	TickReloj();
	                    		            if(!Monitor.TryEnter(cacheDatos2))
	                    		            {
	                    		            	Monitor.Exit(busD);
	                    		                Monitor.Exit(cacheDatos1);
	                                            TicReloj();
	                    		            	
	                    		            }else
	                    		            {
	                    		            	TickReloj();
	                    		            	if(bloque == cacheDatos2[posicion]) 
                								{
                									cacheDatos2[posicion] = -1;
	                    		        		}
	                    		        		Monitor.Exit(cacheDatos2);
	                    		        		
	                    		        		if(!Monitor.TryEnter(cacheDatos3))
		                    		            {
		                    		            	Monitor.Exit(busD);
		                    		                Monitor.Exit(cacheDatos1);
		                                            TicReloj();
	                    		        		}else
	                    		        		{
	                    		        			TickReloj();
	                    		        			if(bloque == cacheDatos3[posicion]) 
	                								{
	                									cacheDatos3[posicion] = -1;
		                    		        		}
		                    		        		Monitor.Exit(cacheDatos3);
		                    		        		conseguido = true;
	                    		        		}
                    		            	}
                    		            }
            						}
            						if((bloque == cacheDatos1[4][posicion]) && (cacheDatos1[5][posicion] == 1]))
            						{
                		               cacheDatos1[palabra][posicion] = ;//registro donde viene
                    		            
            						}
            						memDatos[palabra] = ; //registro donde viene
                					this.FallodeCache(7);
                					Monitor.Exit(busD);
            						Monitor.Exit(cacheDatos1);
                                break;
                               
                                case 2:
                                    //caculo de bloque y palabra
            						bool conseguido = false;
            						while(!conseguido)
            						{
            						    while(!Monitor.TryEnter(cacheDatos2))    
                    		            {
                    		               TicReloj(); 
                    		            }
                    					TickReloj();
                    					
                    					if(!Monitor.TryEnter(busD))
                    		            {
                    		                Monitor.Exit(cacheDatos2); 
                                            TicReloj();
                    		            	
                    		            }else
                    		            {
                    		            	TickReloj();
	                    		            if(!Monitor.TryEnter(cacheDatos1))
	                    		            {
	                    		            	Monitor.Exit(busD);
	                    		                Monitor.Exit(cacheDatos2);
	                                            TicReloj();
	                    		            	
	                    		            }else
	                    		            {
	                    		            	TickReloj();
	                    		            	if(bloque == cacheDatos1[posicion]) 
                								{
                									cacheDatos1[posicion] = -1;
	                    		        		}
	                    		        		Monitor.Exit(cacheDatos1);
	                    		        		
	                    		        		if(!Monitor.TryEnter(cacheDatos3))
		                    		            {
		                    		            	Monitor.Exit(busD);
		                    		                Monitor.Exit(cacheDatos2);
		                                            TicReloj();
	                    		        		}else
	                    		        		{
	                    		        			TickReloj();
	                    		        			if(bloque == cacheDatos3[posicion]) 
	                								{
	                									cacheDatos3[posicion] = -1;
		                    		        		}
		                    		        		Monitor.Exit(cacheDatos3);
		                    		        		conseguido = true;
	                    		        		}
                    		            	}
                    		            }
            						}
            						if((bloque == cacheDatos1[4][posicion]) && (cacheDatos1[5][posicion] == 1]))
            						{
                		               cacheDatos1[palabra][posicion] = ;//registro donde viene
                    		            
            						}
            						memDatos[palabra] = ; //registro donde viene
                					this.FallodeCache(7);
                					Monitor.Exit(busD);
            						Monitor.Exit(cacheDatos1);
                                break;
                               
                                case 3:
                                    //caculo de bloque y palabra
            						bool conseguido = false;
            						while(!conseguido)
            						{
            						    while(!Monitor.TryEnter(cacheDatos3))    
                    		            {
                    		               TicReloj(); 
                    		            }
                    					TickReloj();
                    					
                    					if(!Monitor.TryEnter(busD))
                    		            {
                    		                Monitor.Exit(cacheDatos3); 
                                            TicReloj();
                    		            	
                    		            }else
                    		            {
                    		            	TickReloj();
	                    		            if(!Monitor.TryEnter(cacheDatos1))
	                    		            {
	                    		            	Monitor.Exit(busD);
	                    		                Monitor.Exit(cacheDatos3);
	                                            TicReloj();
	                    		            	
	                    		            }else
	                    		            {
	                    		            	TickReloj();
	                    		            	if(bloque == cacheDatos1[posicion]) 
                								{
                									cacheDatos1[posicion] = -1;
	                    		        		}
	                    		        		Monitor.Exit(cacheDatos1);
	                    		        		
	                    		        		if(!Monitor.TryEnter(cacheDatos2))
		                    		            {
		                    		            	Monitor.Exit(busD);
		                    		                Monitor.Exit(cacheDatos3);
		                                            TicReloj();
	                    		        		}else
	                    		        		{
	                    		        			TickReloj();
	                    		        			if(bloque == cacheDatos2[posicion]) 
	                								{
	                									cacheDatos2[posicion] = -1;
		                    		        		}
		                    		        		Monitor.Exit(cacheDatos2);
		                    		        		conseguido = true;
	                    		        		}
                    		            	}
                    		            }
            						}
            						if((bloque == cacheDatos1[4][posicion]) && (cacheDatos1[5][posicion] == 1]))
            						{
                		               cacheDatos1[palabra][posicion] = ;//registro donde viene
                    		            
            						}
            						memDatos[palabra] = ; //registro donde viene
                					this.FallodeCache(7);
                					Monitor.Exit(busD);
            						Monitor.Exit(cacheDatos1);
                                break;
                            }
    					break;
    
    					case 63 : //FIN
    
    						quantum = -1;  // Para tener el control de que la ultima instruccion fue FIN
    					break;
    				}
    				
    				quantum--; //lo resto al finalizar una instruccion
    				
    				if(quatum < 0)//ultima fue FIN
    				{
    						
    					while(!Monitor.TryEnter(finalizados))
    		            {
    		               TicReloj();
    		            }
    					TicReloj();
    					
    					finalizados.Guardar(PC, ref reg);
    					Monitor.Exit(finalizados);
    				}
    				else
    				{
    					if(quantum == 0)//Se termino el quantum
    					{
    						while(!Monitor.TryEnter(cola))
        		            {
        		               TicReloj(); 
        		            }
    						TicReloj() 
    						cola.Guardar(PC, ref reg);
    						Monitor.Exit(cola);
    					}
    				}
    				TicReloj();
    				
               }//FIN del quantum
           }//FIN del while(true)
        } //FIN de Nucleos    
        
    }//FIN de la clase
}//FIN del namespace