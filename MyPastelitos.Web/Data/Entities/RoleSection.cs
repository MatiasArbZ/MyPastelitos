namespace MyPastelitos.Web.Data.Entities
{
    public class RoleSection
    {
        public required Guid PrivateRecipeRoleId { get; set; }
        public required Guid SectionId { get; set; }

        public PrivateRecipeRole? PrivateRecipeRole { get; set; }

        public Section? Section { get; set; }







    }
}
