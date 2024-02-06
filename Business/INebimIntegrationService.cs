using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Dtos;

namespace Business
{
    public interface INebimIntegrationService
    {
      T RunNebimProc<T>(ProcedureRequest model, Expression<Func<T, bool>> successPredicate = null) where T : class, new();
    
    }
}