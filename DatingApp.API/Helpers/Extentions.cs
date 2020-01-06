using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extentions
    {
        public static void AddAplicationError(this HttpResponse response, string message){
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
        }
    }
}