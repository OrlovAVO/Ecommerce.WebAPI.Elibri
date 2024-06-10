namespace Elibri.Api.Web
{
    public class Routes
    {
        private const string StartRouteSegment = "api/v1";

        #region Auth

        private const string AuthRouteSegment = "auth";
        private const string AuthRoute = $"{StartRouteSegment}/{AuthRouteSegment}";

        public const string RegistrationRoute = $"{AuthRoute}/registration";
        public const string AdminRegistrationRoute = $"{AuthRoute}/adminRegistration";
        public const string LoginRoute = $"{AuthRoute}/login";
        public const string ResetPassword = $"{AuthRoute}/resetPassword";
        public const string ChangePassword = $"{AuthRoute}/changePassword";

        #endregion


        #region Profile

        private const string ReviewRouteSegment = "profile";
        private const string ProfileRoute = $"{StartRouteSegment}/{ReviewRouteSegment}";

        public const string ProfileChangePassword = $"{ProfileRoute}/changePassword";
        public const string GetOrderByIdRoute = $"{ProfileRoute}/userOrders";


        #endregion

        #region Category

        private const string CategoriesRouteSegment = "categories";
        private const string CategoriesRoute = $"{StartRouteSegment}/{CategoriesRouteSegment}";

        public const string GetCategoriesRoute = $"{CategoriesRoute}/all";
        public const string GetCategoryByIdRoute = $"{CategoriesRoute}/categoryId";
        public const string CreateCategoryRoute = $"{CategoriesRoute}/create";
        public const string UpdateCategoryRoute = $"{CategoriesRoute}/update";
        public const string DeleteCategoryRoute = $"{CategoriesRoute}/delete";

        #endregion

        #region Order

        private const string OrderRouteSegment = "order";
        private const string OrderRoute = $"{StartRouteSegment}/{OrderRouteSegment}";

        public const string GetAllOrdersRoute = $"{OrderRoute}/all";
        public const string CreateOrderRoute = $"{OrderRoute}/create";
        public const string DeleteOrderRoute = $"{OrderRoute}/delete";

        #endregion

        #region Product

        private const string ProductRouteSegment = "product";
        private const string ProductRoute = $"{StartRouteSegment}/{ProductRouteSegment}";

        public const string GetAllProductsRoute = $"{ProductRoute}/all";
        public const string GetProductByIdRoute = $"{ProductRoute}/productId";
        public const string GetFilteredProductsRoute = $"{ProductRoute}";
        public const string GetProductWithRelatedRoute = $"{ProductRoute}/withRelated";
        public const string CreateProductRoute = $"{ProductRoute}/create";
        public const string GetProductByCategoryIdRoute = $"{ProductRoute}/categoryId";
        public const string UpdateProductRoute = $"{ProductRoute}/update";
        public const string DeleteProductRoute = $"{ProductRoute}/delete";
        public const string GetProductByNameRoute = $"{ProductRoute}/product/name";

        #endregion


        #region User

        private const string UserRouteSegment = "user";
        private const string UserRoute = $"{StartRouteSegment}/{UserRouteSegment}";

        public const string GetAllUserRoute = $"{UserRoute}/all";
        public const string GetUserByUsernameRoute = $"{UserRoute}/{{username}}";
        public const string GetUserByIdRoute = $"{UserRoute}/{{userId}}";
        public const string GetUserRoute = $"{UserRoute}/update";
        public const string UpdateuserRoute = $"{UserRoute}/update";
        public const string DeleteUserRoute = $"{UserRoute}/delete";

        #endregion


    }
}
