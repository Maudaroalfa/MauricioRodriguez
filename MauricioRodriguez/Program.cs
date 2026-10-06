using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauricioRodriguez
{
    internal class Program
    {
        const int N = 15;  // Ultimo numero del carnet es 5, 5+10 = 15

        static void InicializarVectores(string[] nombres, string[] cedulas)    // String porque no se va a hacer ninguna operacion matematica con las cedulas
        {
            // Deja los dos vectores con N espacios vacios
            for (int i = 0; i < N; i++)
            {
                nombres[i] = "";
                cedulas[i] = "";
            }
        }

        static bool CedulaValida(string cedula)
        {
            // La cedula debe tener 9 caracteres y todos deben ser numeros
            if (cedula.Length != 9)
            {
                return false;
            }
            for (int i = 0; i < cedula.Length; i++)
            {
                if (cedula[i] < '0' || cedula[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        static int BuscarPosicion(string[] cedulas, int cantidad, string cedula)
        {
            for (int i = 0; i < cantidad; i++)
            {
                if (cedulas[i] == cedula)
                {
                    return i;
                }
            }
            return -1;
        }

        static int Registrar(string[] nombres, string[] cedulas, int cantidad)
        {
            if (cantidad == N) // Condicional de control para que no se pueda registrar mas pacientes si el censo esta lleno
            {
                Console.WriteLine("El censo esta lleno, no se pueden registrar mas pacientes.");
                Console.WriteLine();
                return cantidad;
            }

            Console.Write("Numero de cedula (9 digitos): ");  // Se solicita la cedula del paciente
            string cedula = Console.ReadLine();
            if (!CedulaValida(cedula))  // Se valida que la cedula tenga 9 digitos y que sean numericos
            {
                Console.WriteLine("Cedula invalida: deben ser 9 digitos numericos.");
                Console.WriteLine();
                return cantidad;
            }

            if (BuscarPosicion(cedulas, cantidad, cedula) != -1)  // Se valida que la cedula no este ya registrada
            {
                Console.WriteLine("Esa cedula ya esta registrada.");
                Console.WriteLine();
                return cantidad;
            }

            Console.Write("Nombre completo del paciente: ");  // Se solicita el nombre del paciente
            string nombre = Console.ReadLine();
            if (nombre == "")  // Se valida que el nombre no este vacio
            {
                Console.WriteLine("El nombre no puede estar vacio.");
                Console.WriteLine();
                return cantidad;
            }

            nombres[cantidad] = nombre;
            cedulas[cantidad] = cedula;
            cantidad += 1;
            Console.WriteLine("Paciente registrado correctamente.");
            Console.WriteLine();
            return cantidad;
        }

        static void Buscar(string[] nombres, string[] cedulas, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("====El censo esta vacio.====");
                Console.WriteLine();
                return;
            }

            Console.Write("Cedula a buscar: ");
            string cedula = Console.ReadLine();
            int posicion = BuscarPosicion(cedulas, cantidad, cedula);
            if (posicion == -1)
            {
                Console.WriteLine("No se encontro ningun paciente con esa cedula.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Paciente encontrado:");
                Console.WriteLine("Nombre: " + nombres[posicion]);
                Console.WriteLine("Cedula: " + cedulas[posicion]);
            }
        }

        static void CorregirNombre(string[] nombres, string[] cedulas, int cantidad)
        {
            if (cantidad == 0)
            {
                Console.WriteLine("El censo esta vacio.");
                return;
            }

            Console.Write("Cedula del paciente a corregir: ");
            string cedula = Console.ReadLine();
            int posicion = BuscarPosicion(cedulas, cantidad, cedula);
            if (posicion == -1)
            {
                Console.WriteLine("No se encontro ningun paciente con esa cedula.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Nombre actual: " + nombres[posicion]);
            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine();
            if (nuevoNombre == "")
            {
                Console.WriteLine("El nombre no puede estar vacio.");
                Console.WriteLine();
                return;
            }

            nombres[posicion] = nuevoNombre;
            Console.WriteLine("Nombre corregido correctamente.");
        }

        static int DarBaja(string[] nombres, string[] cedulas, int cantidad)
        {
            if (cantidad == 0)  // Se valida que el censo no este vacio
            {
                Console.WriteLine("El censo esta vacio.");
                Console.WriteLine();
                return cantidad;
            }

            Console.Write("Cedula del paciente a dar de baja: ");  // Se solicita la cedula del paciente a dar de baja
            string cedula = Console.ReadLine();
            int posicion = BuscarPosicion(cedulas, cantidad, cedula);
            if (posicion == -1)  // Se valida que la cedula exista en el censo
            {
                Console.WriteLine("No se encontro ningun paciente con esa cedula.");
                Console.WriteLine();
                return cantidad;
            }

            for (int i = posicion; i < cantidad - 1; i++)  // Se recorre el vector desde la posicion del paciente a dar de baja hasta el final del censo
            {
                nombres[i] = nombres[i + 1];
                cedulas[i] = cedulas[i + 1];
            }

            // Se vacia el ultimo espacio del vector y se reduce la cantidad de pacientes registrados
            nombres[cantidad - 1] = "";
            cedulas[cantidad - 1] = "";
            cantidad -= 1;
            Console.WriteLine("Paciente dado de baja correctamente.");
            Console.WriteLine();
            return cantidad;
        }

        static void MostrarCenso(string[] nombres, string[] cedulas, int cantidad)
        {
            Console.WriteLine("----- CENSO ADMINISTRATIVO -----");
            if (cantidad == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < cantidad; i++)
                {
                    Console.WriteLine((i + 1) + " - " + nombres[i] + " | Cedula: " + cedulas[i]);
                }
            }
            Console.WriteLine("Pacientes registrados: " + cantidad);
            Console.WriteLine("Espacios disponibles: " + (N - cantidad));
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            string[] nombres = new string[N];
            string[] cedulas = new string[N];
            InicializarVectores(nombres, cedulas);
            int cantidad = 0;

            string opcion = "";
            while (opcion != "6")
            {
                Console.WriteLine();
                Console.WriteLine("===== HOSPITAL SAN RAFAEL =====");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Buscar paciente por cedula");
                Console.WriteLine("3. Corregir nombre de un paciente");
                Console.WriteLine("4. Dar de baja a un paciente");
                Console.WriteLine("5. Mostrar censo");
                Console.WriteLine("6. Salir");
                Console.Write("Elija una opcion: ");
                opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        cantidad = Registrar(nombres, cedulas, cantidad);
                        break;
                    case "2":
                        Buscar(nombres, cedulas, cantidad);
                        break;
                    case "3":
                        CorregirNombre(nombres, cedulas, cantidad);
                        break;
                    case "4":
                        cantidad = DarBaja(nombres, cedulas, cantidad);
                        break;
                    case "5":
                        MostrarCenso(nombres, cedulas, cantidad);
                        break;
                    case "6":
                        Console.WriteLine("Saliendo del sistema.");
                        break;
                    default:
                        Console.WriteLine("Opcion invalida, intente de nuevo.");
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
}
