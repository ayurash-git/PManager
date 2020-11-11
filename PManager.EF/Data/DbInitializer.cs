using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PManager.Domain.Models;
using PManager.EF.Context;

namespace PManager.EF.Data
{
    public class DbInitializer
    {
        private readonly PManagerDB _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(PManagerDB db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }
        

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");

            _logger.LogInformation("Удаление существующей БД...");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation("Удаление существующей БД выполнено за {0} мс.", timer.ElapsedMilliseconds);

            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync().ConfigureAwait(false); 
            _logger.LogInformation("Миграция БД выполнена за {0} мс.", timer.ElapsedMilliseconds);

            if (await _db.Jobs.AnyAsync()) return;

            await InitializeJobs();
            await InitializeRoles();

            _logger.LogInformation("Инициализация БД выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }

        private const int JobsCount = 18;
        private Job[] _jobs;
        private async Task InitializeJobs()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация таблицы 'Jobs' (типы работ)...");

            _jobs = new Job[JobsCount];
            _jobs[0]  = new Job{ Id = 1,    Name = "Editing",               Details = "Монтаж" };
            _jobs[1]  = new Job{ Id = 2,    Name = "ColorCorrection",       Details = "Цветокоррекция" };
            _jobs[2]  = new Job{ Id = 3,    Name = "2d VFX",                Details = "2d - эффекты" };
            _jobs[3]  = new Job{ Id = 4,    Name = "2d Compose",            Details = "2d композ" };
            _jobs[4]  = new Job{ Id = 5,    Name = "2d Tracking",           Details = "2d трекинг" };
            _jobs[5]  = new Job{ Id = 6,    Name = "3d VFX",                Details = "3d - эффекты" };
            _jobs[6]  = new Job{ Id = 7,    Name = "3d Tracking",           Details = "3d трекинг камеры, сцены" };
            _jobs[7]  = new Job{ Id = 8,    Name = "Keying",                Details = "Кеинг" };
            _jobs[8]  = new Job{ Id = 9,    Name = "Rotoscoping",           Details = "Ротоскоп (ручное маскирование объектов)" };
            _jobs[9]  = new Job{ Id = 10,   Name = "Cleanup",               Details = "Клинап" };
            _jobs[10] = new Job{ Id = 11,   Name = "3d Modeling",           Details = "3d моделирование" };
            _jobs[11] = new Job{ Id = 12,   Name = "Texturing",             Details = "Текстуринг" };
            _jobs[12] = new Job{ Id = 13,   Name = "Shading",               Details = "Шейдинг" };
            _jobs[13] = new Job{ Id = 14,   Name = "Animation",             Details = "Анимация" };
            _jobs[14] = new Job{ Id = 15,   Name = "Rendering",             Details = "Рендеринг" };
            _jobs[15] = new Job{ Id = 16,   Name = "Project Organization",  Details = "Организация проекта" };
            _jobs[16] = new Job{ Id = 17,   Name = "Project Management",    Details = "Ведение проекта" };
            _jobs[17] = new Job{ Id = 18,   Name = "Creative",              Details = "Креатив" };

            await _db.Jobs.AddRangeAsync(_jobs);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация таблицы 'Jobs' выполнена за {0} с.", timer.Elapsed.TotalSeconds);

        }

        private const int RolesCount = 10;
        private Role[] _roles;
        private async Task InitializeRoles()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация таблицы 'Roles' (роли)...");

            _roles = new Role[RolesCount];
            _roles[0] = new Role { Id = 1,  Name = "Producer",           Details = "Продюсер проекта" };
            _roles[1] = new Role { Id = 2,  Name = "Director",           Details = "Режиссер проекта" };
            _roles[2] = new Role { Id = 3,  Name = "Art Director",       Details = "Art Director" };
            _roles[3] = new Role { Id = 4,  Name = "Manager",            Details = "Менеджер проекта" };
            _roles[4] = new Role { Id = 5,  Name = "Color Grader",       Details = "Специалист по цветокоррекции" };
            _roles[5] = new Role { Id = 6,  Name = "Editor",             Details = "Монтажер" };
            _roles[6] = new Role { Id = 7,  Name = "2d Artist",          Details = "2d график" };
            _roles[7] = new Role { Id = 8,  Name = "3d Artist",          Details = "3d график" };
            _roles[8] = new Role { Id = 9,  Name = "Animator",           Details = "Аниматор" };
            _roles[9] = new Role { Id = 10, Name = "3d Modeler",         Details = "3d Modeler" };

            await _db.Roles.AddRangeAsync(_roles);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация таблицы 'Roles' выполнена за {0} с.", timer.Elapsed.TotalSeconds);
        }

        // private async Task ItitializeJobsRoles()
        // {
        //     
        // }
    }
}
