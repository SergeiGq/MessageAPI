using System.Security.Claims;
using DBMessage.Models;

namespace Message.API;

public static class AuthExtenstion
{
   public static int GetId(this ClaimsPrincipal user)
   {
      var x = user.Claims.FirstOrDefault(n=>n.Type=="userId");
      return int.Parse(x.Value);
   }
}