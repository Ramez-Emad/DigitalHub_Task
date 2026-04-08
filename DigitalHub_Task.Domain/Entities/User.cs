using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalHub_Task.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
