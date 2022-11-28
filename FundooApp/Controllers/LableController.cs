using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LableController : ControllerBase
    {
        ILableBL iLableBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;

        NLog nlog = new NLog();


        public LableController(ILableBL iLableBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iLableBL = iLableBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }


        [Authorize]
        [HttpPost("AddLable")]
        public IActionResult AddLable(long NoteID, string LableName)
        {
            long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var dataresult = iLableBL.AddLable(NoteID, LableName, UserID);
            if (dataresult != null )
            {
                nlog.LogInfo("Lable Added");

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
                    nlog.LogInfo("Lable Deleted Successfully");

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
                    nlog.LogInfo("The Created Lable are.");

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
                    nlog.LogInfo("Lable Updated Successfully");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Lable Updated Successfully",
                        data = result
                    });

                }
                else
                {
                    nlog.LogInfo("Unable to Update Lable");
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


        [HttpGet("redisLable")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);

            var cacheKey = "LableList";
            string serializedLableList;
            var LableList = new List<LableEntity>();
            var redisLableList = await distributedCache.GetAsync(cacheKey);
            if (redisLableList != null)
            {
                serializedLableList = Encoding.UTF8.GetString(redisLableList);
                LableList = JsonConvert.DeserializeObject<List<LableEntity>>(serializedLableList);
            }
            else
            {
                LableList = fundooContext.LableTable.ToList();
                serializedLableList = JsonConvert.SerializeObject(LableList);
                redisLableList = Encoding.UTF8.GetBytes(serializedLableList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLableList, options);
            }
            return Ok(LableList);
        }

    }
}
