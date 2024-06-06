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
    public class QuoteCommandHandler : IRequestHandler<QuoteCommand, CustomResponseMessage<bool>>
    {
        private readonly Serilog.ILogger _logger;
        private readonly LocalDatabaseContext _context;

        const string DOMAIN = "IOXFleetServicesAPI - Quote";

        public QuoteCommandHandler(
            Serilog.ILogger logger,
            LocalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CustomResponseMessage<bool>> Handle(QuoteCommand request, CancellationToken cancellationToken)
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
                      .FirstOrDefaultAsync(m => m.PlateNumber == request.PlateNumber);

                if (vehicle is null)
                {
                    _logger.Error($"{DOMAIN} - Vehicle not found for: {request.PlateNumber}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.NotFound,
                        Message = $"Vehicle not found for: {request.AccountNumber}",
                    };
                }

                Quote quoteModel = new Quote
                {
                    Date = DateTime.UtcNow,
                    ValidTo = DateTime.UtcNow.AddYears(1),
                    QuoteNumber = $"{request.PlateNumber}{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}",
                    Description = "Vehicle License Renewal",
                    Amount = 500,
                    Status = "Quoted - not Paid",
                    vehicle = vehicle
                };

                account.Quotes.Add(quoteModel);

                _context.Accounts.Update(account);

                int status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    _logger.Information($"{DOMAIN} - Quote created {JsonConvert.SerializeObject(account)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.Created,
                        Message = $"Quote created for: {request.AccountNumber}",
                    };
                }
                else
                {
                    _logger.Error($"{DOMAIN} - Quote not created {JsonConvert.SerializeObject(account)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.InternalServerError,
                        Message = $"Quote not created for: {request.AccountNumber}",
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
