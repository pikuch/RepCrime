using LawEnforcementApi.Models;

namespace LawEnforcementApi.Data;

public static class LawEnforcementDbContextSeeder
{
    public static void SeedDb(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<LawEnforcementDbContext>();

            if (context == null)
            {
                return;
            }

            if (context.LawEnforcementOfficers.Any())
            {
                return;
            }

            var ranks = new Rank[]
            {
                new Rank() { Name = "officer" },
                new Rank() { Name = "corporal" },
                new Rank() { Name = "sergeant" },
                new Rank() { Name = "lieutenant" },
                new Rank() { Name = "captain" }
            };
            context.Ranks.AddRange(ranks);

            var lawEnforcementOfficers = new LawEnforcementOfficer[]
            {
                new LawEnforcementOfficer() { Codename = "AX32", RankId = ranks[0].Id },
                new LawEnforcementOfficer() { Codename = "BY33", RankId = ranks[1].Id },
                new LawEnforcementOfficer() { Codename = "CZ34", RankId = ranks[2].Id },
                new LawEnforcementOfficer() { Codename = "DQ35", RankId = ranks[3].Id },
                new LawEnforcementOfficer() { Codename = "ER36", RankId = ranks[4].Id },
            };

            context.LawEnforcementOfficers.AddRange(lawEnforcementOfficers);

            context.SaveChanges();
        }
    }
}
