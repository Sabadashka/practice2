using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleApp1
{
    public class ContainerPAYMENT_REQUEST
    {
        public string[] array_of_names = {"id", "amount", "currency", "payer_email", "transaction_id",
                      "payment_request_date", "payment_due_to_date" };

        public List<PAYMENT_REQUEST> array_of_elements;
        private string file_name;
        private Validation validation;

        public ContainerPAYMENT_REQUEST(string file_name)
        {
            this.array_of_elements = new List<PAYMENT_REQUEST>();
            this.file_name = $"../../../{file_name}";
            //this.file_name = $"../../../{file_name}";
            //this.file_name = file_name;
            this.validation = new Validation();
        }

        public override string ToString()
        {
            string str = "";
            foreach (PAYMENT_REQUEST payment in this.array_of_elements)
            {
                str += payment.ToString() + "\n" + "------------------------------" + "\n";
            }
            return str;
        }

        public void read_from_file()
        {
            if (this.validation.validate_file("File name", file_name))
            {

                int number = 0, success = 0;

                List<string> array_values = new List<string>();

                string[] lines = File.ReadAllLines(file_name);

                foreach (string line in lines)
                {
                    if (line[0] == '-')
                    {
                        continue;
                    }
                    array_values.Add(line.Split(' ')[1]);
                }
                array_values.Reverse();

                while (array_values.Count > 0)
                {
                    try
                    {
                        number++;
                        List<string> array_attr = new List<string>();
                        for (int i = 0; i < array_of_names.Length; i++)
                        {
                            string temp = array_values[array_values.Count - 1];
                            array_values.RemoveAt(array_values.Count - 1);
                            array_attr.Add(temp);
                        }

                        PAYMENT_REQUEST payment = new PAYMENT_REQUEST(array_attr[0].Trim(), array_attr[1].Trim(), array_attr[2].Trim(), array_attr[3].Trim(), array_attr[4].Trim(), array_attr[5].Trim(), array_attr[6].Trim());
                        this.array_of_elements.Add(payment);
                        success += 1;
                    }
                    catch (Exception msg)
                    {
                        if (msg.InnerException != null)
                        {
                            Console.WriteLine(msg.InnerException.Message);
                            Console.WriteLine("Номер " + number + " є дефектним");

                            while (array_values.Count > 0 && array_values.Count % array_of_names.Length != 0)
                            {
                                array_values.RemoveAt(array_values.Count - 1);
                            }
                        }
                        else
                            Console.WriteLine(msg.Message);
                    }
                    continue;
                }
                Console.WriteLine("\n" + success + " об'єктiв були успiшно прочитанi з файлу: " + file_name + "\n\n");
            }
        }

        public void write_to_file(string value)
        {
            if (this.validation.validate_file("File name", value))
            {
                string str = "";
                foreach (PAYMENT_REQUEST payment in this.array_of_elements)
                {
                    str += payment.ToString();
                    str += "\n-----------------------\n";
                }
                File.WriteAllText(value, str);
            }
        }

        public void append(PAYMENT_REQUEST payment)
        {
            this.validation.validate_PAYMENT(payment);

            this.array_of_elements.Add(payment);
            if (this.file_name != "None")
            {
                this.write_to_file(this.file_name);
            }
        }

        public void delete(string ID)
        {
            this.validation.validate_ID("ID", ID);

            int i = 0;
            foreach (PAYMENT_REQUEST payment in this.array_of_elements)
            {
                if (payment.Id == ID)
                {
                    array_of_elements.RemoveAt(i);
                    if (this.file_name != "None")
                    {
                        this.write_to_file(this.file_name);
                    }
                    return;
                }
                i++;
            }
            throw new ExceptionPayment("Об’єкт не знайдено або контейнер порожнiй");
        }

        public List<PAYMENT_REQUEST> search_in_container(string search_string)
        {

            List<PAYMENT_REQUEST> wanted_payments = new List<PAYMENT_REQUEST>();
            int counter_of_found_elements = 0;

            PropertyInfo[] properties = typeof(PAYMENT_REQUEST).GetProperties();
            foreach (PAYMENT_REQUEST payment in this.array_of_elements)
            {
                foreach (PropertyInfo property in properties)
                {
                    if (property.GetValue(payment).ToString().ToLower().Contains(search_string.ToLower()))
                    {
                        wanted_payments.Add(payment);
                        counter_of_found_elements++;
                        break;
                    }
                }
            }
            Console.WriteLine("\nУспiшно знайдено " + counter_of_found_elements + " платежiв");
            return wanted_payments;

        }

        public void sort(PropertyInfo property)
        {
            array_of_elements.Sort(new SortBy(property));
            write_to_file(file_name);

            Console.WriteLine("Контейнер було успiшно вiдсортовано за атрибутом: " + property.Name);

        }

        public void edit(string ID, string key, string value)
        {
            PAYMENT_REQUEST payment;
            try
            {
                payment = array_of_elements.First(e => e.Id == ID);
            }
            catch (Exception ex)
            {
                throw new ExceptionPayment("Елемент з iдентифiкатором " + ID + " вiдсутнiй");
            }

            PropertyInfo[] properties = typeof(PAYMENT_REQUEST).GetProperties();
            var dictionaryGets = new Dictionary<string, PropertyInfo>();

            for (int i = 0; i < properties.Length; i++)
                dictionaryGets.Add($"{i + 1}", properties[i]);

            try
            {
                dictionaryGets[key].SetValue(payment, value);
            }
            catch
            {
                throw new ExceptionPayment("Key " + key + " втрачено");
            }
            if (this.file_name != "None")
            {
                this.write_to_file(this.file_name);
            }
            Console.WriteLine("Атрибут успiшно вiдредаговано");
        }

        public void set_the_file(string filename)
        {
            this.file_name = filename;
        }
    }
}
