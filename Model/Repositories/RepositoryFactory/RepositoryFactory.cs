using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Repositories;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories.RepositoryFactory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private ClientDbContext _context;

        public RepositoryFactory(ClientDbContext context)
        {
            _context = context;
            AccountRepository = new AccountRepository(_context);
            CarRatingRepository = new CarRatingRepository(_context);
            DMVRepository = new DMVRepository(_context);
            EquipmentBookingRepository = new EquipmentBookingRepository(_context);
            EquipmentRepository = new EquipmentRepository(_context);
            InquiryRepository = new InquiryRepository(_context);
            VehicleBookingRepository = new VehicleBookingRepository(_context);
            VehicleRepository = new VehicleRepository(_context);
        }

        public IAccountRepository AccountRepository { get; }
        public ICarRatingRepository CarRatingRepository { get; }
        public IDMVRepository DMVRepository { get; }
        public IEquipmentBookingRepository EquipmentBookingRepository { get; }
        public IEquipmentRepository EquipmentRepository { get; }
        public IInquiryRepository InquiryRepository { get; }
        public IVehicleBookingRepository VehicleBookingRepository { get; }
        public IVehicleRepository VehicleRepository { get; }

        public void Dispose()
        {
           _context.Dispose();
        }

    }
}
