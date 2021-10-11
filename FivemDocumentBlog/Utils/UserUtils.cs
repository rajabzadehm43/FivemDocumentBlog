using System.Security.Claims;

namespace FivemDocumentBlog.Utils
{
    public static class UserUtils
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}