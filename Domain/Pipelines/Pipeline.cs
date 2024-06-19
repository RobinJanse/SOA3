using System;
using System.Collections.Generic;

namespace Domain.Pipelines
{
	public class Pipeline
	{
		private string name;
		private readonly List<PipelineJobCommand> commands;
		private PipelineJobStatusType status;

		private PipelineJobCommand currentCommand;

		public Pipeline(List<PipelineJobCommand> commands, string name)
		{
			this.commands = commands;
			this.name = name;
		}

		public void SetStatus(PipelineJobStatusType status)
		{
			this.status = status;
		}

		public void Execute()
		{
			if (commands.Count == 0)
			{
				throw new Exception("Can't execute pipeline without commands");
			}

			foreach (PipelineJobCommand command in commands)
			{
				command.SetStatus(PipelineJobStatusType.Queued);
			}

			status = PipelineJobStatusType.Running;
			foreach (PipelineJobCommand command in commands)
			{
				currentCommand = command;
				command.Execute();
				if (command.GetStatus() == PipelineJobStatusType.FAILED)
				{
					status = PipelineJobStatusType.FAILED;

					//tell here it failed

					return;
				}
				//tell here single job succes
			}
			status = PipelineJobStatusType.FINISHED;
			//tell here whole pipeline succeeded
		}

		public List<string> PipelineOutput()
		{
			List<string> commandOutputs = new List<string>();

			foreach (PipelineJobCommand c in commands)
			{
				commandOutputs.Add(c.GetOutput());
			}

			return commandOutputs;
		}

		public PipelineJobStatusType GetStatus()
		{
			return status;
		}

		public List<PipelineJobCommand> GetCommands()
		{
			return status == PipelineJobStatusType.FINISHED || status == PipelineJobStatusType.FAILED
				? commands
				:
			throw new Exception("Can't get commands while pipeline is not done");
		}

		public PipelineJobCommand GetCurrentCommand()
		{
			return status == PipelineJobStatusType.Off || status == PipelineJobStatusType.Queued
				? throw new Exception("Can't get command when pipeline is not yet running")
				: currentCommand;
		}

		public string GetName()
		{
			return name;
		}

		public void SetName(string name)
		{
			this.name = name;
		}
	}
}
