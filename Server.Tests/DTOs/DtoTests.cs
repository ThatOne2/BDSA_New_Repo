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
    public async Task CreateProjectDTO_ToString_Not_Equal()
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
    public async Task ProjectDescDTO_ToString_Not_Equal()
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
    public async Task ProjectPreviewDTO_ToString_Not_Equal()
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
    public async Task CreateSupervisorDTO_ToString_Not_Equal()
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
    public async Task SupervisorDescDTO_ToString_Not_Equal()
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

    [Fact]
    public async Task SupervisorPreviewDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new SupervisorPreviewDTO
        {
            name = "Name",
            Email = "Fake@mail.com"
        };
        var d2 = new SupervisorPreviewDTO
        {
            name = "Different name",
            Email = "Mail@fake.com"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }


    // Student =======================================================

    [Fact]
    public async Task CreateStudentDTO_ToString_Not_Equal()
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
    public async Task StudentDescDTO_ToString_Not_Equal()
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

    [Fact]
    public async Task StudentPreviewDTO_ToString_Not_Equal()
    {
        //Arrange
        var d1 = new StudentPreviewDTO
        {
            name = "Name"
        };
        var d2 = new StudentPreviewDTO
        {
            name = "Different name"
        };

        //Assert
        Assert.NotEqual(d1.ToString(), d2.ToString());
    }


}