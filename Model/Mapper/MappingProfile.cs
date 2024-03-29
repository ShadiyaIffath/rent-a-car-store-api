﻿using AutoMapper;
using Model.Entities;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Model.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerDto, Account>()
                .ForMember(x => x.firstName, opts => opts.MapFrom(y => y.firstName))
                .ForMember(x => x.lastName, opts => opts.MapFrom(y => y.lastName))
                .ForMember(x => x.email, opts => opts.MapFrom(y => y.email))
                .ForMember(x => x.phone, opts => opts.MapFrom(y => y.phone))
                .ForMember(x => x.dob, opts => opts.MapFrom(y => y.dob.Date))
                .ForMember(x => x.active, opts => opts.MapFrom(y => y.active))
                .ForMember(x => x.activatedDate, opts => opts.MapFrom(y => y.activatedDate.Date))
                .ForMember(x => x.drivingLicense, opt=> opt.Ignore())
                .ForMember(x => x.additionalIdentification, opt => opt.Ignore())
                .ForMember(x => x.id, opt => opt.Ignore())
                .ForMember(x => x.typeId, opt => opt.Ignore());
            CreateMap<CreateVehicleTypeDto, VehicleType>();
            CreateMap<VehicleType, VehicleTypeDto>();
            CreateMap<VehicleTypeDto, VehicleType>();
            CreateMap<CreateVehicleDto, Vehicle>()
                .ForMember(x => x.dayRemoved, opts => opts.Ignore())
                .ForMember(x => x.type, opts => opts.Ignore())
                .ForMember(x => x.image, opts => opts.Ignore());
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>()
                .ForMember(x => x.image, opts => opts.Ignore());
            CreateMap<VehicleBookingDto, VehicleBooking>();
            CreateMap<VehicleBooking, VehicleBookingDto>();
            CreateMap<EquipmentBookingDto, EquipmentBooking>();
            CreateMap<EquipmentBooking, EquipmentBookingDto>();
            CreateMap<EquipmentCategory, EquipmentCategoryDto>();
            CreateMap<EquipmentCategoryDto, EquipmentCategory>();
            CreateMap<CreateEquipmentDto, Equipment>();
            CreateMap<Equipment, EquipmentDto>();
            CreateMap<EquipmentDto, Equipment>();
            CreateMap<CreateCategoryDto, EquipmentCategory>()
                .ForMember(x => x.image, opts => opts.Ignore());
            CreateMap<CreateEquipmentBookingDto, EquipmentBooking>();
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<UpdateEquipmentBookingDto, EquipmentBooking>();
            CreateMap<UpdateVehicleBookingDto, VehicleBooking>();
            CreateMap<CreateInquiryDto, Inquiry>();
            CreateMap<Inquiry, InquiryDto>();
            CreateMap<DMV, DMVDto>();
            CreateMap<CarRating, CarRatingDto>();
        }
    }
}
