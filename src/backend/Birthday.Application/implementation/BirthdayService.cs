using Birthday.Application.contracts;
using Birthday.Application.contracts.Exceptions;
using Birthday.Application.interfaces;
using Birthday.Application.repositories;
using Birthday.Domain;
using Birthday.Domain.Shared.Exceptions;
using System;
using System.IO;
using System.Linq;
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

            //for sorting
            var dateWithoutYear = request.Birthday.AddYears(-request.Birthday.Year+4);


            Person birthday = new Person
            {
                Name = request.Name,
                SecondName = request.SecondName,
                DateWithoutYear = dateWithoutYear,
                Date = request.Birthday
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

            var dateWithoutYear = request.Birthday.AddYears(-request.Birthday.Year + 4);

            birthday.Name = request.Name;
            birthday.SecondName = request.SecondName;
            birthday.Date = request.Birthday;
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

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var birthday = await _repository.FindById(request.Id, cancellationToken);
            if (birthday == null)
            {
                throw new NoBithdayFoundException(request.Id);
            }
          
            var birth = new GetById.Response
            {
                Id = birthday.Id,
                Name = birthday.Name,
                SecondName = birthday.SecondName,
                Date = birthday.Date,
                Age = FindAge(birthday.Date),
                PhotoGuid = birthday.PhotoGuid,
                PhotoName = birthday.PhotoName,
                PhotoType = birthday.PhotoType,
                PhotoContent = birthday.PhotoContent
            };            

            return birth;

        }

        public async Task<GetNext.Response> GetNext(GetNext.Request request, CancellationToken cancellationToken)
        {
           // date  01.01.0004 for start sorting
            var parse = DateTime.TryParse("01.01.0004", out var date1);
            // the same date in 0004 for sorting
            var today = date1.AddDays(DateTime.UtcNow.DayOfYear);
            
            int total = await _repository.Count(cancellationToken);

            //the first time try to get Birthdays before NY
            var nextBirthdays1 = await _birthdayRepository.GetPagedBirthdays(
                p=>(p.DateWithoutYear >= today),
                request.Limit, cancellationToken);

            //if number of Birthdays less than limit, for exemple: today december,31
            var count = nextBirthdays1.Count;

            if (count < request.Limit && count < total)
            {
                var nextBirthdays2 = await _birthdayRepository.GetPagedBirthdays(
                p => (p.DateWithoutYear >= date1 && p.DateWithoutYear < today),
                request.Limit-count, cancellationToken);
                nextBirthdays1.AddRange(nextBirthdays2);
            }

            //get all birtdays
            //var allBirthdays = await _repository.GetPaged(0, total, cancellationToken);

            //foreach (var item in allBirthdays)
            //{
            //    if (item.DateWithoutYear < today)
            //    {
            //        item.DateWithoutYear = item.DateWithoutYear.AddYears(4);
            //    }
            //}
            //var birthdays = allBirthdays
            //     .Where(b => b.DateWithoutYear >= today)
            //     .OrderBy(b => b.DateWithoutYear)
            //     .Take(request.Limit)
            //     .ToList();


            return new GetNext.Response
            {
                Items = nextBirthdays1.Select(birthday => new GetNext.Response.BirthdayNextResponse
                {
                    Id = birthday.Id,
                    Name = birthday.Name,
                    SecondName = birthday.SecondName,
                    Date = birthday.Date,
                    PhotoGuid = birthday.PhotoGuid.ToString(),
                    PhotoName = birthday.PhotoName,
                    PhotoType = birthday.PhotoType,
                    PhotoContent = birthday.PhotoContent,
                    Age= FindAge(birthday.Date)

                }),
                Total = total,
                Limit = request.Limit

            };



        }

        public async Task<GetPagedBirthday.Response> GetPaged(GetPagedBirthday.Request request, CancellationToken cancellationToken)
        {
            int total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new GetPagedBirthday.Response
                {
                    Total = total,
                    Offset = request.Offset,
                    Limit = request.Limit,
                    Items = Array.Empty<GetPagedBirthday.Response.BirthdayResponse>()
                };
            }

            var birthdays = await _birthdayRepository.GetPagedSortByName(request.Offset, request.Limit, cancellationToken);

            return new GetPagedBirthday.Response
            {
                Items = birthdays.Select(birthday => new GetPagedBirthday.Response.BirthdayResponse
                {
                    Id = birthday.Id,
                    Name = birthday.Name,
                    SecondName = birthday.SecondName,
                    Date = birthday.Date,                    
                    PhotoGuid = birthday.PhotoGuid.ToString(),
                    PhotoName = birthday.PhotoName,
                    PhotoType = birthday.PhotoType,
                    PhotoContent = birthday.PhotoContent,
                    Age=FindAge(birthday.Date)

                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit

            };


        }

        public async Task<FindByName.Response> FindByName(FindByName.Request request, CancellationToken cancellationToken)
        {
            var total = await _birthdayRepository.Count(p => p.Name.Contains(request.SearchName) || p.SecondName.Contains(request.SearchName), cancellationToken);
            
            var birthdays = await _birthdayRepository.FindByName(request.SearchName, request.Offset, request.Limit, cancellationToken);


            return new FindByName.Response
            {
                Items = birthdays.Select(birthday => new FindByName.Response.BirthdayResponse
                {
                    Id = birthday.Id,
                    Name = birthday.Name,
                    SecondName = birthday.SecondName,
                    Date = birthday.Date,
                    PhotoGuid = birthday.PhotoGuid.ToString(),
                    PhotoName = birthday.PhotoName,
                    PhotoType = birthday.PhotoType,
                    PhotoContent = birthday.PhotoContent,
                    Age = FindAge(birthday.Date)
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }


        // finding age
        private int? FindAge(DateTime? date)
        {
            int? age = null;
            if (date != null)
            {
                DateTime bday = (DateTime)date;
                int ageA = DateTime.UtcNow.Year - bday.Year;
                if (bday > DateTime.UtcNow.AddYears(-ageA)) ageA--;
                age = ageA;
            }
            return age;
        }

        //Checking dates - now do not use
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
