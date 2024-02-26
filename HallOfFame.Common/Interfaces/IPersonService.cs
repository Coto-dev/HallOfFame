using HallOfFame.Common.DataTransferObjects;

namespace HallOfFame.Common.Interfaces;

public interface IPersonService {
    public Task<List<PersonDto>> GetPersonsAsync();
    public Task<PersonDto> GetPersonAsync(long id);
    public Task CreatePerson(PersonCreateEditDto model);
    public Task EditPersonAsync(PersonCreateEditDto model, long id);
    public Task DeletePersonAsync(long id);
    
}