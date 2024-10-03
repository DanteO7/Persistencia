static class Sistema
{
    static List<string> frases = new();
    static readonly string archivo = "frases.txt";
    static readonly string separadorFrase = "-";

    public static void IngresarCadenaDeTexto()
    {
        Console.Write("Ingrese la cadena de texto: ");
        string cadenaDelUsuario = Console.ReadLine();

        foreach (var frase in frases)
        {
            if(cadenaDelUsuario.ToLower() == frase.ToLower())
            {
                Console.WriteLine("Esa cadena de texto ya existe.");
                return;
            }
        }

        frases.Add(cadenaDelUsuario);
        Console.WriteLine("Cadena ingresada correctamente.");

        // Usé un foreach para la verificación porque con .Contains() tenía que almacenar la cadena 
        // en minúsculas con .ToLower(), y se modificaría de la original que ingreso el usuario
    }

    public static void ContarPalabrasDeUnaFrase()
    {
        if(frases.Count == 0)
        {
            Console.WriteLine("Aún no se han ingresado frases.");
            return;
        }
        Console.WriteLine("Seleccione la frase que desea contar las palabras: ");

        for (int i = 0; i < frases.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {frases[i]}");
        }
        int opcion = int.Parse(Console.ReadLine());

        if(opcion > frases.Count || opcion < 1)
        {
            Console.WriteLine("Opcion de frase no válida.");
            return;
        }

        string[] palabras = frases[opcion - 1].Split(' ');

        Console.WriteLine($"La frase contiene {palabras.Length} palabras.");
    }

    public static void MostrarListaDePalabrasConSuFrecuencia()
    {
        if (frases.Count == 0)
        {
            Console.WriteLine("Aún no se han ingresado frases.");
            return;
        }
        Dictionary<string, int> contadorPalabras = new();

        Console.WriteLine("Seleccione la frase que desea ver las palabras con su frecuencia: ");

        for (int i = 0; i < frases.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {frases[i]}");
        }
        int opcion = int.Parse(Console.ReadLine());

        if (opcion > frases.Count || opcion < 1)
        {
            Console.WriteLine("Opcion de frase no válida.");
            return;
        }

        string[] palabras = frases[opcion - 1].ToLower().Split(' ');

        foreach (var palabra in palabras)
        {
            if (contadorPalabras.ContainsKey(palabra))
            {
                contadorPalabras[palabra]++;
            }
            else
            {
                contadorPalabras.Add(palabra, 1);
            }
        }

        Console.WriteLine("Frecuencia de palabras: ");
        foreach (var palabra in contadorPalabras)
        {
            Console.WriteLine($"{palabra.Key}: {palabra.Value}");
        }
    }

    public static void GuardarDatos()
    {
        using StreamWriter writer = new StreamWriter(archivo);

        foreach(var frase in frases)
        {
            writer.WriteLine(frase);

            writer.WriteLine(separadorFrase);
        }

        Console.WriteLine("Datos guardados correctamente");
    }

    public static void CargarDatos()
    {
        if (File.Exists(archivo))
        {
            using StreamReader reader = new StreamReader(archivo);

            string linea;

            string fraseActual = null;

            while((linea = reader.ReadLine()) != null)
            {
                if(linea == separadorFrase)
                {
                    fraseActual = null;
                }
                else
                {
                    fraseActual = linea;
                    frases.Add(linea);
                }
            }
        }
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
            Console.WriteLine("1. Ingresar cadena de texto.");
            Console.WriteLine("2. Contar cuantas veces aparece cada palabra.");
            Console.WriteLine("3. Mostrar la lista de palabras junto con su frecuencia.");
            Console.WriteLine("4. Guardar y Salir.\n");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Sistema.IngresarCadenaDeTexto();
                    Console.WriteLine("\n");
                    break;
                case 2:
                    Sistema.ContarPalabrasDeUnaFrase();
                    Console.WriteLine("\n");
                    break;
                case 3:
                    Sistema.MostrarListaDePalabrasConSuFrecuencia();
                    Console.WriteLine("\n");
                    break;
                case 4:
                    Sistema.GuardarDatos();
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine("Opción no válida, ingrese una opción correcta.");
                    Console.WriteLine("\n");
                    break;
            }
        }
        while(opcion != 4);
    }
}