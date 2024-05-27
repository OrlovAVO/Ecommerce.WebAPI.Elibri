namespace API.Web
{
    public class Routes
    {
        private const string StartRouteSegment = "eb/v1";

        #region Auth

        private const string AuthRouteSegment = "auth";
        private const string AuthRoute = $"{StartRouteSegment}/{AuthRouteSegment}";

        public const string RegistrationRoute = $"{AuthRoute}/registration";
        public const string AdminRegistrationRoute = $"{AuthRoute}/admin_registration";
        public const string LoginRoute = $"{AuthRoute}/login";
        public const string ResetPassword = $"{AuthRoute}/resetpassword";
        public const string ChangePassword = $"{AuthRoute}/changepassword";

        #endregion

        #region Cart

        private const string CartRouteSegment = "cart";
        private const string CartRoute = $"{StartRouteSegment}/{CartRouteSegment}";

        public const string GetCartsRoute = $"{CartRoute}/all";
        public const string GetCartByIdRoute = $"{CartRoute}/{{cartId}}";
        public const string CreateCartRoute = $"{CartRoute}/create";
        public const string UpdateCartByIdRoute = $"{CartRoute}/update";
        public const string DeleteCartByIdRoute = $"{CartRoute}/delete";

        #endregion

        #region Category

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

        private const string OrderRouteSegment = "order";
        private const string OrderRoute = $"{StartRouteSegment}/{OrderRouteSegment}";

        public const string GetAllOrdersRoute = $"{OrderRoute}/all";
        public const string GetOrderByIdRoute = $"{OrderRoute}/{{orderId}}";
        public const string CreateOrderRoute = $"{OrderRoute}/create";
        public const string UpdateOrderRoute = $"{OrderRoute}/update";
        public const string DeleteOrderRoute = $"{OrderRoute}/delete";

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
