namespace MyPastelitos.Web.DTOs.Role
{
    public class RoleDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<PermissionsForRoleDTO>? Permissions { get; set; }
        public string? PermissionsIds { get; set; }

        public List<SectionsForRoleDTO>? Sections { get; set; }
        public string? SectionsIds   { get; set; }

    }
}
