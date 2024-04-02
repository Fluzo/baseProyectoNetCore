namespace BD_Prueba.DTOs
{
    public class LoginResponseDTO
    {
        public int ID_USUARIO { get; set; }
        public string EMAIL { get; set; }
        public int ID_PERFIL { get; set; }

        public LoginResponseDTO()
        {

        }

        public LoginResponseDTO(int idUsuario, int idPerfil)
        {
            ID_USUARIO = idUsuario;
            ID_PERFIL = idPerfil;
        }


    }
}
