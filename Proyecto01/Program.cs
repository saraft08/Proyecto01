namespace Proyecto01
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static Dictionary<string, Dictionary<string, int>> clientes = new Dictionary<string, Dictionary<string, int>>();

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a Afinia");
            MostrarInformacion();
        }

        static void AgregarCliente()
        {
            Console.Write("Ingresar el número de cédula del cliente: ");
            string cedula = Console.ReadLine();
            Console.Write("Ingresar el estrato del cliente: ");
            int estrato = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingresar la meta de ahorro del cliente: ");
            int metaAhorro = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingresar el consumo actual del cliente: ");
            int consumoActual = Convert.ToInt32(Console.ReadLine());

            clientes[cedula] = new Dictionary<string, int>
        {
            {"estrato", estrato},
            {"meta_ahorro", metaAhorro},
            {"consumo_actual", consumoActual}
        };
            Console.WriteLine("El cliente ha sido agregado correctamente.");
        }

        static void CalcularPrecioPagar()
        {
            Console.Write("Ingresar el número de cédula del cliente: ");
            string cedula = Console.ReadLine();
            if (clientes.ContainsKey(cedula))
            {
                Dictionary<string, int> cliente = clientes[cedula];
                int valorParcial = cliente["consumo_actual"] * 500;
                int valorIncentivo = (cliente["meta_ahorro"] - cliente["consumo_actual"]) * 500;
                int valorPagar = valorParcial - valorIncentivo;
                Console.WriteLine($"El valor a pagar del cliente {cedula} es: ${valorPagar}");
            }
            else
            {
                Console.WriteLine("Error: cliente no encontrado.");
            }
        }

        static void CalcularPromedioConsumo()
        {
            int totalConsumo = 0;
            foreach (var cliente in clientes.Values)
            {
                totalConsumo += cliente["consumo_actual"];
            }
            double promedio = (double)totalConsumo / clientes.Count;
            Console.WriteLine($"El promedio del consumo actual de energía del cliente es: {promedio} kilovatios");
        }

        static void CalcularDescuentos()
        {
            int totalDescuentos = 0;
            foreach (var cliente in clientes.Values)
            {
                if (cliente["consumo_actual"] < cliente["meta_ahorro"])
                {
                    totalDescuentos += (cliente["meta_ahorro"] - cliente["consumo_actual"]) * 500;
                }
            }
            Console.WriteLine($"El valor total de descuento otorgado es: ${totalDescuentos}");
        }

        static void MostrarPorcentajesAhorro()
        {
            Dictionary<int, List<double>> porcentajesAhorro = new Dictionary<int, List<double>>();
            foreach (var cliente in clientes.Values)
            {
                int estrato = cliente["estrato"];
                int consumoActual = cliente["consumo_actual"];
                int metaAhorro = cliente["meta_ahorro"];
                double porcentajeAhorro = ((double)(metaAhorro - consumoActual) / metaAhorro) * 100;

                if (porcentajesAhorro.ContainsKey(estrato))
                {
                    porcentajesAhorro[estrato].Add(porcentajeAhorro);
                }
                else
                {
                    porcentajesAhorro[estrato] = new List<double> { porcentajeAhorro };
                }
            }

            foreach (var kvp in porcentajesAhorro)
            {
                double promedioPorcentaje = kvp.Value.Sum() / kvp.Value.Count;
                Console.WriteLine($"El porcentaje de ahorro promedio para el estrato {kvp.Key} es: {promedioPorcentaje}%");
            }
        }

        static void ContarCobrosAdicionales()
        {
            int totalCobros = 0;
            foreach (var cliente in clientes.Values)
            {
                if (cliente["consumo_actual"] > cliente["meta_ahorro"])
                {
                    totalCobros++;
                }
            }
            Console.WriteLine($" Los clientes con cobro adicional son los siguientes: {totalCobros}");
        }

        static void MostrarInformacion()
        {
            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1: Agregar un nuevo cliente");
            Console.WriteLine("2: Buscar cliente por cédula");
            Console.WriteLine("0: Salir");
            Console.Write("Seleccionar una opción: ");
            int opcion = Convert.ToInt32(Console.ReadLine());

            if (opcion == 0)
            {
                Console.WriteLine("Gracias por escogernos. ¡Vuelva pronto!");
            }
            else if (opcion == 1)
            {
                AgregarCliente();
                MostrarInformacion();
            }
            else if (opcion == 2)
            {
                CalcularPrecioPagar();
                CalcularPromedioConsumo();
                CalcularDescuentos();
                MostrarPorcentajesAhorro();
                ContarCobrosAdicionales();
                MostrarInformacion();
            }
            else
            {
                Console.WriteLine("¡Oops! Algo está incorrecto.");
                MostrarInformacion();
            }

        }
    }
}