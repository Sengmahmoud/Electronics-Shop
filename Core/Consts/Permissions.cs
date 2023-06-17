namespace Core.Consts
{
    public static class Permissions
    {
        public const string GroupName = "Crm";
        public static class Users
        {
            public const string Defualt = $"{GroupName}.Users";
            public const string Add = $"{Defualt}.Add";
            public const string Edit = $"{Defualt}.Edit";
            public const string Delete = $"{Defualt}.Delete";

        }
        public static class Roles
        {
            public const string Defualt = $"{GroupName}.Roles";
            public const string Add = $"{Defualt}.Add";
            public const string Edit = $"{Defualt}.Edit";
            public const string Delete = $"{Defualt}.Delete";
        }
        public static class Products
        {
            public const string Defualt = $"{GroupName}.Products";
            public const string Add = $"{Defualt}.Add";
            public const string Edit = $"{Defualt}.Edit";
            public const string Delete = $"{Defualt}.Delete";
        }
        public static class Categories
        {
            public const string Defualt = $"{GroupName}.Categories";
            public const string Add = $"{Defualt}.Add";
            public const string Edit = $"{Defualt}.Edit";
            public const string Delete = $"{Defualt}.Delete";
        }

    }
}
