using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILableBL
    {
        public LableEntity AddLable(long NoteID, string LableName, long UserID);
        public bool DeleteLable(long LableID);
        public IEnumerable<LableEntity> DisplayLable(long LableID);
        public LableEntity UpdateLable(LableModel lableModel, long LableID);

    }
}
