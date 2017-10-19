using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ImageViewer
{
    class MainWindowViewModel : ViewModelBase
    {
        ImageServiceClient imageServiceClient = new ImageServiceClient();

        #region Images
        ObservableCollection<ImageServiceReference.Image> _images;
        public ObservableCollection<ImageServiceReference.Image> Images
        {
            get
            {
                if (_images == null)
                    _images = imageServiceClient.GetImages();
                return _images;
            }
            set
            {
                _images = value;
                OnPropertyChanged(nameof(Images));
            }
        }
        #endregion

        #region Utilities

        #region CurrentImage

        private ImageServiceReference.Image _currentImage;
        public ImageServiceReference.Image CurrentImage
        {
            get
            {
                if (_currentImage == null)
                    _currentImage = new ImageServiceReference.Image();
                return _currentImage;
            }
            set
            {
                _currentImage = value;
                OnPropertyChanged(nameof(CurrentImage));
            }
        }
        //*-*-*-*-*-*-*-*-*-*-*-* CURRENT TAG *-*-*-*-*-*-*-*-*-*-*-*-*-*-*
        private string _currentTag;
        public string CurrentTag
        {
            get
            {
                if (_currentTag == null)
                    _currentTag = "default";
                return _currentTag;
            }
            set
            {
                _currentTag = value;
                OnPropertyChanged(nameof(CurrentTag));
            }
        }
        #endregion

        #region Selected Image
        private ImageServiceReference.Image _selectedImage;
        private BitmapImage _selectedBitmapImage;
        public ImageServiceReference.Image SelectedImage
        {
            get
            {
                return _selectedImage;
            }
            set
            {
                _selectedImage = value;
                if (_selectedImage != null)
                    SelectedBitmapImage = ImageConvertor.ConvertByteArrayIntoBitmapImage(_selectedImage.Data);
                OnPropertyChanged(nameof(SelectedImage));
            }
        }
        public BitmapImage SelectedBitmapImage
        {
            get
            {
                return _selectedBitmapImage;
            }
            set
            {
                _selectedBitmapImage = value;
                OnPropertyChanged(nameof(SelectedBitmapImage));
            }
        }
        #endregion

        #region Filter String
        private string _filterString;
        public string FilterString
        {
            get
            {
                if (_filterString == null)
                    _filterString = "default";
                return _filterString;
            }
            set
            {
                _filterString = value;
                OnPropertyChanged(nameof(FilterString));
            }
        }
        #endregion

        #endregion

        #region Open
        RelayCommand _openImageCommand;
        public ICommand OpenImage
        {
            get
            {
                if (_openImageCommand == null)
                    _openImageCommand = new RelayCommand(ExecuteOpenImageCommand, CanExecuteOpenImageCommand);
                return _openImageCommand;
            }
        }

        public void ExecuteOpenImageCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "PNG (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                CurrentImage.Data = ImageConvertor.ConvertImageIntoByteArray(new BitmapImage(new Uri(openFileDialog.FileName)));
                SelectedBitmapImage = ImageConvertor.ConvertByteArrayIntoBitmapImage(CurrentImage.Data);
            }
        }
        public bool CanExecuteOpenImageCommand(object parameter)
        {
            return true;
        }
        #endregion

        #region Add
        RelayCommand _addImageCommand;
        public ICommand AddImage
        {
            get
            {
                if (_addImageCommand == null)
                    _addImageCommand = new RelayCommand(ExecuteAddImageCommand, CanExecuteAddImageCommand);
                return _addImageCommand;
            }
        }

        public void ExecuteAddImageCommand(object parameter)
        {
            imageServiceClient.AddImage(CurrentTag, CurrentImage.Data);
            Images = imageServiceClient.GetImages();
            CurrentImage = null;
            CurrentTag = null;
        }
        public bool CanExecuteAddImageCommand(object parameter)
        {
            if (CurrentImage.Data == null)
                return false;
            return true;
        }
        #endregion

        #region Delete
        RelayCommand _deleteImageCommand;
        public ICommand DelteImage
        {
            get
            {
                if (_deleteImageCommand == null)
                    _deleteImageCommand = new RelayCommand(ExecuteDeleteImageCommand, CanExecuteDeleteImageCommand);
                return _deleteImageCommand;
            }
        }
        public void ExecuteDeleteImageCommand(object parameter)
        {
            imageServiceClient.DeleteImage(SelectedImage.Id);
            Images.Remove(SelectedImage);
        }
        public bool CanExecuteDeleteImageCommand(object parameter)
        {
            if (SelectedImage == null)
                return false;
            return true;
        }
        #endregion

        #region Edit
        RelayCommand _editImageCommand;
        public ICommand EditImage
        {
            get
            {
                if (_editImageCommand == null)
                    _editImageCommand = new RelayCommand(ExecuteEditImageCommand, CanExecuteEditImageCommand);
                return _editImageCommand;
            }
        }
        public void ExecuteEditImageCommand(object parameter)
        {
            SelectedImage.TagString = CurrentTag;
            imageServiceClient.EditTag(SelectedImage.Id, CurrentTag);
            ImageServiceReference.Image editedImage = SelectedImage;
            editedImage.TagString = CurrentTag;
            CurrentTag = null;
            Images = imageServiceClient.GetImages();
        }
        public bool CanExecuteEditImageCommand(object parameter)
        {
            if (SelectedImage == null)
                return false;
            return true;
        }
        #endregion

        #region Filter
        RelayCommand _filterImageCommand;
        public ICommand FilterImage
        {
            get
            {
                if (_filterImageCommand == null)
                    _filterImageCommand = new RelayCommand(ExecuteFilterCommand, CanExecuteFilterCommand);
                return _filterImageCommand;
            }
        }
        public void ExecuteFilterCommand(object parameter)
        {
            if (_filterString == "")
                Images = imageServiceClient.GetImages();
            else
            {
                Images = imageServiceClient.GetFilteredImages(FilterString);
            }
        }
        public bool CanExecuteFilterCommand(object parameter)
        {
            return true;
        }
        #endregion

    }
}
