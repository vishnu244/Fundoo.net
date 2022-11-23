using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollabEmail(CollabModel collabModel, long UserID, long NoteID);
        public bool DeleteCollab(long CollabID);
        public IEnumerable<CollabEntity> DisplayCollab(long CollabID);

    }
}
