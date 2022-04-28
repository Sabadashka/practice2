namespace Generic_Container
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    class SortBy<T> : IComparer<T>
    {
        private PropertyInfo _propertyInfo;
        public SortBy(PropertyInfo property)
        {
            _propertyInfo = property;
        }

        public int Compare([AllowNull] T x, [AllowNull] T y)
        {
            if(_propertyInfo.GetValue(x).GetType() == typeof(DateTime))
                return DateTime.Compare((DateTime)_propertyInfo.GetValue(x), (DateTime)_propertyInfo.GetValue(y));
            else if (_propertyInfo.GetValue(x).GetType() == typeof(int))
            {
                if (_propertyInfo.GetValue(x) == _propertyInfo.GetValue(y)) return 0;
                if ((int)_propertyInfo.GetValue(x) > (int)_propertyInfo.GetValue(y)) return 1;
                return -1;
            }
            else if (_propertyInfo.GetValue(x).GetType() == typeof(decimal))
            {
                if (_propertyInfo.GetValue(x) == _propertyInfo.GetValue(y)) return 0;
                if ((decimal)_propertyInfo.GetValue(x) > (decimal)_propertyInfo.GetValue(y)) return 1;
                return -1;
            }
            else return String.Compare(_propertyInfo.GetValue(x).ToString(), _propertyInfo.GetValue(y).ToString());
        }
    }
}