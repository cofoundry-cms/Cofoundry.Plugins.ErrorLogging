using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using AutoMapper.QueryableExtensions;
using Cofoundry.Domain;
using Cofoundry.Plugins.ErrorLogging.Data;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Plugins.ErrorLogging.Domain
{
    public class GetErrorDetailsByIdQueryHandler 
        : IAsyncQueryHandler<GetByIdQuery<ErrorDetails>, ErrorDetails>
        , IPermissionRestrictedQueryHandler<GetByIdQuery<ErrorDetails>, ErrorDetails>
    {
        private readonly ErrorLoggingDbContext _dbContext;

        public GetErrorDetailsByIdQueryHandler(
            ErrorLoggingDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorDetails> ExecuteAsync(GetByIdQuery<ErrorDetails> query, IExecutionContext executionContext)
        {
            var error = await _dbContext
                .Errors
                .AsNoTracking()
                .Where(u => u.ErrorId == query.Id)
                .ProjectTo<ErrorDetails>()
                .SingleOrDefaultAsync();

            return error;
        }

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(GetByIdQuery<ErrorDetails> query)
        {
            yield return new ErrorLogReadPermission();
        }

        #endregion
    }
}
