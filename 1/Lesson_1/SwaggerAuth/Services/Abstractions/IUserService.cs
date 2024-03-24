using SwaggerAuth.DataStore.Entity;

namespace SwaggerAuth.Services.Abstractions
{
    public interface IUserService
    {
        public Guid UserAdd(string name, string password, UserRole roleType);
        public string UserCheck(string name, string password);
    }
}
