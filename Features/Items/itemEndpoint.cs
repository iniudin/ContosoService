namespace ContosoService.Features.Items;

public static class ItemEndpoints
{
    static async Task<IResult> GetItemList(IItemService service)
    {
        var items = await service.List();
        return TypedResults.Ok(items);
    }

    static async Task<IResult> GetItem(int id, IItemService service)
    {
        var item = await service.Find(id);
        return item is not null ? TypedResults.Ok(item) : TypedResults.NotFound();
    }

    static async Task<IResult> CreateItem(ItemDto dto, IItemService service)
    {
        var newItem = await service.Create(dto);
        return TypedResults.Created($"/items/{newItem.Id}", newItem);
    }

    static async Task<IResult> UpdateItem(int id, ItemDto dto, IItemService service)
    {
        var updated = await service.Update(id, dto);
        return updated is not null ? TypedResults.Ok(updated) : TypedResults.NotFound();
    }

    static async Task<IResult> DeleteItem(int id, IItemService service)
    {
        var deleted = await service.Delete(id);
        return deleted ? TypedResults.NoContent() : TypedResults.NotFound();
    }

        public static RouteGroupBuilder MapItemEndpoints(this IEndpointRouteBuilder routes)
    {
        var items = routes.MapGroup("/items");

        items.MapGet("/", GetItemList);
        items.MapGet("/{id:int}", GetItem);
        items.MapPost("/", CreateItem);
        items.MapPut("/{id:int}", UpdateItem);
        items.MapDelete("/{id:int}", DeleteItem);

        return items;
    }
}
