using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace laba7
{

    class Program
    {

        enum Pos
        {
            П,
            С,
            А,
            None
        }



        public const int WATCH_TABLE = 1;
        public const int ADD_RAW = 2;
        public const int REMOVE_RAW = 3;
        public const int UPDATE_RAW = 4;
        public const int FIND_RAW = 5;
        public const int SHOW_LOG = 6;
        public const int ADD_WRITE = 7;
        public const int Sort = 8;
        public const int EXIT = 9;

        public struct HRD
        {
            public string Name; // Фамилия
            public string Postion;//Долнжость
            public int Yers;//Год рождения
            public string Salary;//Оклад

            internal void ShowTable(string name, string Postion, int Yers, string Salary)
            {
                Console.Write("{0,10}", name);
                Console.Write("{0,10}", Postion);
                Console.Write("{0,10}", Yers);
                Console.Write("{0,10}", Salary);
                Console.WriteLine();
            }
        }

        //Списки
        static List<HRD> list = new List<HRD>(50);



        //Список опираций
        enum Operations
        {
            ADD,
            DELETE,
            UPDATE,
            LOOK,
            SEARCH
        };

        //ЛОГИРОВАНИЕ
        struct Logging
        {
            static List<Logging> log = new List<Logging>();
            public DateTime time;
            public Operations action;
            public String data;

            public static Logging Add(DateTime dt, Operations operation, string s)
            {
                log.Add(new Logging(dt, operation, s));
                return log[log.Count - 1];
            }

            public Logging(DateTime Time, Operations Operations, String Date)
            {
                time = Time;
                action = Operations;
                data = Date;
            }

            public static void ShowInfo()
            {

                foreach (Logging l in log)
                {
                    l.PrintLog();
                }
            }
            public void PrintLog()
            {
                Console.Write("{0,10}", time);
                Console.Write("{0,20}  ", action);
                Console.WriteLine("{0,10}", data);
            }



        }


        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                var table = new List<HRD>();
                Console.WriteLine("\n Выберите пункт");
                Console.WriteLine("1 - Просмотр таблицы");
                Console.WriteLine("2 - Добавить запись");
                Console.WriteLine("3 - Удалить запись");
                Console.WriteLine("4 - Обновить запись");
                Console.WriteLine("5 - Поиск записей");
                Console.WriteLine("6 - Просмотреть лог");
                Console.WriteLine("7 - Записать в файл");
                Console.WriteLine("8 - Сортировка");
                Console.WriteLine("9 - Выход");
                choice = int.Parse(Console.ReadLine());
                string path = @"D:\lab.dat";
                switch (choice)
                {
                    case WATCH_TABLE:

                        for (int list_item = 0; list_item < list.Count; list_item++)
                        {
                            HRD t = list[list_item];
                            Console.WriteLine("----------------------------------------------------------------------------");
                            t.ShowTable(t.Name, t.Postion, t.Yers, t.Salary);

                        }
                        StreamReader fstream = new StreamReader(path);
                        string line;
                        while (!fstream.EndOfStream)
                        {
                            line = fstream.ReadLine();
                            Console.Write(line);
                        }
                        fstream.Close();
                        Logging.Add(DateTime.Now, Operations.LOOK, "Просмотрена таблица");
                        break;

                    case ADD_RAW:
                        HRD t1;
                        Console.WriteLine("Введите Фамилию");
                        t1.Name = Console.ReadLine();
                        Console.WriteLine("ВведитеДолжность");
                        t1.Postion = Console.ReadLine();

                    Found1:
                        Console.WriteLine("Введите год рождения");
                        try
                        {
                            int blabla = Convert.ToInt32(Console.ReadLine()); //вводим данные, и конвертируем в целое число  
                            t1.Yers = blabla;
                            if ((blabla < 1895) || (blabla > 2030))
                            {
                                Console.WriteLine("Error. (Введите повторно)");
                                goto Found1;
                            }
                        }
                        catch (FormatException)
                        {
                            t1.Yers = 000;
                            Console.WriteLine("Error. (Введите повторно)");
                            goto Found1;
                        }
                        Pos pro;
                    Found3:
                        Console.WriteLine("Введите оклад");
                        try
                        {
                            string blabla3 = Console.ReadLine();
                            t1.Salary = blabla3;
                            pro = (Pos)Enum.Parse(typeof(Pos), blabla3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error. (Введите повторно)");
                            pro = Pos.None;
                            goto Found3;
                        }

                        list.Add(t1);
                        Console.WriteLine("Строка была добавлена!");
                        Console.WriteLine();
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка добавлена в таблицу!");
                        break;
                    case REMOVE_RAW:
                        string linel = String.Empty;
                        string delet = Console.ReadLine();
                        StreamReader sr = new StreamReader(path);
                        while (sr.Peek() >= 0)
                        {
                            String temp = sr.ReadLine();
                            if (temp.IndexOf(delet) != -1)
                            {

                            }
                            else
                            {
                                linel += temp;
                            }
                        }
                        sr.Close();
                        StreamWriter sw = new StreamWriter(path);
                        sw.Write(linel);
                        sw.Close();
                        Logging.Add(DateTime.Now, Operations.ADD, "строка удалена!");
                        break;
                    case UPDATE_RAW:
                        StreamReader readf = new StreamReader(path);
                        string surname = readf.ReadLine();
                        string pos = readf.ReadLine();
                        var position = Pos.None;
                        if (pos == "П")
                        {
                            position = Pos.П;
                        }
                        else if (pos == "С")
                        {
                            position = Pos.С;
                        }
                        else if (pos == "А")
                        {
                            position = Pos.А;
                        }
                        int year = int.Parse(readf.ReadLine());
                        string salary = readf.ReadLine();
                        HRD newWorker;
                        newWorker.Name = surname;
                        newWorker.Postion = Convert.ToString(position);
                        newWorker.Yers = year;
                        newWorker.Salary = salary;
                        table.Add(newWorker);
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка обновлена!");
                        break;
                    case FIND_RAW:
                        Console.WriteLine("Введите фамилию");
                        string text = Console.ReadLine();
                        HRD FindRaw;
                        for (int item_list = 0; item_list < list.Count; item_list++)
                        {
                            FindRaw = list[item_list];
                            if (FindRaw.Name.ToLower().Equals(text.ToLower()))
                            {
                                Console.Write("{0,10}", FindRaw.Name);
                                Console.Write("{0,10}", FindRaw.Postion);
                                Console.Write("{0,10}", FindRaw.Yers);
                                Console.Write("{0,10}", FindRaw.Salary);
                                Console.WriteLine();
                            }
                        }
                        Logging.Add(DateTime.Now, Operations.ADD, "Строка найдена!");
                        break;
                    case SHOW_LOG:
                        Logging.Add(DateTime.Now, Operations.ADD, "Логи просмотрены!");
                        Logging.ShowInfo();
                        break;
                    case ADD_WRITE:
                        var encod = System.Text.Encoding.GetEncoding(1251);

                        //Для чтения текста из файла:
                        var Reader = new StreamReader(path, encod);
                        String text1 = Reader.ReadToEnd();
                        String text2 = Reader.ReadToEnd();
                        Reader.Close();

                        StreamWriter write = new StreamWriter(@"D:\lab.dat");
                        int list_item1 = 0;
                        HRD d = list[list_item1];
                        write.WriteLine("{0,10} {1,10} {2,10} {3,10}", d.Name, d.Postion, d.Yers, d.Salary);
                        list_item1++;
                        write.Close();
                        //Для записи в файл:
                        var Writer = new StreamWriter(path, true, encod);
                        if (text1 != text2)
                        {
                            Writer.Write(text1);
                        }
                        Writer.Close();

                        Logging.Add(DateTime.Now, Operations.ADD, "Запись в файл");

                        break;
                    case Sort:
                        for (int i = 0; i < list.Count; i++)
                        {
                            int j = i - 1;
                            var tmp = list[i];
                            while (j >= 0 && tmp.Yers < list[j].Yers)
                            {
                                list[j + 1] = list[j];
                                j--;
                            }
                            list[j + 1] = tmp;
                        }
                        using (StreamWriter newText = new StreamWriter(path, false))
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                newText.WriteLine("{0,10} {1,10} {2,10} {3,10}", list[i].Name, list[i].Postion, list[i].Yers, list[i].Salary);
                            }
                        }
                        Console.WriteLine("Таблица готова");
                        Logging.Add(DateTime.Now, Operations.ADD, "Сортировка");

                        break;
                    case EXIT:

                        break;
                }
            } while (choice != 9); 

        }
    }
}
