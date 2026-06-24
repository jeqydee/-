using System;

namespace TransportTask
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Транспортная задача (метод северо-западного угла) ===\n");

                // Ввод размеров
                int m = ReadInt("Количество поставщиков: ");
                int n = ReadInt("Количество потребителей: ");

                int[] supply = new int[m];
                int[] demand = new int[n];
                int[,] cost = new int[m, n];

                // Ввод запасов
                Console.WriteLine("\nЗапасы поставщиков:");
                for (int i = 0; i < m; i++)
                    supply[i] = ReadInt($"  Поставщик {i + 1}: ");

                // Ввод потребностей
                Console.WriteLine("\nПотребности потребителей:");
                for (int j = 0; j < n; j++)
                    demand[j] = ReadInt($"  Потребитель {j + 1}: ");

                // Ввод тарифов
                Console.WriteLine("\nТарифы перевозки:");
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        cost[i, j] = ReadInt($"  {i + 1}->{j + 1}: ");

                // Проверка баланса
                int totalSupply = Sum(supply);
                int totalDemand = Sum(demand);

                Console.WriteLine($"\nСумма запасов: {totalSupply}, Сумма потребностей: {totalDemand}");

                if (totalSupply != totalDemand)
                {
                    Console.WriteLine("Задача НЕ сбалансирована!");
                }
                else
                {
                    Console.WriteLine("Задача сбалансирована.\n");

                    // Метод северо-западного угла
                    int[,] plan = NorthWestCorner(supply, demand);

                    // Вывод плана
                    Console.WriteLine("План перевозок:");
                    for (int i = 0; i < m; i++)
                    {
                        for (int j = 0; j < n; j++)
                            Console.Write($"{plan[i, j],5}");
                        Console.WriteLine();
                    }

                    // Подсчёт стоимости
                    int totalCost = 0;
                    for (int i = 0; i < m; i++)
                        for (int j = 0; j < n; j++)
                            totalCost += plan[i, j] * cost[i, j];

                    Console.WriteLine($"\nОбщая стоимость: {totalCost}");
                }

                Console.Write("\nЕщё раз? (1-да, 0-нет): ");
            }
            while (ReadInt("") == 1);
        }

        // Метод северо-западного угла
        static int[,] NorthWestCorner(int[] supply, int[] demand)
        {
            int m = supply.Length;
            int n = demand.Length;
            int[,] plan = new int[m, n];

            int[] s = (int[])supply.Clone();
            int[] d = (int[])demand.Clone();

            int i = 0, j = 0;
            while (i < m && j < n)
            {
                int x = Math.Min(s[i], d[j]);
                plan[i, j] = x;
                s[i] -= x;
                d[j] -= x;

                if (s[i] == 0) i++;
                if (d[j] == 0) j++;
            }
            return plan;
        }

        // Сумма массива
        static int Sum(int[] arr)
        {
            int sum = 0;
            foreach (int x in arr) sum += x;
            return sum;
        }

        // Безопасный ввод числа
        static int ReadInt(string msg)
        {
            int x;
            Console.Write(msg);
            while (!int.TryParse(Console.ReadLine(), out x) || x < 0)
                Console.Write("Ошибка, повторите: ");
            return x;
        }
    }
}