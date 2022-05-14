using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options=>{
 options.AddPolicy("CORSPolicy",
 builder=>
 {
  builder
  .AllowAnyMethod()
  .AllowAnyHeader()
  .WithOrigins("http://localhost:3000","https://appname.azurestaticapps.net");

 });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
 swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ASP.NET React ", Version = "v1" });

});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
 swaggerUIOptions.DocumentTitle = "ASP.NET REACT";
 swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", " My Web api ");
 swaggerUIOptions.RoutePrefix = string.Empty;



});
app.UseHttpsRedirection();
app.UseCors("CORSPolicy");
app.MapGet("/get-all-posts", async () => await PostsRepository.GetPostsAsync())
.WithTags("Post Endpoints");
app.MapGet("/get-post-by-id/{postId}", async (int postId) =>
{
 Post postToReturn = await PostsRepository.GetPostByIdAsync(postId);
 if (postToReturn != null)
 {
  return Results.Ok(postToReturn);
 }
 else
 {
  return Results.BadRequest();
 }

}).WithTags("Post Endpoints");

app.MapPost("/create-post", async (Post postToCreate) =>
{
 bool createSuccessful = await PostsRepository.CreatePostAsync(postToCreate);

 if (createSuccessful)
 {
  return Results.Ok("Create successsful");
 }
 else
 {
  return Results.BadRequest();
 }

}).WithTags("Post Endpoints");


app.MapPut("/update-post", async (Post postToUpdate) =>
{
 bool updateSuccessful = await PostsRepository.UpdatePostAsync(postToUpdate);

 if (updateSuccessful)
 {
  return Results.Ok("Update successsful");
 }
 else
 {
  return Results.BadRequest();
 }
}).WithTags("Post Endpoints");


app.MapDelete("/delete-post-by-id/{postId}", async (int postId) =>
{
 bool DeleteSuccessful = await PostsRepository.DeletePostAsync(postId);

 if (DeleteSuccessful)
 {
  return Results.Ok("Delete successsful");
 }
 else
 {
  return Results.BadRequest();
 }
}).WithTags("Post Endpoints");


app.Run();

