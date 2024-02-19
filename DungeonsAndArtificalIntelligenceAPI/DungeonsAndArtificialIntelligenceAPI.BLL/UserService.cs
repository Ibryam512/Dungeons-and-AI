using AutoMapper;
using DungeonsAndArtificialIntelligenceAPI.BLL.Helpers;
using DungeonsAndArtificialIntelligenceAPI.BLL.Interfaces;
using DungeonsAndArtificialIntelligenceAPI.Data.Entities;
using DungeonsAndArtificialIntelligenceAPI.Data.Repositories;
using DungeonsAndArtificialIntelligenceAPI.Models.BindingModels;
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

        public string Login(LoginBindingModel loginBindingModel)
        {
            var user = this._userRepository.Get(user => user.Email == loginBindingModel.Email);

            if (user == null)
            {
                return "Not found";
            }

            if (!Hasher.VerifyPassword(loginBindingModel.Password, user.PasswordHash))
            {
                return "Login was unsuccessful";
            }

            return GenerateToken(user);     
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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            string finalToken = new JwtSecurityTokenHandler().WriteToken(token);
            this._userTokenRepository.Add(new UserToken
            {
                UserId = user.Id,
                Token = finalToken,
                CreationDate = DateTime.Now
            });

            return finalToken;
        }
    }
}
