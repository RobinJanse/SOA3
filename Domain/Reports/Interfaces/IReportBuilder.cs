using Domain.Sprints;
using System;

namespace Domain.Reports.Interfaces
{
	public interface IReportBuilder
	{
		void BuildHeader(DateTime date, Sprint sprint, string reportName);

		void BuildBody(string content);

		void BuildFooter();

		Report GetReport(FormatType format);
	}
}
