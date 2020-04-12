using SampleCrudDapper.Models;
using System.Collections.Generic;

namespace SampleCrudDapper.IRepository
{
    interface IProdutosRepository
    {
        int Add(Produtos produto);
        IEnumerable<Produtos> GetAll();
        Produtos Get(int id);
        int Edit(Produtos produto);
        int Delete(int id);
    }
}
