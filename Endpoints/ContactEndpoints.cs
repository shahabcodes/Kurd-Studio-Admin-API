using KurdStudio.AdminApi.Repositories.Interfaces;

namespace KurdStudio.AdminApi.Endpoints;

public static class ContactEndpoints
{
    public static RouteGroupBuilder MapContactEndpoints(this RouteGroupBuilder group)
    {
        var contacts = group.MapGroup("/contacts").WithTags("Contacts").RequireAuthorization();

        contacts.MapGet("/", GetAll).WithName("AdminGetContacts");
        contacts.MapPut("/{id:int}/read", MarkAsRead).WithName("AdminMarkContactRead");
        contacts.MapDelete("/{id:int}", Delete).WithName("AdminDeleteContact");

        return group;
    }

    private static async Task<IResult> GetAll(IContactRepository repository, bool unread = false)
    {
        var contacts = await repository.GetAllAsync(unread);
        return Results.Ok(contacts);
    }

    private static async Task<IResult> MarkAsRead(int id, IContactRepository repository)
    {
        await repository.MarkAsReadAsync(id);
        return Results.Ok(new { Message = "Marked as read" });
    }

    private static async Task<IResult> Delete(int id, IContactRepository repository)
    {
        await repository.DeleteAsync(id);
        return Results.Ok(new { Message = "Deleted" });
    }
}
