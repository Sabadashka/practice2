namespace Generic_Container
{
    using System;

    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MainMenu();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Console.WriteLine($"ПОМИЛКА! {ex.InnerException.Message}\n");
                else
                    Console.WriteLine($"ПОМИЛКА! {ex.Message}\n");
            }
        }

        static private void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("Введiть назву файла: ");
                string choice = Console.ReadLine();

                if (choice == "payments.txt")
                {
                    Menu<Payment> menu = new Menu<Payment>();
                    GenericContainer<Payment> container = new GenericContainer<Payment>("payments.txt");
                    while (true)
                    {
                        int action = PrintMenu();
                        if (action == 0) break; // if user choose to exit
                        try
                        {
                            menu.dictionaryActions[action](container);
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException != null)
                                Console.WriteLine($"ПОМИЛКА! {ex.InnerException.Message}");
                            else
                                Console.WriteLine($"ПОМИЛКА! {ex.Message}");
                        }
                    }
                }
                else if (choice == "jewelry.txt")
                {
                    Menu<Jewelry> menu = new Menu<Jewelry>();
                    GenericContainer<Jewelry> container = new GenericContainer<Jewelry>("jewelry.txt");
                    while (true)
                    {
                        int action = PrintMenu();
                        if (action == 0) break; // if user choose to exit
                        try
                        {
                            menu.dictionaryActions[action](container);
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException != null)
                                Console.WriteLine($"ПОМИЛКА! {ex.InnerException.Message}");
                            else
                                Console.WriteLine($"ПОМИЛКА! {ex.Message}");
                        }
                    }
                }
                else
                    continue;
            }
        }

        static private int PrintMenu()
        {
            while (true)
            {
                string strMenu = "\n\nВиберiть операцiю:\n"
                               + "   1. Вивести список\n"
                               + "   2. Додати новий об'єкт\n"
                               + "   3. Змiнити значення об'єкта\n"
                               + "   4. Видалити об'єкт\n"
                               + "   5. Пошук за значенням\n"
                               + "   6. Сортувати за значенням\n"
                               + "   0. Вихiд\n"
                               + " ---------------------------------------\n";
                Console.Write(strMenu);
                int action;
                try
                {
                    action = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nСпробуйте ще раз");
                    continue;
                }
                if (action >= 0 || action <= 6) return action;
            }
        }
    }
}