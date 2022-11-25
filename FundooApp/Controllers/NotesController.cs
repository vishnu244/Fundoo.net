using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Authorize]

    public class NotesController : ControllerBase
    {
        INotesBL iNotesBL;

        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        private readonly FundooContext fundooContext;

        public NotesController(INotesBL iNotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iNotesBL = iNotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }

/*        [Authorize]
*/        [HttpPost("Add")]
        public IActionResult AddNotes(NotesModel notesModel)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = iNotesBL.AddNotes(notesModel, UserID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Notes Added Successfully",
                        data = result
                    });
                    
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Unable to Add Notes",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [Authorize]
        [HttpPost("Update")]
        public IActionResult UpdateNotes(NotesModel notesModel, long NoteID)
        {
            try
            {
                var result = iNotesBL.UpdateNotes(notesModel, NoteID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Notes Updated Successfully",
                        data = result
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Unable to Update Notes",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [Authorize]
        [HttpPost("Delete")]
        public IActionResult DeleteNotes(long NoteID)
        {
            try
            {
                /*var result = iNotesBL.DeleteNotes(NoteID);*/
                if (iNotesBL.DeleteNotes(NoteID))
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = "Notes Deleted Successfully",
                        
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "No Notes found to delete.",

                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpGet("Retrive")]
        public IActionResult DisplayNotes(long UserID)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = iNotesBL.DisplayNotes(UserID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = result
                    });

                }

                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "No Notes found to display.",

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Authorize]
        [HttpPut("Pin")]
        public IActionResult PinNotes(long NoteID)
        {
            try
            {
                var result = iNotesBL.PinNotes(NoteID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Notes UnPinned"
                    });

                }
                else if(result == null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Notes Pinned"
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Can not process the request.",

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("Archive")]
        public IActionResult ArchiveNotes(long NoteID)
        {
            try
            {
                var result = iNotesBL.ArchiveNotes(NoteID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Notes UnArchived Successfully"
                    });

                }
                else if (result == null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Notes Archived Successfully"
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Can not process the request.",

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Authorize]
        [HttpPut("Trash")]
        public IActionResult TrashNotes(long NoteID)
        {
            try
            {
                var result = iNotesBL.TrashNotes(NoteID);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Notes Restored from Trash"
                    });

                }
                else if (result == null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Notes moved to Trash."
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Can not process the request.",

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [Authorize]
        [HttpPost("Image")]
        public IActionResult Image(long UserID, long NoteID, IFormFile image)
        {
            try
            {
                var result = iNotesBL.Image(UserID, NoteID, image);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Image Uploaded "
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Image is not Uploaded.",

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("Color")]
        public IActionResult Color(long UserID, long NoteID, string Color)
        {
            try
            {
                var result = iNotesBL.Color(UserID, NoteID, Color);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        success = true,
                        data = "Color Added to the Notes."
                    });

                }
                else
                {
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Color is not Added to the Notes.",

                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);

            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
