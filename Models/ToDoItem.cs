using System;
using System.Collections.Generic;

namespace dotnet_backend
{
    public partial class ToDoItem
    {
        public int ToDoId { get; set; }
        public string UserEmail { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool? IsActive { get; set; }
    }

    public class ToDoItemDTO
    {
        public bool? IsActive { get; set; }
        public string? Description { get; set; }
    }
}
