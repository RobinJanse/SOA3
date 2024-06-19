using Domain.Backlogs.Enums;
using Domain.Developers.Enums;
using Domain.Sprints;

namespace DomainTests
{
	public class BackLogStateTests
	{
		//FR_B3 Het systeem moet de verschillende fasen van een backlog item ondersteunen, zoals 'todo', 'doing', 'ready for testing', 'testing', 'tested' en 'done'. 

		//•	Een backlog item moet de status 'todo' krijgen wanneer deze aan een sprintbacklog wordt toegevoegd.
		[Fact]
		public void A_BacklogItem_Gets_Status_Todo_When_Added_To_A_Sprint()
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
			Assert.Equal(BacklogStateType.todo, backlogItem.GetStateType());
		}


		//cant go previous state from todo state
		[Fact]
		public void A_BacklogItem_Cant_Go_To_Previous_State_From_Todo_State()
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
			sprint.AddToSprintBacklog(backlogItem);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);

			//Assert
			Assert.Equal(BacklogStateType.todo, backlogItem.GetStateType());
			_ = Assert.Throws<Exception>(() => backlogItem.GetState().PreviousState());
		}

		//•	Backlogitems van fase veranderen in de volgorde todo, doing, readyfortesting, testing, tested, done.

		[Fact]
		public void A_BacklogItem_Cannot_Change_State_When_Not_Added_To_Sprint()
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


			//Assert
			Assert.Equal(BacklogStateType.todo, backlogItem.GetStateType());
			_ = Assert.Throws<Exception>(() => backlogItem.GetState().NextState());
		}

		//•	Backlogitems van fase veranderen in de volgorde todo, doing, readyfortesting, testing, tested, done.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_Todo_To_Doing()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();

			//Assert
			Assert.Equal(BacklogStateType.doing, backlogItem.GetStateType());

		}

		//•	Wanneer er geen developer is geassigned kan de backlogitem niet naar de doing fase.
		[Fact]
		public void A_BacklogItem_Cannot_Change_State_From_Doing_To_ReadyForTesting_When_NoDeveloperAssigned()
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
			sprint.AddToSprintBacklog(backlogItem);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);

			//Assert
			_ = Assert.Throws<Exception>(() => backlogItem.GetState().NextState());

		}

		//•	Backlogitems van fase veranderen in de volgorde todo, doing, readyfortesting, testing, tested, done.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_Doing_To_ReadyForTesting()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();

			//Assert
			Assert.Equal(BacklogStateType.readyfortesting, backlogItem.GetStateType());

		}

		//•	Backlogitems van fase veranderen in de volgorde todo, doing, readyfortesting, testing, tested, done.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_ReadyForTesting_To_Testing()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);
			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();

			//Assert
			Assert.Equal(BacklogStateType.testing, backlogItem.GetStateType());

		}

		//•	Backlogitems van fase veranderen in de volgorde todo, doing, readyfortesting, testing, tested, done.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_Testing_To_Tested()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();

			//Assert
			Assert.Equal(BacklogStateType.tested, backlogItem.GetStateType());

		}

		//•	Backlogitems van fase veranderen in de volgorde todo, doing, readyfortesting, testing, tested, done.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_Tested_To_Done()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();

			//Assert
			Assert.Equal(BacklogStateType.done, backlogItem.GetStateType());
		}

		//•	Wanneer een item in readyfortesting toch niet af is gaat deze terug naar de todo fase
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_ReadyForTesting_To_Todo_When_Not_Finished()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().PreviousState();

			//Assert
			Assert.Equal(BacklogStateType.todo, backlogItem.GetStateType());
		}

		//•	Wanneer een item in de tested fase niet voldoet aan de DOD gaat hij terug naar de ready for testing fase.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_Tested_To_ReadyForTesting_When_Not_Done()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().PreviousState();

			//Assert
			Assert.Equal(BacklogStateType.readyfortesting, backlogItem.GetStateType());
		}


		//•	Een item kan niet naar de volgende fase wanneer de onderliggende activiteiten niet done zijn.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_Tested_To_ReadyForTesting_When_Not_All_Activities_Done()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);

			Activity activity1 = new("Activity 1");
			Activity activity2 = new("Activity 2");

			backlogItem.AddActivity(activity1);
			backlogItem.AddActivity(activity2);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();


			//Assert
			Assert.Equal(ActivityStatus.Todo, activity1.GetStatus());
			_ = Assert.Throws<Exception>(() => backlogItem.GetState().NextState());
		}


		//•	Een item kan niet naar de volgende fase wanneer de onderliggende activiteiten niet done zijn.
		[Fact]
		public void A_BacklogItem_Can_Change_State_From_Tested_To_ReadyForTesting_When_All_Activities_Done()
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
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.AssignDeveloper(developer1);


			Activity activity1 = new("Activity 1");
			Activity activity2 = new("Activity 2");

			backlogItem.AddActivity(activity1);
			backlogItem.AddActivity(activity2);

			//Act
			project.AddSprint(sprint);
			sprint.AddToSprintBacklog(backlogItem);
			backlogItem.GetState().NextState();

			activity1.NextStatus();
			activity1.NextStatus();
			activity2.NextStatus();
			activity2.NextStatus();

			backlogItem.GetState().NextState();

			//Assert
			Assert.Equal(ActivityStatus.Done, activity1.GetStatus());
			Assert.Equal(ActivityStatus.Done, activity2.GetStatus());
			Assert.Equal(BacklogStateType.readyfortesting, backlogItem.GetStateType());
		}
	}
}

