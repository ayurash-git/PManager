using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PManager.Domain.Models;
using PManager.EF.Context;

namespace PManager.EF.Data
{
    public class DbInitializer
    {
        private readonly PManagerDb _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(PManagerDb db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }
        
        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");

            // _logger.LogInformation("Удаление существующей БД...");
            // await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            // _logger.LogInformation("Удаление существующей БД выполнено за {0} мс.", timer.ElapsedMilliseconds);

            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync().ConfigureAwait(false); 
            _logger.LogInformation("Миграция БД выполнена за {0} мс.", timer.ElapsedMilliseconds);

            if (await _db.Roles.AnyAsync()) return;

            await InitializeGenders();
            await InitializeJobs();
            await InitializeRoles();
            await InitializeRolesJobs();
            await InitializeUsers();
            await InitializeAgencies();
            await InitializeProjects();


            _logger.LogInformation("Инициализация БД выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }
        /// <summary>
        /// Genders Initialization
        /// </summary>
        private async Task InitializeGenders()
        {
            var male   = new Gender { Id = 1, Name = "Male" };
            var female = new Gender { Id = 2, Name = "Female" };
        
            await _db.Genders.AddRangeAsync(male, female);
            await _db.SaveChangesAsync();
        }

        #region JOBS

        /// <summary>
        /// Jobs Initialization
        /// </summary>
        private async Task InitializeJobs()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация таблицы 'Jobs' (типы работ)...");

            var jobs = new List<Job>
            {
                new Job {Id = 1, Name = "Editing", Details = "Монтаж"},
                new Job {Id = 2, Name = "ColorCorrection", Details = "Цветокоррекция"},
                new Job {Id = 3, Name = "2d VFX", Details = "2d - эффекты"},
                new Job {Id = 4, Name = "2d Compose", Details = "2d композ"},
                new Job {Id = 5, Name = "2d Tracking", Details = "2d трекинг"},
                new Job {Id = 6, Name = "3d VFX", Details = "3d - эффекты"},
                new Job {Id = 7, Name = "3d Tracking", Details = "3d трекинг камеры, сцены"},
                new Job {Id = 8, Name = "Keying", Details = "Кеинг"},
                new Job {Id = 9, Name = "Rotoscoping", Details = "Ротоскоп (ручное маскирование объектов)"},
                new Job {Id = 10, Name = "Cleanup", Details = "Клинап"},
                new Job {Id = 11, Name = "3d Modeling", Details = "3d моделирование"},
                new Job {Id = 12, Name = "Texturing", Details = "Текстуринг"},
                new Job {Id = 13, Name = "Shading", Details = "Шейдинг"},
                new Job {Id = 14, Name = "Animation", Details = "Анимация"},
                new Job {Id = 15, Name = "Rendering", Details = "Рендеринг"},
                new Job {Id = 16, Name = "Project Organization", Details = "Организация проекта"},
                new Job {Id = 17, Name = "Project Management", Details = "Ведение проекта"},
                new Job {Id = 18, Name = "Creative", Details = "Креатив"},
                new Job {Id = 19, Name = "Sound Design", Details = "Sound Design"}
            };

            await _db.Jobs.AddRangeAsync(jobs);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация таблицы 'Jobs' выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }
        

        #endregion


        #region ROLES

        /// <summary>
        /// Roles Initialization
        /// </summary>
        private async Task InitializeRoles()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация таблицы 'Roles' (роли)...");

            var roles = new List<Role>
            {
                new Role { Id = 1,  Name = "Producer",              Details = "Продюсер проекта"},
                new Role { Id = 2,  Name = "Director",              Details = "Режиссер проекта" },
                new Role { Id = 3,  Name = "Art Director",          Details = "Art Director" },
                new Role { Id = 4,  Name = "Manager",               Details = "Менеджер проекта" },
                new Role { Id = 5,  Name = "Color Grader",          Details = "Специалист по цветокоррекции" },
                new Role { Id = 6,  Name = "Editor",                Details = "Монтажер" },
                new Role { Id = 7,  Name = "2d Artist",             Details = "2d график" },
                new Role { Id = 8,  Name = "3d Artist",             Details = "3d график" },
                new Role { Id = 9,  Name = "Animator",              Details = "Аниматор" },
                new Role { Id = 10, Name = "3d Modeler",            Details = "3d Modeler" },
                new Role { Id = 11, Name = "CG Generalist",         Details = "CG Generalist" },
                new Role { Id = 12, Name = "Sound Producer",        Details = "Sound Producer" }

            };
            
