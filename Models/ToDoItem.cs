using System;
using System.Collections.Generic;

namespace dotnet_backend
{
    public partial class ToDoItem
    {
        public int UserId { get; set; }
        public string Description { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
