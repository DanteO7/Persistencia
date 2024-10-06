namespace SistemaBiblioteca.Models
{
    public class Usuario
    {
        private string _nombre;
        private List<Prestamo> _prestamos = new List<Prestamo>();

        public string Nombre => _nombre;
        public List<Prestamo> Prestamos => _prestamos;

        public Usuario(string nombre)
        {
            _nombre = nombre;
        }

        public void AgregarPrestamo(Prestamo prestamo)
        {
            _prestamos.Add(prestamo);
        }

        public void QuitarPrestamo(Prestamo prestamo)
        {
            _prestamos.Remove(prestamo);
        }

        public override string ToString()
        {
            return $"{Nombre}";
        }
    }
}
