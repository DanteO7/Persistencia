namespace GestionBiblioteca.Models
{
    public static class Biblioteca
    {
        private static List<Libro> _libros = new List<Libro>();
        private static List<Usuario> _usuarios = new List<Usuario>();
        static readonly string _archivoUsuario = "usuarios.txt";
        static readonly string _archivoLibro = "libros.txt";

        public static List<Libro> Libros => _libros;
        public static List<Usuario> Usuarios => _usuarios;

        public static void AgregarLibro(Libro libro) => _libros.Add(libro);
        public static void AgregarUsuario(Usuario usuario) => _usuarios.Add(usuario);

        public static void RealizarPrestamo(Libro libro, Usuario usuario)
        {
            if(libro.EjemplaresDisponibles <= 0)
            {
                Console.WriteLine("No hay ejemplares disponibles de este libro.");
                return;
            }

            Prestamo prestamo = new Prestamo(libro, DateTime.Now);

            usuario.AgregarPrestamo(prestamo);

            libro.ModificarEjemplares(false);

            Console.WriteLine($"Prestamo realizado con éxito");
        }

        public static void DevolverLibro(Libro libro, Usuario usuario)
        {
            Prestamo prestamo = usuario.Prestamos.Find(p => p.Libro == libro);

            if(prestamo == null)
            {
                Console.WriteLine($"El usuario {usuario.Nombre} no realizo el prestamo del libro {libro.Titulo}");
            }
            else
            {
                usuario.QuitarPrestamo(prestamo);
                libro.ModificarEjemplares(true);
                Console.WriteLine("Prestamo finalizado con éxito");
            }
        }
    }
}
