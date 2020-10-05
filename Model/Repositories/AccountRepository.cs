using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Enums;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UtilityLibrary.Utils;

namespace Model.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private ILogger _logger;
        public AccountRepository(ClientDbContext clientDbContext, ILogger<AccountRepository> logger):base(clientDbContext)
        {
            _logger = logger;
        }

        public string login(string email , string password)
        {
            string valid = null;
            try
            {
                List<Account> accounts = _clientDbContext.Accounts.ToList<Account>();

                foreach (Account ac in accounts)
                {
                    ac.email = EncryptUtil.DecryptString(ac.email);
                    ac.password = EncryptUtil.DecryptString(ac.password);

                    if (ac.email == email && ac.password == password)
                    {
                        valid = _clientDbContext.AccountTypes.Where(x => x.id == ac.typeId).Select(s => s.type).First();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Warning, ex.Message);
            }
            return valid;
        }


        public bool validateEmailInUse(string email)
        {
            List<string> emails = _clientDbContext.Accounts.Select(x => x.email).ToList();
            foreach(var e in emails)
            {
                if(EncryptUtil.DecryptString(e)== email)
                {
                    return true;
                }
            }

            return false;
        }

        public void createCustomerAccount(Account account)
        {
            account.typeId = (int)AccTypes.customer;
            account.type = GetAccountType(account.typeId);
            account.EncryptModel();
            Create(account);
            _logger.LogInformation("Account successfully created");
        }

        private AccountType GetAccountType(int id)
        {
            return _clientDbContext.AccountTypes.FirstOrDefault(x => x.id == id);
        }

        public int getAccountId(string email)
        {
            int id = -1;
            List<Account> accounts = _clientDbContext.Accounts.ToList<Account>();

            foreach (Account ac in accounts)
            {
                ac.email = EncryptUtil.DecryptString(ac.email);
                if (ac.email == email)
                {
                    id = ac.id;
                }
            }
            return id;
        }

        public List<Account> getAccounts()
        {
            List<Account> accounts = _clientDbContext.Accounts.ToList<Account>();
            foreach (Account ac in accounts)
            {
                ac.DecryptModel();
            }
            return accounts;
        }

        public void DeleteById(int id)
        {
            _clientDbContext.Accounts.RemoveRange(_clientDbContext.Accounts.Where(x => x.id == id));
            _clientDbContext.SaveChanges();
            _logger.LogInformation("Account deleted successfully");
        }

        public void UpdateAccountStatus(int id, bool status)
        {
            Account account = _clientDbContext.Accounts.Where(x => x.id == id).FirstOrDefault();

            if (account == null)
            {
                throw new NullReferenceException();
            }
            account.active = status;
            _clientDbContext.SaveChanges();
            _logger.LogInformation("Account status updated");
        }
    }
}
