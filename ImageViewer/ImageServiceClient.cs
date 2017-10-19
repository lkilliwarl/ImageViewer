using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ImageViewer
{
    class ImageServiceClient
    {
        public ObservableCollection<ImageServiceReference.Image> GetImages()
        {
            ImageServiceReference.ImageServiceClient imageServiceClient = new ImageServiceReference.ImageServiceClient();
            ObservableCollection<ImageServiceReference.Image> images = new ObservableCollection<ImageServiceReference.Image>();
            imageServiceClient.Open();
            images = imageServiceClient.GetImages();
            imageServiceClient.Close();
            return images;
        }
        public ObservableCollection<ImageServiceReference.Image> GetFilteredImages(string tagToFilter)
        {
            ImageServiceReference.ImageServiceClient imageServiceClient = new ImageServiceReference.ImageServiceClient();
            ObservableCollection<ImageServiceReference.Image> images = new ObservableCollection<ImageServiceReference.Image>();
            imageServiceClient.Open();
            images = imageServiceClient.FilterImages(tagToFilter);
            imageServiceClient.Close();
            return images;
        }

        public void AddImage(string tagString, byte[] data)
        {
            ImageServiceReference.ImageServiceClient imageServiceClient = new ImageServiceReference.ImageServiceClient();
            imageServiceClient.Open();
            imageServiceClient.AddImage(tagString, data);
            imageServiceClient.Close();
        }
        public void EditTag(int id, string newTag)
        {
            ImageServiceReference.ImageServiceClient imageServiceClient = new ImageServiceReference.ImageServiceClient();
            imageServiceClient.Open();
            imageServiceClient.EditTag(id, newTag);
            imageServiceClient.Close();
        }
        public void DeleteImage(int id)
        {
            ImageServiceReference.ImageServiceClient imageServiceClient = new ImageServiceReference.ImageServiceClient();
            imageServiceClient.Open();
            imageServiceClient.DeleteImage(id);
            imageServiceClient.Close();
        }
        public void GetImage(int id)
        {
            ImageServiceReference.ImageServiceClient imageServiceClient = new ImageServiceReference.ImageServiceClient();
            imageServiceClient.Open();
            imageServiceClient.GetImage(id);
            imageServiceClient.Close();
        }

    }
}
