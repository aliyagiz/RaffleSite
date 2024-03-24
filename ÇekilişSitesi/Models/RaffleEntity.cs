using System.ComponentModel;

namespace ÇekilişSitesi.Models
{
	/// <summary>
	/// That is page's entity
	/// </summary>
	public class RaffleEntity
	{
		public RaffleEntity()
		{
			participants = new List<Participant>();
			prizes = new List<Prize>();
		}

		public List<Participant> participants { get; set; }
		public List<Prize> prizes { get; set; }

		[DefaultValue(false)]
		public bool isMultipleEntryAllowed { get; set; }
		public bool isMultipleWinEnabled { get; set; }

    }
}
