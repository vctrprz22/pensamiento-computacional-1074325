Console.WriteLine("Selecciones una de las opciones");

Console.WriteLine("1. Pasar de Celsius a Fahrenheit");
Console.WriteLine("2. Pasar de Fahrenheit a Celsius");
Console.WriteLine("3. Informacion del programador");
Console.WriteLine("4. Salir");
int opcion = Convert.ToInt32(Console.ReadLine());   

switch (opcion)
{
    case 1:
        Console.WriteLine("Ingrese la temperatura en Celsius: ");
        int gradosC = Convert.ToInt32(Console.ReadLine());
        int celsius = ConvertirFahrenheit(gradosC);
        Console.WriteLine($"La temperatura en Fahrenheit es: {celsius}");
        break;

    case 2:
        Console.WriteLine("Ingrese la temperatura en Fahrenheit: ");
        int gradosF = Convert.ToInt32(Console.ReadLine());
        int fahrenheit = ConvertirCelsius(gradosF);
        Console.WriteLine($"La temperatura en Celsius es: {fahrenheit}");
        break;
    
    case 3:
        Informacion();
        break;
    
    case 4:
        Console.WriteLine("Va a salir del programa");
        break;
}

int ConvertirFahrenheit(int gradosC)
{
    int resultado = (gradosC * 9 / 5) + 32;
    return resultado;
}

int ConvertirCelsius(int gradosF)
{
    int resultado = (gradosF - 32) * 5 / 9;
    return resultado;
}

void Informacion()
{
    Console.WriteLine("Mi nombre es Victor Perez");
    Console.WriteLine("Mi numero de carné es 1074325");
    Console.WriteLine("Naci el 27 de enero de 2207");
    Console.WriteLine("Mi correo es perezvictordavid83@gmail.com");

}