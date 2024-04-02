namespace BD_Prueba.DTOs
{
    public class FiltroUsuariosDTO
    {
        public string? USUARIO { get; set; }
        public string? EMAIL { get; set; }
        public Nullable<int> ID_PERFIL { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
    }
}
