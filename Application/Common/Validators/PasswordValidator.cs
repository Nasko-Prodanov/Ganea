namespace Application.Common.Validators
{
    public static class PasswordValidator
    {

        public static void PasswordEmptyValidator(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password cannot be empty.");
            }        
        }

        public static void PasswordLengthValidator(string? password)
        {
            if (password.Length < 8)
            {
                throw new Exception("Password must be at least 8 characters long.");
            }
        }
        public static void PasswordMaxLengthValidator(string? password)
        {
            if (password.Length > 50)
            {
                throw new Exception("Password must be less than 50 characters long.");
            }
        }
    }
}
