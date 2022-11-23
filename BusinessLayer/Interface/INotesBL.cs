using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public NotesEntity TrashNotes(long NoteID);
        public string Image(long UserID, long NoteID, IFormFile image);
        public NotesEntity Color(long UserID, long NoteID, string Color);

    }
}
