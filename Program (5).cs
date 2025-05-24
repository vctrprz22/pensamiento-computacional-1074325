Console.WriteLine("Ingrese el radio del cilindro:");
double radio1 = Convert.ToDouble(Console.ReadLine());
Console.WriteLine("Ingrese la altura del cilindro:");
double altura1 = Convert.ToDouble(Console.ReadLine());

Cilindro cilindro1 = new Cilindro(radio1, altura1);
cilindro1.MostrarVolumen();
class Cilindro
{
    private double Radio;
    private double Altura;
    private double Volumen;

    public Cilindro(double radio, double altura)
    {
        Radio = radio;
        Altura = altura;
        
    }
    public double CalcularVolumen()
    {
        Volumen = 3.1415926535 * Math.Pow(Radio, 2) * Altura;
        return Volumen;
    }
    
    public void MostrarVolumen()
    
    {
        Console.WriteLine("El volumen del cilindro es: " + CalcularVolumen());
    }
}