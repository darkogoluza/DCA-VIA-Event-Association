using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Core.Tools.ObjectMapper;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries;
using ViaEventAssociation.Presentation.WebAPI.Extensions;
using ViaEventAssociation.Presentation.WebAPI.Mappers;
using ViaEventAssociation.Presentation.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.RegisterDatabase();
builder.Services.RegisterRepositories();
//builder.Services.RegisterCQRS();
builder.Services.RegisterCommandHandlers();
builder.Services.RegisterDispatcher();
builder.Services.RegisterCurrentDateTime();

// Mapper
builder.Services
    .AddTransient<IMappingConfig<BrowseUpcomingEventsEndpointRequest, BrowseUpcomingEvents.Query>,
        BrowseUpcomingEventsRequestMapping>();
builder.Services
    .AddTransient<IMappingConfig<BrowseUpcomingEvents.Answer, BrowseUpcomingEventsEndpointResponse>,
        BrowseUpcomingEventsResponseMapping>();
builder.Services.AddTransient<
    IMappingConfig<EventsEditingOverview.Answer, EventsEditingOverviewEndpointResponse>,
    EventsEditingOverviewResponseMapping>();
builder.Services.AddTransient<
    IMappingConfig<GuestOverview.Answer, GuestOverviewEndpointResponse>,
    GuestOverviewResponseMapping>();
builder.Services.AddTransient<
    IMappingConfig<GuestOverview.Query, GuestOverviewEndpointRequest>,
    GuestOverviewQueryMapping>();
builder.Services.AddTransient<
    IMappingConfig<PersonalProfilePage.Query, PersonalProfilePageEndpointRequest>,
    PersonalProfilePageQueryMapping>();
builder.Services.AddTransient<
    IMappingConfig<PersonalProfilePageEndpointResponse, PersonalProfilePage.Answer>,
    PersonalProfilePageResponseMapping>();
builder.Services.AddTransient<
    IMappingConfig<ViewSingleEvent.Query, ViewSingleEventEndpointRequest>,
    ViewSingleEventQueryMapping>();
builder.Services.AddTransient<
    IMappingConfig<ViewSingleEventEndpointResponse, ViewSingleEvent.Answer>,
    ViewSingleEventResponseMapping>();

builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName.Replace("+", "."));
});

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
