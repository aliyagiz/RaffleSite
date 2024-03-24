using System.ComponentModel;
using System.Reflection;

namespace ÇekilişSitesi.Models
{
    public class Prize
    {
		public Prize(IEnumerable<string> values)
		{
			AssignProperties(values);
		}
		public Prize(string prizeName)
		{
			PrizeName = prizeName;
			Quantity = 1;
		}

		public string PrizeName { get; set; }

        [DefaultValue(1)]
        public short Quantity { get; set; }

		private void AssignProperties(IEnumerable<string> values)
		{
			var properties = GetType().GetProperties();
			var enumerator = values.GetEnumerator();

			foreach (var property in properties)
			{
				if (!enumerator.MoveNext())
					break;

				SetProperty(property, enumerator.Current);
			}
		}

		private void SetProperty(PropertyInfo property, string value)
		{
			var convertedValue = Convert.ChangeType(value, property.PropertyType);

			property.SetValue(this, convertedValue);
		}
	}
}
