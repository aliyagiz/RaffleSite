using System.ComponentModel;
using System.Reflection;

namespace ÇekilişSitesi.Models
{

	/// <summary>
	/// 
	/// That's for User model, very straightforward
	/// Coefficient is for the rate of increasing the number of participation.
	/// Handle can be TwitterHandle, InstagramHandle or whatever you wish!
	/// 
	/// </summary>
	public class Participant
	{
		public Participant()
		{
		}

		public Participant(string fullName, short coefficientFactor = 1, string phoneNumber = "", string mailAddress = "", string handle = "")
		{
			FullName = fullName;
			CoefficientFactor = coefficientFactor;
			PhoneNumber = phoneNumber;
			MailAddress = mailAddress;
			Handle = handle;
			Prizes = new List<Prize>();
		}
		public Participant(IEnumerable<string> values)
		{
			AssignProperties(values);
			Prizes = new List<Prize>();
		}

		public string FullName { get; set; }

		[DefaultValue(1)]
		public short CoefficientFactor { get; set; }

		public string PhoneNumber { get; set; }

		public string MailAddress { get; set; }

		public string Handle { get; set; }
		public List<Prize> Prizes { get; set; }


		/// <summary>
		/// This might be bad case of constructing class but I just want to use it on my project. It is kinda cool (i think?)
		/// </summary>
		/// <param name="values"></param>
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
