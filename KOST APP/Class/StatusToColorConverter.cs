using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace KOST_APP.Class
{
    [ValueConversion(typeof(String), typeof(String))]
    class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String stat = value.ToString();

            if (stat.Equals("1"))
            {
                return "#81c784";
            }
            else if (stat.Equals("2"))
            {
                return "#ef5350";
            }
            else
            {
                return "#cfd8dc";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
