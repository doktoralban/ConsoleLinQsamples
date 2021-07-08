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
            //    MultipleWhereClauses();
            //    Console.WriteLine(" ***** ");
            //    SampleWhereMin();
            //    Console.WriteLine(" ***** ");
            //    TakeWhileSkipwhile();
            //    Console.WriteLine(" ***** ");
            //Distict_();
            //Console.WriteLine(" ***** ");
            DirsNFiles();
            Console.WriteLine(" ***** ");


        } 

        /// <summary>
        /// ilk where( n.Length > 3) den sonra toupper onun içinden endswith
        /// </summary>
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


        /// <summary>
        /// dizideki eleman sayısı en az olanı bulup o sayı kadar elemanı olanlar
        /// </summary>
        static void SampleWhereMin()
        {
            var names = new[] { "Tom", "Dick", "Harry", "Mary", "Jay" }.AsQueryable();
            IQueryable<string> queryable = (
                from n in names
                where n.Length == names.Min(n2 => n2.Length)
                select n
            );

            foreach (var item in names)
            {
                Console.WriteLine(item);
            }
        }


        /// <summary>
        /// TakeWhile = belirlenen(örnekte : 100)den fazla olan(Hariç: örnekte: 234)a gelene kadar öncekiler(örnekte: 3,5,2).
        /// Skipwhile = belirlenen(örnekte : 100)den fazla olanDan(Dahil: örnekte: 234) itibaren sonrakiler (örnekte: 234,4,1)
        /// </summary>
        static void TakeWhileSkipwhile()
        {
            int[] numbers = { 3, 5, 2, 234, 4, 1 };

           var a1= numbers.TakeWhile(n => n < 100) ;

            foreach (var item in a1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(" --- --- --- --- --- --- ");

            var a2 =numbers.SkipWhile(n => n < 100) ;

            foreach (var item in a2)
            {
                Console.WriteLine(item);
            }

        }


        /// <summary>
        /// parçalara ayırır.örnekte: harfleri ayırır
        /// </summary>
        static void Distict_()
        {
            var a1 = "HelloWorld".Distinct();

                 foreach (var item in a1)
            {
                Console.WriteLine(item);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        static void DirsNFiles()
        {
            string sampleDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            System.IO.DirectoryInfo[] dirs = new System.IO.DirectoryInfo(sampleDirectory).GetDirectories();

            var query =
                from d in dirs
                where (d.Attributes & System.IO.FileAttributes.System) == 0
                select new
                {
                    DirectoryName = d.FullName,
                    Created = d.CreationTime,
                    Files = from f in d.GetFiles()
                            where (f.Attributes & System.IO.FileAttributes.Hidden) == 0
                            select new { FileName = f.Name, f.Length, }
                };

 

            // Here's how to enumerate the results manually:

            foreach (var dirFiles in query)
            {
                Console.WriteLine("Directory: " + dirFiles.DirectoryName);
                foreach (var file in dirFiles.Files)
                    Console.WriteLine("    " + file.FileName + "Len: " + file.Length);
            }



        }



    }
}
