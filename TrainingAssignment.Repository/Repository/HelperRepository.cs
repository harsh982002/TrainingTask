using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingAssignment.Repository.Interface;

namespace TrainingAssignment.Repository.Repository
{
    public class HelperRepository 
    {
        public static string UserAvatar(IFormFile formFile)
        {
            using (var stream = formFile.OpenReadStream())
            {
                var bytes = new byte[formFile.Length];
                stream?.Read(bytes, 0, (int)formFile.Length);
                var base64string = Convert.ToBase64String(bytes);
               return "data:image/png;base64," + base64string;
            }
        }
    }
}
