namespace ServiceFinder.API.DI
{
    public class ConstraintValues
    {
        public const int MaximumTitleLength = 100;
        public const int MaximumNameLength = 50;
        public const int MinimumRating = 1;
        public const int MaximumRating = 5;
        public const string PhoneNumberPattern = @"8\(0(29|44|33|25)\)\d{3}-\d{2}-\d{2}";
        public const decimal MinimumPrice = 0;
    }
}
