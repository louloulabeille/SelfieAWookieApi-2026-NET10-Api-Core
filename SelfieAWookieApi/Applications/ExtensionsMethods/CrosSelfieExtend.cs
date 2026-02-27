namespace SelfieAWookieApi.Applications.ExtensionsMethods
{
    public static class CrosSelfieExtend
    {
        public static string DefaultPolicyName { get; } = "DefaultPolicyName";


        extension( IServiceCollection services)
        {
            public IServiceCollection AddCrossOrigin()
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
