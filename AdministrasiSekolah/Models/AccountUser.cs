using System;
using System.Collections.Generic;

namespace AdministrasiSekolah.Models
{
    public partial class AccountUser
    {
        public AccountUser()
        {
            Parent = new HashSet<Parent>();
            Student = new HashSet<Student>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Parent> Parent { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
