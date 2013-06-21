using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace AuthBasicsConsoleApp
{
    public class Program
    {
        static readonly dynamic[] KNOWN_USERS = new dynamic[] { 
            new { Username = "AEinstein", Password = "TensorsAreHard" },
            new { Username = "ENoether", Password = "SymmetryRules"}
        };

        static void Main(string[] args)
        {
            Authenticate(args);
            PrintSensitiveInfo();
        }

        static void Authenticate(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                return;
            }
            else
            {
                var username = args[0];
                var password = args[1];

                if (KNOWN_USERS.Where(x => 
                    x.Username == username &&
                    x.Password == password).Count() == 1)
                {
                    ClaimsIdentity ident = new ClaimsIdentity("Custom");
                    ClaimsPrincipal customPrincipal = new ClaimsPrincipal(ident);
                    Thread.CurrentPrincipal = customPrincipal;
                } // if
            } // if/else
        }

        static void PrintSensitiveInfo()
        {
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                Console.WriteLine("***TOP SECRET***");
                Console.Write("The answer to origin of the universe ");
                Console.WriteLine("and everything else is {0}", 42);
            }
            else
            {
                Console.Write("You are not authenticated and therefore ");
                Console.WriteLine("cannot be shown this top secret information");
            } // if/else
        }
    } // class
} // namespace