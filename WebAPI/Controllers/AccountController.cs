using BusinessObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [EnableQuery]
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAccounts()
        {
            List<BranchAccount> brandAccounts = await _accountRepository.GetAll();
            return Ok(brandAccounts);
        }



        [HttpPost]
        public async Task<IActionResult> AddBranchAccount(BranchAccount branchAccount)
        {
            BranchAccount branchAcc = await _accountRepository.GetAccountByEmail(branchAccount.EmailAddress ?? "");
            if (branchAcc != null)
            {
                return BadRequest("Account is already existed");
            }

            await _accountRepository.Create(branchAccount);
            return Ok(branchAccount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBranchAccount(BranchAccount branchAccount)
        {
            BranchAccount branchAcc = await _accountRepository.GetAccountByEmail(branchAccount.EmailAddress ?? "");
            if (branchAcc == null)
            {
                return BadRequest("Account is not existed");
            }

            await _accountRepository.Update(branchAccount);
            return Ok(branchAccount);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBranchAccount(int id)
        {
            BranchAccount branchAcc = await _accountRepository.Get(id);
            if (branchAcc == null)
            {
                return BadRequest("Account is not existed");
            }

            await _accountRepository.Delete(id);
            return Ok("Account is deleted");
        }
    }
}

