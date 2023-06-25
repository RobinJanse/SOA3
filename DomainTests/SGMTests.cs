using Domain.SCM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTests
{
    public class SGMTests
    {
        //FR_G1 Het systeem moet integratie bieden met bestaande version control systemen zoals Git.

        // •	Het systeem kan een SGM toevoegen aan een project.
        [Fact]
        public void A_Project_Has_A_Repositopry_By_Default()
        {
            //Arrange
            var project = new Project(new Developer("Zjwan", Role.LeadDeveloper), "test");

            

            //Assert
            Assert.Single(project.GetRepository().GetBranches());
        }


        //* Repository should be created with main branch
        [Fact]
        public void A_Repository_Should_Be_Created_With_Main_Branch()
        {
            //Arrange
            var project = new Project(new Developer("Zjwan", Role.LeadDeveloper), "test");


            //Assert
            Assert.Equal("master",project.GetRepository().GetBranches().First().GetName());
        }

        //* Repository should be able to add branches
        [Fact]
        public void A_Repository_Should_Be_Able_To_Add_Branches()
        {
            //Arrange
            var project = new Project(new Developer("Zjwan", Role.LeadDeveloper), "test");
            var branch = new Branch("test");

            //Act
            project.GetRepository().AddBranch(branch);

            //Assert
            Assert.Equal(2, project.GetRepository().GetBranches().Count);
        }

        //•	Er kunnen geen branches met dezelfde naam bestaan.
        [Fact]
        public void A_Repository_Should_Not_Be_Able_To_Add_Branches_With_The_Same_Name()
        {
            //Arrange
            var project = new Project(new Developer("Zjwan", Role.LeadDeveloper), "test");
            var branch = new Branch("master");


            //Assert
            Assert.Throws<Exception>(() => project.GetRepository().AddBranch(branch));
        }

        //•	Er is geen code in een nieuwe branch
        [Fact]
        public void By_Default_No_Code_Is_In_A_Branch()
        {
            //Arrange
            var project = new Project(new Developer("Zjwan", Role.LeadDeveloper), "test");
            var branch = new Branch("test");
            project.GetRepository().AddBranch(branch);

            //Assert
            Assert.Equal("", branch.GetCode().GetCode());
        }

        //* •	Gebruikers kunnen wijzigingen committen naar het repository via het systeem.
        [Fact]
        public void Code_Can_Be_Committed_To_A_Branch()
        {
            //Arrange
            var developer = new Developer("Zjwan", Role.LeadDeveloper);
            var project = new Project(developer, "test");
            var branch = new Branch("test");
            project.GetRepository().AddBranch(branch);

            //Act
            branch.PushCommit(new Commit(developer, new Code("test")));

            //Assert
            Assert.Equal("test", branch.GetCode().GetCode());
        }
    }
}
