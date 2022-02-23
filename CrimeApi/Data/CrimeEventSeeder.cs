using CrimeApi.Models;
using CrimeApi.Services;

namespace CrimeApi.Data;

public static class CrimeEventSeeder
{
    public static async void SeedDb(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var crimeEventRepository = scope.ServiceProvider.GetService<ICrimeEventRepository>();

            if (crimeEventRepository == null)
            {
                return;
            }

            var eventTypes = await crimeEventRepository.GetCrimeEventTypesAsync();

            if (eventTypes.Any())
            {
                return;
            }

            await crimeEventRepository.CreateCrimeEventTypeAsync(
                new CrimeEventType()
                {
                    EventType = "burglary"
                }
            );
            await crimeEventRepository.CreateCrimeEventTypeAsync(
                new CrimeEventType()
                {
                    EventType = "assault"
                }
            );
            await crimeEventRepository.CreateCrimeEventTypeAsync(
                new CrimeEventType()
                {
                    EventType = "murder"
                }
            );

            await crimeEventRepository.CreateCrimeEventAsync(
                new CrimeEvent()
                {
                    Date = DateTime.Now.AddDays(-3.5),
                    CrimeEventType = "burglary",
                    Description = "I came home and all my things were gone!",
                    Place = "Curvy road 235",
                    ReporterEmail = "adamr@mailerrr.com"
                }
            );
            await crimeEventRepository.CreateCrimeEventAsync(
                new CrimeEvent()
                {
                    Date = DateTime.Now.AddDays(-3),
                    CrimeEventType = "assault",
                    Description = "A young male hit another young male in the stomach",
                    Place = "The town pub",
                    ReporterEmail = "uninvolved34@mailerrr.com"
                }
            );
            await crimeEventRepository.CreateCrimeEventAsync(
                new CrimeEvent()
                {
                    Date = DateTime.Now.AddDays(-2.5),
                    CrimeEventType = "burglary",
                    Description = "A pigeon stole a bag of sunflower seeds",
                    Place = "Sunny road 51",
                    ReporterEmail = "birdie@mailerrr.com"
                }
            );
            await crimeEventRepository.CreateCrimeEventAsync(
                new CrimeEvent()
                {
                    Date = DateTime.Now.AddDays(-2),
                    CrimeEventType = "assault",
                    Description = "A cat scratched a woman",
                    Place = "Elemental estate",
                    ReporterEmail = "dogs4ever@mailerrr.com"
                }
            );
            await crimeEventRepository.CreateCrimeEventAsync(
                new CrimeEvent()
                {
                    Date = DateTime.Now.AddDays(-1.5),
                    CrimeEventType = "murder",
                    Description = "A 12 year old boy got really offended",
                    Place = "Forest Primary School",
                    ReporterEmail = "andy@mailerrr.com"
                }
            );
            await crimeEventRepository.CreateCrimeEventAsync(
                new CrimeEvent()
                {
                    Date = DateTime.Now.AddDays(-1),
                    CrimeEventType = "burglary",
                    Description = "My wife took my wallet",
                    Place = "The last street 30",
                    ReporterEmail = "tom@mailerrr.com"
                }
            );

        }
    }
}
