using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using OCAS.WebAPI.Models;
using OCAS.DataAccess;
using OCAS.Core.IRepository;
using OCAS.Core.Repository;

namespace OCAS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ActivitiesController> _logger;
        public ActivitiesController(IUnitOfWork unitOfWork, ILogger<ActivitiesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<ActivityDTO> _retActivityList = null;

            try
            {
                var _activities = await _unitOfWork.Activities.GetAll( c => c.IsActive == true);
                _retActivityList = new List<ActivityDTO>();

                foreach (var activityItem in _activities)
                {
                    ActivityDTO _retItem = new ActivityDTO();
                    _retItem.ActivityId = activityItem.ActivityId;
                    _retItem.ActivityName = activityItem.ActivityName;
                    _retActivityList.Add(_retItem);
                }

                return Ok(_retActivityList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAll)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }



    }


}
