namespace Generic_Container
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    class Menu<T> where T : new()
    {

        private static PropertyInfo[] properties;
        public delegate void Action(GenericContainer<T> container);

        public Menu()
        {
            properties = typeof(T).GetProperties();
        }

        public readonly Dictionary<int, Action> dictionaryActions = new Dictionary<int, Action>()
            {
                { 1, new Action(Print) }, { 2, new Action(Add) }, { 3, new Action(Edit) },
                { 4, new Action(Delete) },{ 5, new Action(Search) }, { 6, new Action(Sort) },
            };

        private static void Print(GenericContainer<T> container)
        {
            Console.WriteLine(container.ToString());
        }

        private static void Add(GenericContainer<T> container)
        {
            string errors = string.Empty;
            var data = EnterData(properties);
            T _object = new T();

            foreach (PropertyInfo property in properties)
            {
                try
                {
                    if (property.GetGetMethod().ReturnType == typeof(decimal))
                        property.SetValue(_object, Decimal.Parse(data[property.Name]));
                    else if (property.GetGetMethod().ReturnType == typeof(int))
                        property.SetValue(_object, Int32.Parse(data[property.Name]));
                    else if (property.GetGetMethod().ReturnType == typeof(DateTime))
                        property.SetValue(_object, DateTime.Parse(data[property.Name]));
                    else
                        property.SetValue(_object, data[property.Name]);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        errors += $"ПОМИЛКА! {ex.InnerException.Message}\n";
                    else
                        errors += $"ПОМИЛКА! {ex.Message}\n";
                }
            }
            if (errors != string.Empty) throw new BadModelException(errors);
            container.Add(_object);
            Console.WriteLine("Елемент успiшно додано до контейнера");
        }

        private static void Edit(GenericContainer<T> container)
        {
            Console.Write("Введiть об'єкт для редагування: ");
            int editId = Int32.Parse(Console.ReadLine());
            container.Edit(editId);
            Console.WriteLine("Редагування завершено");
        }

        private static void Delete(GenericContainer<T> container)
        {
            Console.Write("Введiть об'єкт для видалення: ");
            int deleteId = Int32.Parse(Console.ReadLine());
            container.Delete(deleteId);
            Console.WriteLine("Видалення завершено");
        }

        private static void Search(GenericContainer<T> container)
        {
            Console.Write("Введiть значення для пошуку: ");
            string searchValue = Console.ReadLine();
            List<T> objects = container.Search(searchValue);

            foreach (var _object in objects)
            {
                Console.WriteLine(_object);
                Console.WriteLine("---------------------");
            }

            Console.WriteLine("Пошук завершено");
        }

        private static void Sort(GenericContainer<T> container)
        {
            int index = -1;
            while (index < 0 || index >= properties.Length)
            {
                Console.WriteLine("Виберiть варiант сортування: ");
                for (int i = 0; i < properties.Length; i++)
                    Console.WriteLine($"  {i}. {properties[i].Name}");
                Console.WriteLine("  <. Назад");

                string action = Console.ReadLine();
                if (action == "<") return;
                try
                {
                    index = Convert.ToInt32(action);
                }
                catch
                {
                    Console.WriteLine("Спробуйте ще раз");
                }
            }

            Console.Write("Сортувати за зростанням(а) чи спаданням(d)? (a/d): ");
            string sortType = string.Empty;
            while (sortType != "a" && sortType != "d")
                sortType = Console.ReadLine();

            if (sortType == "a") container.Sort(index, SortingTypeEnum.Ascending);
            else if (sortType == "d") container.Sort(index, SortingTypeEnum.Descending);
            Console.WriteLine("Сортування закiнчено");
        }

        private static Dictionary<string, string> EnterData(PropertyInfo[] properties)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (PropertyInfo property in properties)
            {
                Console.Write($"Введiть {property.Name}: ");
                data.Add(property.Name, Console.ReadLine());
            }

            return data;
        }
    }
}