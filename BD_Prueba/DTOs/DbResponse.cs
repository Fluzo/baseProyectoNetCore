namespace BD_Prueba.DTOs
{
    public class DbResponse<T>
    {
        public T Data { get; set; }
        public string Mensaje { get; set; }
        public int Retcode { get; set; }
        public DbResponse(T data)
        {
            this.Data = data;
        }

    }
}
