namespace MyPastelitos.Web.Data.Entities
{
    public class RolePermission
    {
        public required Guid PrivateRecipeRoleId { get; set; }
        public required Guid PermissionId { get; set; }

        public PrivateRecipeRole? PrivateRecipeRole { get; set; }

        public Permission? Permission { get; set; }







    }
}
