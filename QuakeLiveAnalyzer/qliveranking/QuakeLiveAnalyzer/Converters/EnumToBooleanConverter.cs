using System.Windows.Data;
using System;

namespace QuakeLiveAnalyzer.Converters
{
	public class EnumToBooleanConverter : IValueConverter
	{
		public object Convert(object val, System.Type targetType, object prm, System.Globalization.CultureInfo culture)
		{
			string value = val.ToString();
			string parameter = prm.ToString();
			string[] parameters;

			parameters = (parameter.Contains("|")) ? parameter.Split(new[] { '|' }) : new[] { parameter };

			foreach (string p in parameters)
			{
				if (value.Equals(p, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}

			return false;
		}

		public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}
}