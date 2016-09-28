//Thread.CurrentThread.Name.Equals("2") == variable

               switch(int.Parse(Thread.CurrentThread.Name))
               {
                   
                   case 1:
                        //caculo de bloque y palabra
						bool conseguido = false;
						while(!conseguido)
						{
						    while(!Monitor.TryEnter(cacheDatos))    //cambiar por mi cache
        		            {
        		               TicReloj(); 
        		            }
        					TickReloj();
    						if(!(bloque == cacheDatos[posicion]) && !(cacheDatos[posicion == 1]))
    						{
    						    if(!Monitor.TryEnter(busD))
            		            {
            		                Monitor.Exit(cacheDatos); //cambiar por mi cache de datos
                                    TicReloj();            		               
            		            }else
            		            {
            		                conseguido = true;
            		                TicReloj();
            		                iterador = inicioBloque;    //inicio del bloque a copiar
        						    switch(myID)   //Id del proceso
            						{
            							case 1:
            							    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                    						{
                    							cacheDatos1[i][posicion] = memDatos[iterador];
                    							iterador++;
                    						}
                    						cacheDatos1[4][posicion] = bloque;
                    						cacheDatos1[5][posicion] = 1;
                    						
            							break;
            							case 2:
            							    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                    						{
                    							cacheDatos2[i][posicion] = memDatos[iterador];
                    							iterador++;
                    						}
                    						cacheDatos2[4][posicion] = bloque;
                    						cacheDatos2[5][posicion] = 1;
            							break;
            							case 3:
            							    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                    						{
                    							cacheDatos3[i][posicion] = memDatos[iterador];
                    							iterador++;
                    						}
                    						cacheDatos3[4][posicion] = bloque;
                    						cacheDatos3[5][posicion] = 1;
            							break;
            						}
            		            }
    						}
						}
    					this.FallodeCache(28);
    					Monitor.Exit(busD);
						//Monitor.Exit(); //soltar mi cache 
    					//Se le entrega el dato al registro 
                        
                   break;
                   
                   case 2:
                        //caculo de bloque y palabra
						bool conseguido = false;
						while(!conseguido)
						{
						    while(!Monitor.TryEnter(cacheDatos))    //cambiar por mi cache
        		            {
        		               TicReloj(); 
        		            }
        					TickReloj();
    						if(!(bloque == cacheDatos[posicion]) && !(cacheDatos[posicion == 1]))
    						{
    						    if(!Monitor.TryEnter(busD))
            		            {
            		                Monitor.Exit(cacheDatos); //cambiar por mi cache de datos
                                    TicReloj();            		               
            		            }else
            		            {
            		                conseguido = true;
            		                TicReloj();
            		                iterador = inicioBloque;    //inicio del bloque a copiar
        						    switch(myID)   //Id del proceso
            						{
            							case 1:
            							    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                    						{
                    							cacheDatos1[i][posicion] = memDatos[iterador];
                    							iterador++;
                    						}
                    						cacheDatos1[4][posicion] = bloque;
                    						cacheDatos1[5][posicion] = 1;
                    						
            							break;
            							case 2:
            							    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                    						{
                    							cacheDatos2[i][posicion] = memDatos[iterador];
                    							iterador++;
                    						}
                    						cacheDatos2[4][posicion] = bloque;
                    						cacheDatos2[5][posicion] = 1;
            							break;
            							case 3:
            							    for(int i = 0; i < 4; ++i) //Copia los datos de memoria a Cache
                    						{
                    							cacheDatos3[i][posicion] = memDatos[iterador];
                    							iterador++;
                    						}
                    						cacheDatos3[4][posicion] = bloque;
                    						cacheDatos3[5][posicion] = 1;
            							break;
            						}
            		            }
    						}
						}
    					this.FallodeCache(28);
    					Monitor.Exit(busD);
						//Monitor.Exit(); //soltar mi cache 
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
    						if(!(bloque == cacheDatos3[posicion]) && !(cacheDatos3[posicion == 1]))
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
            							cacheDatos3[i][posicion] = memDatos[iterador];
            							iterador++;
            						}
            						cacheDatos3[4][posicion] = bloque;
            						cacheDatos3[5][posicion] = 1;
            		            }
    						}
						}
    					this.FallodeCache(28);
    					Monitor.Exit(busD);
						//Monitor.Exit(caheDatos3); //soltar mi cache 
    					//Se le entrega el dato al registro 
                        
                   break;
               }