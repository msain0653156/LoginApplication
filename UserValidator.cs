using System;

namespace LoginApplication
{
    public class UserValidator
    {
        public string ValidateLogin(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return "Enter a username and password.";

            if (username.Length > 50 || password.Length > 128)
                return "Username or password is too long.";

            return "";
        }

        public string ValidateNewUser(string username, string password, string confirmPassword)
        {
            string error = ValidateLogin(username, password);

            if (error != "")
                return error;

            if (password.Length < 8)
                return "Password must have at least 8 characters.";

            if (password != confirmPassword)
                return "Passwords do not match.";

            return "";
        }
    }
}
