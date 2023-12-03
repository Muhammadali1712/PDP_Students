using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PDP_Students.Application.Servise;
using PDP_Students.Domain.Entities;
using PDP_Students.Domain.Models;
using PDP_Students.Infrasturucture.DataAcces;

namespace PDP_Students.Infrasturucture.Servises;

public class StudentServise : IStudentServise
{
    private readonly ITokenServise _tokenServise;
    private readonly PDP_StudentDbContext _db;
    private readonly int _lifeTime;

    public StudentServise(ITokenServise tokenServise, PDP_StudentDbContext db, IConfiguration configuration)
    {
        _tokenServise = tokenServise;
        _db = db;
        _lifeTime = int.Parse(configuration["JWT:RefreshTokenLifeTimeInMinute"]);
    }

    public Task<ResponseModel<bool>> DeleteAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<IEnumerable<Student>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseModel<Student>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsValidRefreshToken(string refreshToken, int userId)
    {
        RefreshToken token;
        var res = _db.RefreshTokens.Where(x => x.StudentId.Equals(userId) &&
                                             x.RefreshTokenValue.Equals(refreshToken));
        if (res.Count() != 1) return false;
        token = res.First();
        if (token.ExpiredTime < DateTime.Now)
        {
            _db.RefreshTokens.Remove(token);
            _db.SaveChanges();
            return false;
        }
        return true;
    }

    public async Task<ResponseModel<Token>> LoginAsync(Credential credential)
    {
        credential.Password = _tokenServise.ComputeSha(credential.Password);
        Student? student = _db.Students.Include(x=>x.Roles).SingleOrDefault(x=>x.Password == credential.Password&&
        x.UserName.Equals(credential.UserName));
        if (student == null) return new("Not Found");

        Token token =await _tokenServise.GenerateTokenAsync(student);
        bool success =await SaveRefreshToken(token.RefreshToken, student);
        return success ? new(token) : new("Failed");
    }

    public async Task<ResponseModel<bool>> LogOutAsync()
    {
        return new(true);
    }

    public async Task<ResponseModel<Token>> RefreshTokenAsync(Token token)
    {
        Student student = await _tokenServise.GetClaimFromExpiredTokenAsync(token.AccesToken);
        if (!await IsValidRefreshToken(token.RefreshToken, student.Id))
            return new("Refresh Token Invalid");
        Token token1 = await _tokenServise.GenerateTokenAsync(student);
        bool isSucces = await SaveRefreshToken(token1.RefreshToken, student);
        return isSucces ? new(token1) : new("Failed");
    }

    public async Task<ResponseModel<GetRegisterModel>> RegisterAsync(Student student)
    {
        student.Password = _tokenServise.ComputeSha(student.Password);
        //student.Roles = _db.Roles.Where(x => x.Id == student.Id).ToList();
         _db.Students.Attach(student);
        int effektedRows = await _db.SaveChangesAsync();
        if (effektedRows <= 0) return new("Amalga oshmadi");
        Token token = await _tokenServise.GenerateTokenAsync(student);
        bool isSucces = await SaveRefreshToken(token.RefreshToken, student);
        GetRegisterModel model = new GetRegisterModel()
        {
            token = token,
            student = student,
        };
        return isSucces ? new(model) : new("XATO");
    }

    public async Task<bool> SaveRefreshToken(string refreshToken,  Student entity)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return false;
        RefreshToken token;
        var res = _db.RefreshTokens.Where(x => x.StudentId.Equals(entity.Id));
        if (res.Count() == 0)
        {
            token = new()
            {
                RefreshTokenValue = refreshToken,
                ExpiredTime = DateTime.Now.AddMinutes(_lifeTime),
                StudentId = entity.Id,
            };
            await _db.RefreshTokens.AddAsync(token);
        }
        else if (res.Count() == 1)
        {
            token = res.First();
            token.RefreshTokenValue = refreshToken;
            token.ExpiredTime = DateTime.Now.AddMinutes(_lifeTime);
            _db.RefreshTokens.Update(token);
        }
        else return false;
        int rows = await _db.SaveChangesAsync();
        return rows > 0;
    }

    public Task<ResponseModel<bool>> UpdateAsync(Student entity)
    {
        throw new NotImplementedException();
    }
}
