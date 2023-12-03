using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDP_Students.Domain.Models.MentorDTO;

public class MentorGetDTO:MentorBaseDTO
{
    public int Id { get; set; }
    public string UserName { get; set; }
}
