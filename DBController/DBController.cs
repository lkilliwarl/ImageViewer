using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBController
{
    public class DBController
    {
        ImageDatabaseEntities imageDatabaseEntitie = new ImageDatabaseEntities();

        #region Utilities

        private List<Image> _images;
        private List<Image> GenerateFromDatabase()
        {
            List<Image> images = new List<Image>();
            foreach (Image image in imageDatabaseEntitie.Image)
            {
                images.Add(image);
            }
            return images;
        }
        
        private List<Image> GetFilteredList(string tagToFilter)
        {
            List<Image> filteredImages = new List<Image>();
            foreach (Image image in imageDatabaseEntitie.Image)
            {
                if (image.Tag.TagString == tagToFilter)
                    filteredImages.Add(image);
            }
            return filteredImages;
        }

        public List<Image> Images
        {
            get
            {
                return _images = GenerateFromDatabase();
            }
            set
            {
                _images = value;
            }
        }

        public List<Image> FilteredImages (string tag)
        {
            return GetFilteredList(tag);
        }


        #endregion

        public void AddImage(string tagString, byte[] data)
        {
            Image image = new Image();
            foreach (Tag tag in imageDatabaseEntitie.Tag)
            {
                if (tagString == tag.TagString)
                {
                    image.Tag_Id = tag.Id;
                }
            }
            if (image.Tag_Id == 0)
            {
                Tag newTag = new Tag();
                newTag.TagString = tagString;
                if (newTag.TagString == null || newTag.TagString == "")
                    newTag.TagString = "default";
                imageDatabaseEntitie.Tag.Add(newTag);
                imageDatabaseEntitie.SaveChanges();
                image.Tag_Id = newTag.Id;
            }
            image.Data = data;
            imageDatabaseEntitie.Image.Add(image);
            imageDatabaseEntitie.SaveChanges();
        }

        public void EditTag (int id, string editedTag)
        {
            Image image = imageDatabaseEntitie.Image.Find(id);
            bool isTagChanged = false;
            foreach (Tag tags in imageDatabaseEntitie.Tag)
            {
                if (editedTag == tags.TagString)
                {
                    image.Tag_Id = tags.Id;
                    isTagChanged = true;
                }
            }
            if (isTagChanged == false)
            {
                Tag newTag = new Tag();
                newTag.TagString = editedTag;
                imageDatabaseEntitie.Tag.Add(newTag);
                imageDatabaseEntitie.SaveChanges();
                image.Tag_Id = newTag.Id;
            }
            imageDatabaseEntitie.SaveChanges();
        }

        public void DeleteImage (int id)
        {
            imageDatabaseEntitie.Image.Remove(imageDatabaseEntitie.Image.Find(id));
            imageDatabaseEntitie.SaveChanges();
        }

        public Image GetImage (int id)
        {
            return imageDatabaseEntitie.Image.Find(id);
        }
    }
}
