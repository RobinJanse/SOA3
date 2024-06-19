using Domain.Backlogs.Enums;
using Domain.Developers.Enums;
using Domain.Notifications;
using Domain.SCM;
using Domain.Sprints;

namespace DomainTests
{
	public class NotificatorTests
	{
		//FR_N1 Het systeem moet notificaties kunnen sturen naar developers bij verschillende sprintfasen.


		//•	Het systeem moet een bericht sturen naar developers die berichten willen ontvangen van een fase verandering
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

			Notificator notificator = new(developer1);
			backlogItem.Register(notificator);

			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();
			backlogItem.GetState().NextState();

			//Assert
			Assert.Equal(BacklogStateType.done, backlogItem.GetStateType());
			Assert.True(notificator.GetMessagesSend() > 0);
		}

		//•	Het systeem stuurt een bericht naar developers wanneer er code aan een branch wordt gecommit.
		[Fact]
		public void A_Developers_Should_Recieve_Notification_When_Code_Is_Pushed()
		{
			//Arrange
			Developer developer1 = new("Hans", RoleType.Developer);
			Project project = new(new Developer("Zjwan", RoleType.LeadDeveloper), "test");

			Branch branch = new("test");

			//Act
			project.GetRepository().AddBranch(branch);
			Notificator notificator = new(developer1);


			branch.Register(notificator);

			branch.PushCommit(new Commit(developer1, new Code("New code")));



			//Assert
			Assert.Equal(1, notificator.GetMessagesSend());
		}
	}
}
