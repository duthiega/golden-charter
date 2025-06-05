using GoldenCharter.DocumentGenerator.Core.Decorators;
using GoldenCharter.DocumentGenerator.Core.Deserialisers;
using GoldenCharter.DocumentGenerator.Core.Interfaces;
using GoldenCharter.DocumentGenerator.Core.Options;
using GoldenCharter.DocumentGenerator.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TemplateOptions>(
    builder.Configuration.GetSection("TemplateOptions"));


builder.Services.PostConfigure<TemplateOptions>(opts =>
{
    if (!Path.IsPathRooted(opts.TemplatePath))
    {
        opts.TemplatePath = Path.Combine(builder.Environment.ContentRootPath, opts.TemplatePath);
    }
});



builder.Services.AddSingleton<IDocumentService, DocumentService>();
builder.Services.AddSingleton<ITemplateRepository, TemplateRepository>();
builder.Services.AddSingleton<IDocumentRequestDeserialiser, DanDocumentDeserialiser>();
builder.Services.AddSingleton<IDocumentRequestDeserialiser, IddDocumentDeserialiser>();
builder.Services.AddSingleton<IDocumentRequestDeserialisationResolver, DocumentRequestDeserialisationResolver>();


builder.Services.Decorate<IDocumentService, DocumentServiceExceptionLoggingDecorator>();
builder.Services.Decorate<IDocumentRequestDeserialisationResolver, DocumentRequestDeserialisationResolverExceptionLoggingDecorator>();
builder.Services.Decorate<ITemplateRepository, TemplateRepositoryExceptionLoggingDecorator>();




// Optional: Add CORS for local dev/testing
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(); // If added above
app.UseAuthorization();

app.MapControllers();

app.Run();
