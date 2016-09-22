//Programa en C# los que estaba aqui esta en el archivo comentariosUtiles

using System; //Uso de la pantalla para impimir // igual al using namespace std
static void Main()
{
   Console.WriteLine("Hello World!"); //Escribir en consola
   
   string path = @"c:\temp\MyTest.txt";
   
   using (StreamReader sr = new StreamReader("TestFile.txt")); // objeto que lee de archivo
   sr.Read(Char[],Int32,Int32) ;     //metodo que Lee un máximo especificado de caracteres de la secuencia actual en un búfer, comenzando en el índice especificado
   String line = sr.ReadLine()	//Lee una línea de caracteres de la secuencia actual y devuelve los datos como una cadena.
   sr.Read(); //Lee el siguiente carácter de la secuencia de entrada y hace avanzar la posición de los caracteres en un carácter
}