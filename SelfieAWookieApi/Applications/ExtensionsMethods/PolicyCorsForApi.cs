namespace SelfieAWookieApi.Applications.ExtensionsMethods
{

    // Cors sécurity pour l'API
    // mettre en place le système CORS au niveau du serveur IIS
    // dans le fichier IIS_Verbs.config
    // https://gist.github.com/evan-boissonnot/644aca18f963e1e221a51802675aeef0
    public static class PolicyCorsForApi
    {
        #region
        public const string Default_Policy = "Default_Policy";
        public const string Url_Policy = "Url_Policy";
        public const string Requete_Policy = "Requete_Policy";
        #endregion


        extension(IServiceCollection services)
        {
            #region méthode d'extension pour la configuration de CORS
            public IServiceCollection AddPolicyCorsForApi(IConfiguration builder)
            {
                //var toto = builder["Cors:Origin"];

                return services.AddCors(options =>
                {
                    options.AddPolicy(Default_Policy,
                    //policy=>
                    //{
                    //    policy.AllowAnyOrigin()
                    //           .AllowAnyHeader()
                    //           .AllowAnyMethod();
                    //})

                    // on peut spécifier les origines autorisées
                    policy =>
                    {
                        policy.WithOrigins("http://127.0.0.1:5500")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });

                    //options.AddPolicy(Url_Policy,
                    //    policy =>
                    //    {
                    //        policy.WithOrigins("http://127.0.0.1:5501")
                    //          .AllowAnyHeader()
                    //          .AllowAnyMethod();
                    //    });
                    //options.AddPolicy(Requete_Policy,
                    //    policy =>
                    //    {
                    //        policy.WithOrigins("http://127.0.0.1:5502")
                    //        .AllowAnyHeader()
                    //        .AllowAnyMethod();
                    //    });
                }
                    
                ); 

            }

            #endregion
        }
    }
}
