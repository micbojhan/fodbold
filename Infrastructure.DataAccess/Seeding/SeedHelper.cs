﻿using System.Data.Entity.Migrations;
using System.Linq;

namespace Infrastructure.DataAccess.Seeding
{
    public static class SeedHelper
    {
        public static void Seed(ApplicationContext context)
        {
            for (var i = 0; i <= 10; i++)
            {
                context.Students.AddOrUpdate(SeedingHelper.Student());
            }
            context.SaveChanges();

            for (var i = 0; i <= 2; i++)
            {
                context.Courses.AddOrUpdate(SeedingHelper.Course(context.Students.ToList()));
            }
            context.SaveChanges();

            for (var i = 0; i <= 2; i++)
            {
                context.Teachers.AddOrUpdate(SeedingHelper.Teacher(context.Courses.ToList()));
            }
            context.SaveChanges();

            context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.FirstOrDefault()));
            context.ClassRooms.AddOrUpdate(SeedingHelper.ClassRoom(context.Courses.ToList().Last()));
            context.SaveChanges();
        }
    }
}
