using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity AddNotes(NotesModel notesModel, long UserID);
        public NotesEntity UpdateNotes(NotesModel notesModel, long NoteID);
        public bool DeleteNotes(long NoteID);
        public IEnumerable<NotesEntity> DisplayNotes(long UserID);
        public NotesEntity PinNotes(long NoteID);
        public NotesEntity ArchiveNotes(long NoteID);



    }
}
