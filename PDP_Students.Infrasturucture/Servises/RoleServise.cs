using Microsoft.EntityFrameworkCore;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Infrasturucture.DataAcces;

namespace PDP_Students.Infrasturucture.Servises;

public class RoleServise : IRoleServise
{
    private readonly PDP_StudentDbContext _context;

    public RoleServise(PDP_StudentDbContext context)
    {
        _context = context;
    }
    public async Task<Role> CreateAsync(Role entity)
    {
        _context.Attach(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<bool> DeleteAsync(int Id)
    {
        Role? entity = await _context.Roles.FindAsync(Id);
        if (entity == null)
            return false;

        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        IEnumerable<Role> roles = _context.Roles.Include(x => x.Students)
            .AsNoTracking().AsEnumerable().OrderBy(x => x.Id);

        return roles;
    }

    public async Task<Role> GetByIdAsync(int id)
    {
        Role? roleEntity = await _context.Roles.Include(x => x.Students)
            .FirstOrDefaultAsync(x => x.Id == id);
        return roleEntity;
    }

    public async Task<bool> UpdateAsync(Role entity)
    {
        _context.Roles.Update(entity);
        int executedRows = await _context.SaveChangesAsync();

        return executedRows > 0;
    }
}

