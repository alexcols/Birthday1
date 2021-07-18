using Advertisement.Domain.Shared.Exceptions;
using Birthday.Application.contracts;
using Birthday.Application.contracts.Exceptions;
using Birthday.Application.interfaces;
using Birthday.Application.repositories;
using Birthday.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
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

          
            DateTime dateWithoutYear = CheckDates(request.Day, request.Month, request.Year, out DateTime? date);
            Person birthday = new Domain.Person
            {
                Name = request.Name,
                SecondName = request.SecondName,
                DateWithoutYear = dateWithoutYear,
                Date = date
            }; 

            // Add Photo to bite's array
            if (request.Photo != null && request.Photo.Length > 0)
            {
                try
                {
                    await using (var target = new MemoryStream())
                    {
                        request.Photo.CopyTo(target);

                        birthday.PhotoName = Path.GetFileName(request.Photo.FileName);
                        birthday.PhotoType = Path.GetExtension(birthday.PhotoName);
                        birthday.PhotoGuid = Guid.NewGuid();
                        birthday.PhotoContent = target.ToArray();
                    }

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }
            
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


            birthday.Name = request.Name;
            birthday.SecondName = request.SecondName;
            birthday.Date = date;
            birthday.DateWithoutYear = dateWithoutYear;
              

            // Add Photo to bite's array
            if (request.Photo != null && request.Photo.Length > 0)
            {
                try
                {
                    await using (var target = new MemoryStream())
                    {
                        request.Photo.CopyTo(target);

                        birthday.PhotoName = Path.GetFileName(request.Photo.FileName);
                        birthday.PhotoType = Path.GetExtension(birthday.PhotoName);
                        birthday.PhotoGuid = Guid.NewGuid();
                        birthday.PhotoContent = target.ToArray();
                    }

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            await _repository.Save(birthday, cancellationToken);
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

        //Checking dates
        private DateTime CheckDates(int day, int month, int? year, out DateTime? date)
        {
            string dateStringWithoutYear = day.ToString() + "." + month.ToString() + ".0004";
            string dateString = day.ToString() + "." + month.ToString() + "." + year.ToString();

            DateTime dateWithoutYear;

            bool parse1 = DateTime.TryParse(dateStringWithoutYear, out dateWithoutYear);

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
                bool parse = DateTime.TryParse(dateString, out dateParse);

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
