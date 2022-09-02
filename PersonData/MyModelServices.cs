using System.Net;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Mapping;
using System.Web.Http;


namespace RestApiProject.PersonData;

public class MyModelServices
{
    public static Person CreatePerson(NHibernate.ISession session, int id, string name, string lastName)
    {   
        Person newPerson = new Person()
        {
            Id = id,
            FirstName = name,
            LastName = lastName
        };
        session.Save(newPerson);
        return newPerson;
    }

    public static Person RemovePerson(NHibernate.ISession session, int id)
    {
        Person person = session.Get<Person>(id);
        session.Delete(person);
        return person;
    }

    public static Person UpdatePerson(NHibernate.ISession session, int id, string name, string lastName)
    {
        var person = session.Get<Person>(id);
        if (person is null)
        {
            throw new Exception($"Person with id: {id} not found");
            
        }
        person.FirstName = name;
        person.LastName = lastName;
        session.Update(person);
        session.Save(person);
        return person;
    }

    public static List<Person> GetPerson(NHibernate.ISession session,int? id = null)
    {
        List<Person> personList = new List<Person>(); 
        if (id is not null)
        {
            var person = session.Get<Person>(id);
            if (person is not null)
            {
                personList.Add(person);
                return personList;
            }
        }
        else
        {
            personList = session.CreateCriteria<Person>().List<Person>().ToList();
            return personList;
        }
        throw new HttpResponseException(HttpStatusCode.NotFound);
    }
}