using Domain.Reports.Interfaces;
using Domain.Sprints;
using System;

namespace Domain.Reports.ReportBuilders
{
	public class ReviewReportBuilder : IReportBuilder
	{
		private readonly Report report;

		public ReviewReportBuilder()
		{
			report = new Report();
		}
		public void BuildBody(string content)
		{
			report.Body = new Body { Content = content };
		}

		public void BuildFooter()
		{
			report.Footer = new Footer { CompanyName = "Student Report", CompanyLogo = "" };
		}

		public void BuildHeader(DateTime date, Sprint sprint, string reportName)
		{
			report.Header = new Header { CreationDate = date, SprintName = sprint.GetName(), ReportName = reportName };
		}

		public Report GetReport(FormatType format)
		{
			report.Format = format;
			return report;
		}
	}
}
