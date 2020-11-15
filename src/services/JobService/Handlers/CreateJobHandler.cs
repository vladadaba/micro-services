using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using JobService.Commands;
using JobService.Models;
using Serilog;

namespace JobService.Handlers
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand>
    {
        private readonly JobContext _jobContext;
        private readonly ILogger _logger;

        public CreateJobHandler(ILogger logger, JobContext jobContext)
        {
            _logger = logger;
            _jobContext = jobContext;
        }

        public async Task<Unit> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var newJob = JobItem.From(request);
            var newOutboxItem = OutboxItem.CreateNew(newJob);
            await using var transaction = await _jobContext.Database.BeginTransactionAsync();
            try
            {
                _jobContext.JobItems.Add(newJob);
                _jobContext.OutboxItems.Add(newOutboxItem);

                await _jobContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Adding job to database failed");
            }

            return Unit.Value;
        }
    }
}
