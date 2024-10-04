namespace GestionBiblioteca.Models
{
    public class Prestamo
    {
        private Libro _libro;
        private DateTime _fechaPrestamo;
        private DateTime _fechaDevolucion;

        public Libro Libro => _libro;
        public DateTime FechaPrestamo => _fechaPrestamo;
        public DateTime FechaDevolucion => _fechaDevolucion;

        public Prestamo( Libro libro, DateTime fechaPrestamo)
        {
            _libro = libro;
            _fechaPrestamo = fechaPrestamo;
            _fechaDevolucion.AddDays(10);
        }
    }
}
