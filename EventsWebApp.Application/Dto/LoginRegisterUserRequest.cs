namespace EventsWebApp.Application.Validators
{
    public record LoginRegisterUserRequest
    {
        public LoginRegisterUserRequest()
        {
        }

        public LoginRegisterUserRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
