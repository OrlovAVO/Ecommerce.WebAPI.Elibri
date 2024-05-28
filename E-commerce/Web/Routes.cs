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
        public const string GetCategoryByIdRoute = $"{CategoriesRoute}/{{categoryId}}";
        public const string CreateCategoryRoute = $"{CategoriesRoute}/create";
        public const string UpdateCategoryRoute = $"{CategoriesRoute}/update";
        public const string DeleteCategoryRoute = $"{CategoriesRoute}/delete";

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

        #region OrderDetails

        private const string OrderDetailsRouteSegment = "order_detail";
        private const string OrderDetailsRoute = $"{StartRouteSegment}/{OrderDetailsRouteSegment}";

        public const string GetAllOrdersRedatilsRoute = $"{OrderDetailsRoute}/all";
        public const string GetOrderDetailByIdRoute = $"{OrderDetailsRoute}/{{orderdetailId}}";
        public const string CreateOrderDetailRoute = $"{OrderDetailsRoute}/create";
        public const string UpdateOrderDetailRoute = $"{OrderDetailsRoute}/update";
        public const string DeleteOrderDetailRoute = $"{OrderDetailsRoute}/delete";

        #endregion

        #region Product

        private const string ProductRouteSegment = "product";
        private const string ProductRoute = $"{StartRouteSegment}/{ProductRouteSegment}";

        public const string GetAllProductsRedatilsRoute = $"{ProductRoute}/all";
        public const string GetProductByIdRoute = $"{ProductRoute}/{{productId}}";
        public const string CreateProductRoute = $"{ProductRoute}/create";
        public const string GetProductByCategoryIdRoute = $"{ProductRoute}/{{categoryId}}";
        public const string UpdateProductRoute = $"{ProductRoute}/update";
        public const string DeleteProdctRoute = $"{ProductRoute}/delete";

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
