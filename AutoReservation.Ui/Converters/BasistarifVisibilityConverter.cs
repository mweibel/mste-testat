using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.Converters
{
	public class BasistarifVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if ((AutoKlasse) value == AutoKlasse.Luxusklasse)
			{
				return Visibility.Visible;
			}
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}