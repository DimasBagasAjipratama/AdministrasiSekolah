using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdministrasiSekolah.Models
{
    public partial class Student
    {
        [RegularExpression("^[0-9]*$" , ErrorMessage = "Hanya boleh diisi oleh angka")]
        public string Nis { get; set; }
        [Required(ErrorMessage = "Nama Student tidak boleh kosong")]
        public string NamaStudent { get; set; }
        [Required(ErrorMessage = "Kelas tidak boleh kosong")]
        public string Kelas { get; set; }
        [Required(ErrorMessage = "Angkatan tidak boleh kosong")]
        public string Angkatan { get; set; }
        [Required(ErrorMessage = "Gender tidak boleh kosong")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Alamat tidak boleh kosong")]
        public string Alamat { get; set; }
        [Required(ErrorMessage = "Passwoord tidak boleh kosong")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Id User tidak boleh kosong")]
        public int? IdUser { get; set; }
        [Required(ErrorMessage = "Id Parent tidak boleh kosong")]
        public int? IdParent { get; set; }

        public Parent IdParentNavigation { get; set; }
        public AccountUser IdUserNavigation { get; set; }
    }
}
