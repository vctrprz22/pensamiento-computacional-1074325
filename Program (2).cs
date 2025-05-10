Console.WriteLine("Ejemplo de arreglos");
int promedio = 0;

string[] Estudiantes = ["Juan", "Pedro", "Luisa", "Adriana", "Sofia"];
int [] Nota = [88, 75, 96, 77, 59];

for (int i= 0; i<Estudiantes.Length; i++)
{
    Console.WriteLine(Estudiantes[i]+ " - " + Nota[i]);
}

for (int i=0; i<Nota.Length; i++)
{
    promedio += (Nota[i]);
    
}

Console.WriteLine("El promedio es: " + promedio/Nota.Length);