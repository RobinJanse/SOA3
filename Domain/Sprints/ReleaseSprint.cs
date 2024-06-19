using Domain.Developers;
using Domain.Pipelines;
using Domain.Reports;
using System;
using System.Collections.Generic;

namespace Domain.Sprints
{
	public class ReleaseSprint : Sprint
	{
		public ReleaseSprint(Project project, string name, DateTime startDate, DateTime endDate, Developer scrumMaster, List<Developer> developers, Pipeline pipeline) : base(project, name, startDate, endDate, scrumMaster, developers)
		{
			base.SetPipeline(pipeline);
		}

		public Report GenerateDeploymentReport(string content, string name, DateTime date, FormatType format)
		{
			return ReportBuilderDirector.BuildAvansReport(this, content, name, date, format);
		}
	}
}
