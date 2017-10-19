using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ImageService
{
    public class ImageService : IImageService
    {
        DBController.DBController dbController = new DBController.DBController();
        public void AddImage(string tagString, byte[] data)
        {
            dbController.AddImage(tagString, data);
        }

        public void DeleteImage(int id)
        {
            dbController.DeleteImage(id);
        }

        public void EditTag(int id, string editedTag)
        {
            dbController.EditTag(id, editedTag);
        }

        public List<Image> FilterImages(string tagToFilter)
        {
            List<Image> imageList = new List<Image>();
            foreach (DBController.Image image in dbController.FilteredImages(tagToFilter))
            {
                Image img = new Image();
                img.Id = image.Id;
                img.Tag_Id = image.Tag_Id;
                img.Data = image.Data;
                img.TagString = image.Tag.TagString;
                imageList.Add(img);
            }
            return imageList;
        }

        public Image GetImage(int id)
        {
            DBController.Image image = new DBController.Image();
            Image img = new Image();
            img.Id = image.Id;
            img.Tag_Id = image.Tag_Id;
            img.Data = image.Data;
            img.TagString = image.Tag.TagString;
            return img;
        }

        public ObservableCollection<Image> GetImages()
        {
            ObservableCollection<Image> images = new ObservableCollection<Image>();
            foreach (DBController.Image image in dbController.Images)
            {
                Image img = new Image();
                img.Id = image.Id;
                img.Tag_Id = image.Tag_Id;
                img.Data = image.Data;
                img.TagString = image.Tag.TagString;
                images.Add(img);
            }
            return images;
        }
    }
}
