namespace Application.Common.Validators
{
    public static class UserValidator
    {
        public static void UserEmailValidator(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            }
        }

        public static void UserNameValidator(string? userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be empty.", nameof(userName));
            }
        }

        public static void UserFirstNameValidator(string? firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));
            }
        }

        public static void UserLastNameValidator(string? lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
            }
        }
    }
}
