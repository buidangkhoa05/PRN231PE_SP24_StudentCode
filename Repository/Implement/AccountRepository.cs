using System;
using BusinessObject;
using DAO;
using Repository.Interface;

namespace Repository.Implement
{
    public class AccountRepository : GenericRepository<BranchAccount>, IAccountRepository
    {

        public async Task<List<BranchAccount>> GetAccounts() => (await AccountDAO.Instance.GetAllAsync()).ToList();

        public async Task AddAccount(BranchAccount branchAccount) => await AccountDAO.Instance.CreateAsync(branchAccount);

        public async Task<BranchAccount?> GetAccountByEmail(string email) => await AccountDAO.Instance.GetAccountByEmail(email);

    }
}

