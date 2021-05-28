using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using Microsoft.Extensions.Logging;
using OCAS.WebAPI.Models;
using OCAS.WebAPI.Helpers;
using OCAS.DataAccess;
using OCAS.Core.IRepository;
using OCAS.Core.Repository;
using OCAS.Core.Specifications;

namespace OCAS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UsersController> _logger;
      
        public UsersController(IUnitOfWork unitOfWork, ILogger<UsersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<UserDTO> _retUserList = null;
            try
            {

                var _users = await _unitOfWork.Users.GetAll(null, null, new List<string>() { "Activity" });

                _retUserList = new List<UserDTO>();
                foreach (var userItem in _users)
                {
                    UserDTO _retUser = new UserDTO();
                    _retUser.FirstName = userItem.FirstName;
                    _retUser.LastName = userItem.LastName;
                    _retUser.EmailAddress = userItem.EmailAddress;
                    _retUser.Comments = string.Empty;
                    if ( !string.IsNullOrEmpty(userItem.Comments) )
                    {
                        _retUser.Comments = userItem.Comments;
                    }
                    _retUser.ActivityId = userItem.ActivityId;
                    _retUser.ActivityName = string.Empty;
                    if ( userItem.Activity != null)
                    {
                        _retUser.ActivityName = userItem.Activity.ActivityName;
                    }
                    _retUserList.Add(_retUser);
                }

                return Ok( new { ActionResult = 1, ActionMessage = "Get All Users Successfully.", DataList = _retUserList });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetAll)}");
                return StatusCode(500,  new { ActionResult = 0, ActionMessage = "Internal Server Error. Please Try Again Later." });
            }
        }


        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(UserInsertDTO userInsertDTO )
        {
        
            try
            {
                if ( !ModelState.IsValid )
                {
                    return StatusCode(500, new { ActionResult = 1, ActionMessage = "Model State is invalid ." } );
                }

                User _userItem = new User();
                _userItem.UserGuid = Guid.NewGuid().ToString();
                _userItem.FirstName = userInsertDTO.FirstName;
                _userItem.LastName = userInsertDTO.LastName;
                _userItem.EmailAddress = userInsertDTO.EmailAddress;
                _userItem.ActivityId = userInsertDTO.ActivityId;
                _userItem.Comments = userInsertDTO.Comments;
                _userItem.DateCreated = DateTime.Now;
                _userItem.DateUpdated = DateTime.Now;

               await _unitOfWork.Users.Insert(_userItem);
               await _unitOfWork.Save();

               return Ok( new { ActionResult = 1, ActionMessage = "New User Sign Up Successfully.", Data = _userItem});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Insert)}");
                return StatusCode(500, new { ActionResult = 0, ActionMessage = "Internal Server Error. Please Try Again Later." } );
            }
        }



        [HttpPost("Search")]
        public async Task<ActionResult<Pagination<UserDTO>>> Search([FromBody]UserSpecParams userParams )
        {

            try
            {

                var _userList = await _unitOfWork.Users.GetAll(null, null, new List<string>() { "Activity" });
               
                if ( userParams.ActivityId.HasValue)
                {
                    _userList = _userList.Where(c => c.ActivityId == userParams.ActivityId.Value).ToList();
                }

                if ( ! string.IsNullOrEmpty( userParams.SortName))
                {
                    switch( userParams.SortName)
                    {
                        case "firstNameAsc":
                            _userList = _userList.OrderBy(c => c.FirstName).ToList();
                            break;
                        case "firstNameDesc":
                            _userList = _userList.OrderByDescending(c => c.FirstName).ToList();
                            break;
                        case "lastNameAsc":
                            _userList = _userList.OrderBy(c => c.LastName).ToList();
                            break;
                        case "lastNameDesc":
                            _userList = _userList.OrderByDescending(c => c.LastName).ToList();
                            break;
                        case "emailAddressAsc":
                            _userList = _userList.OrderBy(c => c.EmailAddress).ToList();
                            break;
                        case "emailAddressDesc":
                            _userList = _userList.OrderByDescending(c => c.EmailAddress).ToList();
                            break;
                    }
                }

                if ( ! string.IsNullOrEmpty(userParams.SearchWords))
                {
                    string _searchWords = userParams.SearchWords.ToLower();
                    _userList = _userList.Where(x => x.FirstName.ToLower().Contains(_searchWords) ||
                                                x.LastName.ToLower().Contains( _searchWords) ||
                                                x.EmailAddress.ToLower().Contains(_searchWords)
                    ).ToList();
                }


                int totalItems = _userList.Count();

                // PageIndex from PageItemNN to PageItemNN
                // var pageIndexItemFrom = userParams.PageSize * (userParams.PageIndex - 1)

                var pageItems = _userList.Skip(userParams.PageSize * (userParams.PageIndex - 1)).Take(userParams.PageSize);

                List<UserDTO> _userDTOList = new List<UserDTO>();

                foreach(var userItem in pageItems)
                {
                     UserDTO _retUser = new UserDTO();
                    _retUser.FirstName = userItem.FirstName;
                    _retUser.LastName = userItem.LastName;
                    _retUser.EmailAddress = userItem.EmailAddress;
                    _retUser.Comments = string.Empty;
                    if (!string.IsNullOrEmpty(userItem.Comments))
                    {
                        _retUser.Comments = userItem.Comments;
                    }
                    _retUser.ActivityId = userItem.ActivityId;
                    _retUser.ActivityName = string.Empty;
                    if (userItem.Activity != null)
                    {
                        _retUser.ActivityName = userItem.Activity.ActivityName;
                    }
                    _userDTOList.Add(_retUser);
                }

                return Ok(new Pagination<UserDTO>(userParams.PageIndex, userParams.PageSize, totalItems, _userDTOList));
              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Insert)}");
                return StatusCode(500, new { ActionResult = 0, ActionMessage = "Internal Server Error. Please Try Again Later." });
            }
        }


    }

}
