﻿namespace Gamestore.BLL.Models;

public record RoleModel
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public List<string>? Permissions { get; set; }
}
