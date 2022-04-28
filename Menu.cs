using System;
using System.Collections.Generic;
using System.Text;

namespace Staff_Project
{
    public class Menu
    {
        public Tuple<string, int> PrintBaseMenu()
        {
            int choice = -1;
            while (choice < 0 || choice > 2)
            {
                Console.WriteLine("\n----------------- Online Bank -----------------\n" +
                                  "Ви не ввiйшли. Будь ласка, увiйдiть!\n\n" +
                                  "1. Увiйти\n" +
                                  "2. Зареєструватися\n" +
                                  "0. Вийти з онлайн-банкiнгу\n" +
                                  "------------------------------------------------\n");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch { continue; }
            }

            return new Tuple<string, int>("BaseMenu", choice);
        }

        public Tuple<string, int> PrintStaffMenu()
        {
            int choice = -1;
            while (choice < 0 || choice > 8)
            {
                Console.WriteLine("\n----------------- Online Bank -----------------\n" +
                                  "Ви авторизованi в системi. Ласкаво просимо!\n\n" +
                                  "1. Зробiти новий платiж\n" +
                                  "2. Редагувати платiж зi статусом тратти\n" +
                                  "3. Видалити платiж зi статусом тратти\n" +
                                  "4. Переглянути всi мої платежi\n" +
                                  "5. Переглянути всi мої платежi за фiльтром\n" +
                                  "6. Переглянути всi схваленi платежi iнших користувачiв\n" +
                                  "7. Вiдредагувати та вiдправити вiдхилений платiж на модерацiю\n" +
                                  "8. Вийти\n" +
                                  "0. Вийти з онлайн - банкiнгу\n" +
                                  "------------------------------------------------\n");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch { continue; }
            }

            return new Tuple<string, int>("StaffMenu", choice);
        }

        public Tuple<string, int> PrintAdminMenu()
        {
            int choice = -1;
            while (choice < 0 || choice > 11)
            {
                Console.WriteLine("\n-------------------- Online Bank --------------------\n" +
                                  "Ви авторизованi в системі. Ласкаво просимо!\n\n" +
                                  "1.  Зробити новий платiж\n" +
                                  "2.  Редагувати платiж зi статусом тратти\n" +
                                  "3.  Видалити платiж зi статусом тратти\n" +
                                  "4.  Переглянути всi мої платежi\n" +
                                  "5.  Переглянути всi мої платежi за фiльтром\n" +
                                  "6.  Переглянути всi схваленi платежi iнших користувачiв\n" +
                                  "7.  Вiдредагувати та вiдправити вiдхилений платiж на модерацiю\n" +
                                  "8.  Вийтиn\n" +
                                  "9.  Показати всi платежi\n" +
                                  "10. Показати всi відхиленi платежi\n" +
                                  "11. Встановити статус платежу користувача\n" +
                                  "0.  Вийти з онлайн - банкiнгу\n" +
                                  "------------------------------------------------------\n");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch { continue; }
            }

            return new Tuple<string, int>("AdminMenu", choice);
        }
    }
}
