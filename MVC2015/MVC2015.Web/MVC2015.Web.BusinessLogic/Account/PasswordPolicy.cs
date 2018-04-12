using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.BusinessLogic.Account
{

    public interface IPasswordPolicy
    {
        string GeneratePassword();
    }

    public class RandomPassword : IPasswordPolicy
    {
        string[] codeSerial = { "2", "3", "4", "5", "6", "7", "a", "c", "d", "e", "f", "h", "j", "k", "m", "n", "p", "r", "tA", "C", "D", "E", "F", "G", "H", "J", "K", "M", "N", "P", "Q", "R", "U", "V", "W", "X", "Y", "Z" };

        public string GeneratePassword()
        {
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            string Password = string.Empty;
            for (int i = 0; i <= 6; i++)
            {
                Password += codeSerial[rand.Next(0, codeSerial.Length - 1)];
            }

            return Password;
        }

        public static string GeneratePassword(int length, List<string> codeSerial)
        {
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            string Password = string.Empty;
            for (int i = 0; i <= length; i++)
            {
                Password += codeSerial[rand.Next(0, codeSerial.Count - 1)];
            }

            return Password;
        }
    }

    //public class RulePassword : IPasswordPolicy
    //{
    //    VM.CompanyPasswordPolicy.CompanyPasswordPolicyModel PasswordPolicy;

    //    public RulePassword(VM.CompanyPasswordPolicy.CompanyPasswordPolicyModel passwordPolicy)
    //    {
    //        PasswordPolicy = passwordPolicy;
    //    }

    //    public string GeneratePassword()
    //    {
    //        List<string> BlankSerial = new List<string> { " " };
    //        List<string> SpecialSerial = new List<string> { "@", "#", "$", "*", "-", "+", "=" };
    //        List<string> NumberSerial = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9" };
    //        List<string> LetterSerial = new List<string> { "a", "c", "d", "e", "f", "h", "j", "k", "m", "n", "p", "r", "g", "b", "u", "v", "w", "x", "y", "z", "A", "C", "D", "E", "F", "B", "G", "H", "J", "K", "M", "N", "P", "Q", "R", "U", "V", "W", "X", "Y", "Z" };
    //        List<string> AllChar = new List<string>();

    //        AllChar.AddRange(LetterSerial);
    //        AllChar.AddRange(SpecialSerial);
    //        AllChar.AddRange(NumberSerial);
    //        //AllChar.AddRange(BlankSerial);

    //        string Password = string.Empty;
    //        int Length = 0;

    //        if (PasswordPolicy != null)
    //        {
    //            if (PasswordPolicy.MinEnLetterLength.HasValue && PasswordPolicy.MinEnLetterLength.Value > 0)
    //            {
    //                Password = RandomPassword.GeneratePassword(PasswordPolicy.MinEnLetterLength.Value, LetterSerial);
    //                Length += PasswordPolicy.MinEnLetterLength.Value;
    //            }
    //            //if (PasswordPolicy.NoBlankSpace)
    //            //{
    //            //    Password += BlankSerial.First();
    //            //    Length++;
    //            //}
    //            if (PasswordPolicy.NeedSpecialChar)
    //            {
    //                Password += RandomPassword.GeneratePassword(1, SpecialSerial);
    //                Length++;
    //            }
    //            if (PasswordPolicy.NeedNumber)
    //            {
    //                Password += RandomPassword.GeneratePassword(1, NumberSerial);
    //                Length++;
    //            }
    //            if (Length < PasswordPolicy.MinLength)
    //            {
    //                int LeftLength = 0;

    //                if (PasswordPolicy.MaxLength.HasValue)
    //                {
    //                    LeftLength = PasswordPolicy.MaxLength.Value - Length;
    //                }
    //                else
    //                {
    //                    LeftLength = PasswordPolicy.MinLength - Length + 6;
    //                }

    //                Password += RandomPassword.GeneratePassword(LeftLength, AllChar);
    //            }
    //        }

    //        if (string.IsNullOrWhiteSpace(Password))
    //        {
    //            Password = RandomPassword.GeneratePassword(8, AllChar);
    //        }

    //        return Password;
    //    }

    //}
}
