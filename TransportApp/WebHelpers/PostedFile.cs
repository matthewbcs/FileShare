using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TransportApp.WebHelpers
{
    public interface IPostedFile
    {
        string FileName { get; }
        int ContentLength { get; }
        byte[] FileContents { get; }

        void SaveFileToDisk(string fullFilePath);
    }
    public class PostedFile : IPostedFile
    {
        private readonly HttpPostedFileBase _file;

        public PostedFile(HttpPostedFileBase file)
        {
            _file = file;
        }

        public string FileName => _file.FileName;
        public int ContentLength => _file.ContentLength;
        public byte[] FileContents
        {
            get
            {
                using (MemoryStream target = new MemoryStream())
                {
                    _file.InputStream.CopyTo(target);
                    return target.ToArray();
                }
            }
        }

        public void SaveFileToDisk(string filename)
        {
            _file.SaveAs(filename);
        }
    }
}