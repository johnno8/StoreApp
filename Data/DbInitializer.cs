using StoreApp.Models;
using System;
using System.Linq;

namespace StoreApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreAppContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{Name="Jim Fig",Address="Portlaw, Co. Waterford",Nationality="Irish"},
            new User{Name="Jane Doe",Address="Kilmacow, Co. Kilkenny",Nationality="American"},
            new User{Name="Joe Bloggs",Address="Carrick on Suir, Co. Tipperary",Nationality="British"},
            new User{Name="Ann Other",Address="Ferrybank, Co. Waterford",Nationality="Irish"}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var permissions = new Permission[]
            {
            new Permission{Title="ReadOnly",Description="Restricted to Read Only access"},
            new Permission{Title="Write",Description="Allows Read and Write access"},
            new Permission{Title="Delete",Description="Allows user to Delete files"}
            };
            foreach (Permission p in permissions)
            {
                context.Permissions.Add(p);
            }
            context.SaveChanges();

            // var userpermissions = new UserPermission[]
            // {
            // new UserPermission{UserID=1,PermissionID=2},
            // new UserPermission{UserID=1,PermissionID=3},
            // new UserPermission{UserID=2,PermissionID=1},
            // new UserPermission{UserID=3,PermissionID=2},
            // new UserPermission{UserID=4,PermissionID=2}
            // };
            var userpermissions = new UserPermission[]
            {
                new UserPermission {
                    UserID = users.Single(u => u.Name == "Jim Fig").ID,
                    PermissionID = permissions.Single(p => p.Title == "Write").PermissionID
                },
                new UserPermission {
                    UserID = users.Single(u => u.Name == "Jim Fig").ID,
                    PermissionID = permissions.Single(p => p.Title == "Delete").PermissionID
                },
                new UserPermission {
                    UserID = users.Single(u => u.Name == "Jane Doe").ID,
                    PermissionID = permissions.Single(p => p.Title == "ReadOnly").PermissionID
                },
                new UserPermission {
                    UserID = users.Single(u => u.Name == "Joe Bloggs").ID,
                    PermissionID = permissions.Single(p => p.Title == "Write").PermissionID
                },
                new UserPermission {
                    UserID = users.Single(u => u.Name == "Ann Other").ID,
                    PermissionID = permissions.Single(p => p.Title == "Write").PermissionID
                }
            };
            foreach (UserPermission u in userpermissions)
            {
                context.UserPermissions.Add(u);
            }
            context.SaveChanges();
        }
    }
}