            await _db.Roles.AddRangeAsync(roles);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация таблицы 'Roles' выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }

        #endregion


        #region ROLES-JOBS

        /// <summary>
        /// Roles-Jobs Initialization
        /// </summary>
        private async Task InitializeRolesJobs()
        {
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 1).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 16));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 2).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 18));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 3).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 18));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 4).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 17));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 5).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 5));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 6).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 1));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 3));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 4));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 5));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 7));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 8));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 9));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 10));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 12));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 7).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 18));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 8).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 6));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 8).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 7));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 8).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 11));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 8).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 12));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 8).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 13));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 8).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 14));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 8).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 15));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 9).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 14));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 10).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 11));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 10).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 12));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 3));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 4));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 5));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 6));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 7));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 8));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 9));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 10));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 11));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 12));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 13));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 14));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 15));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 11).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 18));
            _db.Roles.FirstOrDefaultAsync(r => r.Id == 12).Result.Jobs.Add(_db.Jobs.FirstOrDefault(j => j.Id == 19));
            
            await _db.SaveChangesAsync();
        }

        #endregion


        #region USERS

        /// <summary>
        /// Users Initialization
        /// </summary>
        private async Task InitializeUsers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация таблицы 'Users' ...");
        
            var users = new List<User>
            {
                new User
                {
                    Username = "ayurash",
                    Password = "123",
                    Email = "ayurash@me.com",
                    FirstName = "Алексей",
                    SecondName = "Юраш",
                    RoleId = 7,
                    GenderId = 1,
                    Phone = "79647973197",
                    Birthday = new DateTime(1977, 7, 27)
                },
                new User
                {
                    Username = "vaverin",
                    Password = "123",
                    Email = "vaverin@postpro18.com",
                    FirstName = "Валерий",
                    SecondName = "Аверин",
                    RoleId = 7,
                    GenderId = 1
                }
            };
            
            await _db.Users.AddRangeAsync(users);
            await _db.SaveChangesAsync();
        
            _logger.LogInformation("Инициализация таблицы 'Users' выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }

        #endregion


        #region AGENCIES

        private async Task InitializeAgencies()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация таблицы 'Agencies'...");

            var agencies = new List<Agency>
            {
                new Agency {Name = "JWT", FullName = "ООО \"Джэй-Дабл-Ю-Ти\""},
                new Agency {Name = "Instinct", FullName = "ЗАО \"Инстинкт\""},
                new Agency {Name = "Star Richers", FullName = "ООО \"Стар Ричерз\""},
                new Agency {Name = "Ogilvi", FullName = "ООО \"Огилви энд Мейзер\""},
                new Agency {Name = "Park Production", FullName = "АО \"Парк Продакшн\""},
                new Agency {Name = "Saatchi & Saatchi", FullName = "Saatchi & Saatchi"},
                new Agency {Name = "Slava", FullName = "ООО \"СЛАВА Групп\""},
                new Agency {Name = "Fetish Production", FullName = "ООО \"Фетиш Продакшн\""},
                new Agency {Name = "Havas Worldwide", FullName = "ООО \"ХАВАС Ворлдвайд\""}
            };

            await _db.Agencies.AddRangeAsync(agencies);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация таблицы 'Agency' выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }

        #endregion

        #region PROJECTS

        private async Task InitializeProjects()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация таблицы 'Projects'...");

            var projects = new List<Project>
            {
                new Project
                {
                    Name = "SP Zavod", 
                    Agency = _db.Agencies.FirstOrDefault(a => a.Name == "Slava"),
                    Owner = _db.Users.FirstOrDefault(u => u.Username == "ayurash")
                },
                new Project
                {
                    Name = "Avito",
                    Agency = _db.Agencies.FirstOrDefault(a => a.Name == "Slava"),
                    Owner = _db.Users.FirstOrDefault(u => u.Username == "ayurash")
                }
            };

            await _db.Projects.AddRangeAsync(projects);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация таблицы 'Projects' выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }

        #endregion

    }
}
