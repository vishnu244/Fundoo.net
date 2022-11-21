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
    public class NotesRL : INotesRL
    {
        FundooContext fundooContext;

        private readonly IConfiguration config;

        public NotesRL(FundooContext fundooContext, IConfiguration config)

        {
            this.fundooContext = fundooContext;
            this.config = config;
        }

        public NotesEntity AddNotes(NotesModel notesModel, long UserID)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = notesModel.Title;
                notesEntity.Description = notesModel.Description;
                notesEntity.Remainder = notesModel.Remainder;
                notesEntity.Color = notesModel.Color;
                notesEntity.Image = notesModel.Image;
                notesEntity.Archive = notesModel.Archive;
                notesEntity.Trash = notesModel.Trash;
                notesEntity.Pin = notesModel.Pin;
                notesEntity.Createdat = notesModel.Createdat;
                notesEntity.Editedat = notesModel.Editedat;
                notesEntity.UserId = UserID;

                fundooContext.NotesTable.Add(notesEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return notesEntity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public NotesEntity UpdateNotes(NotesModel notesModel, long NoteID)
        {
            try
            {
                NotesEntity notesEntity = fundooContext.NotesTable.Where(X => X.NoteID == NoteID).FirstOrDefault();
                if (notesEntity != null)
                {
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Description = notesModel.Description;
                    notesEntity.Remainder = notesModel.Remainder;
                    notesEntity.Color = notesModel.Color;
                    notesEntity.Image = notesModel.Image;
                    notesEntity.Archive = notesModel.Archive;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Pin = notesModel.Pin;
                    notesEntity.Createdat = notesModel.Createdat;
                    notesEntity.Editedat = notesModel.Editedat;



                    fundooContext.NotesTable.Update(notesEntity);
                    fundooContext.SaveChanges();
                    return notesEntity;
                }


               
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool DeleteNotes(long NoteID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.NoteID == NoteID);

/*                fundooContext.Remove(result);
*/                if (result != null)
                {
                    fundooContext.Remove(result);

                }
                int DeleteNotes = fundooContext.SaveChanges();
                if (DeleteNotes > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
