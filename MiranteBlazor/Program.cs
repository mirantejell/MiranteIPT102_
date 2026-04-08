using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiranteBlazor.Components;
using MiranteBlazor.Components.Account;
using MiranteBlazor.Data;
using Domain.Commands;
using Framework.Commands;
using Domain.Queries;
using Framework.Queries;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<ICreateBook, CreateBook>();
builder.Services.AddScoped<IDeleteBook, DeleteBook>();
builder.Services.AddScoped<IUpdateBook, UpdateBook>();
builder.Services.AddScoped<IGetAllBooks, GetAllBooks>();
builder.Services.AddScoped<IReadBookById, ReadBookById>();

// Student services
builder.Services.AddScoped<Domain.Commands.ICreateStudent, Framework.Commands.CreateStudent>();
builder.Services.AddScoped<Domain.Commands.IUpdateStudent, Framework.Commands.UpdateStudent>();
builder.Services.AddScoped<Domain.Commands.IDeleteStudent, Framework.Commands.DeleteStudent>();
builder.Services.AddScoped<Domain.Queries.IGetAllStudents, Framework.Queries.GetAllStudents>();
builder.Services.AddScoped<Domain.Queries.IReadStudentById, Framework.Queries.ReadStudentById>();

// Repository
var repoConnStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<Repository.Interfaces.IRepository>(new Repository.Repository(repoConnStr));
builder.Services.AddSingleton(new MiranteBlazor.Data.DatabaseInitializer(repoConnStr));





builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// Initialize database tables and stored procedures
await app.Services.GetRequiredService<MiranteBlazor.Data.DatabaseInitializer>().InitializeAsync();

app.Run();
