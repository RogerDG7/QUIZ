using System;
using QUIZ.Modelado;
using System.Linq;


namespace QUIZ
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            using (var context = new Model1())
            {
                //Cambiar appconfig para la conexion <connectionStrings>
                Console.WriteLine("--------- Respuestas primer punto -------- ");
                var customers = (from c in context.Customers
                                 select c).ToList();

                foreach (var item in customers)
                {
                    Console.WriteLine(item.CustomerID + "-" + item.ContactName);
                }

                Console.WriteLine("--------- Respuestas segundo punto -------- ");
                var clientesPaisGer = context.Customers
                                      .Where(x => x.Country.Equals("Germany"))
                                      .Select(x => new {x.ContactName,x.CustomerID,x.Country})
                                      .ToList();

                foreach (var cliente in clientesPaisGer)
                {
                    Console.WriteLine(cliente.CustomerID + " - " + cliente.ContactName + " - " + cliente.Country);
                }

                Console.WriteLine("---------Clientes registrados en la BD-------- ");
                int numeroClientes = context.Customers.Select(x => x.CustomerID).Count();
                Console.WriteLine("total: " + numeroClientes);
                Console.WriteLine("---------Ciudades de clientes-------- ");
                var ciudadesClientes = context.Customers
                                      .Select(x => x.City)
                                      .Distinct()
                                      .ToList();
                foreach (var ciudades in ciudadesClientes)
                {
                    Console.WriteLine(ciudades);
                }
                Console.WriteLine("---------Contactos en forma Descendente-------- ");
                var contactosDesc = context.Customers
                                      .Select(x => x.ContactName)
                                      .OrderByDescending(x => x)
                                      .ToList();
                foreach (var contacto in contactosDesc)
                {
                    Console.WriteLine(contacto);
                }

                Console.WriteLine("---------Contiene las letras OM-------- ");
                var contieneOM = context.Customers
                                      .Select(x => x.ContactName)
                                      .Where(x => x.Contains("OM"))
                                      .ToList();
                foreach (var contiene in contieneOM)
                {
                    Console.WriteLine(contiene);
                }

                Console.WriteLine("--------- Respuestas Tercer punto -------- ");
                var productos = (from x in context.Products
                                 select x).ToList();

                foreach (var item in productos)
                {
                    Console.WriteLine(item.ProductID + "-" + item.ProductName);
                }

                Console.WriteLine("--------- Respuestas Cuarto punto -------- ");
                var preciosUni = context.Products
                                      .GroupBy(x => x.CategoryID)
                                      .Select(x => new
                                                {sumaPrecios = x.Sum(y => y.UnitPrice),
                                                 categoriaID = x.Max(y=> y.CategoryID)})
                                      .ToList();
                foreach (var item in preciosUni)
                {
                    Console.WriteLine(item.sumaPrecios + "-" + item.categoriaID);
                }

                Console.WriteLine("--------- Productos promedio -------- ");
                decimal prom = (decimal)context.Products.Average(x => x.UnitPrice);

                var precios = context.Products
                                     .Where(x => x.UnitPrice > prom)
                                     .Select(x => x.UnitPrice).ToList();

                Console.WriteLine("Promedio: " + prom);
                foreach (decimal item in precios)
                {
                    Console.WriteLine("precios mayores al promedio:" + item);
                }

            }

            Console.ReadKey();
        }
        
    }
}
