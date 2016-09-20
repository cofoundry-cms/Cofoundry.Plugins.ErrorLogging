using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cofoundry.Plugins.ErrorLogging.Data;
using Cofoundry.Plugins.ErrorLogging.Domain;

namespace Cofoundry.Plugins.ErrorLogging.Bootstrap
{
    public class ErrorAutoMapProfile : Profile
    {
        public ErrorAutoMapProfile()
        {
            CreateMap<Error, ErrorDetails>();
            CreateMap<Error, ErrorSummary>();
        }
    }
}
