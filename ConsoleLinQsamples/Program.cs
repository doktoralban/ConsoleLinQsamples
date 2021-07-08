using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLinQsamples
{
    class Program
    {
        static void Main(string[] args)
        {
            MultipleWhereClauses();


        }





        static void MultipleWhereClauses()
            {
            var names = new[] { "Tom", "Dick", "Harry", "Mary", "Jay" }.AsQueryable();

            var query =
                from n in names
                where n.Length > 3
                let u = n.ToUpper()
                where u.EndsWith("Y")
                select u;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }



        }



    }
}
