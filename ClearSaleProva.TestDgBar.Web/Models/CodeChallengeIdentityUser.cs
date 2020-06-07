using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Models
{
	public class CodeChallengeIdentityUser
	{
		public string AccessToken { get; set; }
	}

	public static class CustomUserClaims
	{
		public static string BearerTokenClaimType = nameof(BearerTokenClaimType);
	}

	public class BearerTokenClaimFactory : UserClaimsPrincipalFactory<CodeChallengeIdentityUser>
	{
		public BearerTokenClaimFactory(UserManager<CodeChallengeIdentityUser> userManager, IOptions<IdentityOptions> options) : base(userManager, options)
		{

		}

		protected override async Task<ClaimsIdentity> GenerateClaimsAsync(CodeChallengeIdentityUser user)
		{
			var identity = await base.GenerateClaimsAsync(user);
			identity.AddClaim(new Claim(CustomUserClaims.BearerTokenClaimType, user.AccessToken));
			return identity;
		}
	}
}
