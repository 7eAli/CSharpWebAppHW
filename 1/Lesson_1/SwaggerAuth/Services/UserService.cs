using Microsoft.IdentityModel.Tokens;
using SwaggerAuth.DataStore.Entity;
using SwaggerAuth.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace SwaggerAuth.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UserService(AppDbContext context, IConfiguration configuration)
        {

            _context = context;
            _configuration = configuration;

        }
        public Guid UserAdd(string name, string password, UserRole roleType)
        {
            var users = new List<UserEntity>();
            using (_context)
            {
                var userExists = _context.Users.Where(x => !x.Login.ToLower().Equals(name.ToLower()));
                UserEntity entity = null;
                if (userExists != null)
                {
                    return default;
                }
                entity = new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Login = name,
                    Password = password,
                    RoleId = roleType
                };
                _context.Users.Add(entity);
                _context.SaveChanges();

                return entity.Id;
            }
        }

        public string UserCheck(string name, string password)
        {
            using (_context)
            {
                var entity = _context.Users.
                    FirstOrDefault(
                    x => x.Login.ToLower().Equals(name.ToLower()) &&
                    x.Password.Equals(password));

                if (entity == null)
                {
                    return "";
                }

                var model = new UserModel
                { 
                    UserName = entity.Login,
                    Password = entity.Password,
                    Role = entity.RoleId
                };

                return GenerateToken(model);
            }
        }

        private string GenerateToken(UserModel model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Role, model.Role.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
