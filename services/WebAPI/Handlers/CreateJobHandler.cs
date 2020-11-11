using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebAPI.Commands;
using WebAPI.Models;

namespace WebAPI.Handlers
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand>
    {
        private readonly JobContext _jobContext;

        public CreateJobHandler(JobContext jobContext)
        {
            _jobContext = jobContext;
        }

        public async Task<Unit> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var newJob = JobItem.From(request);
            var newOutboxItem = OutboxItem.From(newJob);
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
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }

            return Unit.Value;
        }
    }
}
