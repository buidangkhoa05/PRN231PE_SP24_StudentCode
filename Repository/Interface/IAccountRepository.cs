using System;
using BusinessObject;

namespace Repository.Interface
{
    public interface IAccountRepository :IGenericRepository<BranchAccount>
    {
        Task<BranchAccount> GetAccountByEmail(string email);
    }
}

