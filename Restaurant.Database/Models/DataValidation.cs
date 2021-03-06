﻿namespace Restaurant.Database.Models
{
    public class DataValidation
    {
        public static class Item
        {
            public const int NameMaxLength = 50;
        }

        public static class Role
        {
            public const int NameMaxLength = 15;
        }

        public static class User
        {
            public const int NameMaxLength = 30;

            public const int UsernameMaxLength = 20;
        }

        public static class Category
        {
            public const int NameMaxLength = 30;
        }

        public static class Payment
        {
            public const int DesriptionMaxLength = 30;
        }
    }
}
