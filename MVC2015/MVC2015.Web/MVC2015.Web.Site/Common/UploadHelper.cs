using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using DevExpress.Web;
using DevExpress.Web.ASPxUploadControl;
using System.Web.Mvc;

namespace MVC2015.Web.Site.Common
{
    public class UploadHelper
    {

        public static readonly ValidationSettings UploadValidationSettings = new ValidationSettings
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".bmp" },
            MaxFileSize = 10485760
        };

        public static readonly ValidationSettings FreightUploadValidationSettings = new ValidationSettings()
        {
            //AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" },
            MaxFileSize = 10485760
        };

        public static readonly ValidationSettings PictureUploadValidationSettings = new ValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" },
            MaxFileSize = 10485760
        };

        public static void FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                e.CallbackData = "true";
            }
        }

        public static readonly ValidationSettings CommonUploadValidationSettings = new ValidationSettings
        {
            //AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png" },
            MaxFileSize = 10485760
        };
    }
}