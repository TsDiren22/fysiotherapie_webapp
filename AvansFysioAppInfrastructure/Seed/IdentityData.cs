using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AvansFysioAppInfrastructure.Seed
{
    public class IdentityData
    {
        private const string physiotherapistUser = "Physiotherapist";
        private const string emailPhysio = "abc@abc.com";
        private const string physiotherapistPassword = "abcd";

        private const string intern = "Intern";
        private const string emailIntern = "def@def.com";
        private const string internPassword = "abcd";


        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByNameAsync(physiotherapistUser);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = physiotherapistUser,
                    Email = emailPhysio
                };
                await userManager.CreateAsync(user, physiotherapistPassword);
                await userManager.AddClaimAsync(user, new Claim("Physiotherapist", "true"));
            }

            IdentityUser player = await userManager.FindByNameAsync(intern);
            if (player == null)
            {
                player = new IdentityUser
                {
                    UserName = intern,
                    Email = emailIntern
                };
                await userManager.CreateAsync(player, internPassword);
                await userManager.AddClaimAsync(player, new Claim("Intern", "true"));
            }
        }
    }
}
