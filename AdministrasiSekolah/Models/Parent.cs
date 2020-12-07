using System;
using System.Collections.Generic;

namespace AdministrasiSekolah.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Student = new HashSet<Student>();
        }

        public string NamaIbu { get; set; }
        public string NamaAyah { get; set; }
        public string Pekerjaan { get; set; }
        public int? PendapatanAyah { get; set; }
        public int? PendapatanIbu { get; set; }
        public string Password { get; set; }
        public int? IdUser { get; set; }
        public int IdParent { get; set; }

        public AccountUser IdUserNavigation { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
