using Domain.Developers;
using System;

namespace Domain.SCM
{
	public class Commit
	{
		private readonly Developer developer;
		private readonly DateTime createdOn;
		private readonly Code code;

		public Commit(Developer developer, Code code)
		{
			this.developer = developer;
			createdOn = DateTime.Now;
			this.code = code;
		}


		public Code GetCode()
		{
			return code;
		}
	}
}