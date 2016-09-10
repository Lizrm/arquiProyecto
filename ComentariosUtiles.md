Deben haber 4 hilos
el primero pide los datos de la cantidad de hilos, ubicacion del archivo y quantum

Debemos implementar lo de proporcionar el directorio para ejecutar

Corroborar que la invalidacion se hizo o no ERROR

BARRERA

Esperar hasta que el numero de hilos indicado alcance la barrera
pthread_barrier_wait(&barrera); // &barrera => puntero a la barrera

Destruir la barrera
pthread_barrier_destroy(&barrera);

    /**   
  *   inicializa la barrera
  *   &barrera => puntero a la barrera
  *   NULL => atributos de la barrrera, por ser NULL 
  *           se usa la barrera por defecto
  *   4 => cantidad de hilos que debe esperar la barrera para 
  *        continuar en este caso son 4, los tres procesadores 
  *        y el hilo principal
  *
  **/    

//pthread_barrier_init(&barrera, NULL, 4);

