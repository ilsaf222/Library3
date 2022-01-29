using Library.Domain;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.Quartz
{
    public class OrdersCheker : IJob
    {
        private readonly IRepository<BookStatus> repository;
        private double Time24hours { get; } = new TimeSpan(24, 0, 0).TotalHours;

        public OrdersCheker(IRepository<BookStatus> repository)
        {
            this.repository = repository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var getAllBooksStatuses = await repository.GetAll()
                .Where(x => x.Status == Status.Booked)
                .ToListAsync();

            if (getAllBooksStatuses != null)
            {
                foreach (var bookStatus in getAllBooksStatuses)
                {
                    if (DateTime.Now.Subtract(bookStatus.LastTime).TotalHours >= Time24hours)
                    {
                        bookStatus.Status = Status.Avaliable;

                        await repository.UpdateAsync(bookStatus, context.CancellationToken);
                    }
                }
            }
        }
    }
}
