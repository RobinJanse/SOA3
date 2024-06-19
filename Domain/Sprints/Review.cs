using Domain.Developers;

namespace Domain.Sprints
{
	public class Review
	{
		private readonly string review;
		private readonly Sprint sprint;

		public Developer Author { get; }

		public Review(string review, Developer author, Sprint sprint)
		{
			this.review = review;
			Author = author;
			this.sprint = sprint;
		}
	}
}
