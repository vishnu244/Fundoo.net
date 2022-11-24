using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LableBL : ILableBL
    {
        ILableBL iLableBL;

        private ILableRL iLableRL;

        public LableBL(ILableRL iLableRL)
        {
            this.iLableRL = iLableRL;
        }
        public LableEntity AddLable(long NoteID, string LableName, long UserID)
        {
            try
            {
                return iLableRL.AddLable(NoteID, LableName, UserID);
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
                return iLableRL.DeleteLable(LableID);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<LableEntity> DisplayLable(long LabelID)
        {
            try
            {
                return iLableRL.DisplayLable(LabelID);

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
                return iLableRL.UpdateLable(lableModel, LableID);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
