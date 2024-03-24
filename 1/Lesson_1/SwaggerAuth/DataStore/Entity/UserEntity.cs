namespace SwaggerAuth.DataStore.Entity
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Login { get; set; }        
        public string Password { get; set; }
        public UserRole RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
