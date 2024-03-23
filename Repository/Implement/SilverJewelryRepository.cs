using BusinessObject;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class SilverJewelryRepository : GenericRepository<SilverJewelry>, ISilverJewelryRepository
    {
           public async Task<List<SilverJewelry>> GetJewelries() => await SilverJewelryDAO.Instance.FindAllAsync();
    }
}
