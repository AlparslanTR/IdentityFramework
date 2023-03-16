namespace IdentityFrameworkWepApp.Permissions
{
    public static class Permission
    {
        public static class Stock
        {
            public const string Read = "Stock.Read";
            public const string Create = "Stock.Create";
            public const string Update = "Stock.Update";
            public const string Delete = "Stock.Delete";
        }

        public static class Order
        {
            public const string Read = "Order.Read";
            public const string Create = "Order.Create";
            public const string Update = "Order.Update";
            public const string Delete = "Order.Delete";
        }

        public static class Catalog
        {
            public const string Read = "Catalog.Read";
            public const string Create = "Catalog.Create";
            public const string Update = "Catalog.Update";
            public const string Delete = "Catalog.Delete";
        }
    }
}
