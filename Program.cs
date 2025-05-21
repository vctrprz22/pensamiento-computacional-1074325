class Vehiculo // Clase que representa un vehiculo
{
    public String Marca { get; set; } //marca del vehiculo
    public string Color { get; set; } //color del vehiculo
    public string Placa { get; set; } //placa del vehiculo
    public string Tipo { get; set; } // Tipo: moto, sedan, suv.
    public int HoraEntrada { get; set; } //hora de entrada del vehiculo

    // constructor para los atributos del vehiculo
    public Vehiculo(string marca, string color, string placa, string tipo, int hora)
    {
        this.Marca = marca;
        this.Color = color;
        this.Placa = placa;
        this.Tipo = tipo;
        this.HoraEntrada = hora;
    }
}

// Clase principal para el programa
class Programa
{
    // matriz para los codigos del estacionamiento
    static string[,] codigos;

    // matriz de estado, 0 = vacio, 1 = libre
    static int[,] estado;

    // matriz de objetos "Vehiculos" para guardar los datos
    static Vehiculo[,] vehiculos;

    // configuracion inicial
    static int pisos, porPiso; // Numero de pisos y espacios por pisos
    static int motos, suvs, sedans; // Cantidad de espacios para cada tipo
    static Random rand = new Random(); // Generador aleatorio

