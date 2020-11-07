using AutoMapper;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.Models;
using Model.Repositories.Interfaces;
using ProjectAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IMapper _mapper;
        private ILogger _logger;
        private readonly IMailService _mailService;
        private IInquiryRepository _inquiryRepository;

        public InquiryService(IMapper mapper, IInquiryRepository inquiryRepository, ILogger<InquiryService> logger, IMailService mailService)
        {
            _mapper = mapper;
            _inquiryRepository = inquiryRepository;
            _logger = logger;
            _mailService = mailService;
        }
        public void MakeInquiry(CreateInquiryDto inquiryDto)
        {
            Inquiry inquiry = _mapper.Map<Inquiry>(inquiryDto);
            _inquiryRepository.Create(inquiry);
            _logger.LogInformation("Inquiry Successfully Created");
        }

        public List<InquiryDto> GetInquiries()
        {
            return _mapper.Map<List<InquiryDto>>(_inquiryRepository.GetInquiries());
        }

        public void DeleteInquiry(int id)
        {
            _inquiryRepository.DeleteById(id);
            _logger.LogInformation("Inquiry Successfully deleted");
        }

        public void RespondeToInquiry(InquiryDto dto)
        {
            _mailService.SendInquiryResponseEmail(dto.email, dto.name, dto.response, dto.inquiry, dto.createdOn.ToString());
            _inquiryRepository.UpdateInquiry(dto.id);
            _logger.LogInformation("Inquiry Response Sent");
        }
    }
}
