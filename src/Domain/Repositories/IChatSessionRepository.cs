using Domain.Entities;
using Domain.Primitives;

namespace Domain.Repositories;

public interface IChatSessionRepository
{
    Task Create(ChatSession chatSession);
    Task<Maybe<ChatSession>> GetById(string id);
}