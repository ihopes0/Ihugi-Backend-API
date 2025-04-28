using Ihugi.Domain.Abstractions;
using Ihugi.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Ihugi.Infrastructure.BackgroundJobs;

// TODO: XML docs
[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(IPublisher publisher, AppDbContext dbContext)
    {
        _publisher = publisher;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                outboxMessage.Content,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

            // TODO: добавить логирование
            if (domainEvent is null)
            {
                continue;
            }

            // TODO: добавить обработку ошибки
            try
            {
                await _publisher.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                continue;
            }

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}