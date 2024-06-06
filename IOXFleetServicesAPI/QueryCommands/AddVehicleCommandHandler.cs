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
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, CustomResponseMessage<bool>>
    {
        private readonly Serilog.ILogger _logger;
        private readonly LocalDatabaseContext _context;

        const string DOMAIN = "IOXFleetServicesAPI - AddVehicle";

        public AddVehicleCommandHandler(
            Serilog.ILogger logger,
            LocalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CustomResponseMessage<bool>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                var account = await _context.Accounts
                      .AsNoTracking()
                      .FirstOrDefaultAsync(m => m.AccountNumber == request.AccountNumber);

                if (account is null)
                {
                    _logger.Error($"{DOMAIN} - Account not found for: {request.AccountNumber}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.NotFound,
                        Message = $"Account not found for: {request.AccountNumber}",
                    };
                }

                var vehicle = await _context.Vehicles
                      .AsNoTracking()
                      .FirstOrDefaultAsync(m =>
                      m.PlateNumber == request.PlateNumber ||
                      m.VIN == request.VIN ||
                      m.LicenseNumber == request.LicenseNumber);

                if (vehicle is not null)
                {
                    _logger.Error($"{DOMAIN} - Vehicle exists for: {request.PlateNumber} {request.VIN} {request.LicenseNumber}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.BadRequest,
                        Message = $"Vehicle exists for: {request.PlateNumber} {request.VIN} {request.LicenseNumber}",
                    };
                }


                Vehicle vehicleModel = new Vehicle
                {
                    VIN = request.VIN,
                    LicenseNumber = request.LicenseNumber,
                    PlateNumber = request.PlateNumber,
                    LicenseExpiry = request.LicenseExpiry,
                    Model = request.Model,
                    Color = request.Color,
                };

                account.Vehicles.Add(vehicleModel);

                _context.Accounts.Update(account);

                int status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    _logger.Information($"{DOMAIN} - Vehicle created {JsonConvert.SerializeObject(vehicleModel)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.Created,
                        Message = $"Vehicle created for: {request.AccountNumber}",
                    };
                }
                else
                {
                    _logger.Error($"{DOMAIN} - Vehicle not created {JsonConvert.SerializeObject(vehicleModel)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.InternalServerError,
                        Message = $"Vehicle not created for: {request.AccountNumber}",
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"{DOMAIN} - Error {JsonConvert.SerializeObject(ex.Message)}");

                return new CustomResponseMessage<bool>()
                {
                    MessageCode = (int)HttpStatusCode.InternalServerError,
                    Message = $"Error {JsonConvert.SerializeObject(ex.Message)}",
                };
            }
        }
    }
}
