using System.Reflection;
using PP.Common.Configuration;
using PP.Core.Features.Arcana;
using PP.Core.Features.Character.Commands;
using PP.Core.Features.Commands;
using PP.Core.Features.Game.Commands;
using PP.Core.Features.Shared.Services;
using PP.Core.Features.SocialLink;
using PP.Core.Services;
using PP.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<PersonaConfigurationBuilder>();
builder.Services.AddScoped(s => s.GetService<PersonaConfigurationBuilder>()!.Build());
builder.Services.AddDbContext<IDbService, PersonaDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddApplicationPart(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(x => {
    x.RegisterServicesFromAssemblyContaining<PersonaCommandResponse>();
});
builder.Services.AddOpenApiDocument();

builder.Services.AddTransient<PersonaValidationLibrary>();

builder.Services.AddTransient<AddGameCommandValidator>();
builder.Services.AddTransient<UpdateGameCommandValidator>();
builder.Services.AddTransient<DeleteGameCommandValidator>();

builder.Services.AddTransient<AddCharacterCommandValidator>();
builder.Services.AddTransient<UpdateCharacterValidator>();
builder.Services.AddTransient<DeleteCharacterCommandValidator>();

builder.Services.AddTransient<ListArcanaQueryValidator>();
builder.Services.AddTransient<AddArcanaCommandValidator>();
builder.Services.AddTransient<UpdateArcanaCommandValidator>();
builder.Services.AddTransient<DeleteArcanaCommandValidator>();

builder.Services.AddTransient<ListSocialLinksQueryValidator>();
builder.Services.AddTransient<AddSocialLinkCommandValidator>();
builder.Services.AddTransient<UpdateSocialLinkCommandValidator>();
builder.Services.AddTransient<DeleteSocialLinkCommandValidator>();
builder.Services.AddTransient<AddSocialLinkDialogueCommandValidator>();
builder.Services.AddTransient<ListSocialLinkDialogueQueryValidator>();
builder.Services.AddTransient<DeleteSocialLinkDialogueCommandValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();