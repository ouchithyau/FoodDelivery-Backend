var builder = WebApplication.CreateBuilder(args);

// Add YARP Reverse Proxy services
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseHttpsRedirection();

// Map YARP proxy routes
app.MapReverseProxy();

app.Run();