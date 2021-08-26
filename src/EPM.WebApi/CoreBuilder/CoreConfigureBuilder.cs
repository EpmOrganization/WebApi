using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace EPM.WebApi.CoreBuilder
{
    public class CoreConfigureBuilder : ICoreConfigurationBuilder
    {
        private readonly IApplicationBuilder _app;
        private readonly IConfiguration _configuration;

        public CoreConfigureBuilder(IApplicationBuilder app, IConfiguration configuration)
        {
            _app = app;
            _configuration = configuration;
        }


        public void UseSwagger()
        {
            _app.UseSwagger();
            _app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPM.WebApi v1"));
        }
    }
}
