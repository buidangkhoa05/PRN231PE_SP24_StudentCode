using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class SilverJewelryDAO : GenericDAO<SilverJewelry>
    {
        private static SilverJewelryDAO instance = null;
        public static new SilverJewelryDAO Instance => instance ??= new SilverJewelryDAO();

        public override async Task<List<SilverJewelry>> FindAllAsync()
        {
            return await _dbSet.Include(x => x.Category).ToListAsync();
        }
    }
}
