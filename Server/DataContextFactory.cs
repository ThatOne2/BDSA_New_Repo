using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TrialProject.Shared;
using TrialProject.Shared.DTO;

namespace TrialProject.Server;
public class DataContextFactory : IDesignTimeDbContextFactory<Controllers.DataContext>
{
    public Controllers.DataContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("BDSA");

        var optionsBuilder = new DbContextOptionsBuilder<Controllers.DataContext>()
            .UseSqlServer(connectionString);

        return new Controllers.DataContext(optionsBuilder.Options);
    }

    public static void Seed(Controllers.DataContext context)
    {
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE dbo.ProjectTag");
        context.Database.ExecuteSqlRaw("DELETE dbo.Tag");
        context.Database.ExecuteSqlRaw("DELETE dbo.Supervisors");
        context.Database.ExecuteSqlRaw("DELETE dbo.Projects");
        context.Database.ExecuteSqlRaw("DELETE dbo.Students");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Projects', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Supervisors', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Students', RESEED, 0)");


        var Tag1 = new Tag { Name = TagsEnums.Engineering.ToString() };
        var Tag2 = new Tag { Name = TagsEnums.Programming.ToString() };
        var Tag3 = new Tag { Name = TagsEnums.Database.ToString() };
        var Tag4 = new Tag { Name = TagsEnums.Security.ToString() };

        
        var Project1 = new Project {    name = "Photoscanning Thesis", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag1 }, 
                                        shortDescription = "Photoscanning. What is it? I don't know! Help me find out! PLEASE!",
                                        longDescription = "I'm serious. I have no idea! Please help me find out. If you, or someone you know, is familiar with photoscanning, then please come and help me! I'm a desperate professor in need of assistance!"
                                    };
        var Project2 = new Project {    name = "Database Efficiency Study", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag2, Tag3 },
                                        shortDescription = "Come with me and help me see how far we can go with databases!",
                                        longDescription = "In this project idea, I am hoping to work with somebody that is able to help me solve the mysteries behind how fast one can go in database management." 
                                        };
        var Project3 = new Project {    name = "Frontend Research Project", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag1, Tag4 },
                                        shortDescription = "Frontend has been evolving violently - but how does this affect the economy?",
                                        longDescription = "The recent breakthroughs in frontend development, as well as higher salaries, are the reason more and more labourers are quitting their dayjobs to get into coding."
                                    };
        var Project4 = new Project {    name = "RESTful Github Activity", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag3 },
                                        shortDescription = "It's kinda like regular Github, but RESTful",
                                        longDescription = "We're all familiar with Github, and most of us know what a RESTful API is. Now we want to get the best of both worlds."
                                        };

        var Project5 = new Project {    name = "Making a proper GoLang tutorial - the project", 
                                        ProjectStatus = Status.Ongoing, 
                                        Tags = new List<Tag> { Tag4 },
                                        shortDescription = "It bad - we wanna make it better",
                                        longDescription = "We're trying to make a proper tutorial on how to use GoLang. Join us!"};

      
        context.Projects!.AddRange(
            Project1,
            Project2,
            Project3,
            Project4,
            Project5
        );

        context.SaveChanges();

        
        var Supervisor1 = new Supervisor {  isSupervisor = true,
                                            name = "Bjørn Sørensen",
                                            Projects = new List<Project> { Project1 },
                                            Email = "bsørensen@FakeMail.com" };
        var Supervisor2 = new Supervisor {  isSupervisor = true,
                                            name = "Rasmus Rasmussen",
                                            Projects = new List<Project> { Project2 },
                                            Email = "rrasmussen@FakeMail.com" };
        var Supervisor3 = new Supervisor {  isSupervisor = true,
                                            name = "Karl Karlsson",
                                            Projects = new List<Project> { Project3 },
                                            Email = "kkarlsson@FakeMail.com" };
        var Supervisor4 = new Supervisor {  isSupervisor = true,
                                            name = "Niels Nielson",
                                            Projects = new List<Project> { Project4, Project5 },
                                            Email = "nielsnson@FakeMail.com" };

        var Student1 = new Student {  isSupervisor = false,
                                            name = "Jens Jensen",
                                            Email = "jjensen@FakeMail.com" };
        var Student2 = new Student {  isSupervisor = false,
                                            name = "Søren Sørensen",
                                            Email = "ssørensen@FakeMail.com" };
        var Student3 = new Student {  isSupervisor = false,
                                            name = "Lars Larsen",
                                            Email = "llarsen@FakeMail.com" };

          context.Supervisors!.AddRange(
            Supervisor1,
            Supervisor2,
            Supervisor3,
            Supervisor4
        );

        context.Students!.AddRange(
            Student1,
            Student2,
            Student3
        );

         context.SaveChanges();

    }


}
