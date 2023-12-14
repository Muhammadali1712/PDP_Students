using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models;

namespace PDP_Students.UI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleServise _roleServise;

    public RoleController(IRoleServise roleServise)
    {
        _roleServise = roleServise ?? throw new ArgumentNullException(nameof(roleServise));
    }
    [HttpGet]
   //[EnableCors("corspolicy")]
   public async Task<IEnumerable<Role>> GetAll()
        =>await _roleServise.GetAllAsync();
  /*
    [HttpGet]
    public async Task<ResponseModel<Role>> GetById(int id)
        =>await _roleServise.GetByIdAsync(id);
    [HttpPost]
    public async Task<ResponseModel<Role>> CreateRole(string roleName)
    {
        Role role = new()
        {
            Name = roleName,
        };
        return await _roleServise.CreateAsync(role);
    }*/

    [HttpDelete]
    public async Task<bool> DeleteRole(int id)
        => await _roleServise.DeleteAsync(id);

    [HttpPatch]
    public async Task<bool> UpdateRole(Role role)
        => await _roleServise.UpdateAsync(role);

}
