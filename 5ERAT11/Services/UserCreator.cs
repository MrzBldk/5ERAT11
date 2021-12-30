using _5ERAT11.Models;

namespace _5ERAT11.Services
{
    static class UserCreator
    {
        public static string email = TestDataReader.GetSettings().UserSetting.Email;
        public static string password = TestDataReader.GetSettings().UserSetting.Password;
        public static User WithCredentialsFromProperty()
        {
            return new User(email, password);
        }

        public static User WithEmptyEmail()
        {
            return new User("", password);
        }

        public static User WithEmptyPassword()
        {
            return new User(email, "");
        }
    }
}
