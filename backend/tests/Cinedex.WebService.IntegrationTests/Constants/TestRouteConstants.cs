namespace Cinedex.WebService.IntegrationTests.Constants;

internal static class TestRouteConstants
{
    public const string CinedexServiceBasePath = "/cinedex-svc";

    public static class Title
    {
        public const string Endpoint = $"{CinedexServiceBasePath}/titles";
        public const string LocationPrefix = "/titles/";
    }

    public static class Genre
    {
        public const string Endpoint = $"{CinedexServiceBasePath}/genres";
        public const string LocationPrefix = "/genres/";
    }
}