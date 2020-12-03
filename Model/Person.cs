namespace Diary.Model
{
    // typ bazowy, nadtyp
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public Address Address { get; set; }

        public Person()
        {
            Address = new Address();
        }

        public abstract string GetInfo();

    }
}
