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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CustomResponseMessage<bool>>
    {
        private readonly Serilog.ILogger _logger;
        private readonly LocalDatabaseContext _context;

        const string DOMAIN = "IOXFleetServicesAPI - CreateUser";

        public CreateUserCommandHandler(
            Serilog.ILogger logger,
            LocalDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CustomResponseMessage<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                var accountExisting = await _context.Accounts
                      .AsNoTracking()
                      .FirstOrDefaultAsync(m => m.AccountNumber == request.AccountNumber);

                if (accountExisting is not null)
                {
                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.BadRequest,
                        Message = $"Account already exists for: {request.AccountNumber}",
                    };
                }

                var userExisting = await _context.Users
                      .AsNoTracking()
                      .FirstOrDefaultAsync(m => m.IDNumber == request.IDNumber || m.Email == request.Email);

                if (userExisting is not null)
                {
                    _logger.Error($"{DOMAIN} - User already exists for: {request.IDNumber} {request.Email}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.BadRequest,
                        Message = $"User already exists for: {request.IDNumber} {request.Email}",
                    };
                }

                User userModel = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IDNumber = request.IDNumber,
                    Password = request.Password,
                    Email = request.Email,
                };

                Account accountModel = new Account
                {
                    AccountNumber = request.AccountNumber,
                    TotalAmount = 0,
                    User = userModel,
                };

                _context.Accounts.Add(accountModel);

                int status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    _logger.Information($"{DOMAIN} - User created {JsonConvert.SerializeObject(userModel)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.Created,
                        Message = $"User created for: {request.AccountNumber}",
                    };
                }
                else
                {
                    _logger.Error($"{DOMAIN} - User not created {JsonConvert.SerializeObject(userModel)}");

                    return new CustomResponseMessage<bool>()
                    {
                        MessageCode = (int)HttpStatusCode.InternalServerError,
                        Message = $"User not created for: {request.AccountNumber}",
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
