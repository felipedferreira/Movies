using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Genres.CreateGenre;
using Movies.Application.Genres.DeleteGenre;
using Movies.Application.Genres.GetGenreById;
using Movies.Application.Genres.ListGenres;
using Movies.Application.Genres.UpdateGenre;
using Movies.Application.Titles.CreateTitle;
using Movies.Application.Titles.DeleteTitle;
using Movies.Application.Titles.GetTitleById;
using Movies.Application.Titles.ListTitles;
using Movies.Application.Titles.UpdateTitle;

namespace Movies.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateTitleHandler, CreateTitleHandler>();
        services.AddScoped<IUpdateTitleHandler, UpdateTitleHandler>();
        services.AddScoped<IDeleteTitleHandler, DeleteTitleHandler>();
        services.AddScoped<IGetTitleByIdHandler, GetTitleByIdHandler>();
        services.AddScoped<IListTitlesHandler, ListTitlesHandler>();

        services.AddScoped<ICreateGenreHandler, CreateGenreHandler>();
        services.AddScoped<IUpdateGenreHandler, UpdateGenreHandler>();
        services.AddScoped<IDeleteGenreHandler, DeleteGenreHandler>();
        services.AddScoped<IGetGenreByIdHandler, GetGenreByIdHandler>();
        services.AddScoped<IListGenresHandler, ListGenresHandler>();

        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly,
            ServiceLifetime.Scoped,
            includeInternalTypes: true);

        return services;
    }
}