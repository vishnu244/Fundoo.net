using BusinessLayer.Interface;
using CommonLayer.Model;
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
    }
}
