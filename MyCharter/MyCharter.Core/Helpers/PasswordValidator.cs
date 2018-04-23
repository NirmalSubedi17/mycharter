using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyCharter.Core.Utils;

namespace MyCharter.Core.Helpers
{
    public class PasswordValidator
    {
        public const String ERROR_IN_LENGTH = "Password must be between 5 and 12 characters long.";
        public const String ERROR_IN_LETTER_AND_DIGIT = "Password must contain both a letter and a digit.";
        public const String ERROR_IN_PASSWORD_SEQUENCE_REPEATED = "Password must not contain any sequence of characters immediately followed by the same sequence.";

        private static Regex regexLetterAndDigit =new Regex("[a-z]+\\d+|\\d+[a-z]+", RegexOptions.IgnoreCase);
        private static Regex regexSequenceRepetition =new Regex("(\\w{2,})\\1", RegexOptions.Compiled);

        private static ObservableDictionary<string,string> passwordErrors = new ObservableDictionary<string,string>();


        public static ObservableDictionary<string, string>ValidateInput(string password)
        {
            passwordErrors.Clear();

            if (password.Length < 5 || password.Length > 12)
                passwordErrors.Add(nameof(ERROR_IN_LENGTH),ERROR_IN_LENGTH);

            if (!regexLetterAndDigit.Match(password).Success)
                passwordErrors.Add(nameof(ERROR_IN_LETTER_AND_DIGIT),ERROR_IN_LETTER_AND_DIGIT);

            if (regexSequenceRepetition.Match(password).Success)
                passwordErrors.Add(nameof(ERROR_IN_PASSWORD_SEQUENCE_REPEATED),ERROR_IN_PASSWORD_SEQUENCE_REPEATED);


            return passwordErrors;
        }

    }
}
