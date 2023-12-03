using Microsoft.EntityFrameworkCore;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models;
using PDP_Students.Infrasturucture.DataAcces;

namespace PDP_Students.Infrasturucture.Servises;

public class StudentCRUDServise : IStudentCRUDServise
{
	private readonly PDP_StudentDbContext _context;

	public StudentCRUDServise(PDP_StudentDbContext context)
	{
		_context = context;
	}

	public async Task<Student> CreateAsync(Student entity)
	{
		_context.Attach(entity);
		await _context.SaveChangesAsync();
		return entity;
	}

	public async Task<bool> DeleteAsync(int Id)
	{
		Student? entity = await _context.Students.FindAsync(Id);
		if (entity == null)
			return false;

		_context.Remove(entity);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<IEnumerable<Student>> GetAllAsync()
	{
		IEnumerable<Student> students = _context.Students.Include(x => x.Mentor).OrderBy(x => x.Id);

		return students;
	}

	public async Task<Student> GetByIdAsync(int id)
	{
		Student? studentEntity = await _context.Students.Include(x => x.Mentor)
		   .FirstOrDefaultAsync(x => x.Id == id);
		return studentEntity;
	}

	public async Task<bool> UpdateAsync(Student entity)
	{

		_context.Students.Update(entity);
		int executedRows = await _context.SaveChangesAsync();

		return executedRows > 0;
	}
}
