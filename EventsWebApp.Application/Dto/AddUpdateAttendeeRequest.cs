namespace EventsWebApp.Application.Validators
{
    public record AddUpdateAttendeeRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }

        public AddUpdateAttendeeRequest()
        {

        }
        public AddUpdateAttendeeRequest(string Name, string Surname, string Email, string DateOfBirth)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Email = Email;
            this.DateOfBirth = DateOfBirth;
        }
    }
}
