using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        public static string menu()
        {
            Console.WriteLine("\n\nВиберiть операцiю:\n"
                   + "1. Вивести список\n"
                   + "2. Пошук за значенням\n"
                   + "3. Сортувати за полем\n"
                   + "4. Видалити оплату за допомогою ID\n"
                   + "5. Додати нову оплату\n"
                   + "6. Змiнити iснуючу оплату\n\n"
                   + "7. Вихiд з програми\n");
            string action = Console.ReadLine();
            return action;
        }

        static void Main()
        {
            ContainerPAYMENT_REQUEST container;
            while (true)
            {

                try
                {
                    container = read_in_container();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
            var dictionaryActions = new Dictionary<string, Func<ContainerPAYMENT_REQUEST, bool>>(){
                { "1", print_container},
                { "2", search_in_container},
                { "3", sort_container},
                { "4", delete_from_container},
                { "5", add_to_container},
                { "6", edit_attribute_container}
            };
            while (true)
            {
                try
                {
                    string action = menu();
                    if (action == "7") { break; }
                    else { dictionaryActions[action](container); }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message + "\nСпробуйте ще раз");
                }
                catch (ExceptionPayment e)
                {
                    Console.WriteLine(e.Message + "\nСпробуйте ще раз");
                }
            }
        }

        public static ContainerPAYMENT_REQUEST read_in_container()
        {
            Console.WriteLine("\nВведiть назву файла: ");
            string filename = Console.ReadLine();
            ContainerPAYMENT_REQUEST container = new ContainerPAYMENT_REQUEST(filename);
            container.read_from_file();
            return container;
        }

        public static bool print_container(ContainerPAYMENT_REQUEST container)
        {
            Console.WriteLine(container.ToString());
            return true;
        }

        public static bool search_in_container(ContainerPAYMENT_REQUEST container)
        {
            Console.WriteLine("\n\n\nВведiть значення для пошуку: ");
            string attr = Console.ReadLine();
            List<PAYMENT_REQUEST> list = container.search_in_container(attr);

            Console.WriteLine("\n\n\n---------------------- Cписок ----------------------");
            Console.WriteLine("\nЗнайдено платежi:\n\n");
            foreach (PAYMENT_REQUEST payment in list)
            {
                Console.WriteLine(payment);
                Console.WriteLine("-----------------");
            }
            return true;
        }

        public static bool sort_container(ContainerPAYMENT_REQUEST container)
        {
            Console.WriteLine("\n\n\nВиберiть варiант сортування:\n");

            PropertyInfo[] properties = typeof(PAYMENT_REQUEST).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {properties[i].Name}\n");
            }

            int attr = Convert.ToInt32(Console.ReadLine());
            container.sort(properties[attr - 1]);
            Console.WriteLine(container.ToString());
            return true;
        }

        public static bool delete_from_container(ContainerPAYMENT_REQUEST container)
        {
            Console.WriteLine("\n\n\n---------------------- Cписок ----------------------");
            Console.WriteLine(container.ToString());
            Console.WriteLine("Введiть запис ID: ");
            string id = Console.ReadLine().Trim();
            container.delete(id);
            return true;
        }

        public static bool add_to_container(ContainerPAYMENT_REQUEST container)
        {
            Console.WriteLine("\n\n\nВведiть ID: \n");
            string id = Console.ReadLine();
            Console.WriteLine("\n\nВведiть суму: \n");
            string amount = Console.ReadLine();
            Console.WriteLine("\n\nВведiть валюту (usd/eur/uah): \n");
            string currency = Console.ReadLine();
            Console.WriteLine("\n\nВведiть пошту: \n");
            string email = Console.ReadLine();
            Console.WriteLine("\n\nВведiть iдентифiкатор транзакцiї: \n");
            string transaction_id = Console.ReadLine();
            Console.WriteLine("\n\nВведiть дату(вiд) (день.місяць.рiк): \n");
            string request_date = Console.ReadLine();
            Console.WriteLine("\n\nВведiть дату(до) (день.місяць.рiк): \n");
            string due_to_date = Console.ReadLine();

            PAYMENT_REQUEST payment = new PAYMENT_REQUEST(id, amount, currency, email, transaction_id, request_date.Trim(), due_to_date.Trim());
            container.append(payment);
            return true;
        }

        public static bool edit_attribute_container(ContainerPAYMENT_REQUEST container)
        {
            Console.WriteLine("\n\n\n---------------------- Cписок ----------------------");
            Console.WriteLine(container.ToString());
            Console.WriteLine("Введiть запис ID: ");
            string id = Console.ReadLine();
            Console.WriteLine("\nВиберiть варiант для редагування:\n");

            PropertyInfo[] properties = typeof(PAYMENT_REQUEST).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {properties[i].Name}\n");
            }

            string attr = Console.ReadLine();
            Console.WriteLine("\nВведiть нове значення: ");
            string value = Console.ReadLine();
            container.edit(id, attr, value);
            return true;
        }
    }
}


