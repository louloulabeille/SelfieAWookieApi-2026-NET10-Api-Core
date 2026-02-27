namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    public static class CorsSelfieExtend
    {
        public const string DefaultPolicyName = "DefaultPolicyName";


        extension( IServiceCollection services)
        {
            public IServiceCollection AddCorsOrigin()
            {
                return services.AddCors(options =>
                {
                    options.AddPolicy("DefaultPolicyName", builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
                });
            }
        }
    }
}
