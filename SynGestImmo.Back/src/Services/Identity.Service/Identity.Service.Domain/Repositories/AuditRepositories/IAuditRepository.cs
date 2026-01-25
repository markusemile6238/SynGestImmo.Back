using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories
{
    public interface IAuditRepository
    {
        // ========== COMMANDS ==========
        Task<CqsResult<Guid>> CreateAsync(AuditLog auditLog);
        Task<CqsResult> CleanupOldLogsAsync(int daysToKeep);

        // ========== QUERIES ==========
        Task<CqsResult<AuditLog>> GetByIdAsync(Guid id);
        Task<CqsResult<IEnumerable<AuditLog>>> GetByUserIdAsync(Guid userId, int limit = 100);
        Task<CqsResult<IEnumerable<AuditLog>>> GetByEntityAsync(string entityType, Guid entityId, int limit = 100);
        Task<CqsResult<IEnumerable<AuditLog>>> GetByActionAsync(string action, DateTime? startDate = null, DateTime? endDate = null);
        Task<CqsResult<IEnumerable<AuditLog>>> GetBetweenDatesAsync(DateTime startDate, DateTime endDate, int page = 1, int pageSize = 50);
        Task<CqsResult<int>> CountAsync(string? entityType = null, string? action = null, DateTime? startDate = null, DateTime? endDate = null);
    }
}
