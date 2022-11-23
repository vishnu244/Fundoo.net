using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        FundooContext fundooContext;

        private readonly IConfiguration config;

        public CollabRL(FundooContext fundooContext, IConfiguration config)

        {
            this.fundooContext = fundooContext;
            this.config = config;
        }

        public CollabEntity AddCollabEmail(CollabModel collabModel, long UserID, long NoteID)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();

                collabEntity.CollabEmail = collabModel.CollabEmail;
                collabEntity.UserId = UserID;
                collabEntity.NoteID = NoteID;

                fundooContext.CollabTable.Add(collabEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return collabEntity;
                }
                return null;
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
                var result = fundooContext.CollabTable.Where(x => x.CollabID == CollabID).FirstOrDefault();

                if (result != null)
                {
                    fundooContext.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }

                return false;
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
                var result = fundooContext.CollabTable.Where(x => x.CollabID == CollabID);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
