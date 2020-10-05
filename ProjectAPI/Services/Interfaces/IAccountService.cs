using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IAccountService
    {
        string AuthenticateUser(LoginDto logincredentials);

        bool RegisterUser(CreateCustomerDto customerDto);

        List<AccountDto> GetAccounts();

        void DeleteAccountById(int id);

        void UpdateAccountStatus(int id, bool status);
    }
}
