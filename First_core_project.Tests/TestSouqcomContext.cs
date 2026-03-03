using First_core_project.Models;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Tests
{
    // الكلاس ده "ابن" الكلاس الأصلي، وظيفته بس إنه يوقف الـ SQL Server وقت التيست
    public class TestSouqcomContext : SouqcomContext
    {
        public TestSouqcomContext(DbContextOptions<SouqcomContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // بنسيبها فاضية عشان نمنع الكود الأصلي إنه يفتح اتصال مع SQL Server
        }
    }
}