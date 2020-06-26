using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace retrowebcore.Data
{
    public static class StringExtension
    {
        private const string Controller = "controller";
        private const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }

    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        const string InvalidPrefix = "asp_net_";
        const string ValidPrefix = "app_";

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var tablename = entity.GetTableName().ToSnakeCase();
                if (tablename.StartsWith(InvalidPrefix))
                    entity.SetTableName(tablename.Replace(InvalidPrefix, ValidPrefix));
                else
                    entity.SetTableName(tablename);
            }
        }
    }
}
