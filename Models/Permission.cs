using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System;
using StoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters.")]
        public string Title { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Description cannot be longer than 150 characters.")]
        public string Description { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}