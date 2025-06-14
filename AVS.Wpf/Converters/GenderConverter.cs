﻿
using System;
using System.Globalization;
using System.Windows.Data;

namespace AVS.Wpf.Converters;

public class GenderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        return value.ToString() == parameter.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((bool)value && parameter != null)
            return parameter.ToString();

        return Binding.DoNothing;
    }
}
