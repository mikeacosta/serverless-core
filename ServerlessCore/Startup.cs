using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerlessCore.Libs.Mappers;
using ServerlessCore.Libs.Repositories;
using ServerlessCore.Services;

namespace ServerlessCore
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IContentService, ContentService>();
            services.AddSingleton<IJobService, JobService>();

            services.AddSingleton<IProfileRepository, ProfileRepository>();
            services.AddSingleton<IContentRepository, ContentRepository>();
            services.AddSingleton<IJobRepository, JobRepository>();
            services.AddSingleton<IMapper, Mapper>();

            services.AddAWSService<IAmazonS3>();
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddDefaultAWSOptions(new AWSOptions
            {
                Region = RegionEndpoint.GetBySystemName("us-west-2")
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
