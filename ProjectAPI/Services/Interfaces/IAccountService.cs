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

        void RegisterUser(CreateCustomerDto customerDto);

        List<AccountDto> GetAccounts();

        void DeleteAccountById(int id);

        void UpdateAccountStatus(int id, bool status);

        AccountDto GetAccountById(int id);

        bool UpdateAccount(AccountDto dto);

        string PasswordConfirmation(AccountDto dto);

        void UpdateAccountPassword(int id, string password);

        DashboardCardsView GetCardDetails();

        bool validateEmail(string email);

        bool validateLicense(string id);
    }
}
