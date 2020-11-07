using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repositories
{
    public class InquiryRepository : RepositoryBase<Inquiry>, IInquiryRepository
    {
        private ILogger _logger;
        public InquiryRepository(ClientDbContext clientDbContext, ILogger<InquiryRepository> logger) : base(clientDbContext)
        {
            _logger = logger;
        }

        public List<Inquiry> GetInquiries()
        {
            return _clientDbContext.Inquiries.ToList();
        }

        public void DeleteById(int id)
        {
            _clientDbContext.Inquiries.RemoveRange(_clientDbContext.Inquiries.Where(x => x.id == id));
            _clientDbContext.SaveChanges();
        }

        public void UpdateInquiry(int id)
        {
            Inquiry inquiry = _clientDbContext.Inquiries.Where(x => x.id == id).FirstOrDefault();
            inquiry.responded = true;
            _clientDbContext.SaveChanges();
            _logger.LogInformation("Inquiry status has been updated");
        }
    }
}
