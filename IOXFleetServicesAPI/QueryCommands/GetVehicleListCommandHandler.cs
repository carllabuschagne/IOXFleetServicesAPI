using IOXFleetServicesAPI.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IOXFleetServicesAPI.QueryCommands
{
    public class GetVehicleListCommandHandler : IRequestHandler<GetVehicleListCommand, CustomResponseMessage<List<Vehicle>>>
    {
        private readonly Serilog.ILogger _logger;
        private readonly LocalDatabaseContext _context;

        const string DOMAIN = "IOXFleetServicesAPI - GetVehicleList";

        public GetVehicleListCommandHandler(
            Serilog.ILogger logger,
            LocalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CustomResponseMessage<List<Vehicle>>> Handle(GetVehicleListCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var account = await _context.Accounts
                .Include(m => m.Vehicles)
                  .AsNoTracking()
                  .FirstOrDefaultAsync(m => m.AccountNumber == request.AccountNumber);

            if (account is null)
            {
                _logger.Error($"{DOMAIN} - Account not found for: {request.AccountNumber}");

                return new CustomResponseMessage<List<Vehicle>>()
                {
                    MessageCode = (int)HttpStatusCode.NotFound,
                    Message = $"Account not found for: {request.AccountNumber}",
                };
            }

            var vehicleList = await _context.Vehicles
                .Where(m =>
                m.VIN.Contains(request.Filter) ||
                m.LicenseNumber.Contains(request.Filter) ||
                m.PlateNumber.Contains(request.Filter) ||
                m.Model.Contains(request.Filter) ||
                m.Color.Contains(request.Filter))
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new CustomResponseMessage<List<Vehicle>>()
            {
                MessageCode = (int)HttpStatusCode.OK,
                Message = "",
                Data = vehicleList,
            };

        }
    }
}
