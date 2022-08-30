using Microsoft.AspNetCore.Mvc;
using RestApiProject.Person;

namespace RestApiProject.Controllers;

[ApiController]
[Route("[controller]")]

public class MyModelController : ControllerBase
{
    private static List<MyModel> personList = new List<MyModel>()
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
            foreach (MyModel person in personList)
            {
                if (person.Id == id)
                {
                    return Ok(person);
                }
            }

            return StatusCode(404, $"Person with id {id} not found");
        }
        return Ok(personList);
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<MyModel>> AddPerson(int id, string name, string lastName)
    {
        MyModel person;
        try
        {
            person = MyModelServices.CreatePerson(id, name, lastName);
            personList.Add(person);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e);
        }
        return StatusCode(201, person);
    }

    // DELETE
    [HttpDelete]
    public async Task<ActionResult<MyModel>> DeletePerson(int id)
    {
        bool isDeleted = MyModelServices.DeletePerson(personList, id);
        if (isDeleted)
        {
            return StatusCode(200, $"Person with id: {id} has been deleted");
        }

        return StatusCode(404, $"Person with id: {id} not found");
    }
}