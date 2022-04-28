namespace Generic_Container
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Validation
    {
        public static class Payment
        {
            private static string[] _allowedCurrencies = new string[] { "usd", "eur", "uah" };
            private static string _patternEmail = @"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";
            private static string _patternTransactionId = @"^\d{8}-\d{2}$";

            public static int ValidateId(int id)
            {
                if (id < 0 || id > 10000000)
                    throw new BadModelException("ID має бути не менше 0 i не бiльше 10000000");
                return id;
            }

            public static decimal ValidateAmount(decimal amount)
            {
                if (amount <= 0 || amount > 10000000)
                    throw new BadModelException("Сума має бути не менше 0 i не бiльше 999999 10000000");
                return amount;
            }

            public static string ValidateCurrency(string currency)
            {
                if (_allowedCurrencies.FirstOrDefault(c => c == currency) is null)
                    throw new BadModelException("має бути лише USD/EUR/UAH");
                return currency;
            }

            public static string ValidateEmail(string email)
            {
                if (!Regex.IsMatch(email, _patternEmail))
                    throw new BadModelException("Некоректна адреса");
                return email;
            }

            public static DateTime ValidateRequestDate(DateTime requestDate, DateTime dueToDate)
            {
                if (requestDate.Year < 1980)
                    throw new BadModelException("Рік дати запиту не може бути меншим за 1980");
                if (requestDate > DateTime.Now)
                    throw new BadModelException($"Дата запиту не може бути пізніше {DateTime.Now.ToString("d")}");
                if (dueToDate.ToString("d") != "01.01.0001" && requestDate > dueToDate)
                    throw new BadModelException("Дата запиту має бути раніше встановленого терміну");

                return requestDate;
            }

            public static DateTime ValidateDueToDate(DateTime dueToDate, DateTime requestDate)
            {
                if (dueToDate.Year < 1980)
                    throw new BadModelException("Рік сплати не може бути меншим за 1980");
                if (dueToDate < requestDate)
                    throw new BadModelException("Дата запиту має бути раніше встановленого терміну");

                return dueToDate;
            }

            public static string ValidateTransactionId(string transactionId)
            {
                if (!Regex.IsMatch(transactionId, _patternTransactionId))
                    throw new BadModelException("Поганий формат Transaction_Id. Має бути: ********-** і містити лише цифри");

                return transactionId;
            }
        }
    }
}