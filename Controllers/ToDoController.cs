using Microsoft.AspNetCore.Mvc;

namespace dotnet_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{

    [HttpGet("{email}")]
    public List<ToDoItem> GetQuery(string email)
    {

        using (var context = new postgresContext())
        {
            var usersToDos = context.ToDoItems.Where(toDo => toDo.UserEmail == email).ToList();
            return usersToDos;
        }        
    }

    [HttpPost()]
    public async Task<ActionResult<ToDoItem>> AddToDoItem(ToDoItem newToDo)
    {
        using (var context = new postgresContext())
        {
            context.ToDoItems.Add(newToDo);
            await context.SaveChangesAsync();
            return CreatedAtAction("AddToDoItem", newToDo);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoItem(int id, ToDoItemDTO update)
    {
        using (var context = new postgresContext())
        {
            var toDoToUpdate = context.ToDoItems.FirstOrDefault(item => item.ToDoId == id);
            if (toDoToUpdate == null) return BadRequest();

            if (update.Description != null) toDoToUpdate.Description = update.Description;
            if (update.IsActive != null) toDoToUpdate.IsActive = update.IsActive;
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}