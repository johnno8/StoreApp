using System;
using System.Collections.Generic;
using StoreApp.Data;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}