using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class JobService
    {
        private readonly JobContext _jobContext;

        public JobService(JobContext jobContext)
        {
            _jobContext = jobContext;
        }

        public async Task CreateJob(JobItem newJob)
        {
            await using var transaction = await _jobContext.Database.BeginTransactionAsync();
            try
            {
                _jobContext.JobItems.Add(newJob);
                _jobContext.OutboxItems.Add(OutboxItem.FromJob(newJob));
                
                await _jobContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        public Task<List<JobItem>> GetAll()
        {
            return _jobContext.JobItems.ToListAsync();
        }

        public ValueTask<JobItem> Get(Guid id)
        {
            return _jobContext.JobItems.FindAsync(id);
        }
    }
}
