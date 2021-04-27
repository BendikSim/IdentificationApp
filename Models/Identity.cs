namespace Models
{
    public class Identity
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }

        public Identity(string id, string firstName, string lastName, string dateOfBirth)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dateOfBirth = dateOfBirth;
        }
    }
}