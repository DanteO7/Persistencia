namespace SistemaBiblioteca.Models
{
    public class Program
    {
        static void Main()
        {
            Biblioteca.CargarDatos(Biblioteca.ArchivoLibro, "-");
            Biblioteca.CargarDatos(Biblioteca.ArchivoUsuario, "-");
            Biblioteca.CargarDatos(Biblioteca.ArchivoPrestamo, "-");

            int opcion;

            do
            {
                Console.WriteLine("========= Biblioteca =========");
                Console.WriteLine("1. Agregar libro.");
                Console.WriteLine("2. Agregar usuario.");
                Console.WriteLine("3. Realizar préstamo.");
                Console.WriteLine("4. Devolver libro.");
                Console.WriteLine("5. Guardar y Salir.\n");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Biblioteca.AgregarLibro();
                        Console.WriteLine("\n");
                        break;
                    case 2:
                        Biblioteca.AgregarUsuario();
                        Console.WriteLine("\n");
                        break;
                    case 3:
                        Biblioteca.RealizarPrestamo();
                        Console.WriteLine("\n");
                        break;
                    case 4:
                        Biblioteca.DevolverLibro();
                        Console.WriteLine("\n");
                        break;
                    case 5:
                        Console.WriteLine("Presione cualquier tecla para salir.");
                        Console.ReadKey();
                        Console.WriteLine("\n");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, ingrese una opción correcta.");
                        Console.WriteLine("\n");
                        break;
                }
            }
            while (opcion != 5);
        }
    }
}