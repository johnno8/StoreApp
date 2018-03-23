using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using StoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        //public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}