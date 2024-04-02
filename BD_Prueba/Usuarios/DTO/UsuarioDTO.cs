namespace BD_Prueba.DTOs
{
    public class UsuarioDTO
    {
        public int ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public int ID_PERFIL { get; set; }
        public string PERFIL { get; set; }
        public bool ACTIVO { get; set; }

        //public virtual List<PedidoDTO> Pedidos { get; set; }
        //public virtual List<PerfileDTO> Perfiles { get; set; }
    }
}
