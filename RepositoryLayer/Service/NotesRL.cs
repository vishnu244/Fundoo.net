using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
                var result = fundooContext.NotesTable.Where(x => x.NoteID == NoteID).FirstOrDefault();

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

        public IEnumerable<NotesEntity> DisplayNotes(long UserID)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.UserId == UserID);             
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity PinNotes(long NoteID)
        {
            try
            {
                NotesEntity result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteID == NoteID);
                if (result.Pin == true)
                {
                    result.Pin = false;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                result.Pin = true;
                this.fundooContext.SaveChanges();
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public NotesEntity ArchiveNotes(long NoteID)
        {
            try
            {
                NotesEntity result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteID == NoteID);
                if (result.Archive == true)
                {
                    result.Archive = false;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                result.Archive = true;
                this.fundooContext.SaveChanges();
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public NotesEntity TrashNotes(long NoteID)
        {
            try
            {
                NotesEntity result = fundooContext.NotesTable.FirstOrDefault(x => x.NoteID == NoteID);
                if (result.Trash == true)
                {
                    result.Trash = false;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                result.Trash = true;
                this.fundooContext.SaveChanges();
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public string Image(long UserID, long NoteID, IFormFile image)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(x => x.NoteID == NoteID && x.UserId == UserID).FirstOrDefault();

                if (result != null)
                {
                    Account account = new Account(
                   this.config["CloudinarySettings:CloudName"],
                   this.config["CloudinarySettings:ApiKey"],
                   this.config["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),

                    };
                    var uploadResult = cloudinary.Upload(uploadParams);

                    string ImagePath = uploadResult.Url.ToString();
                    result.Image = ImagePath;
                    fundooContext.SaveChanges();

                    return "Image uploaded";
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


        public NotesEntity Color(long UserID, long NoteID, string Color)
        {
            try
            {
                NotesEntity result = this.fundooContext.NotesTable.Where(x => x.NoteID == NoteID && x.UserId == UserID).FirstOrDefault();
                if (result.Color != null)
                {
                    result.Color = Color;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
