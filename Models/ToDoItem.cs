using System;
using System.Collections.Generic;

namespace dotnet_backend
{
    public partial class ToDoItem
    {
        public string UserEmail { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual User UserEmailNavigation { get; set; } = null!;
    }
}
