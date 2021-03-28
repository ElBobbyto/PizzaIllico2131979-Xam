namespace PizzaIllico.Mobile.Services
{
    public static class Urls
    {
        private const string ROOT = "api/v1";

        public const string REFRESH_TOKEN = ROOT + "/authentication/refresh";//auth needed
        public const string LOGIN_WITH_CREDENTIALS = ROOT + "/authentication/credentials";//auth needed
        public const string SET_PASSWORD = ROOT + "/authentication/credentials/set";//auth needed
        
        public const string USER_PROFILE = ROOT + "/accounts/me";//auth needed
        public const string CREATE_USER = ROOT + "/accounts/register";
        public const string SET_USER_PROFILE = ROOT + "/accounts/me";//auth needed
        
        public const string LIST_SHOPS = ROOT + "/shops";
        public const string LIST_PIZZA = ROOT + "/shops/{shopId}/pizzas";
        public const string GET_IMAGE = ROOT + "/shops/{shopId}/pizzas/{pizzaId}/image";
        public const string DO_ORDER = ROOT + "/shops/{shopId}";//auth needed
        public const string LIST_ORDERS = ROOT + "/orders";//auth needed
    }
}