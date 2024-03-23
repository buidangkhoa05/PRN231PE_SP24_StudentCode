using BusinessObject;
using DAO;

namespace Repository.Interface
{
    public interface ISilverJewelryRepository  : IGenericRepository<SilverJewelry>
    {
        Task<List<SilverJewelry>> GetJewelries();
    }
}
