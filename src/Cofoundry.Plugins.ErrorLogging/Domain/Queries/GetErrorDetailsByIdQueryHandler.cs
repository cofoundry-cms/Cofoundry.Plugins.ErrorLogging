using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using Cofoundry.Domain;
using Cofoundry.Plugins.ErrorLogging.Data;

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
