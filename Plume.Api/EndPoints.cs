namespace Plume.Api;

public static class EndPoints
{
    public static class Articles
    {
        private const string Base = "api/articles";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";

        // User-scoped (current author)
        public const string UserCreate = $"{Base}/me";
        public const string UserGetAll = $"{Base}/me";
        public const string UserGet = $"{Base}/me/{{id:guid}}";
        public const string UserUpdate = $"{Base}/me/{{id:guid}}";
        public const string UserDelete = $"{Base}/me/{{id:guid}}";
    }
}
