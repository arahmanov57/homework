using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LINQ_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new[] { -18, 121, 353, 8, -48, 5, 21 };

            Console.WriteLine($"Разность: {(GetDifference(numbers))}");

            var strs = File.ReadAllLines("db.txt");
            var dataBase = new List<Record>();

            foreach (var str in strs)
            {
                var data = str.Split();

                var record = new Record()
                {
                    ClientID = int.Parse(data[0]),
                    Year = int.Parse(data[1]),
                    Month = int.Parse(data[2]),
                    Duration = int.Parse(data[3])
                };

                dataBase.Add(record);
            }

            PrintTheMostActiveClientID(2019, dataBase);

            Console.ReadKey();
        }
 
        static void PrintTheMostActiveClientID(int year, List<Record> db)
        {
            var lines = db
            .Where(r => r.Year == year)
            .GroupBy(r => r.ClientID)
            .Select(g => (g.Key, g.Sum(s => s.Duration)))
            .OrderByDescending(x => x.Item2)
            .ThenByDescending(x => x.Key);
            if (lines.Count() > 0)
            {
                Console.WriteLine($"Данные за {year} г.");
                foreach (var line in lines)
                    Console.WriteLine($"Продолжительность занятий  у клиента {line.Key}: {line.Item2}");
            }
            else
                Console.WriteLine($"За {year} г. нет данных.");
        }

        static int GetDifference(int[] array)
        {
            return array
            .Take(array.Length / 2)
            .Where(x => x % 2 != 0).Sum() -
            array
            .Skip(array.Length / 2)
            .Where(x => x % 2 == 0).Sum();
        }

    }
}
