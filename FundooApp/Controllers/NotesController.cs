using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Linq;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL iNotesBL;

        public NotesController(INotesBL iNotesBL)
        {
            this.iNotesBL = iNotesBL;
        }

        [Authorize]
        [HttpPost("Addnotes")]
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
        [HttpPost("UpdateNotes")]
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
        [HttpPost("DeleteNotes")]
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

    }
}
