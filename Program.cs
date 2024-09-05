class Movie
{
  public string Title {get; set;}

  public Movie(string title)
  {
    Title = title;
  }
}

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton<List<Movie>>();
        var app = builder.Build();

        // READ: Get all movies
        app.MapGet("/movies", (List<Movie> movies) => movies);

        // CREATE: Adds a new movie
        app.MapPost("/movies", (Movie? movie, List<Movie> movies) => 
        {
          if (movie == null)
          {
            return Results.BadRequest();
          }

          movies.Add(movie);

          return Results.Created();
        });

        // DELETE: Delete a movie with id
        app.MapDelete("/movies/{id}", (int Id) => $"Delete movie with id: {Id}");

        // UPDATE: Update a movie
        app.MapPut("/movies/{id}", (int Id) => $"Update movie with id: {Id}");

        // System status
        app.MapGet("/health", () => "System healthy");

        app.Run();
    }
}