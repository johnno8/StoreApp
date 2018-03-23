using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreApp.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}