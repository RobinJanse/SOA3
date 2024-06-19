using Domain.Reports.Interfaces;
using Domain.Reports.ReportBuilders;
using Domain.Sprints;
using System;

namespace Domain.Reports
{
	public static class ReportBuilderDirector
	{
		public static Report BuildAvansReport(Sprint sprint, string content, string reportName, DateTime date, FormatType format)
		{
			IReportBuilder builder = new DeploymentReportBuilder();
			builder.BuildHeader(date, sprint, reportName);
			builder.BuildBody(content);
			builder.BuildFooter();
			return builder.GetReport(format);
		}

		public static Report BuildStudentReport(Sprint sprint, string content, string reportName, DateTime date, FormatType format)
		{
			IReportBuilder builder = new ReviewReportBuilder();
			builder.BuildHeader(date, sprint, reportName);
			builder.BuildBody(content);
			builder.BuildFooter();
			return builder.GetReport(format);
		}
	}
}
