using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain.Data;
using Cofoundry.Domain;
using Cofoundry.Plugins.ErrorLogging.Data;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Plugins.ErrorLogging.Domain
{
    public class SearchErrorSummariesQueryHandler 
        : IAsyncQueryHandler<SearchErrorSummariesQuery, PagedQueryResult<ErrorSummary>>
        , IPermissionRestrictedQueryHandler<SearchErrorSummariesQuery, PagedQueryResult<ErrorSummary>>
    {
        #region constructor

        private readonly ErrorLoggingDbContext _dbContext;
        private readonly IQueryExecutor _queryExecutor;

        public SearchErrorSummariesQueryHandler(
            ErrorLoggingDbContext dbContext,
            IQueryExecutor queryExecutor
            )
        {
            _dbContext = dbContext;
            _queryExecutor = queryExecutor;
        }

        #endregion

        #region execution


        public async Task<PagedQueryResult<ErrorSummary>> ExecuteAsync(SearchErrorSummariesQuery query, IExecutionContext executionContext)
        {
            var result = await CreateQuery(query).ToPagedResultAsync(query);

            return result;
        }

        #endregion

        #region helpers

        private IQueryable<ErrorSummary> CreateQuery(SearchErrorSummariesQuery query)
        {
            var dbQuery = _dbContext
                .Errors
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.Text))
            {
                dbQuery = dbQuery.Where(u => 
                    u.Url.Contains(query.Text)
                    || u.UserAgent.Contains(query.Text)
                    || u.ExceptionType.Contains(query.Text)
                    );
            }

            return dbQuery
                .OrderByDescending(u => u.CreateDate)
                .ProjectTo<ErrorSummary>();
        }


        #endregion

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(SearchErrorSummariesQuery query)
        {
            yield return new ErrorLogReadPermission();
        }

        #endregion
    }
}
