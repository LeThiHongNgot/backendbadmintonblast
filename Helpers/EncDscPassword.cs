using System.Text;

namespace BadmintonBlast.Helpers
{
    public class EncDscPassword
    {
        public static string secretKey = "lehongngot17102003";
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            else
            {
                password = password + secretKey;
                var passwordinBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordinBytes);
            }
        }
        public static string DecryptPassword(string encryptedPassword)
        {
            if (string.IsNullOrEmpty(encryptedPassword))
            {
                return "";
            }

            try
            {
                var encodedBytes = Convert.FromBase64String(encryptedPassword);
                var actualPassword = Encoding.UTF8.GetString(encodedBytes);
                actualPassword = actualPassword.Substring(0, actualPassword.Length - secretKey.Length);
                return actualPassword;
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("The provided string is not a valid Base-64 string.", ex);
            }
        }
    }
}
