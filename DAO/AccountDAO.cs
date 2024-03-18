using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class AccountDAO : GenericDAO<BranchAccount>
    {  
        private static AccountDAO instance = null;
        public static new AccountDAO Instance => instance ??= new AccountDAO();

        public async Task<BranchAccount?> GetAccountByEmail(string email)
        {
            return await _dbSet.Where(x => x.EmailAddress == email).FirstOrDefaultAsync();
        }
    }
}

