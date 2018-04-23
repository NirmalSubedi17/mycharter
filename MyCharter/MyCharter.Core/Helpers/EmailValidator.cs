using System;
using System.Text.RegularExpressions;

namespace MyCharter.Core.Helpers
{
    public class EmailValidator
    {
        public EmailValidator()
        {
        }

        public static bool IsValid(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",RegexOptions.Compiled);
            var match = regex.Match(email);
            return match.Success;
        }
    }
}
