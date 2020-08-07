using Microsoft.Extensions.Logging;
using Model.DatabaseContext;
using Model.Entities;
using Model.Repositories.Base;
using Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Repositories
{
    public class BookingRepository : RepositoryBase<VehicleBooking>, IBookingRepository
    {
        private ILogger _logger;

        public BookingRepository(ClientDbContext clientDbContext, ILogger<BookingRepository> logger) : base(clientDbContext)
        {
            _logger = logger;
        }
    }

}
