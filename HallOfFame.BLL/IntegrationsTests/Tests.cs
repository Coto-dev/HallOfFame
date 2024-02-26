using HallOfFame.BLL.Services;
using HallOfFame.DAL.Data;
using HallOfFame.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using Xunit;

namespace HallOfFame.BLL.IntegrationsTests;


public class Tests {
    private IQueryable<Person> GetTestEntities(){
        return new List<Person>
        {
            new Person { Id = 1, Name = "Alex" , Skills = new List<Skill>() {new Skill {
                    Id = 1,
                    Name = "asp net",
                    Level = 3,
                }
            }},
            new Person { Id = 2, Name = "Ivan" , Skills = new List<Skill>() {new Skill {
                    Id = 2,
                    Name = "ef core",
                    Level = 10,
                }
            }},
            new Person { Id = 3, Name = "Mark" , Skills = new List<Skill>() {new Skill {
                    Id = 3,
                    Name = "mvc",
                    Level = 5,
                }
            }},
        }.AsQueryable();
    }
    
    [Fact]
    public async void GetPersonsAsync_Count_3() {
        //Arrange
        var options = new DbContextOptionsBuilder<BackendDbContext>()
            .Options;
        var dbContextMock = new Mock<BackendDbContext>(options);
        var logger = new Mock<ILogger<PersonService>>();

        var testEntities = GetTestEntities();
        dbContextMock.Setup<DbSet<Person>>(x => x.Persons)
            .ReturnsDbSet(testEntities);

        var service = new PersonService(dbContextMock.Object, logger.Object);
        //Act
        var result = await service.GetPersonsAsync();
        //Assert
        Assert.Equal(3, result.Count);
    }
    [Fact]
    public async void GetPersonsAsync_NameOfPersonWithId3_Mark() {
        //Arrange
        var options = new DbContextOptionsBuilder<BackendDbContext>()
            .Options;
        var dbContextMock = new Mock<BackendDbContext>(options);
        var logger = new Mock<ILogger<PersonService>>();

        var testEntities = GetTestEntities();
        dbContextMock.Setup<DbSet<Person>>(x => x.Persons)
            .ReturnsDbSet(testEntities);

        var service = new PersonService(dbContextMock.Object, logger.Object);
        //Act
        var result = await service.GetPersonsAsync();
        //Assert
        Assert.Equal("Mark", result.FirstOrDefault(r=>r.Id == 3)?.Name);
    }
    
    [Fact]
    public async Task GetPersonAsync_PersonWithId1_NameAlex() {
        //Arrange
        var options = new DbContextOptionsBuilder<BackendDbContext>()
            .Options;
        var dbContextMock = new Mock<BackendDbContext>(options);
        var logger = new Mock<ILogger<PersonService>>();

        var testEntities = GetTestEntities();
        dbContextMock.Setup<DbSet<Person>>(x => x.Persons)
            .ReturnsDbSet(testEntities);

        var service = new PersonService(dbContextMock.Object, logger.Object);
        //Act
        var result = await service.GetPersonsAsync();
        //Assert
        Assert.Equal("Alex", result.FirstOrDefault(r=>r.Id == 1)?.Name);
    }
}