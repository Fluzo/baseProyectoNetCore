namespace BD_Prueba.DTOs
{
    public class RespuestaAutenticacion
    {
        public int ID_USUARIO { get; set; }
        public int ID_PERFIL { get; set; }
        public string TOKEN { get; set; }
        public string EMAIL { get; set; }
        public DateTime EXPIRACION { get; set; }
    }
}
