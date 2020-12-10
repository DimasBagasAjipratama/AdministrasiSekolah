using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdministrasiSekolah.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Student = new HashSet<Student>();
        }

        [Required(ErrorMessage = "Nama Ibu tidak boleh kosong")]
        public string NamaIbu { get; set; }
        [Required(ErrorMessage = "Nama Ayah tidak boleh kosong")]
        public string NamaAyah { get; set; }
        [Required(ErrorMessage = "Pekerjaan tidak boleh kosong")]
        public string Pekerjaan { get; set; }
        [Required(ErrorMessage = "Pendapan Ayah tidak boleh kosong")]
        public int? PendapatanAyah { get; set; }
        [Required(ErrorMessage = "Pendapatan Ibu tidak boleh kosong")]
        public int? PendapatanIbu { get; set; }
        [Required(ErrorMessage = "Password tidak boleh kosong")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Id User tidak boleh kosong")]
        public int? IdUser { get; set; }
        [Required(ErrorMessage = "Id Parent tidak boleh kosong")]
        public int IdParent { get; set; }

        public AccountUser IdUserNavigation { get; set; }
        public ICollection<Student> Student { get; set; }
    }
}
