using System;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.IO;
using System.Globalization;

namespace ConsoleApp1
{
    [Serializable]
    public class ExceptionPayment : Exception
    {
        public ExceptionPayment() { }

        public ExceptionPayment(string message)
            : base(message) { }

        public ExceptionPayment(string message, Exception innerException)
            : base(message, innerException) { }

        protected ExceptionPayment(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    class Validation
    {
        public bool validate_PAYMENT(PAYMENT_REQUEST value)
        {
            try
            {
                this.validate_ID("ID", value.Id);
                this.validate_amount("Amount", value.Amount);
                this.validate_currency("Currency", value.Currency);
                this.validate_email("Email", value.PayerEmail);
                this.validate_transactionID("TransactionID", value.TransactionId);
                this.validate_date("Request date", value.RequestDate);
                this.validate_date("Due to date", value.DueToDate);
            }
            catch (Exception message)
            {
                throw new ExceptionPayment(" ПОМИЛКА перевiрки платежу" + message.Message);
            }
            return true;
        }

        public bool validate_file(string key, string value)
        {
            if (!value.EndsWith(".txt"))
            {
                throw new ArgumentException(value + "має бути у форматi txt");
            }

            if (!File.Exists(value))
            {
                throw new ArgumentException(value + "файл не iснує");
            }
            return true;
        }

        public bool validate_ID(string key, string value)
        {
            try
            {
                if (!new Regex(@"[0-9]{1,6}").IsMatch(value))
                {
                    throw new ArgumentException(key + " ID повинен мiстити лише 1-6 цифр");
                }
                if (Int32.Parse(value) < 0 || Int32.Parse(value) > 999999)
                {
                    throw new ArgumentException(key + " ID має бути не менше 0 i не бiльше 999999");
                }
            }
            catch (ArgumentException message)
            {
                throw new ExceptionPayment(" value: " + value + " ПОМИЛКА перевiрки ID: " + message.Message);
            }
            return true;
        }

        public bool validate_amount(string key, string value)
        {
            try
            {
                if (!new Regex(@"[0-9]{1,6}").IsMatch(value))
                {
                    throw new ArgumentException(key + " повинен мiстити лише 1-6 цифр");
                }
                if (Int32.Parse(value) < 0 || Int32.Parse(value) > 999999)
                {
                    throw new ArgumentException(key + " Сума має бути не менше 0 i не бiльше 999999");
                }
            }
            catch (ArgumentException message)
            {
                throw new ExceptionPayment(" value: " + value + "  ПОМИЛКА перевiрки суми: " + message.Message);
            }
            return true;

        }

        public bool validate_currency(string key, string value)
        {
            try
            {
                if (value != "usd" && value != "eur" && value != "uah")
                {
                    throw new ArgumentException(key + " має бути лише USD/EUR/UAH  " + " value: " + value);
                }
            }
            catch (ArgumentException message)
            {
                throw new ExceptionPayment(" value: " + value + "  ПОМИЛКА перевiрки валюти: " + message.Message);
            }
            return true;
        }

        public bool validate_date(string key, string value)
        {
            try
            {
                try
                {
                    DateTime dateTime = DateTime.Parse(value);

                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentException(key + " дата має бути в такому форматi: mm.dd.yyyy");
                }

            }
            catch (ArgumentException message)
            {
                throw new ExceptionPayment(" value: " + value + "  ПОМИЛКА перевiрки дaти: " + message.Message);
            }
            return true;
        }

        public void validate_two_dates(string date1, string date2)
        {
            if (DateTime.Parse(date1) > DateTime.Parse(date2))
            {
                throw new ExceptionPayment("'Due to date' не може бути пiзнiше нiж 'request to date'");
            }
        }

        public bool validate_email(string key, string value)
        {
            try
            {
                if (!new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b").IsMatch(value))
                {
                    throw new ArgumentException(key + "Некоректна адреса");
                }
            }
            catch (Exception message)
            {
                throw new ExceptionPayment(" value: " + value + "  ПОМИЛКА перевiрки пошти" + message.Message);
            }
            return true;
        }

        public bool validate_transactionID(string key, string value)
        {
            try
            {
                if (!new Regex(@"\d{8}-\d{2}").IsMatch(value))
                {
                    throw new ExceptionPayment(key + "має бути у форматi: ********-** i мiстити лише цифри");
                }
            }
            catch (Exception message)
            {
                throw new ExceptionPayment(" value: " + value + "  ПОМИЛКА transaction ID: " + message.Message);
            }
            return true;
        }
    }
}
