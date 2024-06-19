using System;

namespace Domain.Pipelines.PipelineCommands
{
	public class PipelineJobPackageCommand : PipelineJobCommand
	{
		private bool isSucces = true;

		public PipelineJobPackageCommand(string name, string command) : base(name, command)
		{

		}

		public override void Execute()
		{
			base.SetStatus(PipelineJobStatusType.Running);

			Console.WriteLine($"Running command {base.GetName()} type {GetType().Name} with command {base.GetCommand()}");

			base.SetStatus(PipelineJobStatusType.Running);

			if (isSucces)
			{
				base.SetOutput("Sources succesfully retrieved");
				base.SetStatus(PipelineJobStatusType.FINISHED);
			}
			else
			{
				base.SetOutput("Sources unsuccesfully retrieved");
				base.SetStatus(PipelineJobStatusType.FAILED);
			}
		}

		//Just for testing purposes
		public void MakePipelineFail()
		{
			isSucces = false;
		}
	}
}
