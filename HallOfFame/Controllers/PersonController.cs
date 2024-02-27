using HallOfFame.Common.DataTransferObjects;
using HallOfFame.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.Controllers;
/// <summary>
/// Controller for persons
/// </summary>
[ApiController]
[Route("api/v1/persons")]
public class PersonController: ControllerBase {
    private readonly IPersonService _personService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="personService"></param>
    public PersonController(IPersonService personService) {
        _personService = personService;
    }

    /// <summary>
    /// Get all persons
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetPersonsAsync() {
        return await _personService.GetPersonsAsync();
    }
    
    /// <summary>
    /// Get person by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<PersonDto>> GetPersonByIdAsync(long id) {
        return await _personService.GetPersonAsync(id);
    }
    
    /// <summary>
    /// Create person
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<PersonDto>> CreatePersonAsync(PersonCreateEditDto model) {
         await _personService.CreatePersonAsync(model);
         return Ok();
    }
    
    /// <summary>
    /// Edit person by id
    /// </summary>
    /// <param name="model"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<PersonDto>> EditPersonAsync(PersonCreateEditDto model, long id) {
         await _personService.EditPersonAsync(model, id);
         return Ok();
    }
    
    /// <summary>
    /// Delete person by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<PersonDto>> DeletePersonAsync(long id) {
         await _personService.DeletePersonAsync(id);
         return Ok();
    }

}