namespace SistemaBiblioteca.Models
{
    public class Program
    {
        static void Main()
        {
            Biblioteca.CargarDatos(Biblioteca.ArchivoLibro);
            Biblioteca.CargarDatos(Biblioteca.ArchivoUsuario);
            Biblioteca.CargarDatos(Biblioteca.ArchivoPrestamo);

            int opcion;

            try
            {
                do
                {
                    Console.WriteLine("========= Biblioteca =========");
                    Console.WriteLine("1. Agregar libro.");
                    Console.WriteLine("2. Agregar usuario.");
                    Console.WriteLine("3. Mostrar libros.");
                    Console.WriteLine("4. Mostrar usuarios.");
                    Console.WriteLine("5. Realizar préstamo.");
                    Console.WriteLine("6. Devolver libro.");
                    Console.WriteLine("7. Guardar y Salir.\n");
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
                            Biblioteca.MostrarLibros();
                            Console.WriteLine("\n");
                            break;
                        case 4:
                            Biblioteca.MostrarUsuarios();
                            Console.WriteLine("\n");
                            break;
                        case 5:
                            Biblioteca.RealizarPrestamo();
                            Console.WriteLine("\n");
                            break;
                        case 6:
                            Biblioteca.DevolverLibro();
                            Console.WriteLine("\n");
                            break;
                        case 7:
                            Console.Write("Presione cualquier tecla para salir...");
                            Console.ReadKey();
                            Console.WriteLine("\n");
                            break;
                        default:
                            Console.WriteLine("Opción no válida, ingrese una opción correcta.");
                            Console.WriteLine("\n");
                            break;
                    }
                }
                while (opcion != 7);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
