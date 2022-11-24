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
    public class LableRL : ILableRL
    {
        FundooContext fundooContext;
        public LableRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;

        }

        public LableEntity AddLable(long NoteID, string LableName, long UserID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    LableEntity lableEntity = new LableEntity();

                    lableEntity.NoteID = result.NoteID;
                    lableEntity.LableName = LableName;
                    lableEntity.UserId = result.UserId;

                    fundooContext.LableTable.Add(lableEntity);
                    fundooContext.SaveChanges();
                    return lableEntity;
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


        public bool DeleteLable(long LableID)
        {
            try
            {
                var result = fundooContext.LableTable.Where(x => x.LableID == LableID).FirstOrDefault();

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


        public IEnumerable<LableEntity> DisplayLable(long LableID)
        {
            try
            {
                var result = fundooContext.LableTable.Where(x => x.LableID == LableID);
                if (result != null)
                {
                    return result;
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


        public LableEntity UpdateLable(LableModel lableModel, long LableID)
        {
            try
            {
                LableEntity lableEntity = fundooContext.LableTable.Where(X => X.LableID == LableID).FirstOrDefault();
                if (lableEntity != null)
                {
                    lableEntity.LableName = lableModel.LableName;

                    fundooContext.LableTable.Update(lableEntity);
                    fundooContext.SaveChanges();
                    return lableEntity;
                }
                else
                {
                    return lableEntity;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }     
}
