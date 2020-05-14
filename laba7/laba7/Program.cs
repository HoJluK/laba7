using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace laba7
{
    class Program
    {
        public const string path = @"D:\laba7.txt";
        public const string path1 = @"D:\laba7_1.txt";
        static void Main(string[] args)
        {
            Random rnd = new Random();
            long count1 = 0;
            long count2 = 0;
            int length = 100;
            int[] arrarevers = new int[length];
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = rnd.Next(-999, 999);
                arrarevers[i] = array[i];
            }
            TimeSpan SWT;
            TimeSpan IWT;
            TimeSpan BWT;
            TimeSpan ShakerWT;
            TimeSpan ShellWT;
            ShellSort(array, length, out ShellWT, out count1, out count2);
            Array.Reverse(array);
            ShellSort(arrarevers, length, out ShellWT, out count1, out count2);
            using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate)))
            {
                foreach (int number in arrarevers)
                {
                    sw.Write(number + "\n");
                }
            }
            using (StreamWriter sw1 = new StreamWriter(new FileStream(path1, FileMode.OpenOrCreate)))
            {
                foreach (int number in array)
                {
                    sw1.Write(number + "\n");
                }
            }
            int select = 0;
            while (select != 4)
            {
                Console.WriteLine("1 - Random array");
                Console.WriteLine("2 - array> and <");
                Console.WriteLine("3 - array< and >");
                Console.WriteLine("4 - exit");
                select = int.Parse(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        Console.WriteLine("Rnd array");
                        SelectionSort(array, length, out SWT, out count1, out count2);
                        InsertionSort(array, length, out IWT, out count1, out count2);
                        BubbleSort(array, length, out BWT, out count1, out count2);
                        ShakerSort(array, length, out ShakerWT, out count1, out count2);
                        ShellSort(array, length, out ShellWT, out count1, out count2);
                        break;
                    case 2:
                        Console.WriteLine("Array> and <\n");
                        SelectionSort(array, length, out SWT, out count1, out count2);
                        InsertionSort(array, length, out IWT, out count1, out count2);
                        BubbleSort(array, length, out BWT, out count1, out count2);
                        ShakerSort(array, length, out ShakerWT, out count1, out count2);
                        ShellSort(array, length, out ShellWT, out count1, out count2);
                        break;
                    case 3:
                        Console.WriteLine("Array < and >\n");
                        SelectionSort(arrarevers, length, out SWT, out count1, out count2);
                        InsertionSort(arrarevers, length, out IWT, out count1, out count2);
                        BubbleSort(arrarevers, length, out BWT, out count1, out count2);
                        ShakerSort(arrarevers, length, out ShakerWT, out count1, out count2);
                        ShellSort(arrarevers, length, out ShellWT, out count1, out count2);
                        break;
                }
            }
        }
        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        static void SelectionSort(int[] array, int length, out TimeSpan SWT, out long count1, out long count2)
        {
            count1 = 0;
            count2 = 0;
            DateTime startTime = DateTime.Now;
            for (int i = length - 1; i >= 0; i--)
            {
                int min = i;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (array[j] < array[min])
                        min = j;
                    count1++;
                }
                Swap(ref array[i], ref array[min]);
                if (i != min)
                    count2++;
            }
            DateTime endTime = DateTime.Now;
            SWT = endTime - startTime;
            Console.WriteLine("The sorting took a while(Selection) " + SWT + "\nCount of transpositions: " + count2 + "\nCount of comparisons: " + count1);
        }
        static void InsertionSort(int[] array, int length, out TimeSpan IWT, out long count1, out long count2)
        {
            count1 = 0;
            count2 = 0;
            DateTime startTime = DateTime.Now;
            for (int i = 1; i < length; i++)
            {
                int j = i;
                int temp = array[i];
                while (j > 0 && temp > array[j - 1])
                {
                    count1++;
                    array[j] = array[j - 1];
                    j--;
                }
                count1++;
                array[j] = temp;
                if (j != i)
                    count2++;
            }
            if (count2 > 0)
                count2--;
            DateTime endTime = DateTime.Now;
            IWT = endTime - startTime;
            Console.WriteLine("The sorting took a while(Insert) " + IWT + "\nCount of transpositions: " + count1 + "\nCount of comparisons: " + count2);
        }
        static void BubbleSort(int[] array, int length, out TimeSpan BWT, out long count1, out long count2)
        {
            count1 = 0;
            count2 = 0;
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < length; i++)
            {
                for (int j = length - 1; j > i; j--)
                {
                    if (array[j - 1] < array[j])
                    {
                        Swap(ref array[j - 1], ref array[j]);
                        count2++;
                    }
                    count1++;
                }
            }
            DateTime endTime = DateTime.Now;
            BWT = endTime - startTime;
            Console.WriteLine("The sorting took a while(Bubble) " + BWT + "\nCount of transpositions: " + count2 + "\nCount of comparisons: " + count1);
        }
        static void ShakerSort(int[] array, int length, out TimeSpan ShakerWT, out long count1, out long count2)
        {
            count1 = 0;
            count2 = 0;
            int startIndex = 0;
            DateTime startTime = DateTime.Now;
            do
            {
                for (int i = startIndex; i < length - 1; i++)
                {
                    if (array[i] < array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        count2++;
                    }
                    count1++;
                }
                length--;

                for (int i = length - 1; i > startIndex; i--)
                {
                    if (array[i] > array[i - 1])
                    {
                        Swap(ref array[i], ref array[i - 1]);
                        count2++;
                    }
                    count1++;
                }
                startIndex++;
            }
            while (startIndex <= length - 1);
            DateTime endTime = DateTime.Now;
            ShakerWT = endTime - startTime;
            Console.WriteLine("The sorting took a while(Shaker) " + ShakerWT + "\nCount of transpositions: " + count2 + "\nCount of comparisons: " + count1);
        }
        static void ShellSort(int[] array, int length, out TimeSpan ShellWT, out long count1, out long count2)
        {
            count1 = 0;
            count2 = 0;
            DateTime startTime = DateTime.Now;
            int[] steps = { 41, 25, 15, 9, 5, 3, 1 };
            foreach (int step in steps)
            {
                for (int i = step; i < length; i++)
                {
                    int j = i;
                    int temp = array[i];
                    while (j >= step && temp > array[j - step])
                    {
                        count1++;
                        array[j] = array[j - step];
                        j -= step;
                    }
                    count1++;
                    array[j] = temp;
                    if (j != i)
                        count2++;
                }
            }
            if (count2 > 0)
                count2--;
            DateTime endTime = DateTime.Now;
            ShellWT = endTime - startTime;
            Console.WriteLine("The sorting took a while(Shell) " + ShellWT + "\nCount of transpositions: " + count2 + "\nCount of comparisons: " + count1);
        }
    }
}


