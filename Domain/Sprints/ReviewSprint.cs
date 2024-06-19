using Domain.Developers;
using Domain.Reports;
using System;
using System.Collections.Generic;

namespace Domain.Sprints
{
	public class ReviewSprint : Sprint
	{
		private Review _review;
		private bool _isReviewDone = false;
		public ReviewSprint(Project project, string name, DateTime startDate, DateTime endDate, Developer scrumMaster, List<Developer> developers) : base(project, name, startDate, endDate, scrumMaster, developers)
		{
		}

		public void AddReview(Review review)
		{
			_review = review;
		}

		public void SetReviewItem(Review review)
		{
			if (base.GetState().GetSprintState() == SprintStateTypes.Finished)
			{
				_review = review;
				_isReviewDone = true;
			}
			else
			{
				throw new Exception("Review can't be added to sprint that is not finished");
			}
		}

		public Review GetReview()
		{
			return _review;
		}

		public bool GetIsReviewDone()
		{
			return _isReviewDone;
		}

		public Report GenerateReviewReport(string content, string name, DateTime date, FormatType format)
		{
			return ReportBuilderDirector.BuildStudentReport(this, content, name, date, format);
		}
	}
}
