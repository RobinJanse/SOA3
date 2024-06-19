using Domain.Developers.Enums;
using Domain.Pipelines;
using Domain.Pipelines.PipelineCommands;
using Domain.Sprints;

namespace DomainTests
{
	public class SprintTests
	{
		//FR_S1 Het systeem moet de mogelijkheid bieden om sprints aan te maken
		//•	Een sprint moet toegevoegd kunnen worden
		[Fact]
		public void A_Sprint_Can_Be_Added_To_A_Project()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			//Act
			project.AddSprint(sprint);

			//Assert
			Assert.Contains(sprint, project.GetSprints());
		}

		//•	Een sprint moet aangepast kunnen worden.
		[Fact]
		public void A_Sprint_Can_Be_Edited()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			//Act
			project.AddSprint(sprint);
			sprint.SetName("Sprint 2");

			//Assert
			Assert.Equal("Sprint 2", sprint.GetName());
		}

		//•	Een sprint moet ingezien kunnen worden.

		[Fact]
		public void A_Sprint_Can_Be_Viewed()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			//Act
			project.AddSprint(sprint);

			//Assert
			Assert.Contains(sprint, project.GetSprints());
			Assert.Equal("Sprint 1", sprint.GetName());
			Assert.Equal(productOwner, sprint.GetScrumMaster());
		}

		//FR_S2 Het systeem moet de mogelijkheid bieden om backlog items aan een sprintbacklog toe te voegen.
		//•	Een backlog item moet aan een sprintbacklog toegevoegd kunnen worden.
		[Fact]
		public void A_BacklogItem_Can_Be_Added_To_A_Sprint()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			BacklogItem backlogItem = new("BacklogItem 1", "Description 1", 1, backlog);


			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);

			//Assert
			Assert.Contains(backlogItem, sprint.GetBacklogItems());
		}


		//FR_S3 Een sprint moet de fases Scheduled, InProgress en Finished ondersteunen.
		//•	Een sprint moet de fase Scheduled ondersteunen.
		[Fact]
		public void A_Sprint_Can_Be_Scheduled()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			//Act
			project.AddSprint(sprint);

			//Assert
			Assert.Equal(SprintStateTypes.Scheduled, sprint.GetState().GetSprintState());
		}

		//•	Een sprint moet de fase InProgress ondersteunen.
		[Fact]
		public void A_Sprint_Can_Go_From_Scheduled_State_To_InProgress()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			//Act
			project.AddSprint(sprint);
			sprint.GetState().NextState();

			//Assert
			Assert.Equal(SprintStateTypes.InProgress, sprint.GetState().GetSprintState());
		}

		//•	Een sprint moet de fase Finished ondersteunen.
		[Fact]
		public void A_Sprint_Can_Go_From_InProgress_State_To_Finished()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			project.AddSprint(sprint);
			sprint.GetState().NextState();

			//Act
			sprint.GetState().NextState();

			//Assert
			Assert.Equal(SprintStateTypes.Finished, sprint.GetState().GetSprintState());
		}

		// * Een sprint kan niet naar de vorige fase wanneer de fase schedules is.
		[Fact]
		public void A_Sprint_Can_Not_Go_From_Scheduled_State_To_Previous_State()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();
			ReviewSprint sprint = new(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			project.AddSprint(sprint);

			//Act


			//Assert
			Assert.Equal(SprintStateTypes.Scheduled, sprint.GetState().GetSprintState());
			_ = Assert.Throws<Exception>(() => sprint.GetState().PreviousState());
		}


		// * Check if finished sprint starts a Pipeline
		[Fact]
		public void A_ReleaseSprint_Starts_Pipeline_When_Finished()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobAnalyzeCommand("Hey", "analyze the code"), new PipelineJobBuildCommand("Hey", "Biuld the code", true) }, "First");

			SprintFactory sf = new();
			ReleaseSprint sprint = sf.GetReleaseSprint(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers, pipeline);

			//Assert.

			project.AddSprint(sprint);

			//Act
			sprint.GetState().NextState();
			sprint.GetState().NextState();


			//Assert
			Assert.Equal(PipelineJobStatusType.FINISHED, pipeline.GetStatus());

		}

		//•	Zolang de activiteiten van de development pipeline worden uitgevoerd, kan de sprint niet gewijzigd worden.

		[Fact]
		public void IF_Pipeline_Is_Running_Sprint_Cant_Be_Changed()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();
			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobAnalyzeCommand("Hey", "analyze the code"), new PipelineJobBuildCommand("Hey", "Biuld the code", true) }, "First");

			SprintFactory sf = new();
			ReleaseSprint sprint = sf.GetReleaseSprint(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers, pipeline);

			//Assert.

			project.AddSprint(sprint);
			sprint.GetState().NextState();
			sprint.GetState().NextState();

			//Act
			pipeline.SetStatus(PipelineJobStatusType.Running);


			//Assert
			_ = Assert.Throws<Exception>(() => sprint.SetName("Finshed sprint2"));
		}


		[Fact]
		public void IF_Pipeline_Is_Not_Running_Sprint_Can_Be_Changed()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobAnalyzeCommand("Hey", "analyze the code"), new PipelineJobBuildCommand("Hey", "Biuld the code", true) }, "First");

			SprintFactory sf = new();
			ReleaseSprint sprint = sf.GetReleaseSprint(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers, pipeline);

			//Assert.

			project.AddSprint(sprint);
			sprint.GetState().NextState();
			sprint.GetState().NextState();

			//Act
			sprint.SetName("Finshed sprint");

			//Assert
			Assert.Equal("Finshed sprint", sprint.GetName());
		}

		//•	De reviewSprint kan alleen afgesloten worden als een sprint helemaal is afgerond.
		[Fact]
		public void A_ReviewSprint_Can_Only_Be_Closed_When_Sprint_Is_Finished()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			_ = new Pipeline(new List<PipelineJobCommand> { new PipelineJobAnalyzeCommand("Hey", "analyze the code"), new PipelineJobBuildCommand("Hey", "Biuld the code", true) }, "First");

			SprintFactory sf = new();
			ReviewSprint sprint = sf.GetReviewSprint(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

			//Assert.

			project.AddSprint(sprint);
			sprint.GetState().NextState();
			sprint.GetState().NextState();

			//Act

			//Assert

			Review review = new("This is the review", productOwner, sprint);
			sprint.SetReviewItem(review);

			//Act
			Assert.Equal(SprintStateTypes.Finished, sprint.GetState().GetSprintState());
			Assert.True(sprint.GetIsReviewDone());
			Assert.Equal(sprint.GetReview(), review);
		}

		//•	De reviewSprint kan alleen afgesloten worden als een sprint helemaal is afgerond.
		[Fact]
		public void A_ReviewSprint_Cannot_Be_Closed_When_Sprint_Is_Not_Finished()
		{
			// Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			List<Developer> developers = new() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();
			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobAnalyzeCommand("Hey", "analyze the code"), new PipelineJobBuildCommand("Hey", "Biuld the code", true) }, "First");

			SprintFactory sf = new();
			ReviewSprint sprint = sf.GetReviewSprint(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);



			project.AddSprint(sprint);
			sprint.GetState().NextState();

			//Act
			Review review = new("This is the review", productOwner, sprint);

			//Assert
			_ = Assert.Throws<Exception>(() => sprint.SetReviewItem(review));
		}
	}
}
