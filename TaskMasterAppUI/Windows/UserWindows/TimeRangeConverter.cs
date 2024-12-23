using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskMasterAppUI.Windows.UserWindows
{
    public class TimeRangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dueDate)
            {
                return $"{dueDate:HH:mm dd/MM/yyyy}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
