using System;

namespace Domain.Pipelines.PipelineCommands
{
	public class PipelineJobBuildCommand : PipelineJobCommand
	{
		private bool isSucces = true;

		private readonly bool buildWithDebugOn = false;

		public PipelineJobBuildCommand(string name, string command, bool buildWithDebugOn) : base(name, command)
		{
			this.buildWithDebugOn = buildWithDebugOn;
		}

		public override void Execute()
		{
			base.SetStatus(PipelineJobStatusType.Running);

			Console.WriteLine($"Running command {base.GetName()} type {GetType().Name} with command {base.GetCommand()} BuildDebugOnStats {buildWithDebugOn}");

			base.SetStatus(PipelineJobStatusType.Running);

			if (isSucces)
			{
				base.SetOutput($"Sources succesfully retrieved BuildDebugOnStatus = {buildWithDebugOn}");
				base.SetStatus(PipelineJobStatusType.FINISHED);
			}
			else
			{
				base.SetOutput($"Sources unsuccesfully retrieved BuildDebugOnStatus = {buildWithDebugOn}");
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
