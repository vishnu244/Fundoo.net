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
    public class CollabController : ControllerBase
    {
        ICollabBL iCollabBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;
        NLog nlog = new NLog();


        public CollabController(ICollabBL iCollabBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iCollabBL = iCollabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
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
                nlog.LogInfo("Mail Collaborated");

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
        [HttpDelete("DeleteCollab")]
        public IActionResult DeleteCollab(long CollabID)
        {
            try
            {
                if (iCollabBL.DeleteCollab(CollabID))
                {
                    nlog.LogInfo("Collabarated Mail Deleted Successfully");

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
                    nlog.LogInfo("The Collabarated mails are.");
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


        [HttpGet("redisCollab")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);

            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = fundooContext.CollabTable.ToList();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }
    }
}
