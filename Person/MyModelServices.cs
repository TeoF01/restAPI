namespace RestApiProject.Person;

public class MyModelServices
{
    public static MyModel CreatePerson(int id, string name, string lastName)
    {
        MyModel newPerson = new MyModel()
        {
            Id = id,
            FirstName = name,
            LastName = lastName
        };
        return newPerson;
    }

    public static bool DeletePerson(List<MyModel> source, int id)
    {
        foreach (MyModel person in source)
        {
            if (person.Id.Equals(id))
            {
                source.Remove(person);
                return true;
            }
        }

        return false;
    }

    public static MyModel UpdatePerson(List<MyModel> source, int id, string name, string lastName)
    {
        MyModel updatePerson = new MyModel();
        foreach (MyModel person in source)
        {
            if (person.Id.Equals(id))
            {
                person.FirstName = name;
                person.LastName = lastName;
                updatePerson = person;
                break;
            }
        }
        return updatePerson;
    }
}