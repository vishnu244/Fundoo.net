using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        ICollabBL iCollabBL;

        private ICollabRL iCollabRL;

        public CollabBL(ICollabRL iCollabRL)
        {
            this.iCollabRL = iCollabRL;
        }
        public CollabEntity AddCollabEmail(CollabModel collabModel, long UserID, long NoteID)
        {
            try
            {
                return iCollabRL.AddCollabEmail(collabModel, UserID, NoteID);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool DeleteCollab(long CollabID)
        {
            try
            {
                return iCollabRL.DeleteCollab(CollabID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CollabEntity> DisplayCollab(long CollabID)
        {
            try
            {
                return iCollabRL.DisplayCollab(CollabID);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
