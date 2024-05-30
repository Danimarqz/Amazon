using Amazon.Models;

namespace Amazon
{
    public class Global
    {
        public static Usuarios user {  get; set; }
        public static bool IsAdmin { get { return user.userType == "administrador"; } }
    }
}
