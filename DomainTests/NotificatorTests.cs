﻿using Domain.Notifications;
using Domain.SCM;
using Domain.Sprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var productOwner = new Developer("John", Role.Developer);

            var developer1 = new Developer("Hans", Role.Developer);
            var developer2 = new Developer("Jan", Role.Developer);
            var developer3 = new Developer("Hans2", Role.Tester);
            var developers = new List<Developer> { developer1, developer2, developer3, productOwner };

            var name = "Project 1";
            var project = new Project(productOwner, name);
            var backlog = project.GetBacklog();
            var sprint = new ReviewSprint(project, "Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), productOwner, developers);

            var backlogItem = new BacklogItem("BacklogItem 1", "Description 1", 1, backlog);
            sprint.AddToSprintBacklog(backlogItem);
            backlogItem.AssignDeveloper(developer1);

            //Act
            project.AddSprint(sprint);
            sprint.AddToSprintBacklog(backlogItem);

            var notificator = new Notificator(developer1);
            backlogItem.Register(notificator);

            backlogItem.GetState().NextState();
            backlogItem.GetState().NextState();
            backlogItem.GetState().NextState();
            backlogItem.GetState().NextState();
            backlogItem.GetState().NextState();

            //Assert
            Assert.Equal(EBacklogStates.done, backlogItem.GetStateType());
            Assert.True(notificator.GetMessagesSend() > 0);
        }

        //•	Het systeem stuurt een bericht naar developers wanneer er code aan een branch wordt gecommit.
        [Fact]
        public void A_Developers_Should_Recieve_Notification_When_Code_Is_Pushed()
        {
            //Arrange
            var developer1 = new Developer("Hans", Role.Developer);
            var project = new Project(new Developer("Zjwan", Role.LeadDeveloper), "test");

            var branch = new Branch("test");

            //Act
            project.GetRepository().AddBranch(branch);
            var notificator = new Notificator(developer1);


            branch.Register(notificator);

            branch.PushCommit(new Commit(developer1, new Code("New code")));



            //Assert
            Assert.Equal(1, notificator.GetMessagesSend());
        }
    }
}
