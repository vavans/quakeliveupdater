using System.Windows.Data;
using System;

namespace QuakeLiveAnalyzer.Converters
{
	public class MultipleEnumToBooleanConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			foreach (object val in values)
			{
				if (val.ToString().Equals(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}

			return false;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}