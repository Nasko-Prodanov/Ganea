using System.Text.RegularExpressions;


namespace Application.Common.Validators
{
    public static class UserValidator
    {
        public const string EmailRegexStrict =
                     @"^(?=.{1,254}$)(?=.{1,64}@)(?!\.)(?!.*\.\.)[A-Za-z0-9._%+\-]+(?<!\.)@" +
        @"(?:[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,63}$";

        private static readonly Regex EmailRegex = new(EmailRegexStrict, RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);


        public static void EmailRegexValidator(string? email)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new ArgumentException("Invalid email format.", nameof(email));
            }
        }

        public static void UserEmailValidator(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            }
        }

        public static void EmailDuplicateValidator(string? email, bool exist)
        {
            if (exist != false)
            {
                throw new ArgumentException("This email was already registered.");
            }
        }

        public static void UserNameValidator(string? userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be empty.", nameof(userName));
            }
        }

        public static void UserNameDuplicateValidator(string userName,bool exist)
        {
            if (exist != false)
            {
                throw new ArgumentException("This user name was already registered.");
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
