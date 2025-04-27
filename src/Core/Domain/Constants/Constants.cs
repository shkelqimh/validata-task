namespace Domain.Constants;

public static class Constants
{
    public static class Customer
    {
        public const int FirstNameMaxLength = 20;
        public const int LastNameMaxLength = 20;
        public const int AddressMaxLength = 40;
        public const int ZipCodeMaxLength = 6;
    }
    
    public static class Product
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 20;
    }
}