using System;
namespace PizzaIllico.Mobile.Controls
{
    public class User
    {
        public bool is_connected= false;
        public static string client_id ="MOBILE";
        public static string client_secret = "UNIV";
        public string access_token;
        public string refresh_token;
        public string token_type;
        public int expires_in;
        
        private static User instance;

        private User()
        {
        
        }
        

        public static User Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new User();
                }
                return instance;
            }
        }
    }
}