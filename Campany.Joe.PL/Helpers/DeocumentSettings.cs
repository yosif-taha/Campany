using Elfie.Serialization;

namespace Campany.Joe.PL.Helpers
{
    public static class DeocumentSettings
    {
        //Upload
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get File location
            /* var filePath = Directory.GetCurrentDirectory() + @"wwwroot\files" +folderName;*/   //Function: Directory.GetCurrentDirectory() :- is return current path of this project in local server.
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files" , folderName);  // Function: Path.Combine:- its concat between pathes.(Its Location Of File) 

            // Get File Name And Make It Unique

            var fileName = $"{Guid.NewGuid()}{file.FileName}"; // Guid:- For Unique - file.FileName:- For get file name from FileName Into file (inerface) 
            var endfilePath= Path.Combine(filePath, fileName); //concat filepath and name of file to use it.

            using  var fileStream = new FileStream(endfilePath,FileMode.Create);

            file.CopyTo(fileStream); //get copy for end of filepath
            return fileName; //return File Name For restore in DB
        
        }
        //Delete

        public static void DeleteFile(string fileName,string folderName)
        {
            var filePath= Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName,fileName);  // Function: Path.Combine:- its concat between pathes.(Its Location Of File) 

            if (File.Exists(filePath)) 
                File.Delete(filePath);
        }
    }
}
