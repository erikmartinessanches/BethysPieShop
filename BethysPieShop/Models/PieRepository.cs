using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethysPieShop.Models
{
    public class PieRepository:IPieRepository
    {
        private readonly AppDbContext _appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> GetAllPies()
        {
            /*Will check if the Pies collection on the context has already been 
             populated, if not it will load the data from the db. */
            return _appDbContext.Pies;
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.FirstOrDefault(p => p.Id == pieId);
        }
    }
}
