namespace _5ERAT11.Models
{
    public class User
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public User() { }

        public override string ToString()
        {
            return $"User[email = {Email}, password = {Password}]";
        }

        public override bool Equals(object obj)
        {
            return obj is User user && user.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Email.GetHashCode() + Password.GetHashCode();
        }
    }
}
