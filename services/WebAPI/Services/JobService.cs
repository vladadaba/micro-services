using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Messaging;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class JobService
    {
        private readonly JobContext _jobContext;
        private readonly MessageProducer _producer;

        public JobService(JobContext jobContext, MessageProducer producer)
        {
            _jobContext = jobContext;
            _producer = producer;
        }

        public async Task CreateJob(JobItem newJob)
        {
            await using var transaction = await _jobContext.Database.BeginTransactionAsync();
            try
            {
                _jobContext.Add(newJob);
                _producer.SendMessage(newJob.Name);
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
