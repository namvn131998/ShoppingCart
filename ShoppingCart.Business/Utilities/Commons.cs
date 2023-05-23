using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Business.Utilities
{
    public static class Commons
    {
        public static string GetPathImage(string hostName)
        {
            string host = "https://" + hostName + "/";
            return host;
        }
    }
}
