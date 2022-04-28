namespace Staff_Project
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;

    static class Validation
    {
        public static class UserT
        {
            private static string _patternNameSname = @"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$";
            private static string _patternEmail = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
            private static string _patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])\S{8,16}$";

            public static bool NameIsNotValid(string name)
            {
                if (!Regex.IsMatch(name, _patternNameSname))
                {
                    Console.WriteLine("Помилка! >> Поганий формат!!");
                    return true;
                }
                return false;
            }

            public static bool EmailIsNotValid(string email)
            {
                if (!Regex.IsMatch(email, _patternEmail))
                {
                    Console.WriteLine("Помилка! >> Поганий формат!");
                    return true;
                }
                return false;
            }

            public static bool EmailAlreadyExist(string email, List<User> users, bool registration = true)
            {
                foreach (User user in users)
                {
                    if (email == user.Email)
                    {
                        if (registration) Console.WriteLine("Помилка! >> Користувач з цiєю електронною адресою вже iснує");
                        return true;
                    }
                }
                if (!registration) Console.WriteLine("Помилка! >> Користувач з цiєю електронною поштою не зареєстрований");
                return false;
            }

            public static bool PasswordIsNotValid(string password)
            {
                if (!Regex.IsMatch(password, _patternPassword))
                {
                    Console.WriteLine("Помилка! >> Неправильний формат пароля");
                    return true;
                }
                return false;
            }

            public static bool PasswordIsIncorrect(string email, string password, List<User> users)
            {
                if (users.FirstOrDefault(u => u.Email == email)?.Password != password)
                {
                    Console.WriteLine("Помилка! >> Невiрний пароль");
                    return true;
                }
                return false;
            }
        }

        public static class Payment
        {
            private static string[] _allowedCurrencies = new string[] { "usd", "eur", "uah" };
            private static string _patternEmail = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
            private static string _patternTransactionId = @"^\d{8}-\d{2}$";

            public static int ValidateId(int id)
            {
                if (id < 0 || id > 10000000)
                    throw new BadModelException("ID повинен бути не меншим за 0 i не бiльший за 10000000");
                return id;
            }

            public static decimal ValidateAmount(decimal amount)
            {
                if (amount <= 0 || amount > 10000000)
                    throw new BadModelException("Сума має бути додатною i меншою за 10000000");
                return amount;
            }

            public static string ValidateCurrency(string currency)
            {
                if (_allowedCurrencies.FirstOrDefault(c => c == currency) is null)
                    throw new BadModelException("Вказано неправильно валюту");
                return currency;
            }

            public static string ValidateEmail(string email)
            {
                if (!Regex.IsMatch(email, _patternEmail))
                    throw new BadModelException("Поганий формат електронної пошти");
                return email;
            }

            public static DateTime ValidateRequestDate(DateTime requestDate, DateTime dueToDate)
            {
                if (requestDate.Year < 1980)
                    throw new BadModelException("Рiк дати запиту не може бути меншим за 1980");
                if (requestDate > DateTime.Now)
                    throw new BadModelException($"Дата запиту не може бути пiзнiше {DateTime.Now.ToString("d")}");
                if (dueToDate.ToString("d") != "01.01.0001" && requestDate > dueToDate)
                    throw new BadModelException("Дата запиту має бути ранiше встановленого термiну");

                return requestDate;
            }

            public static DateTime ValidateDueToDate(DateTime dueToDate, DateTime requestDate)
            {
                if (dueToDate.Year < 1980)
                    throw new BadModelException("Рiк сплати не може бути меншим за 1980");
                if (dueToDate < requestDate)
                    throw new BadModelException("Дата запиту має бути ранiше встановленого термiну");

                return dueToDate;
            }

            public static string ValidateTransactionId(string transactionId)
            {
                if (!Regex.IsMatch(transactionId, _patternTransactionId))
                    throw new BadModelException("Поганий формат Transaction_ID. Має бути: ********-** i мiстити лише цифри");

                return transactionId;
            }
        }
    }
}