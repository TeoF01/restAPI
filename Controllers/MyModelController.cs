using RestApiProject.DbConnection;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using RestApiProject.PersonData;

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
    
    // GET
    [HttpGet]
    public async Task<ActionResult<List<Person>>> Get(int? id)
    {
        ISessionFactory session = CreateDbConnection.Start();
        var newSession = session.OpenSession();
        {
            using (var tx = newSession.BeginTransaction())
            {
                try
                {
                    var personsList = MyModelServices.GetPerson(newSession, id);
                    newSession.Close();
                    return personsList;
                }
                catch (Exception err)
                {
                    _logger.Log(LogLevel.Error, message: $"{err}");
                    return StatusCode(StatusCodes.Status404NotFound, $"Person with id: {id} not found.");
                }
            }
        }
    }
    
    // POST
    [HttpPost]
    public async Task<ActionResult<Person>> AddPerson(int id, string name, string lastName)
    {
        Person person;
        ISessionFactory session = CreateDbConnection.Start();
        var newSession = session.OpenSession();
        using (var tx = newSession.BeginTransaction())
        {
            try
            {
                person = MyModelServices.CreatePerson(newSession, id, name, lastName);
                newSession.Save(person);
                tx.Commit();
            }
            catch (Exception err)
            {
                _logger.Log(LogLevel.Error, message: $"{err}");
                return StatusCode(400, err);
            }
        }
        return StatusCode(201, person);
    }

    
    //UPDATE
    [HttpPut]
    public async Task<ActionResult<Person>> UpdatePerson(int id, string name, string lastName)
    {
        Person personUpdate = new Person();
        ISessionFactory session = CreateDbConnection.Start();
        var newSession = session.OpenSession();
        using (var tx = newSession.BeginTransaction())
        {
            try
            {
                personUpdate = MyModelServices.UpdatePerson(newSession, id, name, lastName);
                tx.Commit();
                return personUpdate;
            }
            catch (Exception err)
            {
                _logger.Log(LogLevel.Error, $"{err}");
                return StatusCode(StatusCodes.Status404NotFound, $"Person with id: {id} not found");
            }
        }
    }
    
    
    // DELETE
    [HttpDelete]
    [Route("{personId:int?}")]
    public async Task<ActionResult<Person>> DeletePerson(int personId)
    {
        ISessionFactory session = CreateDbConnection.Start();
        var newSession = session.OpenSession();

        using (var tx = newSession.BeginTransaction())
        {
            MyModelServices.RemovePerson(newSession, personId);
            tx.Commit();
            newSession.Close();
        }

        _logger.Log(LogLevel.Error, message: $"Person with id: {personId} not found");
        return StatusCode(404, $"Person with id: {personId} not found");
    }
}