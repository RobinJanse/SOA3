using Domain.Developers.Enums;
using Domain.Pipelines;
using Domain.Pipelines.PipelineCommands;
using Domain.Reports;
using Domain.Sprints;

namespace DomainTests
{
	public class ReportTests
	{
		//FR_R1 Het systeem moet de mogelijkheid bieden om rapporten te genereren voor elke sprint.
		//•	Gebruikers kunnen rapporten genereren voor specifieke sprints.
		[Fact]
		public void A_Report_Can_Be_Generated_For_A_Sprint()
		{
			//Arrange
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
			Report report = sprint.GenerateReviewReport("conent", "Report name", DateTime.Now, FormatType.PDF);

			//Assert
			Assert.NotNull(report);
		}

		//FR_R2 Het systeem moet de mogelijkheid bieden om headers en footers toe te passen op de gegenereerde rapporten.
		//•	Gebruikers kunnen headers en footers toevoegen aan de rapporten.
		[Fact]
		public void A_Report_Can_Have_A_Header_And_A_Footer()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			ReviewSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner });
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateReviewReport("content", "Report name", DateTime.Now, FormatType.PDF);

			Assert.NotNull(report.Header);
			Assert.NotNull(report.Body);
			Assert.NotNull(report.Footer);
		}



		//•	•	Headers en footers kunnen informatie bevatten zoals bedrijfsnaam/logo, projectnaam, versie en datum
		[Fact]
		public void A_Report_Can_Have_A_Header_And_A_Footer_With_Information()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			ReviewSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner });
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateReviewReport("content", "Report name", DateTime.Now, FormatType.PDF);

			report.Header.CompanyName = "Avans";
			report.Footer.CompanyLogo = "Logo";

			Assert.Equal("Avans", report.Header.CompanyName);
			Assert.Equal("Logo", report.Footer.CompanyLogo);
		}

		//FR_R3 Het systeem moet de mogelijkheid bieden om de gegenereerde rapporten op te slaan in verschillende formaten, zoals pdf en png.
		//•	Gebruikers kunnen rapporten opslaan in verschillende formaten.
		[Fact]
		public void A_Report_Can_Be_Saved_In_PDF_Formats()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			ReviewSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner });
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateReviewReport("content", "Report name", DateTime.Now, FormatType.PDF);

			Assert.Equal(FormatType.PDF, report.Format);
		}

		[Fact]
		public void A_Report_Can_Be_Saved_In_PNG_Formats()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			ReviewSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner });
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateReviewReport("content", "Report name", DateTime.Now, FormatType.PNG);

			Assert.Equal(FormatType.PNG, report.Format);
		}


		[Fact]
		public void A_Report_Can_Be_Saved_In_XML_Formats()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			ReviewSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner });
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateReviewReport("content", "Report name", DateTime.Now, FormatType.XML);

			Assert.Equal(FormatType.XML, report.Format);
		}


		[Fact]
		public void A_ReleaseReport_Can_Be_Generated_For_A_Sprint()
		{
			//Arrange
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);

			Developer developer1 = new("Hans", RoleType.Developer);
			Developer developer2 = new("Jan", RoleType.Developer);
			Developer developer3 = new("Hans2", RoleType.Tester);
			_ = new List<Developer>() { developer1, developer2, developer3, productOwner };

			string name = "Project 1";
			Project project = new(productOwner, name);
			_ = project.GetBacklog();
			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobDeployCommand("test", "test.exe -t") }, "first");
			ReleaseSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner }, pipeline);

			//Act
			project.AddSprint(sprint);
			Report report = sprint.GenerateDeploymentReport("conent", "Report name", DateTime.Now, FormatType.PDF);

			//Assert
			Assert.NotNull(report);
		}

		//FR_R2 Het systeem moet de mogelijkheid bieden om headers en footers toe te passen op de gegenereerde rapporten.
		//•	Gebruikers kunnen headers en footers toevoegen aan de rapporten.
		[Fact]
		public void A_ReleaseReport_Can_Have_A_Header_And_A_Footer()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobDeployCommand("test", "test.exe -t") }, "first");
			ReleaseSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner }, pipeline);
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateDeploymentReport("content", "Report name", DateTime.Now, FormatType.PDF);

			Assert.NotNull(report.Header);
			Assert.NotNull(report.Body);
			Assert.NotNull(report.Footer);
		}



		//•	•	Headers en footers kunnen informatie bevatten zoals bedrijfsnaam/logo, projectnaam, versie en datum
		[Fact]
		public void A_RealeaseReport_Can_Have_A_Header_And_A_Footer_With_Information()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobDeployCommand("test", "test.exe -t") }, "first");
			ReleaseSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner }, pipeline);
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateDeploymentReport("content", "Report name", DateTime.Now, FormatType.PDF);

			report.Header.CompanyName = "Avans";
			report.Footer.CompanyLogo = "Logo";

			Assert.Equal("Avans", report.Header.CompanyName);
			Assert.Equal("Logo", report.Footer.CompanyLogo);
		}

		//FR_R3 Het systeem moet de mogelijkheid bieden om de gegenereerde rapporten op te slaan in verschillende formaten, zoals pdf en png.
		//•	Gebruikers kunnen rapporten opslaan in verschillende formaten.
		[Fact]
		public void A_ReleaseReport_Can_Be_Saved_In_PDF_Formats()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobDeployCommand("test", "test.exe -t") }, "first");
			ReleaseSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner }, pipeline);
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateDeploymentReport("content", "Report name", DateTime.Now, FormatType.PDF);

			Assert.Equal(FormatType.PDF, report.Format);
		}

		[Fact]
		public void A_ReleaseReport_Can_Be_Saved_In_PNG_Formats()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobDeployCommand("test", "test.exe -t") }, "first");
			ReleaseSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner }, pipeline);
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateDeploymentReport("content", "Report name", DateTime.Now, FormatType.PNG);

			Assert.Equal(FormatType.PNG, report.Format);
		}


		[Fact]
		public void A_ReleaseReport_Can_Be_Saved_In_XML_Formats()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);

			Pipeline pipeline = new(new List<PipelineJobCommand> { new PipelineJobDeployCommand("test", "test.exe -t") }, "first");
			ReleaseSprint sprint = new(project, name, DateTime.Now, DateTime.Now.AddDays(14), productOwner, new List<Developer> { productOwner }, pipeline);
			project.AddSprint(sprint);

			//act
			Report report = sprint.GenerateDeploymentReport("content", "Report name", DateTime.Now, FormatType.XML);

			Assert.Equal(FormatType.XML, report.Format);
		}
	}
}
