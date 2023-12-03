using PDP_Students.Domain.Models;

namespace PDP_Students.Application.Repositories;

public interface IIdentityRepositories<T>
{
    Task<ResponseModel<GetRegisterModel>> RegisterAsync(T entity);
    Task<ResponseModel<Token>> LoginAsync(Credential credential);
    Task<ResponseModel<bool>> LogOutAsync();
    Task<ResponseModel<Token>> RefreshTokenAsync(Token token);
    Task<ResponseModel<bool>> UpdateAsync(T entity);
    Task<ResponseModel<bool>> DeleteAsync(int Id);
    Task<ResponseModel<IEnumerable<T>>> GetAllAsync();
    Task<ResponseModel<T>> GetByIdAsync(int id);

    Task<bool> SaveRefreshToken ( string refreshToken, T entity);
    Task<bool> IsValidRefreshToken(string refreshToken, int userId);
}
