using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        string login(string email, string password);

        bool validateEmailInUse(string email);

        void createCustomerAccount(Account account);

        int getAccountId(string email);
    }
}