    // Metodo principal del programa
    static void Main()
    {
        Console.WriteLine("Bienvenido al sistema de gestion de parqueos.");
        ConfigurarSistema(); // para solicitar la configuracion que ingrese el usaurio
        int opcion;
        do
        {
            // Mostrar el menu de opciones
            Console.WriteLine("1. Ingresar vehiculo");
            Console.WriteLine("2. Ingresar lote vehiculos");
            Console.WriteLine("3. Encontrar vehiculo");
            Console.WriteLine("4. Retirar vehiculo");
            Console.WriteLine("5. Salir");

            // Intenta ller un numero para la opcion 
            if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = 0;

            // Que se realizara segun la opcion seleccionada
            switch (opcion)
            {
                case 1:
                    IngresarVehiculo();
                    break;
                case 2:
                    IngresarLoteVehiculos();
                    break;
                case 3:
                    EncontrarVehiculo();
                    break;
                case 4:
                    RetirarVehiculo();
                    break;
                case 5:
                    Console.WriteLine("Saliendo del sistema");
                    break;
                default:
                    Console.WriteLine("Opcion no valida, intente de nuevo.");
                    break;
            }
        } while (opcion != 5);
    }
    // Se le solicita la configuracion del sitema al usuario 
    static void ConfigurarSistema()
    {
        Console.WriteLine("Ingrese el numero de pisos del estacionamiento:"); // ingresa el numero de  pisos
        pisos = int.Parse(Console.ReadLine());

        Console.WriteLine("Ingrese el numero de espacios por piso:"); // Ingresa el numero de estacionamientos por piso
        porPiso = int.Parse(Console.ReadLine());

        Console.WriteLine("Ingrese el numero de espacios para motos:"); // Ingresa el numero de estacionamientos para motos
        motos = int.Parse(Console.ReadLine());

        Console.WriteLine("Ingrese el numero de espacios para SUVs:"); // Ingresa el numero de estacionamientos para SUVs
        suvs = int.Parse(Console.ReadLine());

        sedans = pisos * porPiso - motos - suvs; // Espacios para sedans

        // Inicializa las matrices
        codigos = new string[pisos, porPiso];
        estado = new int[pisos, porPiso];
        vehiculos = new Vehiculo[pisos, porPiso];

        // Genera codigos para cada espacio
        for (int i = 0; i < pisos; i++)
        {
            for (int j = 0; j < porPiso; j++)
            {
                codigos[i, j] = $"{(char)('A' + i)}{j + 1}";
            }
        }
        Console.WriteLine($"Configuracion completa: {pisos} pisos, {porPiso} espacios por piso, {motos} motos, {suvs} SUVs, {sedans} sedans.");
    }
    // Ingreso manual de un vehiculo
    static void IngresarVehiculo()
    {
        Console.WriteLine("Ingrese la marca del vehiculo:");
        string marca = Console.ReadLine(); //EL usuario ingresa la marca del vehiculo

        Console.WriteLine("Ingrese el color del vehiculo:");
        string color = Console.ReadLine();//EL usuario ingresa el color del vehiculo

        string placa;
        do
        {
            Console.WriteLine("Ingrese una cadena  de 6 caracteres.");
            placa = Console.ReadLine().ToUpper();
        } while (placa.Length != 6);//EL usuario ingresa la placa del vehiculo

        string tipo;
        do
        {
            Console.WriteLine("Ingrese el tipo de vehiculo (moto, sedan, suv):");
            tipo = Console.ReadLine().ToLower();
        } while (tipo != "moto" && tipo != "sedan" && tipo != "suv");//EL usuario ingresa el tipo de vehiculo

        int hora;
        do
        {
            Console.WriteLine("Ingrese la hora de entrada (6-20):");
            hora = int.Parse(Console.ReadLine());//EL usuario ingresa la hora de entrada del vehiculo
        } while (hora < 6 || hora > 20);
        MostrarMapa(tipo); //Mostrar los espacios disponibles
        Console.WriteLine("Ingrese el codigo del espacio donde desea ingresar el vehiculo:");
        string codigo = Console.ReadLine().ToUpper();//EL usuario ingresa el codigo del espacio donde desea ingresar el vehiculo

        if (RegistrarVehiculo(codigo, new Vehiculo(marca, color, placa, tipo, hora)))// Registrar el vehiculo en el espacio especifico
        {
            Console.WriteLine("Vehiculo ingresado exitosamente.");
        }
        else
        {
            Console.WriteLine("Error al ingresar el vehiculo. Espacio no disponible o codigo invalido.");
        }
    }
    // Ingreso de varios vehiculos
    static void IngresarLoteVehiculos()
    {
        int cantidad = rand.Next(2, 7);
        string[] marcas = { "Toyota", "Honda", "Mazda", "Hyundai", "Suzuki" };// Lista de marcas
        string[] colores = { "Rojo", "Azul", "Gris", "Negro", "Blanco" };// Lista de colores
        string[] tipos = { "moto", "sedan", "suv" };// Lista de tipos

        Console.WriteLine($"Ingresand lote de {cantidad} vehiculos..");// Mensaje de ingreso de lote
        for (int k = 0; k < cantidad; k++)// Ingreso de vehiculos 
        {
            string marca = marcas[rand.Next(marcas.Length)];// Selecciona una marca aleatoria
            string color = colores[rand.Next(colores.Length)];// Selecciona un color aleatorio
            string tipo = tipos[rand.Next(tipos.Length)];// Selecciona un tipo aleatorio
            string placa = GenerarPlaca();// Genera una placa aleatoria
            int hora = rand.Next(6, 21);// Genera una hora aleatoria entre 6 y 20

            if (!RegistrarVehiculoLote(new Vehiculo(marca, color, placa, tipo, hora)))// Registrar el vehiculo en cualquier espacio disponible
                Console.WriteLine($"No hay espacio disponible para vehiculo {placa} ({tipo}).");
            else
                Console.WriteLine($"Vehiculo {placa} ({tipo}) ingresado exitosamente.");

        }
        Console.WriteLine("Matriz de estacionamiento actual:");// Mostrar la matriz de estacionamiento
        Console.WriteLine("Espacios ocupados: X, Espacios libres: Espacio");
        MostrarMapa("");
    }
    static void EncontrarVehiculo()
    {
        Console.WriteLine("Ingrese la placa del vehiculo a buscar:");// EL usuario ingresa la placa del vehiculo a buscar
        string placa = Console.ReadLine().ToUpper();

        for (int i = 0; i < pisos; i++)// Recorre los pisos
        {
            for (int j = 0; j < porPiso; j++)// Recorre los espacios por piso
            {
                if (vehiculos[i, j] != null && vehiculos[i, j].Placa == placa)// Verifica si el vehiculo existe
                {
                    Console.WriteLine($"Vehiculo encontrado en el espacio {codigos[i, j]}.");
                    return;// Muestra el espacio donde se encuentra el vehiculo
                }
            }
        }
        Console.WriteLine("Vehiculo no encontrado.");// Muestra que el vehiculo no fue encontrado
    }
    //Retiro de vehiculo y calculo de pago
    static void RetirarVehiculo()
    {
        Console.WriteLine("Ingrese el codigo de estacionamiento");// EL usuario ingresa el codigo de estacionamiento
        string codigo = Console.ReadLine().ToUpper();//EL usuario ingresa el codigo de estacionamiento
        (int i, int j) = ObtenerPosicion(codigo);// Convierte el codigo a indices de matriz
        if (i == -1 || estado[i, j] == 0)//Verificar si el espacio es valido o ocupado
        {
            Console.WriteLine("Espacio no valido o vacio.");
            return;
        }

        int horaEntrada = vehiculos[i, j].HoraEntrada;// Hora de entrada del vehiculo
        int tiempo = rand.Next(0, 25 - horaEntrada); // Tiempo de estancia aleatorio
        int costo = CalcularCosto(tiempo);
        
        

        Console.WriteLine($"Su estadia a sido de: {tiempo} horas, por lo tanto el costo es de: Q{costo}");
        Console.WriteLine("Ingrese su forma de pago: 1-Tarjeta, 2-Efectivo, 3-Sticker");

        int formaPago = LeerEntero("Forma de pago");// EL usuario ingresa la forma de pago
        if (formaPago == 2)
        {
            int pago = LeerEntero("Ingrese monto efectivo: Q");
            if (pago < costo)
            {
                Console.WriteLine("Monto insuficiente.");
                return;
            }
            else
            {
                int vuelto = pago - costo; // Calcula el vuelto
                Console.WriteLine($"Su cambio es de: {vuelto}");
                CalcularVuelto(vuelto);
            }
        }
        else
        {
            Console.WriteLine("Pago realizado con exito.");

        }

        estado[i, j] = 0; // Cambia el estado a vacio
        vehiculos[i, j] = null; // Elimina el vehiculo de la matriz
        Console.WriteLine($"Vehiculo retirado del espacio {codigos[i, j]}.");// Muestra el espacio donde se encontraba el vehiculo
        MostrarMapa("");
    }
    // Registrar vehiculo en espacio especifico
    static bool RegistrarVehiculo(string codigo, Vehiculo vehiculo)// Registrar el vehiculo en un espacio especifico
    {
        (int i, int j) = ObtenerPosicion(codigo);// Convierte el codigo a indices de matriz
        if (i == -1 || estado[i, j] == 1 || !EspacioCompatible(vehiculo.Tipo, i, j))
            return false; // Espacio no valido o ocupado

        estado[i, j] = 1; // Cambia el estado a ocupado
        vehiculos[i, j] = vehiculo; // Guarda el vehiculo en la matriz
        return true;
    }

