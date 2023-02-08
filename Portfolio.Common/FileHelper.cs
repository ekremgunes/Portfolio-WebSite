using Microsoft.AspNetCore.Http;
using System.IO;

namespace Portfolio.Common
{
    public static class FileHelper
    {
        public static async Task<String> CreateFile(IFormFile file)
        {
            var format = Path.GetExtension(file.FileName);
            var randomName = string.Format($"{file.FileName.Replace(format, "")}_{Guid.NewGuid()}{format}");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs\\gallery", randomName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return randomName;
        }
        public static async Task<String> ReplaceFile(string oldImgName, IFormFile file)
        {
            DeleteFile(oldImgName);
            var ImgName = await CreateFile(file);
            return ImgName;
        }
        public static void DeleteFile(string ImgName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), ("wwwroot/imgs/gallery/"), ImgName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        
    }
}