using Auth.Application.ExternalServices;
using Auth.Domain.Entities;
using Auth.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Implementation
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationContext _context;

        public SessionRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Session> Create(Session session)
        {
            var entity = _context.Sessions.Add(session);

            await _context.SaveChangesAsync();

            return entity.Entity;
        }

        public async Task<Session?> GetSessionByUserId(long userId)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.UserId == userId);

            return session;
        }

        public async Task<Session?> GetSessionByRefreshToken(string refreshToken)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.RefreshToken == refreshToken && s.ExpiresDate > DateTime.UtcNow);

            return session;
        }

        public async Task UpdateSession(Session session)
        {
            _context.Sessions.Update(session);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteSession(Session session)
        {
            _context.Sessions.Remove(session);

            await _context.SaveChangesAsync();
        }

        public async Task<Session?> GetSessionByAccessToken(string accessToken)
        {
            var session = await _context.Sessions.FirstOrDefaultAsync(s => s.AccessToken == accessToken);

            return session;
        }
    }
}
