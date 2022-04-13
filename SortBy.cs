using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace ConsoleApp1
{
    class SortBy : IComparer<PAYMENT_REQUEST>
    {
        private PropertyInfo _propertyInfo;
        public SortBy(PropertyInfo property)
        {
            _propertyInfo = property;
        }

        public int Compare([AllowNull] PAYMENT_REQUEST x, [AllowNull] PAYMENT_REQUEST y)
        {
            DateTime dateParsed1, dateParsed2;
            if (DateTime.TryParseExact(_propertyInfo.GetValue(x).ToString(), "dd'.'MM'.'yyyy",
                CultureInfo.CurrentCulture, DateTimeStyles.None, out dateParsed1) &&
                DateTime.TryParseExact(_propertyInfo.GetValue(y).ToString(), "dd'.'MM'.'yyyy",
                CultureInfo.CurrentCulture, DateTimeStyles.None, out dateParsed2))
            {
                return DateTime.Compare(dateParsed1, dateParsed2);
            }

            decimal decParse1, decParse2;
            if (Decimal.TryParse(_propertyInfo.GetValue(x).ToString(), out decParse1)
                && Decimal.TryParse(_propertyInfo.GetValue(y).ToString(), out decParse2))
            {
                if (decParse1 == decParse2) return 0;
                if (decParse1 > decParse2) return 1;
                return -1;
            }

            int intParse1, intParse2;
            if (int.TryParse(_propertyInfo.GetValue(x).ToString(), out intParse1)
                && int.TryParse(_propertyInfo.GetValue(y).ToString(), out intParse2))
            {
                if (intParse1 == intParse2) return 0;
                if (intParse1 > intParse2) return 1;
                return -1;
            }
            else return String.Compare(_propertyInfo.GetValue(x).ToString(), _propertyInfo.GetValue(y).ToString());
        }
    }
}
