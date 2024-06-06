using IOXFleetServicesAPI.Helpers;
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
    public class RenewLicenseCommandHandler : IRequestHandler<RenewLicenseCommand, CustomResponseMessage<bool>>
    {
        private readonly Serilog.ILogger _logger;
        private readonly LocalDatabaseContext _context;

        const string DOMAIN = "IOXFleetServicesAPI - RenewLicense";

        public RenewLicenseCommandHandler(
            Serilog.ILogger logger,
            LocalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CustomResponseMessage<bool>> Handle(RenewLicenseCommand request, CancellationToken cancellationToken)
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

                var quote = await _context.Quotes
                    .Include(m => m.vehicle)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.QuoteNumber == request.QuoteNumber && m.Status == "Quoted - not Paid");

                if (quote is null)
                {
                    _logger.Error($"{DOMAIN} - Quote not found for: {request.QuoteNumber}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.NotFound,
                        Message = $"Quote not found for: {request.AccountNumber}",
                    };
                }

                Validations validations = new Validations();

                if (!validations.HasSufficientFundsCheck(account.TotalAmount, quote.Amount))
                {
                    _logger.Error($"{DOMAIN} - Insufficient funds for: {request.QuoteNumber}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.OK,
                        Message = $"Insufficient funds for: {request.AccountNumber}",
                    };
                }

                //Deduct money from Account
                account.TotalAmount -= quote.Amount;

                //Create Transaction record
                Transaction transaction = new Transaction
                {
                    Date = DateTime.UtcNow,
                    Type = "DEBIT",
                    Amount = quote.Amount,
                };

                _context.Transactions.Add(transaction);

                //Set status of Qoute to Paid
                quote.Status = "Paid";

                //Set Expiration date 1 year from now
                quote.vehicle.LicenseExpiry = DateTime.UtcNow.AddYears(1);

                account.Quotes.Add(quote);

                _context.Accounts.Update(account);

                int status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    _logger.Information($"{DOMAIN} - License has been renewed for {JsonConvert.SerializeObject(account)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.Created,
                        Message = $"License has been renewed for: {request.AccountNumber}",
                    };
                }
                else
                {
                    _logger.Error($"{DOMAIN} - License has been renewed for {JsonConvert.SerializeObject(account)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.InternalServerError,
                        Message = $"License has not been renewed for: {request.AccountNumber}",
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
