namespace GestionBiblioteca.Models
{
    public class Libro
    {
        private string _codigo;
        private string _titulo;
        private string _autor;
        private int _ejemplaresDisponibles;

        public string Codigo => _codigo;
        public string Titulo => _titulo;
        public string Autor => _autor;
        public int EjemplaresDisponibles => _ejemplaresDisponibles;

        public Libro(string codigo, string titulo, string autor, int ejemplaresDisponibles)
        {
            _codigo = codigo;
            _titulo = titulo;
            _autor = autor;
            _ejemplaresDisponibles = ejemplaresDisponibles;
        }

        public void ModificarEjemplares(bool incrementar)
        {
            if (incrementar)
            {
                _ejemplaresDisponibles++;
            }
            else
            {
                _ejemplaresDisponibles--;
            }
        }
    }
}
