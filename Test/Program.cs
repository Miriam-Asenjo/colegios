using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shared.Security;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var generatedPassword = PasswordHash.CreateHash("mirichip");

            Console.WriteLine("Gernated Password: " + generatedPassword + " Length: " + generatedPassword.Length);

            var generatedPassword2 = PasswordHash.CreateHash("1?ahsdfsjdhfdshfsdgfdsgfdskgf");

            Console.WriteLine("Gernated Password: " + generatedPassword2 + " Length: " + generatedPassword2.Length);

            var generatedPassword3 = PasswordHash.CreateHash("?#agfjd");

            Console.WriteLine("Gernated Password: " + generatedPassword3 + " Length: " + generatedPassword3.Length);


        }
    }
}
