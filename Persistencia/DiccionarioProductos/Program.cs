class Producto
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int CantidadStock { get; set; }

    public Producto(string codigo, string nombre, int cantidadStock)
    {
        Codigo = codigo;
        Nombre = nombre;
        CantidadStock = cantidadStock;
    }

    public override string ToString()
    {
        return $"{Codigo}-{Nombre}-{CantidadStock}";
    }
}

static class Menu
{
    static Dictionary<string, Producto> CantidadProductos = new();
    public static readonly string archivo = "productos.txt";
    public static readonly string separadorProductos = "-";

    public static void AgregarProducto()
    {
        Console.Write("Ingrese el código del producto: ");
        string codigoProducto = Console.ReadLine();

        Console.Write("Ingrese el nombre del producto: ");
        string nombreProducto = Console.ReadLine();

        Console.Write("Ingrese la cantidad del producto: ");
        int cantidadProducto = int.Parse(Console.ReadLine());

        if (CantidadProductos.ContainsKey(codigoProducto))
        {
            Console.WriteLine($"El producto {codigoProducto} ya existe.");
            return;
        }

        Producto producto = new Producto(codigoProducto, nombreProducto, cantidadProducto);
        Archivo.GuardarDatos(archivo, producto);
        CantidadProductos.Add(codigoProducto, producto);
        Console.WriteLine("Producto agregado correctamente!");
    }

    public static void AgregarProducto(string[] partes)
    {
        Producto producto = new Producto(partes[0], partes[1], int.Parse(partes[2]));
        CantidadProductos.Add(partes[0], producto);
    }

    public static void ActualizarCantidadDeUnProducto()
    {
        if (CantidadProductos.Count == 0)
        {
            Console.WriteLine("No hay productos para actualizar la cantidad.");
            return;
        }

        Console.Write("Ingrese el codigo del producto que desea actualizar la cantidad: ");
        string codigoActualizar = Console.ReadLine();

        if (!CantidadProductos.ContainsKey(codigoActualizar))
        {
            Console.WriteLine("Código no encontrado");
            return;
        }

        Console.Write($"Ingrese la nueva cantidad de {CantidadProductos[codigoActualizar].Nombre}: ");
        int nuevaCantidad = int.Parse(Console.ReadLine());

        CantidadProductos[codigoActualizar].CantidadStock = nuevaCantidad;
        Console.WriteLine("Cantidad en stock actualizada!");
    }

    public static void MostrarProductos()
    {
        if(CantidadProductos.Count == 0)
        {
            Console.WriteLine("No hay productos para mostrar.");
            return;
        }

        Console.WriteLine("Productos en stock:");
        foreach(var producto in CantidadProductos.Values)
        {
            Console.WriteLine(producto);
        }
    }
}

static class Archivo
{
    public static void GuardarDatos<T>(string archivo, T entidad) where T : class
    {
        using StreamWriter writer = new StreamWriter(archivo, true);
        writer.WriteLine(entidad);
    }

    public static void GuardarDatos<T>(string archivo, List<T> entidades) where T : class
    {
        using StreamWriter writer = new StreamWriter(archivo, true);
        foreach (T entidad in entidades)
        {
            writer.WriteLine(entidad);
        }
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

                Menu.AgregarProducto(partes);
            }
            Console.WriteLine("Datos cargados correctamente.");
        }
    }
}

public class Program
{
    static void Main()
    {
        Archivo.CargarDatos(Menu.archivo, Menu.separadorProductos);

        int opcion;

        do
        {
            Console.WriteLine("============= Menú =============");
            Console.WriteLine("1 - Agregar un nuevo producto");
            Console.WriteLine("2 - Actualizar la cantidad en stock de un producto");
            Console.WriteLine("3 - Mostrar todos los productos en stock");
            Console.WriteLine("4 - Salir\n");

            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Menu.AgregarProducto();
                    Console.WriteLine("\n");
                    break;
                case 2:
                    Menu.ActualizarCantidadDeUnProducto();
                    Console.WriteLine("\n");
                    break;
                case 3:
                    Menu.MostrarProductos();
                    Console.WriteLine("\n");
                    break;
                case 4:
                    Console.WriteLine("Pulse cualquier tecla para salir.");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Opción inválida, ingrese nuevamente su opción.\n");
                    break;
            }
        }
        while (opcion != 4);
    }
}
