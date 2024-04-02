using System.ComponentModel.DataAnnotations;

namespace BD_Prueba.Entidades
{
    public partial class Tokens
    {
        [Key]
        public int ID_TOKEN { get; set; }
        public int ID_USUARIO { get; set; }

        [Required]
        [StringLength(250)]
        public string AUTH_TOKEN { get; set; }
        public DateTime ISSUED_ON { get; set; }
        public DateTime EXPIRES_ON { get; set; }

    }
}
