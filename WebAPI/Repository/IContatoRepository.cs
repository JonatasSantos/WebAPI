using WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository
{
    public interface IContatoRepository
    {
        Contato Selecionar(string id);
        IEnumerable<Contato> Listar();
        void Persistir(Contato contato);
        void Atualizar(Contato contato);
        void Excluir(string id);

    }
}
