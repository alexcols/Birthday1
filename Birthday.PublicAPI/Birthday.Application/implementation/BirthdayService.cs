using Advertisement.Domain.Shared.Exceptions;
using Birthday.Application.contracts;
using Birthday.Application.interfaces;
using Birthday.Application.repositories;
using Birthday.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.Application.implementation
{
    public class BirthdayService : IBirthdayService
    {
        private readonly IBirthdayRepository _birthdayRepository;
        private readonly IRepository<Person, int> _repository;

        public BirthdayService(IBirthdayRepository birthdayRepository, IRepository<Person, int> repository)
        {
            _birthdayRepository = birthdayRepository;
            _repository = repository;
        }

        public async Task<CreateBirthday.Response> Create(CreateBirthday.Request request, CancellationToken cancellationToken)
        {
           
            string dateStringWithoutYear;
            string dateString;

            dateStringWithoutYear =request.Day.ToString() + "." + request.Month.ToString()+".0004";
            dateString = request.Day.ToString() + "." + request.Month.ToString() +"."+ request.Year.ToString();
            
            DateTime dateWithoutYear = CheckDate(dateStringWithoutYear);
            DateTime? date;



            if (request.Year == null || request.Year == 0)
            {
                date = null;
            }
            else
            {
                date = CheckDate(dateString);
            }
         
            
            //parseDate = DateTime.TryParse(dateString, out dateTime);
            var birthday = new Domain.Person
            {
                Name = request.Name,
                SecondName = request.SecondName,
                DateWithoutYear = dateWithoutYear,
                Date = date
            };

            await _repository.Save(birthday, cancellationToken);

            return new CreateBirthday.Response
            {
                Id = birthday.Id
            };
            
        }

        public Task Delete(DeleteBirthday.Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Edit(EditBirthday.Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<GetPagedBirthday.Response> GetPaged(GetPagedBirthday.Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //Checking date 
        private DateTime CheckDate(string date)
        {
            DateTime dateTime;
            bool parse = DateTime.TryParse(date, out dateTime);
            if (!parse)
            {
                throw new ConflictException("Date is not exist");
            }

            return dateTime;
        }
    }
}
