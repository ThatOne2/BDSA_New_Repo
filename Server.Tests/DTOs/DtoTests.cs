using Xunit;
using System.Threading.Tasks;
using TrialProject.Shared.DTO;


namespace Server.Tests.DTOs;

public class DtoTests
{
    public DtoTests()
    {

    }

    [Fact]
    public void CreateProjectDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new CreateProjectDTO
        {
            name = "Name",
            shortDescription = "wow",
            longDescription = "Just so long"
        };
        var d2 = new CreateProjectDTO
        {
            name = "Different name",
            shortDescription = "bad",
            longDescription = "Very short"
        };

        //Assert
        Assert.NotEqual(d1.ToString(),d2.ToString());
    }

    [Fact]
    public void ProjectDescDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new ProjectDescDTO
        {
            name = "Name",
            shortDescription = "wow",
            longDescription = "Just so long"
        };
        var d2 = new ProjectDescDTO
        {
            name = "Different name",
            shortDescription = "bad",
            longDescription = "Very short"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }

    [Fact]
    public void ProjectPreviewDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new ProjectPreviewDTO
        {
            name = "Name",
            shortDescription = "wow"
        };
        var d2 = new ProjectPreviewDTO
        {
            name = "Different name",
            shortDescription = "bad"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }

    // Supervisor =======================================================

    [Fact]
    public void CreateSupervisorDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new CreateSupervisorDTO
        {
            name = "Name",
            Email = "Fake@mail.com"
        };
        var d2 = new CreateSupervisorDTO
        {
            name = "Different name",
            Email = "Mail@fake.com"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }

    [Fact]
    public void SupervisorDescDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new SupervisorDescDTO
        {
            name = "Name",
            Email = "Fake@mail.com"
        };
        var d2 = new SupervisorDescDTO
        {
            name = "Different name",
            Email = "Mail@fake.com"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }


    // Student =======================================================

    [Fact]
    public void CreateStudentDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new CreateStudentDTO
        {
            name = "Name",
            Email = "Fake@mail.com"
        };
        var d2 = new CreateStudentDTO
        {
            name = "Different name",
            Email = "Mail@fake.com"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }

    [Fact]
    public void StudentDescDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new StudentDescDTO
        {
            name = "Name",
            Email = "Fake@mail.com"
        };
        var d2 = new StudentDescDTO
        {
            name = "Different name",
            Email = "Mail@fake.com"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }


}