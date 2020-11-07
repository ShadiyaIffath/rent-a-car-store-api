using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services.Interfaces
{
    public interface IInquiryService
    {
        public void MakeInquiry(CreateInquiryDto inquiryDto);

        public List<InquiryDto> GetInquiries();

        public void DeleteInquiry(int id);

        public void RespondeToInquiry(InquiryDto dto);
    }
}
