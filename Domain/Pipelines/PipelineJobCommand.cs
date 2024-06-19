namespace Domain.Pipelines
{
	public abstract class PipelineJobCommand
	{
		private readonly string name;

		private readonly string command;

		private string output;

		private PipelineJobStatusType status;

		public PipelineJobCommand(string name, string command)
		{
			this.name = name;
			this.command = command;
			status = PipelineJobStatusType.Off;
		}

		public string GetOutput()
		{
			return output;
		}

		public void SetOutput(string output)
		{
			this.output = output;
		}

		public string GetCommand()
		{
			return command;
		}

		public string GetName()
		{
			return name;
		}

		public PipelineJobStatusType GetStatus()
		{
			return status;
		}

		public void SetStatus(PipelineJobStatusType status)
		{
			this.status = status;
		}

		public abstract void Execute();
	}
}
