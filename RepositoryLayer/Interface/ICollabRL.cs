using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity AddCollabEmail(string CollabEmail, long NoteID);
        public bool DeleteCollab(long CollabID);
        public IEnumerable<CollabEntity> DisplayCollab(long CollabID);

    }
}
