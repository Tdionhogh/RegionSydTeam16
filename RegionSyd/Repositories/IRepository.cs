using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();   // Hent alle entiteter
        T GetById(int id);         // Hent en entitet ved ID
        void Add(T entity);        // Tilføj en ny entitet
        void Update(T entity);     // Opdater en eksisterende entitet
        void Delete(int id);       // Slet en entitet ved ID
    }
}
