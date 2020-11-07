using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.Interfaces
{
    public interface IInquiryRepository : IRepositoryBase<Inquiry>
    {
        List<Inquiry> GetInquiries();

        void DeleteById(int id);

        void UpdateInquiry(int id);
    }
}