    // Registrar vehiculo en cualquier espacio disponible
    static bool RegistrarVehiculoLote(Vehiculo vehiculo)
    {
        for (int i = 0; i < pisos; i++)// Recorre los pisos
        {
            for (int j = 0; j < porPiso; j++)// Recorre los espacios por piso
            {
                if (estado[i, j] == 0 && EspacioCompatible(vehiculo.Tipo, i, j)) // Espacio libre
                {
                    estado[i, j] = 1; // Cambia el estado a ocupado
                    vehiculos[i, j] = vehiculo; // Guarda el vehiculo en la matriz
                    Console.WriteLine($"Vehiculo {vehiculo.Placa} ingresado en el espacio {codigos[i, j]}.");
                    return true;
                }
            }
        }
        return false; // No hay espacio disponible
    }
    // Convertir el codigo tipo "A1" a indices de matriz
    static (int, int) ObtenerPosicion(string codigo)
    {
        if (codigo.Length < 2) return (-1, -1); // Codigo invalido
        int i = codigo[0] - 'A'; // Convertir letra a indice
        int j = int.Parse(codigo.Substring(1)) - 1; // Convertir numero a indice por ejemñplo A1 = 0,0, A2 = 0,1
        if (i < 0 || i >= pisos || j < 0 || j >= porPiso) return (-1, -1); // Fuera de rango
        return (i, j);
    }
    // verificar si el espacio es compatible con el tipo de vehiculo
    static bool EspacioCompatible(string tipo, int i, int j)
    {
        int index = i * porPiso + j;
        if (tipo == "moto" && index < motos) return true; // Espacio para moto
        if (tipo == "suv" && index >= motos && index < motos + suvs) return true; // Espacio para SUV       
        if (tipo == "sedan" && index >= motos + suvs) return true; // Espacio para sedan
        return false; // Espacio no compatible
    }
    // Mostrar el mapa de la disponibilidad de espacios
    static void MostrarMapa(string tipo)//Mostrar el mapa de disponibilidad de espacios
    {
        for (int i = 0; i < pisos; i++)// Recorre los pisos
        {
            for (int j = 0; j < porPiso; j++)// Recorre los espacios por piso
            {
                if (estado[i, j] == 0 && (tipo == "" || EspacioCompatible(tipo, i, j)))//Espacio libre
                {
                    Console.Write(codigos[i, j] + " ");// Muestra el codigo del espacio
                }
                else
                {
                    Console.Write("X "); // X indica que el espacio esta ocupado

                }
            }
            Console.WriteLine();// Nueva linea para el siguiente piso
        }
    }
    // Calcular el vuelto y muestro del costo
    static void CalcularVuelto(int vuelto)
    {
        int[] billetes = { 100, 50, 20, 10, 5, 1 };// Denominaciones de billetes
        int[] cantidadBilletes = new int[billetes.Length];// Cantidad de billetes de cada denominacion
        for (int i = 0; i < billetes.Length; i++)// Recorre las denominaciones
        {
            cantidadBilletes[i] = vuelto / billetes[i];// Calcula la cantidad de billetes
            vuelto %= billetes[i];// Calcula el vuelto restante
        }
        Console.WriteLine("Su vuelto es:");
        for (int i = 0; i < billetes.Length; i++)// Recorre las denominaciones
        {
            if (cantidadBilletes[i] > 0)// Si hay billetes de esa denominacion
            {
                Console.WriteLine($"Q{billetes[i]}: {cantidadBilletes[i]} billetes");// Muestra la cantidad de billetes
            }
        }


    }
    // Calcular el costo segun el timepo de estadia
    static int CalcularCosto(int tiempo)
    {
        if (tiempo <= 2) return 10; // Costo por 2 horas
        if (tiempo <= 4) return 20; // Costo por 4 horas
        if (tiempo <= 6) return 30; // Costo por 6 horas
        return 40; // Costo por mas de 6 horas
    }
    // Geberar una placa aleatoria
    static string GenerarPlaca()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";// Lista de caracteres
        char[] placa = new char[6];// Arreglo para la placa
        for (int i = 0; i < 6; i++)// Recorre los 6 caracteres
        {
            placa[i] = chars[rand.Next(chars.Length)];// Selecciona un caracter aleatorio
        }
        return new string(placa);
    }
    // Leer un entero con mensaje
    static int LeerEntero(string mensaje)// Metodo para leer un entero con mensaje
    {
        int numero;
        do
        {
            Console.WriteLine(mensaje);
            if (!int.TryParse(Console.ReadLine(), out numero))//intenta convertir la entrada a enteri
            {
                Console.WriteLine("Entrada no valida, intente de nuevo.");
            }
        } while (numero <= 0);
        return numero;
    }
}
