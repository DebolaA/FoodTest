using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QAFoodDb;

namespace QAFoodApi
{
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region fields
        protected readonly IQAFoodDb _dbContext;
        protected readonly ILogger<T> _logger;
        #endregion
        public BaseController(IQAFoodDb dbContext, ILogger<T> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        
    }
}
