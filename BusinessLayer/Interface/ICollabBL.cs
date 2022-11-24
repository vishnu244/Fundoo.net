using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollabEmail(string CollabEmail, long NoteID);
        public bool DeleteCollab(long CollabID);
        public IEnumerable<CollabEntity> DisplayCollab(long CollabID);

    }
}
