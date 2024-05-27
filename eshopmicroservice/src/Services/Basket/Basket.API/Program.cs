var builder = WebApplication.CreateBuilder(args);
// Add Services to the container
var app = builder.Build();

// configure the http request pipline

app.Run();
