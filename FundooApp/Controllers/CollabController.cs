using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabBL iCollabBL;

        public CollabController(ICollabBL iCollabBL)
        {
            this.iCollabBL = iCollabBL;
        }

        [Authorize]
        [HttpPost("AddCollab")]
        public IActionResult AddCollabEmail(long NoteID, string CollabEmail)
        {
            /*long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            long NoteID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "NoteID").Value);*/
            var result = iCollabBL.AddCollabEmail( CollabEmail, NoteID);
            if(result != null)
            {
                return this.Ok(new
                {
                    success = true,
                    message = "Mail Collaborated",
                    data = result
                });

            }
            else
            {
                return this.BadRequest(new
                {
                    success = false,
                    message = "Unable to Collaborate.",

                });
            }

        }


        [Authorize]
        [HttpPost("DeleteCollab")]
        public IActionResult DeleteCollab(long CollabID)
        {
            try
            {
                if (iCollabBL.DeleteCollab(CollabID))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Collabarated Mail Deleted Successfully",

                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Can not delete the Collabarated Mail.",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [Authorize]
        [HttpGet("RetriveCollab")]
        public IActionResult DisplayCollab(long CollabID)
        {
            try
            {
                var result = iCollabBL.DisplayCollab(CollabID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "The Collabarated mails are.",
                        data = result
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "No Collabarated mails to display.",

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
