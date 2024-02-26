using HallOfFame.Common.DataTransferObjects;
using HallOfFame.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.Controllers;
[ApiController]
[Route("api/v1/persons")]
public class PersonController: ControllerBase {
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService) {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PersonDto>>> GetPersonsAsync() {
        return await _personService.GetPersonsAsync();
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<PersonDto>> GetPersonByIdAsync(long id) {
        return await _personService.GetPersonAsync(id);
    }
    
    [HttpPost]
    public async Task<ActionResult<PersonDto>> CreatePersonAsync(PersonCreateEditDto model) {
         await _personService.CreatePerson(model);
         return Ok();
    }
    
    [HttpPut]
    public async Task<ActionResult<PersonDto>> EditPersonAsync(PersonCreateEditDto model, long id) {
         await _personService.EditPersonAsync(model, id);
         return Ok();
    }
    
    [HttpDelete]
    public async Task<ActionResult<PersonDto>> DeletePerson(long id) {
         await _personService.DeletePersonAsync(id);
         return Ok();
    }

}