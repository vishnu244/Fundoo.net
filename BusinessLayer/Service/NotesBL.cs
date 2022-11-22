using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        INotesRL inotesRL;
        private INotesRL iNotesRL;

        public NotesBL(INotesRL iNotesRL)
        {
            this.iNotesRL = iNotesRL;
        }
        public NotesEntity AddNotes(NotesModel notesModel, long UserID)
        {
            try
            {
                return iNotesRL.AddNotes(notesModel, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity UpdateNotes(NotesModel notesModel, long NoteID)
        {
            try
            {
                return iNotesRL.UpdateNotes(notesModel, NoteID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteNotes(long NoteID)
        {
            try
            {
                return iNotesRL.DeleteNotes(NoteID);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NotesEntity> DisplayNotes(long UserID)
        {
            try
            {
                return iNotesRL.DisplayNotes(UserID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity PinNotes(long NoteID)
        {
            try
            {
                return iNotesRL.PinNotes(NoteID);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public NotesEntity ArchiveNotes(long NoteID)
        {
            try
            {
                return iNotesRL.ArchiveNotes(NoteID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity TrashNotes(long NoteID)
        {
            try
            {
                return iNotesRL.TrashNotes(NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Image(long UserID, long NoteID, IFormFile image)
        {
            try
            {
                return iNotesRL.Image(UserID, NoteID, image);   
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
