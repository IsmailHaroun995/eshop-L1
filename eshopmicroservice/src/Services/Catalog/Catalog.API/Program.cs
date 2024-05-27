using BuildingBlocks.ValidationBehavior;
using Catalog.API.Datal;
var builder = WebApplication.CreateBuilder(args);
// Add Services to the container

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CataloginitData>();

builder.Services.AddHealthChecks();


var app = builder.Build();
// configure the http request  pipelineeLightweightSessions();

app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health");
app.Run();
