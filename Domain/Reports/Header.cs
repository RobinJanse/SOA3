using System;

namespace Domain.Reports
{
	public class Header
	{
		public string CompanyName { get; set; }

		public string CompanyLogo { get; set; }

		public string SprintName { get; set; }

		public string ReportName { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
