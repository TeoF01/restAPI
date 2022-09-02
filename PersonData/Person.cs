namespace RestApiProject.PersonData;

    public class Person
    {   
        public virtual int Id { get; set; }
        
        public virtual string FirstName { get; set; } = string.Empty;
        
        public virtual string LastName { get; set; } = string.Empty;
        
    }
