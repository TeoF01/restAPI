using Microsoft.AspNetCore.Mvc;
using RestApiProject.Person;

namespace RestApiProject.Controllers;

[ApiController]
[Route("[controller]")]

public class MyModelController : ControllerBase
{   
    private readonly ILogger<MyModelController> _logger;

    public MyModelController(ILogger<MyModelController> logger)
    {
        _logger = logger;
    }
    private static List<MyModel> _personList = new List<MyModel>()
    {
        new MyModel() { Id = 1, FirstName = "John", LastName = "Doe" },
        new MyModel() { Id = 2, FirstName = "Aron", LastName = "Dust" },
        new MyModel() { Id = 3, FirstName = "Alonso", LastName = "Lopez" },
    };

    // GET
    [HttpGet]
    public async Task<ActionResult<MyModel>> Get(int? id)
    {
        if (id is not null)
        {
            foreach (MyModel person in _personList)
            {
                if (person.Id == id)
                {
                    return Ok(person);
                }
            }
            _logger.Log(LogLevel.Error, message:$"Person with id {id} not found");
            return StatusCode(404, $"Person with id {id} not found");
        }
        return Ok(_personList);
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<MyModel>> AddPerson(int id, string name, string lastName)
    {
        MyModel person;
        try
        {
            person = MyModelServices.CreatePerson(id, name, lastName);
            _personList.Add(person);
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, message:$"{e}");
            return StatusCode(400, e);
        }
        return StatusCode(201, person);
    }

    // DELETE
    [HttpDelete]
    public async Task<ActionResult<MyModel>> DeletePerson(int id)
    {
        bool isDeleted = MyModelServices.DeletePerson(_personList, id);
        if (isDeleted)
        {
            return StatusCode(200, $"Person with id: {id} has been deleted");
        }
        _logger.Log(LogLevel.Error, message:$"Person with id: {id} not found");
        return StatusCode(404, $"Person with id: {id} not found");
    }
    
    //UPDATE
    [HttpPut]
    public async Task<ActionResult<MyModel>> UpdatePerson(int id, string name, string lastName)
    {
        return MyModelServices.UpdatePerson(_personList, id, name, lastName);
    }
}