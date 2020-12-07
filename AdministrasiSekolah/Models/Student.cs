using System;
using System.Collections.Generic;

namespace AdministrasiSekolah.Models
{
    public partial class Student
    {
        public string Nis { get; set; }
        public string NamaStudent { get; set; }
        public string Kelas { get; set; }
        public string Angkatan { get; set; }
        public string Gender { get; set; }
        public string Alamat { get; set; }
        public string Password { get; set; }
        public int? IdUser { get; set; }
        public int? IdParent { get; set; }

        public Parent IdParentNavigation { get; set; }
        public AccountUser IdUserNavigation { get; set; }
    }
}
