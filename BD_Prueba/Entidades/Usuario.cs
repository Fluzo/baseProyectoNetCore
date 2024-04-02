using System;
using System.Collections.Generic;

namespace BD_Prueba.Entidades
{
    public class Usuario
    {
        //public Usuario()
        //{
        //    Pedidos = new HashSet<Pedido>();
        //    Perfiles = new HashSet<Perfile>();
        //}

        public int ID_USUARIO { get; set; }
        public string USUARIO   { get; set; }
        public string EMAIL     { get; set; }
        public string PASSWORD  { get; set; }
        public int ID_PERFIL  { get; set; }
        public string PERFIL { get; set; }
        public bool ACTIVO { get; set; }



        //public virtual ICollection<Pedido> Pedidos { get; set; }
        //public virtual ICollection<Perfile> Perfiles { get; set; }
    }
}
