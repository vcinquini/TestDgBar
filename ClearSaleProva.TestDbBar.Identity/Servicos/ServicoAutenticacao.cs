using ClearSaleProva.TestDgBar.Dominio;
using ClearSaleProva.TestDgBar.Identity.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClearSaleProva.TestDgBar.Identity.Servicos
{
	public class ServicoAutenticacao : IServicoAutenticacao
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes(Settings.Secret);

        public AutenticacaoResultViewModel Authenticate(string usuario, string senha)
        {
            if (usuario == "admin" && senha == "1234")
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, usuario)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
                string jwtBearerToken = jwtSecurityTokenHandler.WriteToken(token);

                return Authorized(jwtBearerToken);
            }

            return Unauthorized();
        }

        public AutenticacaoResultViewModel Authorized(string accessToken)
        {
            return new AutenticacaoResultViewModel
            {
                ResultadoAutenticacao = StatusAutenticacao.Autorizado,
                AccessToken = accessToken
            };
        }

        private AutenticacaoResultViewModel Unauthorized()
        {
            return new AutenticacaoResultViewModel
            {
                ResultadoAutenticacao = StatusAutenticacao.NaoAutorizado
            };
        }
    }
}
