using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LinqPlayground
{
    class Introduction
    {
        static void Intoduce()
        {
            //IEnumrableを簡易に生成する例
            var q = Enumerable.Range(1, 10).Where(i => i > 3);

            //
            var evens = Enumerable.Range(1, 100).Where(i => i % 2 == 0).ToArray();
            //var evens0 = Enumerable.Where(Enumerable.Range(1, 100), i => i % 2 == 0);
            //var evens1 = Enumerable.Where(Enumerable.Range(1, 100), LinqSample.IsEven);

            var queryEvents = from even in Enumerable.Range(1, 100)
                              where even % 2 == 0
                              select even;

            //Console.WriteLine(string.Join(",", queryEvents));


            string[] words = { "pen", "apple", "apple pen", "pineapple", "pineapple pen", "pen pineapple apple pen" };

            var queryOrderedWords = from word in words
                                    where word.Length > 5
                                    orderby word.Length descending
                                    select word;

            var orderedWords = words.Where(word => word.Length > 5).OrderByDescending(word => word.Length);

            Console.WriteLine(string.Join(",", orderedWords));
            Console.WriteLine(string.Join(",", queryOrderedWords));

            string extended = "拡張メソッド";
            Console.WriteLine(extended.Extend());

            string path = @"C:\Users\deton\Development\dotnet\NBitcoin";

            var lines = from file in Directory.EnumerateFiles(path, "*.cs", SearchOption.AllDirectories)
                        from line in File.ReadLines(file)
                        where line.Contains("class")
                        select line;

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            //Console.CancelKeyPress += s, e) => { Console.WriteLine("Bye from lambda"); };

            Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

            //Console.WriteLine(string.Join(",", orderedWords));

            //string path = @"";

            //foreach (var word in orderedWords)
            //{
            //    Console.WriteLine(word);
            //}


            //foreach (var i in evens)
            //{
            //    Console.WriteLine(i);
            //}

            //Console.WriteLine(evens.Average());
            //51
            //Console.WriteLine(evens.Sum());
            //2550
        }

        public static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Bye from defined delegate!");
        }
    }

    public static class LinqSample
    {
        public static string Extend(this string source)
        {
            return source.Aggregate("拡張するよ！", (s, c) => s + "   " + c);
        }

        public static bool IsEven(int i)
        {
            return i % 2 == 0;
        }
    }
}
