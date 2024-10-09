using System.Linq;

namespace SistemaBiblioteca.Models
{
    public static class Biblioteca
    {
        private static List<Libro> _libros = new List<Libro>();
        private static List<Usuario> _usuarios = new List<Usuario>();
        public static readonly string ArchivoUsuario = "usuarios.txt";
        public static readonly string ArchivoLibro = "libros.txt";
        public static readonly string ArchivoPrestamo = "prestamos.txt";

        public static List<Libro> Libros => _libros;
        public static List<Usuario> Usuarios => _usuarios;

        public static void AgregarLibro()
        {
            Console.Write("Ingrese el código del libro: ");
            string codigo = Console.ReadLine();

            Console.Write("Ingrese el título del libro: ");
            string titulo = Console.ReadLine();

            Console.Write("Ingrese el autor del libro: ");
            string autor = Console.ReadLine();

            Console.Write("Ingrese la cantidad de ejemplares disponibles: ");
            int ejemplares = int.Parse(Console.ReadLine());

            Libro nuevoLibro = new Libro(codigo, titulo, autor, ejemplares);

            foreach (var libro in _libros)
            {
                if(libro.Codigo == nuevoLibro.Codigo)
                {
                    Console.WriteLine("Este código de libro ya existe.");
                    return;
                }
            }
            _libros.Add(nuevoLibro);
            GuardarDatos(ArchivoLibro, nuevoLibro);
            Console.WriteLine("Libro agregado exitosamente!");
        }
        public static void AgregarUsuario()
        {
            Console.Write("Ingrese el nombre del usuario: ");
            string nombreUsuario = Console.ReadLine();

            Usuario usuarioNuevo = new Usuario(nombreUsuario);

            if (Usuarios.Contains(usuarioNuevo))
            {
                Console.WriteLine("Ya existe un usuario con ese nombre.");
            }

            _usuarios.Add(usuarioNuevo);
            Console.WriteLine("Usuario agregado exitosamente!");
            GuardarDatos(ArchivoUsuario, usuarioNuevo);
        }

        public static void RealizarPrestamo()
        {
            if(Usuarios.Count == 0 || Libros.Count == 0)
            {
                Console.WriteLine("No hay suficientes libros o usuarios disponibles para realizar un prestamo");
                return;
            }

            Console.WriteLine("Elige el usuario que va a realizar el préstamo:");
            for (int i = 0; i < Usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Usuarios[i].Nombre}");
            }
            int usuarioSeleccionado = int.Parse(Console.ReadLine());

            Console.WriteLine("Elige el libro que el usuario quiere llevarse:");
            for (int i = 0; i < Libros.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Libros[i].Codigo}, {Libros[i].Titulo}");
            }
            int libroSeleccionado = int.Parse(Console.ReadLine());

            if (Libros[libroSeleccionado - 1].EjemplaresDisponibles <= 0)
            {
                Console.WriteLine("No hay ejemplares disponibles de este libro.");
                return;
            }

            Prestamo prestamo = new Prestamo(Usuarios[usuarioSeleccionado - 1],Libros[libroSeleccionado - 1], DateTime.Now);

            Usuarios[usuarioSeleccionado - 1].AgregarPrestamo(prestamo);
            GuardarDatos(ArchivoPrestamo, prestamo);
            Libros[libroSeleccionado - 1].ModificarEjemplares(false);

            Console.WriteLine($"Prestamo realizado con éxito!");
        }

        public static void DevolverLibro()
        {
            Console.WriteLine("Elige el usuario que va a devolver el libro:");
            for (int i = 0; i < Usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Usuarios[i].Nombre}");
            }
            int usuarioDevuelto = int.Parse(Console.ReadLine());

            Console.WriteLine("Elige el libro que el usuario devuelve:");
            for (int i = 0; i < Libros.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Libros[i].Codigo}, {Libros[i].Titulo}");
            }
            int libroDevuelto = int.Parse(Console.ReadLine());

            Prestamo prestamo = Usuarios[usuarioDevuelto - 1].Prestamos.Find(p => p.Libro == Libros[libroDevuelto - 1]);

            if (prestamo == null)
            {
                Console.WriteLine($"El usuario {Usuarios[usuarioDevuelto - 1].Nombre} no realizo el prestamo del libro {Libros[libroDevuelto - 1].Titulo}");
            }
            else
            {
                Usuarios[usuarioDevuelto - 1].QuitarPrestamo(prestamo);
                Libros[libroDevuelto - 1].ModificarEjemplares(true);
                Console.WriteLine("Prestamo finalizado con éxito!");
            }
        }

        public static void GuardarDatos<T>(string archivo, T entidad) where T : class
        {
            using StreamWriter writer = new StreamWriter(archivo, true);

            writer.WriteLine(entidad);
        }

        public static void CargarDatos(string archivo, string separador)
        {
            if (File.Exists(archivo))
            {
                using StreamReader reader = new StreamReader(archivo);

                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] partes = linea.Split(separador);

                    if(archivo == "libros.txt")
                    {
                        Libro libro = new Libro(partes[0], partes[1], partes[2], int.Parse(partes[3]));
                        Libros.Add(libro);
                    }
                    else if (archivo == "usuarios.txt")
                    {
                        Usuario usuario = new Usuario(linea);
                        Usuarios.Add(usuario);
                    }
                    else
                    {
                        foreach(var libro in Libros)
                        {
                            if(libro.Codigo == partes[1])
                            {
                                foreach(var usuario in Usuarios)
                                {
                                    if(usuario.Nombre == partes[0])
                                    {
                                        Prestamo prestamo = new Prestamo(usuario, libro, DateTime.Parse(partes[2]));
                                        usuario.AgregarPrestamo(prestamo);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void MostrarLibros()
        {
            foreach(var libro in Libros)
            {
                Console.WriteLine(libro);
            }
        }

        public static void MostrarUsuarios()
        {
            foreach (var usuario in Usuarios)
            {
                Console.WriteLine(usuario);
                foreach(var prestamo  in usuario.Prestamos)
                {
                    Console.WriteLine(prestamo);
                }
            }
        }
    }
}
