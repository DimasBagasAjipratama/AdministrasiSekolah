using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdministrasiSekolah.Models
{
    public partial class Admin
    {
        [Required(ErrorMessage = "Id Admin tidak boleh kosong")]
        public string IdAdmin { get; set; }
        [Required(ErrorMessage = "Nama Admin tidak boleh kosong")]
        public string NamaAdmin { get; set; }
        [Required(ErrorMessage = "Password tidak boleh kosong")]
        public string Password { get; set; }
    }
}
