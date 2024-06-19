using Domain.Backlogs;
using Domain.Developers;
using System;
using System.Collections.Generic;

namespace Domain.Forums
{
	public class Thread
	{
		private readonly List<Comment> comments;
		private readonly string title;
		private readonly Developer author;
		private readonly Activity activity;
		private readonly DateTime creationDate;

		public Thread(string title, Developer author, Activity activity)
		{
			this.title = title;
			this.author = author;
			this.activity = activity;
			comments = new List<Comment>();
			creationDate = DateTime.Now;
		}

		public void AddComment(Comment comment)
		{
			//if comment is null or whitespace throw exception
			if (string.IsNullOrWhiteSpace(comment.GetText()))
			{
				throw new Exception("Comment can't be null or whitespace");
			}

			//if activity is done dont allow comments
			if (activity.GetStatus() == ActivityStatus.Done)
			{
				throw new Exception("Can't add comments to done activities");
			}

			foreach (Comment c in comments)
			{
				c.GetAuthor().SendNotification($"New comment from {comment.GetAuthor().GetName()} has been posted to thread: {title}");
			}

			comments.Add(comment);
		}

		public void DeleteComment(Comment comment)
		{
			if (!comments.Contains(comment))
			{
				throw new Exception("Comment does not exist");
			}

			if (activity.GetStatus() == ActivityStatus.Done)
			{
				throw new Exception("Can't delete comments from done activities");
			}

			_ = comments.Remove(comment);
		}

		public List<Comment> GetComments()
		{
			return comments;
		}

		public Activity GetActivity()
		{
			return activity;
		}

		public string GetTitle()
		{
			return title;
		}

		public DateTime GetCreationDate()
		{
			return creationDate;
		}


	}
}
