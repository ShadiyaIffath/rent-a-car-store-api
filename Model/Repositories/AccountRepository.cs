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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UtilityLibrary.Utils;

namespace Model.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ClientDbContext clientDbContext):base(clientDbContext)
        {
        }

        public Account login(string email, string password)
        {
            Account valid = null;
            List<Account> accounts = _clientDbContext.Accounts.Include(a => a.type).ToList<Account>();

            foreach (Account ac in accounts)
            {
                ac.DecryptModel();

                if (ac.email == email && ac.password == password && ac.active)
                {
                    valid = ac;
                    break;
                }
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
                ac.DecryptModel();
                if (ac.email.Equals(email))
                {
                    id = ac.id;
                }
            }
            return id;
        }

        public void UpdatePassword(int id, string password)
        {
            Account ac = _clientDbContext.Accounts.Where(x => x.id == id).FirstOrDefault();
            if (ac == null)
            {
                throw new NullReferenceException();
            }
            ac.password = password;
            _clientDbContext.SaveChanges();
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
            _clientDbContext.VehicleBookings.RemoveRange(_clientDbContext.VehicleBookings.Where(x => x.account.id == id));
            _clientDbContext.EquipmentBookings.RemoveRange(_clientDbContext.EquipmentBookings.Where(x => x.vehicleBooking.account.id == id));
            _clientDbContext.SaveChanges();
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
            if (status == false)
            {
                (from v in _clientDbContext.VehicleBookings where v.account.id == id && (v.status=="Created" || v.status == "Collected") select v).ToList().ForEach(s => s.status = "Cancelled");
                _clientDbContext.SaveChanges();
            }
        }

        public Account GetAccountById(int id)
        {
            return _clientDbContext.Accounts.Where(x => x.id == id).FirstOrDefault();
        }

        public bool CheckIfEmailIsUsed(string email, int id)
        {
            List<string> emails = _clientDbContext.Accounts.Where(y => y.id != id).Select(x => x.email).ToList();
            foreach (var e in emails)
            {
                if (EncryptUtil.DecryptString(e) == email)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
