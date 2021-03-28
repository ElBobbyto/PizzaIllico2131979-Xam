using System;
namespace PizzaIllico.Mobile.Controls
{
    public class UserInfo
    {
        public static string client_id;
        public static string client_secret;
        public static string access_token;
        public static string refresh_token;
        public static string token_type;
        public static int expires_in;
        private static UserInfo instance;
        
        private UserInfo() {}

        public static UserInfo Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new UserInfo();
                }
                return instance;
            }
        }
    }
}