﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientLab2
{
    internal class ClientView
    {

        static void MainMenu()
        {
            Console.WriteLine("\nВыберите действие с файлом\n" +
                "1. Выбрать файл для чтения и записи\n" +
                "2. Вывод всех записей\n" +
                "3. Вывод записи по номеру\n" +
                "4. Удаление записи из файла\n" +
                "5. Добавление записи в файл\n" +
                "ESC. Выйти из программы\n");
        }
        static void SetPath(ClientController controller)
        {
            //Console.WriteLine("Введите путь к файлу:\n");
            string path = "C:\\Users\\artem\\Desktop\\Архитектура ИС\\lab1\\CsvFile.csv";//Console.ReadLine();
            if (!controller.SetAndCheckPath(path))
            {
                Console.WriteLine("Введите корректный путь к файлу!");
                SetPath(controller);
            }
        }

        static void doActionMainMenu(ClientController controller)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("Ввведите путь к файлу:\n");
                    string path = Console.ReadLine();
                    //C:\\Users\\artem\\Desktop\\Архитектура ИС\\lab1\\CsvFile.csv
                    controller.SetAndCheckPath(path);
                    break;
                case ConsoleKey.D2:
                    controller.GetAllRecords().ForEach(Console.WriteLine);
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Введите номер записи:\n");
                    try
                    {
                       var a =controller.GetSepRecord(int.Parse(Console.ReadLine()));
                        Console.WriteLine(a[0]);
                    }
                    catch
                    {
                        Console.WriteLine("Вы можете вводить только цифры от 1 до 5!");
                    }
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Введите номер записи:\n");
                    try
                    {
                        controller.DeleteRecord(int.Parse(Console.ReadLine()));
                    }
                    catch
                    {
                        Console.WriteLine("Запись под таким номером не существует!");
                    }
                    break;
                case ConsoleKey.D5:
                    Console.WriteLine("Введите запись в формате:\n" +
                        "Иван;Иванов;4050;3;True");
                    try
                    {
                        controller.AddRecord(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Формат записи не был соблюдён!");
                    }
                    break;
                case ConsoleKey.Escape:
                    controller.ShutDown();
                    Environment.Exit(0);
                    break;
            }
        }

        static void Main(string[] args)
        {
            ClientController controller = new("127.0.0.1", 8080);
            SetPath(controller);
            while (true)
            {
                MainMenu();
                doActionMainMenu(controller);
            }
        }
    }
}
