namespace SwaggerAuth.DataStore.Entity
{
    public class Role
    {
        public UserRole RoleId { get; set; }
        public string Name { get; set; }
        public virtual List<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
