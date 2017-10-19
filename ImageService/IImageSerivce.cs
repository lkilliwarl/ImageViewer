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
    [ServiceContract]
    public interface IImageService
    {

        [OperationContract]
        void AddImage(string tagString, byte[] data);

        [OperationContract]
        void EditTag(int id, string editedTag);

        [OperationContract]
        void DeleteImage(int id);

        [OperationContract]
        Image GetImage(int id);

        [OperationContract]
        ObservableCollection<Image> GetImages();

        [OperationContract]
        List<Image> FilterImages(string tagToFilter);
    }

    [DataContract]
    public class Image
    {
        private int _id;
        private int _tag_id;
        private byte[] _data;
        private string _tagString;

        [DataMember]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        [DataMember]
        public int Tag_Id
        {
            get
            {
                return _tag_id;
            }
            set
            {
                _tag_id = value;
            }
        }
        [DataMember]
        public byte[] Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
        [DataMember]
        public string TagString
        {
            get
            {
                return _tagString;
            }
            set
            {
                _tagString = value;
            }
        }
    }
}
