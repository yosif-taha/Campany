using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E=>E.Salary).HasColumnType("decimal(18,2)");
            builder.HasOne(E => E.Department)
                    .WithMany(D => D.employees)
                    .HasForeignKey(E => E.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);// when delete any department set departmentId = Null not remove this employe, but departmentId must Nullable.
        }
    }
}
