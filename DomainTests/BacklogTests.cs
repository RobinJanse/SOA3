using Domain.Developers.Enums;

namespace DomainTests
{
	public class BacklogTests
	{
		//FR_B1 Het systeem moet een product backlog kunnen aanmaken en bijhouden. 

		//Het systeem moet een nieuwe backlog item kunnen toevoegen.
		[Fact]
		public void A_New_BacklogItem_Can_Be_Created()
		{ //Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();

			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);
			//Act
			backlog.AddBacklogItem(backlogItem1);

			//Assert
			Assert.Contains(backlogItem1, project.GetBacklog().GetBacklogItems());
		}


		//•	Het systeem moet een backlog item kunnen bewerken.
		[Fact]
		public void A_BacklogItem_Can_Be_Edited()
		{ //Arrange
			Developer productOwner = new("John", RoleType.Developer);
			Developer developer = new("Hans", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();

			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);

			//Act
			backlog.AddBacklogItem(backlogItem1);

			backlogItem1.SetName("Task Hello world");
			backlogItem1.SetDescription("Hello world2");
			backlogItem1.SetEffort(10);
			backlogItem1.AssignDeveloper(developer);

			//Assert
			Assert.Contains(backlogItem1, project.GetBacklog().GetBacklogItems());
			Assert.Equal("Task Hello world", backlogItem1.GetName());
			Assert.Equal("Hello world2", backlogItem1.GetDescription());
			Assert.Equal(10, backlogItem1.GetEffort());
			Assert.Equal(developer, backlogItem1.GetAssignedDeveloper());
		}

		//•	Gebruikers moeten de details van een backlog item kunnen bekijken, zoals beschrijving, gebruikersverhalen en bijlagen.
		[Fact]
		public void The_Details_Of_A_BacklogItem_Can_Be_Viewed()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();

			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);

			//Act
			backlog.AddBacklogItem(backlogItem1);

			//Assert
			Assert.Contains(backlogItem1, project.GetBacklog().GetBacklogItems());
			Assert.Equal("task1", backlogItem1.GetName());
			Assert.Equal("Hello world", backlogItem1.GetDescription());
			Assert.Equal(5, backlogItem1.GetEffort());
			Assert.Equal(productOwner, backlogItem1.GetAssignedDeveloper());
		}

		//•	Het systeem moet de mogelijkheid bieden om backlog items te verwijderen.
		[Fact]
		public void A_BacklogItem_Can_Be_Deleted()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();

			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);

			//Act
			backlog.AddBacklogItem(backlogItem1);
			backlog.RemoveBacklogItem(backlogItem1);

			//Assert
			Assert.DoesNotContain(backlogItem1, project.GetBacklog().GetBacklogItems());
		}

		//•	Een backlog mag niet dezelfde backlogitems bevatten.
		[Fact]
		public void A_BacklogItem_Cannot_Be_Added_Twice()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();

			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);

			//Act
			backlog.AddBacklogItem(backlogItem1);

			//Assert
			_ = Assert.Throws<Exception>(() => backlog.AddBacklogItem(backlogItem1));
			_ = Assert.Single(project.GetBacklog().GetBacklogItems());
		}

		//FR_B2 Het systeem moet activiteiten binnen een backlog item kunnen aanmaken.

		//•	Aan een backlog item moet een activiteit toegevoegd kunnen worden.
		[Fact]
		public void An_Activity_Can_Be_Added_To_A_BacklogItem()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();


			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);
			Activity activity1 = new("activity1");
			activity1.SetAssignedDeveloper(productOwner);

			//Act
			backlog.AddBacklogItem(backlogItem1);
			backlogItem1.AddActivity(activity1);

			//Assert
			Assert.Contains(activity1, backlogItem1.GetActivities());
		}

		//•	Een activiteit moet aangepast kunnen worden.
		[Fact]
		public void An_Activity_Can_Be_Edited()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			Developer developer = new("Hans", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();

			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);
			Activity activity1 = new("activity1");
			activity1.SetAssignedDeveloper(productOwner);

			//Act
			backlog.AddBacklogItem(backlogItem1);
			backlogItem1.AddActivity(activity1);

			activity1.SetDescription("Hello world2");
			activity1.SetAssignedDeveloper(developer);

			//Assert
			Assert.Contains(activity1, backlogItem1.GetActivities());
			Assert.Equal("Hello world2", activity1.GetDescription());
			Assert.Equal(developer, activity1.GetAssignedDeveloper());
		}

		//•	Een activiteit moet ingezien kunnen worden.
		[Fact]
		public void An_Activity_Can_Be_Viewed()
		{
			//Arrange
			Developer productOwner = new("John", RoleType.Developer);
			_ = new Developer("Hans", RoleType.Developer);
			string name = "Project 1";
			Project project = new(productOwner, name);
			Backlog backlog = project.GetBacklog();

			BacklogItem backlogItem1 = new("task1", "Hello world", 5, backlog);
			backlogItem1.AssignDeveloper(productOwner);
			Activity activity1 = new("activity1");
			activity1.SetAssignedDeveloper(productOwner);

			//Act
			backlog.AddBacklogItem(backlogItem1);
			backlogItem1.AddActivity(activity1);

			//Assert
			Assert.Contains(activity1, backlogItem1.GetActivities());
			Assert.Equal("activity1", activity1.GetDescription());
			Assert.Equal(productOwner, activity1.GetAssignedDeveloper());
		}


	}
}