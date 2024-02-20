using AutoMapper;
using DungeonsAndArtificialIntelligenceAPI.BLL.Helpers;
using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Data.Repositories;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
using DungeonsAndArtificialIntelligenceAPI.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DungeonsAndArtificialIntelligenceAPI.BLL
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserToken> _userTokenRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(IRepository<User> userRepository, IRepository<UserToken> userTokenRepository, IMapper mapper, IConfiguration config)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this._userTokenRepository = userTokenRepository ?? throw new ArgumentNullException(nameof(userTokenRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public LoginResponeViewModel Login(LoginBindingModel loginBindingModel)
        {
            var user = this._userRepository.Get(user => user.Email == loginBindingModel.Email);

            if (user == null)
            {
                return new LoginResponeViewModel
                {
                    Status = "Not found"
                };
            }

            if (!Hasher.VerifyPassword(loginBindingModel.Password, user.PasswordHash))
            {
                return new LoginResponeViewModel
                {
                    Status = "Login was unsuccessful"
                };
            }

            return new LoginResponeViewModel
            {
                Status = "Login was successful",
                Token = GenerateToken(user),
                UserName = user.UserName
            };     
        }

        public void Register(RegisterBindingModel registerBinding)
        {
            var user = this._mapper.Map<User>(registerBinding);
            this._userRepository.Add(user);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            this._userTokenRepository.Add(new UserToken
            {
                UserId = user.Id,
                Token = stringToken,
                CreationDate = DateTime.Now
            });

            return stringToken;
        }
    }
}
