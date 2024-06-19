using System;
using System.Collections.Generic;

namespace Domain.SCM
{
	public class Repository
	{
		private readonly List<Branch> branches;
		private readonly Branch masterBranch;

		public Repository(string masterBranch)
		{
			this.masterBranch = new Branch(masterBranch);
			branches = new List<Branch>
			{
				this.masterBranch
			};
		}

		public void AddBranch(Branch branch)
		{
			foreach (Branch b in branches)
			{
				if (b.GetName() == branch.GetName())
				{
					throw new Exception("Branch already exists");
				}
			}
			branches.Add(branch);
		}

		public List<Branch> GetBranches()
		{
			return branches;
		}
	}
}
