class Contacto
{
    public string Nombre { get; set; }
    public int NumeroTelefono { get; set; }
    public string CorreoElectronico { get; set; }

    public Contacto(string nombre, int numeroTelefono, string correoElectronico)
    {
        Nombre = nombre;
        NumeroTelefono = numeroTelefono;
        CorreoElectronico = correoElectronico;
    }

    public override string ToString()
    {
        return $"Nombre: {Nombre}, Teléfono: {NumeroTelefono}, Correo Electrónico: {CorreoElectronico}";
    }
}

static class Sistema
{
    static List<Contacto> ListaContactos = new();

    static readonly string archivo = "contactos.txt";
    static readonly string separadorContactos = "-";

    public static void AgregarContacto()
    {
        Console.Write("Ingrese el nombre del contacto: ");
        string nombreContacto = Console.ReadLine();

        Console.Write("Ingrese el telefono del contacto: ");
        int telefonoContacto = int.Parse(Console.ReadLine());

        Console.Write("Ingrese el correo electrónico del contacto: ");
        string correoContacto = Console.ReadLine();
        
        foreach(var c in ListaContactos)
        {
            if(c.Nombre == nombreContacto && c.NumeroTelefono == telefonoContacto)
            {
                Console.WriteLine("Este contacto ya existe.");
                return;
            }
        }
        
        Contacto contacto = new Contacto(nombreContacto, telefonoContacto, correoContacto);
        ListaContactos.Add(contacto);
        Sistema.GuardarContacto(contacto);
        Console.WriteLine("Contacto agregado correctamente.");
    }

    public static void MostrarContactos()
    {
        if (ListaContactos.Count == 0)
        {
            Console.WriteLine("No hay contactos aún.");
            return;
        }
        Console.WriteLine("Lista de contactos: ");
        foreach (var contacto in ListaContactos)
        {
            Console.WriteLine($"{contacto}\n");
        }
    }

    public static void CargarDatos()
    {
        if (File.Exists(archivo))
        {
            using StreamReader reader = new StreamReader(archivo);

            string linea;

            Contacto contactoActual = null;

            while((linea = reader.ReadLine()) != null)
            {
                if(linea == separadorContactos)
                {
                    contactoActual = null;
                }
                else
                {
                    string[] partes = linea.Split("|");

                    string nombre = partes[0];
                    int telefono = int.Parse(partes[1]);
                    string correo = partes[2];

                    Contacto contacto = new Contacto(nombre, telefono, correo);
                    ListaContactos.Add(contacto);
                }
            }
            Console.WriteLine("Datos cargados correctamente.");
        }
    }

    public static void GuardarContacto(Contacto contacto)
    {
        using StreamWriter writer = new StreamWriter(archivo, true);
        writer.WriteLine($"{contacto.Nombre}|{contacto.NumeroTelefono}|{contacto.CorreoElectronico}");
        writer.WriteLine(separadorContactos);
    } 
}

class Program
{
    static void Main()
    {
        Sistema.CargarDatos();

        int opcion;

        do
        {
            Console.WriteLine("============= Menú =============");
            Console.WriteLine("1. Agregar contacto.");
            Console.WriteLine("2. Mostrar todos los contactos.");
            Console.WriteLine("3. Guardar y Salir.\n");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Sistema.AgregarContacto();
                    Console.WriteLine("\n");
                    break;
                case 2:
                    Sistema.MostrarContactos();
                    Console.WriteLine("\n");
                    break;
                case 3:
                    Console.WriteLine("Saliendo del programa...");
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine("Opción no válida, ingrese una opción correcta.");
                    Console.WriteLine("\n");
                    break;
            }
        }
        while (opcion != 3);
    }
}
/*public void GuardarDatos<T>(string archivo, T entidad) where T : class
{
    if (File.Exist(archivo))
    {
        using StreamWriter writer = new StreamWriter(archivo, true)
        writer.WriteLine(entidad);
    }
}

public void GuardarDatos<T>(string archivo, List<T> entidades) where T : class
{
    if (File.Exist(archivo))
    {
        using StreamWriter writer = new StreamWriter(archivo, true)
        foreach(var e in entidades)
        {
            writer.WriteLine(e);
        } 
    }
}*/
