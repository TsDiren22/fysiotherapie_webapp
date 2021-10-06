using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AvansFysioAppInfrastructure.Seed
{
    public class IdentityData
    {
        private const string physiotherapistUser = "Physiotherapist";
        private const string physiotherapistPassword = "abcd";

        private const string intern = "Intern";
        private const string internPassword = "abcd";


        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(physiotherapistUser);
            if (user == null)
            {
                user = new IdentityUser("Physiotherapist");
                await userManager.CreateAsync(user, physiotherapistPassword);
                await userManager.AddClaimAsync(user, new Claim("Physiotherapist", "true"));
            }

            IdentityUser player = await userManager.FindByIdAsync(intern);
            if (player == null)
            {
                player = new IdentityUser("Intern");
                await userManager.CreateAsync(player, internPassword);
                await userManager.AddClaimAsync(player, new Claim("Intern", "true"));
            }
        }
    }
}
