using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace infrastructure.services.jobs;

[DisallowConcurrentExecution]
public class LoggingBackgroundJob : IJob
{
    private readonly ILogger<LoggingBackgroundJob> _logger;
    private readonly AppDbContext _dbContext;

    public LoggingBackgroundJob(
        ILogger<LoggingBackgroundJob> logger, 
        AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var topTags = await _dbContext.Tags.OrderByDescending(t => t.TagCount).Take(5).ToListAsync();
        var loggingInfo = new StringBuilder();

        loggingInfo.AppendLine("====================== Top Tags ============================");
        foreach (var tag in topTags)
        {
            loggingInfo.AppendLine($"{tag.TagName}: {tag.TagCount}");
        }
        
        _logger.LogInformation(loggingInfo.ToString());
    }
}

public class LoggingBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobkey = JobKey.Create(nameof(LoggingBackgroundJob));
        options
            .AddJob<LoggingBackgroundJob>(builder => builder.WithIdentity(jobkey))
            .AddTrigger(trigger 
                => trigger.ForJob(jobkey)
                    .WithSimpleSchedule(schedule 
                        => schedule.WithIntervalInMinutes(20)
                            .RepeatForever()));
    }
}