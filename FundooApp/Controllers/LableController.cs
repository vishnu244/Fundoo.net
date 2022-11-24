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
    public class LableController : ControllerBase
    {
        ILableBL iLableBL;

        public LableController(ILableBL iLableBL)
        {
            this.iLableBL = iLableBL;
        }


        [Authorize]
        [HttpPost("AddLable")]
        public IActionResult AddLable(long NoteID, string LableName)
        {
            long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var dataresult = iLableBL.AddLable(NoteID, LableName, UserID);
            if (dataresult != null )
            {
                return this.Ok(new
                {
                    success = true,
                    message = "Lable Added",
                    data = dataresult
                });

            }
            else
            {
                return this.BadRequest(new
                {
                    success = false,
                    message = "Lable is not Added.",

                });
            }

        }



        [Authorize]
        [HttpDelete("DeleteLable")]
        public IActionResult DeleteLable(long LableID)
        {
            try
            {
                if (iLableBL.DeleteLable(LableID))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Lable Deleted Successfully",

                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Can not delete the Lable.",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }



        [Authorize]
        [HttpGet("RetriveLable")]
        public IActionResult DisplayLable(long LableID)
        {
            try
            {
                var dataresult = iLableBL.DisplayLable(LableID);
                if (dataresult != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "The Created Lable are.",
                        data = dataresult
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "No Lables to display.",

                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        [Authorize]
        [HttpPost("UpdateLable")]
        public IActionResult UpdateLable(LableModel lableModel, long LableID)
        {
            try
            {
                var result = iLableBL.UpdateLable(lableModel, LableID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Lable Updated Successfully",
                        data = result
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Unable to Update Lable",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

    }
}
