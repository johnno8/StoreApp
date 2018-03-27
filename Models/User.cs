using System;
using System.Collections.Generic;
using StoreApp.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Address cannot be longer than 150 characters.")]
        public string Address { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Nationality cannot be longer than 150 characters.")]
        public string Nationality { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}