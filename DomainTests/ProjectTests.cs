using Domain.Developers.Enums;

namespace DomainTests
{
	public class ProjectTests
	{
		//FR_P1 Het systeem moet de mogelijkheid bieden om projecten aan te maken.
		[Fact]
		public void Creating_A_Project_Should_Not_Throw_An_Exception()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";

			//Act
			Project project = new(productOwner, name);

			//Assert
			Assert.NotNull(project);
			Assert.Equal(project.GetName(), name);
			Assert.NotNull(project.GetBacklog());
			Assert.NotNull(project.GetRepository());
			Assert.NotNull(project.GetPipelines());
			Assert.Equal(project.GetProductOwner(), productOwner);

			Assert.Null(project.GetForum());
		}


	}
}
