using HallOfFame.Common.DataTransferObjects;
using HallOfFame.Common.Exceptions;
using HallOfFame.Common.Interfaces;
using HallOfFame.DAL.Data;
using HallOfFame.DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HallOfFame.BLL.Services;

public class PersonService : IPersonService {
    private readonly BackendDbContext _dbContext;
    private readonly ILogger<PersonService> _logger;
    public PersonService(BackendDbContext dbContext, ILogger<PersonService> logger) {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<PersonDto>> GetPersonsAsync() {
        var persons = await _dbContext.Persons
            .Include(p => p.Skills)
            .ToListAsync();
        return persons.Select(p => new PersonDto {
            Id = p.Id,
            Name = p.Name,
            DisplayName = p.DisplayName,
            Skills = p.Skills.Select(s=> new SkillDto {
                Id = s.Id,
                Name = s.Name,
                Level = s.Level
            }).ToList()
        }).ToList();
    }

    public async Task<PersonDto> GetPersonAsync(long id) {
        var person = await _dbContext.Persons
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (person == null)
            throw new NotFoundException($"Person with id: {id} not found");
        return new PersonDto() {
            Id = person.Id,
            Name = person.Name,
            DisplayName = person.DisplayName,
            Skills = person.Skills.Select(s => new SkillDto {
                Id = s.Id,
                Name = s.Name,
                Level = s.Level
            }).ToList()
        };
    }

    public async Task CreatePerson(PersonCreateEditDto model) {
        var person = new Person {
            Name = model.Name,
            DisplayName = model.DisplayName,
        };
        var skills = model.Skills
            .Select(s => new Skill {
                Name = s.Name,
                Level = s.Level,
                Person = person
            });
        await _dbContext.AddAsync(person);
        await _dbContext.AddRangeAsync(skills);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Person successfully created");
    }

    public async Task EditPersonAsync(PersonCreateEditDto model, long id) {
        var person = await _dbContext.Persons
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (person == null)
            throw new NotFoundException($"Person with id: {id} not found");
        person.Name = model.Name;
        person.DisplayName = model.DisplayName;
        var skills = model.Skills
            .Select(s => new Skill {
                Name = s.Name,
                Level = s.Level,
                Person = person
            });
        await _dbContext.AddRangeAsync(skills);
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Person successfully edited");
        
    }

    public async Task DeletePersonAsync(long id) {
        var person = await _dbContext.Persons
            .Include(p => p.Skills)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (person == null)
            throw new NotFoundException($"Person with id: {id} not found");
        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Person with id: {id} successfully deleted");

    }
}