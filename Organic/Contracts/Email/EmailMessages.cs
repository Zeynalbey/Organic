namespace Organic.Contracts.Email
{
    public static class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Hesabin aktivlesdirilmesi";
            public const string NEWPASSWORD_MESSAGE = $"Yeni şifrənin yaradılması";
        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Sizin activation urliniz : {EmailMessageKeywords.ACTIVATION_URL}";
            public const string NEWPASSWORD_MESSAGE = $"Yeni şifrə yazmaq üçün keçid edin : {EmailMessageKeywords.ACTIVATION_URL}";
        }
    }
}
