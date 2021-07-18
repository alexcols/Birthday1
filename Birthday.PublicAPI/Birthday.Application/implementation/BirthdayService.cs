using Advertisement.Domain.Shared.Exceptions;
using Birthday.Application.contracts;
using Birthday.Application.contracts.Exceptions;
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

            //string dateStringWithoutYear;
            //string dateString;

            //dateStringWithoutYear =request.Day.ToString() + "." + request.Month.ToString()+".0004";
            //dateString = request.Day.ToString() + "." + request.Month.ToString() +"."+ request.Year.ToString();

            //DateTime dateWithoutYear = CheckDate(dateStringWithoutYear);
            //DateTime? date;



            //if (request.Year == null || request.Year == 0)
            //{
            //    date = null;
            //}
            //else
            //{
            //    date = CheckDate(dateString);
            //}
            DateTime dateWithoutYear = CheckDates(request.Day, request.Month, request.Year, out DateTime? date);


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

        public async Task Delete(DeleteBirthday.Request request, CancellationToken cancellationToken)
        {
            var birthday = await _repository.FindById(request.Id, cancellationToken);
            if (birthday == null)
            {
                throw new NoBithdayFoundException(request.Id);
            }

            await _repository.Remove(birthday, cancellationToken);

        }

        public async Task Edit(EditBirthday.Request request, CancellationToken cancellationToken)
        {
            var birthday = await _repository.FindById(request.Id, cancellationToken);
            if (birthday == null)
            {
                throw new NoBithdayFoundException(request.Id);
            }

            DateTime dateWithoutYear = CheckDates(request.Day, request.Month, request.Year, out DateTime? date);

            await _repository.Save(new Person
            {
                Id = birthday.Id,
                Name = request.Name,
                SecondName = request.SecondName,
                Date = date,
                DateWithoutYear = dateWithoutYear,
                PhotoId = birthday.PhotoId

            }
                , cancellationToken);
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

        private DateTime CheckDates(int day, int month, int? year, out DateTime? date)
        {
            string dateStringWithoutYear = day.ToString() + "." + month.ToString() + ".0004";
            string dateString = day.ToString() + "." + month.ToString() + "." + year.ToString();

            DateTime dateWithoutYear;

            bool parse1 = DateTime.TryParse(dateString, out dateWithoutYear);

            if (!parse1)
            {
                throw new ConflictException("Date is not exist");
            }

            if (year == null || year == 0)
            {
                date = null;
            }
            else
            {
                DateTime dateParse;
                bool parse = DateTime.TryParse(dateStringWithoutYear, out dateParse);

                if (!parse)
                {
                    throw new ConflictException("Date is not exist");
                }
                date = dateParse;
            }

            return dateWithoutYear;
        }
    }
}
