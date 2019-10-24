using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Model;
using curso_api.Repository.Interfaces;
using curso_api.Security;

namespace curso_api.Business
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly IUserRepository _repository;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfiguration;

        public LoginBusiness(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfiguration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }

        public async Task<object> Login(User user)
        {
            bool credentialsIsValid = false;
            var baseUser = await _repository.FindByLogin(user.Username);
            credentialsIsValid = (baseUser != null && baseUser.Password == user.Password);

            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Username, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
                    }
                );

                DateTime creationDate = DateTime.Now;
                DateTime expirationDate = creationDate + TimeSpan.FromMinutes(_tokenConfiguration.Minutes);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, creationDate, expirationDate, handler);
                return SuccessObject(creationDate, expirationDate, token);
            }
                return ExceptionObject();
        }

        private string CreateToken(ClaimsIdentity identity, DateTime creationDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var secutiryToken = handler.CreateToken
            (
                new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Issuer = _tokenConfiguration.Issuer,
                    Audience = _tokenConfiguration.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = creationDate,
                    Expires = expirationDate
                }
            );
            var token = handler.WriteToken(secutiryToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                authenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SuccessObject(DateTime creationDate, DateTime expirationDate, string token)
        {
            return new
            {
                authenticated = true,
                created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "Ok"
            };
        }

        public async Task<object> Register(User user) 
        {
            var baseUser = await _repository.FindByLogin(user.Username);
            if (baseUser != null)
            {
                return new { message = "Username already registered" };
            }
            try
            {
                await _repository.InsertAsync(user);
                return await Login(user);
            }
            catch (Exception ex)
            {
                return new { message = ex.Message };
            }
        }
    }
}