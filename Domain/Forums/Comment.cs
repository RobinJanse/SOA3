using Domain.Developers;
using System;

namespace Domain.Forums
{
	public class Comment
	{
		private readonly Thread thread;
		private readonly string comment;
		private readonly Developer author;
		private readonly DateTime date;

		public Comment(Thread thread, string comment, Developer author)
		{
			this.thread = thread;
			this.comment = comment;
			this.author = author;
			date = DateTime.Now;
		}

		public Thread GetThread()
		{
			return thread;
		}

		public string GetText()
		{
			return comment;
		}

		public Developer GetAuthor()
		{
			return author;
		}

		public DateTime GetDate()
		{
			return date;
		}
	}
}
