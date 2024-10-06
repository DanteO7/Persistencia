namespace SistemaBiblioteca.Models
{
    public class Prestamo
    {
        private Usuario _usuario;
        private Libro _libro;
        private DateTime _fechaPrestamo;
        private DateTime _fechaDevolucion;

        public Usuario Usuario => _usuario;
        public Libro Libro => _libro;
        public DateTime FechaPrestamo => _fechaPrestamo;
        public DateTime FechaDevolucion => _fechaDevolucion;

        public Prestamo(Usuario usuario, Libro libro, DateTime fechaPrestamo)
        {
            _usuario = usuario;
            _libro = libro;
            _fechaPrestamo = fechaPrestamo;
            _fechaDevolucion = _fechaPrestamo.AddDays(10);
        }

        public override string ToString()
        {
            return $"{Usuario}-{Libro.Codigo}-{FechaPrestamo}-{FechaDevolucion}";
        }
    }
}
