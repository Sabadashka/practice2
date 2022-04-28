namespace Staff_Project
{
    using System;
    using System.Collections.Generic;
    using static Validation.UserT;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    static class Helpers
    {
        public static Dictionary<string, string> EnterDataAtLogin(List<User> users)
        {
            Console.WriteLine("----- Login -----");
            string email;
            do
            {
                Console.Write("Введiть email: ");
                email = Console.ReadLine();
            } while (EmailIsNotValid(email) || !EmailAlreadyExist(email, users, registration: false));

            string password;
            do
            {
                Console.Write("Введiть пароль: ");
                password = Console.ReadLine();
                if (PasswordIsNotValid(password)) continue;

                password = GetHash(password);
            } while (PasswordIsIncorrect(email, password, users));

            Dictionary<string, string> data = new Dictionary<string, string>();
            data["email"] = email;
            data["password"] = password;
            return data;
        }

        public static Dictionary<string, string> EnterDataAtRegistration(List<User> users)
        {
            Console.WriteLine("----- Register -----");
            string firstName;
            do
            {
                Console.Write("Введiть iм'я: ");
                firstName = Console.ReadLine();
            } while (NameIsNotValid(firstName));

            string lastName;
            do
            {
                Console.Write("Введiть прiзвище: ");
                lastName = Console.ReadLine();
            } while (NameIsNotValid(lastName));

            string email;
            do
            {
                Console.Write("Введiть email: ");
                email = Console.ReadLine();
            } while (EmailIsNotValid(email) || EmailAlreadyExist(email, users));

            string password;
            do
            {
                Console.Write("Введiть пароль: ");
                password = Console.ReadLine();
            } while (PasswordIsNotValid(password));

            password = GetHash(password);

            Dictionary<string, string> data = new Dictionary<string, string>();
            data["firstName"] = firstName;
            data["lastName"] = lastName;
            data["email"] = email;
            data["password"] = password;
            return data;
        }

        public static void SetPropertyPayment(Payment payment, PropertyInfo property, string value)
        {
            if (property.GetGetMethod().ReturnType == typeof(DateTime))
                property.SetValue(payment, DateTime.Parse(value));
            else if (property.GetGetMethod().ReturnType == typeof(decimal))
                property.SetValue(payment, decimal.Parse(value));
            else if (property.GetGetMethod().ReturnType == typeof(int))
                property.SetValue(payment, int.Parse(value));
            else
                property.SetValue(payment, value);
        }

        public static Respond GetRespondFromRejectedPayments(List<Respond> responds, Payment payment)
        {
            return
                responds
                .FirstOrDefault(r => r.Id == payment.Id &&
                                     r.Amount == payment.Amount &&
                                     r.Currency == payment.Currency &&
                                     r.PayerEmail == payment.PayerEmail &&
                                     r.RequestDate == payment.RequestDate &&
                                     r.DueToDate == payment.DueToDate &&
                                     r.TransactionId == payment.TransactionId);
        }

        private static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }

    public class JsonConverterr : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(User));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (jo["Role"].Value<string>() == "1")
                return jo.ToObject<Staff>(serializer);

            if (jo["Role"].Value<string>() == "0")
                return jo.ToObject<Admin>(serializer);

            return null;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

