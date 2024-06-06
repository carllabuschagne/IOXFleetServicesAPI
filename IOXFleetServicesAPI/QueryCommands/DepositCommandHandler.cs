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
    public class DepositCommandHandler : IRequestHandler<DepositCommand, CustomResponseMessage<bool>>
    {
        private readonly Serilog.ILogger _logger;
        private readonly LocalDatabaseContext _context;

        const string DOMAIN = "IOXFleetServicesAPI - Deposit";

        public DepositCommandHandler(
            Serilog.ILogger logger,
            LocalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CustomResponseMessage<bool>> Handle(DepositCommand request, CancellationToken cancellationToken)
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

                //apply amount to Account Total
                account.TotalAmount += request.Amount;

                _context.Accounts.Update(account);

                Transaction transaction = new Transaction
                {
                    Date = DateTime.UtcNow,
                    Type = "CREDIT",
                    Amount = request.Amount,
                };

                _context.Transactions.Add(transaction);

                int status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    _logger.Information($"{DOMAIN} - Deposit created {JsonConvert.SerializeObject(request)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.Created,
                        Message = $"Deposit created for: {request.AccountNumber}",
                    };
                }
                else
                {
                    _logger.Error($"{DOMAIN} - Deposit not created {JsonConvert.SerializeObject(request)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.InternalServerError,
                        Message = $"Deposit not created for: {request.AccountNumber}",
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
