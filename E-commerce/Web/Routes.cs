namespace API.Web
{
    public class Routes
    {
        private const string StartRouteSegment = "ys/v1";

        #region Auth

        private const string AuthRouteSegment = "auth";
        private const string AuthRoute = $"{StartRouteSegment}/{AuthRouteSegment}";

        public const string RegistrationRoute = $"{AuthRoute}/registration";
        public const string EmailLoginRoute = $"{AuthRoute}/loginemail";
        public const string PhoneLoginRoute = $"{AuthRoute}/loginphone";

        #endregion

        #region Cart

        private const string CategoriesRouteSegment = "categories";
        private const string CategoriesRoute = $"{StartRouteSegment}/{CategoriesRouteSegment}";

        public const string GetCategoriesRoute = $"{CategoriesRoute}/all";
        public const string GetCategoryRoute = $"{CategoriesRoute}/{{categoryId}}";
        public const string AddCategoryRoute = $"{CategoriesRoute}/add";
        public const string EditCategoryRoute = $"{CategoriesRoute}/edit";

        #endregion

        #region Category

        private const string SalaryRouteSegment = "salary";
        private const string SalaryRoute = $"{StartRouteSegment}/{SalaryRouteSegment}";

        public const string AddSalaryRoute = $"{SalaryRoute}/add";
        public const string GetSalaryRoute = $"{SalaryRoute}/{{salaryId}}";

        #endregion

        #region Order

        private const string CurrencyRouteSegment = "salary";
        private const string CurrencyRoute = $"{StartRouteSegment}/{CurrencyRouteSegment}";

        public const string GetUserCurrencyRoute = $"{CurrencyRoute}/currency";

        #endregion


        /*        #region OrderDetails

                private const string 
                private const string 

                public const string 

                #endregion


                #region Product

                private const string 
                private const string 

                public const string 

                #endregion


                #region User

                private const string 
                private const string 

                public const string 

                #endregion*/
    }
}
