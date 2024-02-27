using HallOfFame.Common.DataTransferObjects;

namespace HallOfFame.Common.Interfaces;

public interface IPersonService {
    /// <summary>
    /// Get all persons
    /// </summary>
    /// <returns></returns>
    public Task<List<PersonDto>> GetPersonsAsync();
    /// <summary>
    /// Get person by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<PersonDto> GetPersonAsync(long id);
    /// <summary>
    /// Create person
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task CreatePersonAsync(PersonCreateEditDto model);
    /// <summary>
    /// Edit person
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task EditPersonAsync(PersonCreateEditDto model, long id);
    /// <summary>
    /// Delete person
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task DeletePersonAsync(long id);
    
}