using RecycleHelperApplication.Service.Helper.APIHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace RecycleHelperApplication.Service.Helper
{
    public static class UserProfilePictureManager
    {
        public static string Upload(HttpPostedFileBase file, int userID)
        {
            try
            {
                string fileName = null;
                if (file != null)
                {
                    fileName = $"{userID}{Path.GetExtension(file.FileName)}";
                    string path = Path.Combine(HostingEnvironment.MapPath("~/Uploads/UserProfilePictures"), fileName);
                    file.SaveAs(path);
                }
                return fileName;
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException("Error while uploading photo");
            }
        }

        public static string GetProfilePictureOrDefaultIfFileNameIsNull(string fileName)
        {
            const string DEFAULT_PICTURE_PATH = "/Content/images/default-avatar.png";
            if (fileName != null)
            {
                return $"/Uploads/UserProfilePictures/{fileName}";
            }
            else
            {
                return DEFAULT_PICTURE_PATH;
            }
        }
    }
}
