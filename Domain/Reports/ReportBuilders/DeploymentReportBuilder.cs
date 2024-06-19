using Domain.Reports.Interfaces;
using Domain.Sprints;
using System;

namespace Domain.Reports.ReportBuilders
{
	public class DeploymentReportBuilder : IReportBuilder
	{
		private readonly Report report;

		public DeploymentReportBuilder()
		{
			report = new Report();
		}

		public void BuildBody(string content)
		{
			report.Body = new Body { Content = content };
		}

		public void BuildFooter()
		{
			report.Footer = new Footer { CompanyName = "Avans", CompanyLogo = "AvansLogo" };
		}

		public void BuildHeader(DateTime date, Sprint sprint, string reportName)
		{
			report.Header = new Header { CompanyName = "Avans", CompanyLogo = "AvansLogo", CreationDate = date, ReportName = reportName, SprintName = sprint.GetName() };
		}

		public Report GetReport(FormatType format)
		{
			report.Format = format;
			return report;
		}
	}
}
