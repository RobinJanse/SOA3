namespace Domain.Reports
{
	public class Report
	{
		public Header Header { get; set; }

		public Body Body { get; set; }

		public Footer Footer { get; set; }

		public FormatType Format { get; set; }
	}
}
