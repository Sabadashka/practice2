using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    public static class Rflx
    {
        public static object GetAttr(this object obj, string name)
        {
            Type type = obj.GetType();
            BindingFlags flags = BindingFlags.Instance |
                                     BindingFlags.Public |
                                     BindingFlags.GetProperty;

            return type.InvokeMember(name, flags, Type.DefaultBinder, obj, null);
        }
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            return (T)retval;
        }
    }

    public class PAYMENT_REQUEST
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string PayerEmail { get; set; }
        public string TransactionId { get; set; }
        public string RequestDate { get; set; }
        public string DueToDate { get; set; }

        private Validation validation;

        public PAYMENT_REQUEST(string id, string amount, string currency, string payer_email, string transaction_id, string payment_request_date, string payment_due_to_date)
        {
            this.validation = new Validation();

            try
            {
                if (this.validation.validate_ID("ID", id)) { this.Id = id; }
                if (this.validation.validate_amount("Amount", amount)) { this.Amount = amount; }
                if (this.validation.validate_currency("Currency", currency)) { this.Currency = currency; }
                if (this.validation.validate_email("Email", payer_email)) { this.PayerEmail = payer_email; }
                if (this.validation.validate_transactionID("Transaction ID", transaction_id)) { this.TransactionId = transaction_id; }
                if (this.validation.validate_date("Request date", payment_request_date)) { this.RequestDate = payment_request_date; }
                if (this.validation.validate_date("Due to date", payment_due_to_date)) { this.DueToDate = payment_due_to_date; }
                this.validation.validate_two_dates(this.RequestDate, this.DueToDate);
            }
            catch (Exception message)
            {
                throw new ArgumentException(message.Message + "\nПомилка створення об'єкта");
            }
        }

        public override string ToString()
        {
            var properties = typeof(PAYMENT_REQUEST).GetProperties();

            string payments = String.Empty;

            foreach (var property in properties)
                payments += $"{property.Name}: {property.GetValue(this)}\n";

            return payments.Substring(0, payments.Length - 1);
        }
    }
}
