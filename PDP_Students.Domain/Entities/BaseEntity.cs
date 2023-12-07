using PDP_Students.Domain.States;
using System.ComponentModel;

namespace PDP_Students.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
	[DisplayName("Full Name")]

	public string FullName { get; set; }
    public Gender Gender { get; set; }
    [DisplayName("User Name")]
    public string UserName { get; set; }
    public string? Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? City { get; set; }

}
