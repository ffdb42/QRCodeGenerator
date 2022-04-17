using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;
using QRCodeGenerator.Models;
using System.Collections.Generic;

namespace QRCodeGenerator
{
    class QRViewModel: INotifyPropertyChanged
    {
        private string _data = string.Empty;
        private int _version;
        private int _ecc = 0;
        private int _encodementType = 0;
        private BitmapImage _qrImage = null;
        private QREncoderModel encoder;
        private QRRendererModel renderer;

        public event PropertyChangedEventHandler PropertyChanged;

        public int GenerateQRCode(bool time, bool date)
        {
            if (time)
                _data += DateTime.Now.ToString("-HH:mm");
            if (date)
                _data += DateTime.Now.ToString("-dd.MM.yyyy");
            encoder = new QREncoderModel(_data, _version, _ecc, _encodementType);
            encoder.DataEncodement();
            renderer = new QRRendererModel(encoder.Ver, _ecc);
            QRImage = bitmapToImageSource(renderer.GenerateImage(encoder.BinaryData));
            if (_version != encoder.Ver)
                return -1;
            return 0;
        }

        public string Data
        {
            get => _data;
            set
            {
                if (value != null)
                    _data = value;
                else
                    _data = string.Empty;
                OnPropertyChanged(nameof(Data));
            }
        }

        public int Version
        {
            get => _version;
            set
            {
                _version = value-1;
                OnPropertyChanged(nameof(Version));
            }
        }

        public int ECC
        {
            get => _ecc;
            set
            {
                _ecc = value;
                OnPropertyChanged(nameof(ECC));
            }
        }

        public int EncodementType
        {
            get => _encodementType;
            set
            {
                _encodementType = value;
                OnPropertyChanged(nameof(EncodementType));
            }
        }

        public BitmapImage QRImage
        {
            get => _qrImage;
            private set
            {
                _qrImage = value;
                OnPropertyChanged(nameof(QRImage));
            }
        }

        public bool DataTypeValidation()
        {
            bool result = true;
            if (_data.Length != 0)
            {
                switch (EncodementType)
                {
                    case 0:
                        {
                            foreach (char c in Data)
                            {
                                if (!Char.IsDigit(c))
                                {
                                    result = false;
                                    break;
                                }
                            }
                            break;
                        }
                    case 1:
                        {
                            foreach (char c in Data)
                            {
                                if (!isAlphaNumeric(c))
                                {
                                    result = false;
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
            return result;
        }

        public bool SaveImage(string path)
        {
            if (_qrImage is null)
            {
                return false;
            }
            else
            {
                renderer.SaveImage(path);
                return true;
            }
        }

        public string ReadFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        private BitmapImage bitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        private bool isAlphaNumeric(char c)
        {
            string alphaNumeric = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ $%*+-./:";
            return alphaNumeric.Contains(c.ToString());
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}