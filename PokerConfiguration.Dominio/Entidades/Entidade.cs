using PokerConfiguration.Dominio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerConfiguration.Dominio.Entidades
{
    public class Entidade<T> : IEntidade<T>
    {
        public T Id { get; set; }
    }
}
