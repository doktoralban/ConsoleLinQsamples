namespace ConsoleLinQsamples
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        internal static void Main(string[] args)
        {
            //    MultipleWhereClauses();
            //    Console.WriteLine(" ***** ");
            //    SampleWhereMin();
            //    Console.WriteLine(" ***** ");
            //    TakeWhileSkipwhile();
            //    Console.WriteLine(" ***** ");
            //Distict_();
            //Console.WriteLine(" ***** ");
            //DirsNFiles();
            //Console.WriteLine(" ***** ");
            //CrossArraySelect();
            //Console.WriteLine(" ***** ");
            CrossJoinlementVsElement();
            Console.WriteLine(" ***** ");
        }

        /// <summary>
        /// ilk where( n.Length > 3) den sonra toupper onun içinden endswith.
        /// </summary>
        internal static void MultipleWhereClauses()
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
        /// dizideki eleman sayısı en az olanı bulup o sayı kadar elemanı olanlar.
        /// </summary>
        internal static void SampleWhereMin()
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
        /// Skipwhile = belirlenen(örnekte : 100)den fazla olanDan(Dahil: örnekte: 234) itibaren sonrakiler (örnekte: 234,4,1).
        /// </summary>
        internal static void TakeWhileSkipwhile()
        {
            int[] numbers = { 3, 5, 2, 234, 4, 1 };

            var a1 = numbers.TakeWhile(n => n < 100);

            foreach (var item in a1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(" --- --- --- --- --- --- ");

            var a2 = numbers.SkipWhile(n => n < 100);

            foreach (var item in a2)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// parçalara ayırır.örnekte: harfleri ayırır.
        /// </summary>
        internal static void Distict_()
        {
            var a1 = "HelloWorld".Distinct();

            foreach (var item in a1)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// belirtilen foldera ait ve alt folder ve  files.
        /// </summary>
        internal static void DirsNFiles()
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

        /// <summary>
        /// //1a 
        //1b 
        //2a 
        //2b 
        //3a 
        //3b 
        //4a 
        //4b.
        /// </summary>
        internal static void CrossArraySelect()
        {
            var numbers = new[] { 1, 2, 3, 4 }.AsQueryable();
            var letters = new[] { "a", "b" }.AsQueryable();

            IEnumerable<string> query =
                from n in numbers
                from l in letters
                select n.ToString() + l;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Jay vs Mary 
        //Jay vs Tom
        //Mary vs Tom.
        /// </summary>
        internal static void CrossJoinlementVsElement()
        {
            var players = new[] { "Tom", "Jay", "Mary" }.AsQueryable();

            IEnumerable<string> query =
                from name1 in players
                from name2 in players
                where name1.CompareTo(name2) < 0
                orderby name1, name2
                select name1 + " vs " + name2;

            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}
