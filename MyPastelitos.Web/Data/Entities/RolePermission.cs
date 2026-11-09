namespace MyPastelitos.Web.Data.Entities
{
    public class RolePermission
    {
        public required Guid PrivateRecipeId { get; set; }
        public required Guid PermisssionId { get; set; }

        public PrivateRecipeRole? PrivateRecipeRole { get; set; }

        public Permission? Permission { get; set; }







    }
}
