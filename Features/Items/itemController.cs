using Microsoft.AspNetCore.Mvc;

namespace ContosoService.Features.Items;

[ApiController]
[Route("api/items")]
public class ItemController(IItemService service) : ControllerBase
{
    private readonly IItemService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<Item>>> GetItemList()
    {
        return Ok(await _service.GetItems());
    }

    [HttpGet("{parentId:int}/variants")]
    public async Task<ActionResult<List<Item>>> GetVariants(int parentId)
    {
        return Ok(await _service.GetVariants(parentId));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Item>> GetItem(int id)
    {
        var item = await _service.GetItem(id);
        return item is not null ? Ok(item) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Item>> CreateItem(CreateItemDto dto)
    {
        return Ok(await _service.CreateItem(dto));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Item>> UpdateItem(int id, UpdateItemDto dto)
    {
        var item = await _service.UpdateItem(id, dto);
        return item is not null ? Ok(item) : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteItem(int id)
    {
        if (await _service.DeleteItem(id)) return NoContent();
        return NotFound();
    }

}
