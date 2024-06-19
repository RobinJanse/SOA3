using Domain.Developers.Enums;
using Domain.Pipelines;
using Domain.Pipelines.PipelineCommands;
using Domain.Sprints;

namespace DomainTests
{
	public class PipelineTests
	{
		//FR_P1 Het systeem moet ondersteuning bieden voor verschillende typen acties binnen een development pipeline, zoals Sources, Package, Build, Test, Analyse, Deploy en Utility.

		//•	Gebruikers kunnen activiteiten van elk type toevoegen aan een development pipeline.
		[Fact]
		public void A_Pipeline_Can_Be_Created()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			_ = new Project(productOwner, name);


			//Act
			PipelineJobAnalyzeCommand pipelineCommand = new("Analyze the code using blabla", "Command to use");

			Pipeline pipeline = new(new List<PipelineJobCommand> { pipelineCommand }, "First pipeline");

			pipeline.Execute();

			//Assert
			Assert.True(pipeline.GetCommands().Count == 1);
		}

		//•	De acties worden correct uitgevoerd volgens de gedefinieerde volgorde in de pipeline.
		[Fact]
		public void A_Pipeline_Can_Be_Executed()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			_ = new Project(productOwner, name);

			PipelineJobAnalyzeCommand pipelineCommand1 = new("Analyze the code using blabla", "Command to use");
			PipelineJobBuildCommand pipelineCommand2 = new("Build the code", "Command to use", false);
			PipelineJobDeployCommand pipelineCommand3 = new("ADeploy", "Command to use upload.exe");

			Pipeline pipeline = new(new List<PipelineJobCommand> { pipelineCommand1, pipelineCommand2, pipelineCommand3 }, "First pipeline");

			//Act
			pipeline.Execute();

			//Assert
			Assert.True(pipelineCommand1.GetStatus() == PipelineJobStatusType.FINISHED);
		}



		//FR_P2 Het systeem moet de mogelijkheid bieden om een development pipeline te koppelen aan een deployment sprint.

		//•	Gebruikers kunnen een pipeline definiëren en koppelen aan een Sprint.
		[Fact]
		public void A_Pipeline_Can_Be_Added_To_A_Sprint()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			PipelineJobAnalyzeCommand pipelineCommand1 = new("Analyze the code using blabla", "Command to use");
			PipelineJobBuildCommand pipelineCommand2 = new("Build the code", "Command to use", false);
			PipelineJobDeployCommand pipelineCommand3 = new("ADeploy", "Command to use upload.exe");

			Pipeline pipeline = new(new List<PipelineJobCommand> { pipelineCommand1, pipelineCommand2, pipelineCommand3 }, "First pipeline");

			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer>());

			//Act
			sprint.SetPipeline(pipeline);

			//Assert
			Assert.Equal(sprint.GetPipeline(), pipeline);
		}

	}
}
