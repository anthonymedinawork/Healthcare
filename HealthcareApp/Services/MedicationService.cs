using AutoMapper;
using Data.Models;
using HealthcareApp.Data.Entities;
using HealthcareApp.Repositories.Interfaces;
using HealthcareApp.Services.Interfaces;
using HealthcareApp.Services.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace HealthcareApp.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _repository;
        private readonly IMapper _mapper;

        public MedicationService(IMedicationRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async Task CreateAsync(MedicationViewModel model)
        {
            Medication med = _mapper.Map<Medication>(model);

            await _repository.CreateAsync(med);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new KeyNotFoundException(nameof(id));
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<List<MedicationViewModel>> GetAllAsync()
        {
            var meds = await _repository.GetAll().ToListAsync();

            return _mapper.Map<List<MedicationViewModel>>(meds);
        }

        public async Task<List<MedicationViewModel>> GetAllAsync(Expression<Func<MedicationViewModel, bool>> filter)
        {
            var medFilter = _mapper.Map<Expression<Func<Medication, bool>>>(filter);

            List<Medication> meds = await _repository.GetAll(medFilter).ToListAsync();

            return _mapper.Map<List<MedicationViewModel>>(meds);
        }

        public async Task<MedicationViewModel> GetByIdAsync(string id)
        {
            Medication? med = await _repository.GetByIdAsync(id);

            if (med is null)
            {
                throw new ArgumentNullException("No such medication exists!");
            }
            return _mapper.Map<MedicationViewModel>(med);
        }

        public async Task UpdateAsync(MedicationViewModel model)
        {
            Medication med = _mapper.Map<Medication>(model);

            await _repository.UpdateAsync(med);
        }

        public async Task LoadDbAsync()
        {
            var meds = await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&limit=736");
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=737&limit=484"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=1222&limit=837"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=2060&limit=841"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=2902&limit=392"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=3295&limit=5"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=3301&limit=41"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=3343&limit=28"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=3372&limit=441"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=3814&limit=53"));
            meds.AddRange(await LoadFromAPIAsync("https://api.fda.gov/drug/event.json?api_key=9ZRmYbF03vVYDKhrKqgnwsCXdKmcu9OiYax9ro6K&skip=3868&limit=82"));

            await _repository.CreateManyAsync(meds);
        }

        public async Task DeleteTableDataAsync()
        {
            await _repository.DeleteTableDataAsync();
        }

        private async Task<List<Medication>> LoadFromAPIAsync(string requestUri)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri),
            };

            List<Medication> meds = new();

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject(body);

                foreach (var item in data.results)
                {
                    if (item.patient.drug[0].openfda is not null)
                    {
                        await Console.Out.WriteLineAsync(item.patient.drug[0].openfda.ToString());

                        if (item.patient.drug[0].openfda.generic_name.HasValues && item.patient.drug[0].openfda.brand_name.HasValues
                            && item.patient.drug[0].drugindication != null)
                        {
                            meds.Add(new Medication()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = (string)item.patient.drug[0].openfda.generic_name[0],
                                BrandName = (string)item.patient.drug[0].openfda.brand_name[0],
                                Indication = (string)item.patient.drug[0].drugindication
                            });
                        }
                    }
                }
            }

            return meds.DistinctBy(m => new { m.Name, m.Indication }).ToList();
        }
    }
}
