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

        public CollabEntity AddCollabEmail( string CollabEmail, long NoteID)
        {
            try
            {
                var noteResult = fundooContext.CollabTable.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var EmailResult = fundooContext.CollabTable.Where(x => x.CollabEmail == CollabEmail).FirstOrDefault();
                if (noteResult != null && EmailResult != null)
                {
                    CollabEntity collabEntity = new CollabEntity();

                    collabEntity.NoteID = noteResult.NoteID;
                    collabEntity.CollabEmail = EmailResult.CollabEmail;
                    collabEntity.UserId = EmailResult.UserId;

                    fundooContext.CollabTable.Add(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
                }
                else
                {
                    return null;
                }
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
