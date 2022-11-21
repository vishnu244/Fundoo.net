﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity AddNotes(NotesModel notesModel, long UserID);
        public NotesEntity UpdateNotes(NotesModel notesModel, long NoteID);

    }
}
