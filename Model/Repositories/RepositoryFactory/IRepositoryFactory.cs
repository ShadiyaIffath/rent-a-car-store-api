using Microsoft.EntityFrameworkCore;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.RepositoryFactory
{
    public interface IRepositoryFactory : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ICarRatingRepository CarRatingRepository { get; }
        IDMVRepository DMVRepository { get; }
        IEquipmentBookingRepository EquipmentBookingRepository { get; }
        IEquipmentRepository EquipmentRepository { get; }
        IInquiryRepository InquiryRepository { get; }
        IVehicleBookingRepository VehicleBookingRepository { get; }
        IVehicleRepository VehicleRepository { get; }
    }
}
