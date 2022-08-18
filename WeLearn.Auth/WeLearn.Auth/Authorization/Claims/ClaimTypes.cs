using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeLearn.Auth.Authorization.Claims;

public static class ClaimTypes
{
    public const string Role = "role";
    public const string StudyYearAdmin = "year-admin";
    public const string CourseAdmin = "course-admin";

    public static class StudyYear
    {
        public const string Name = "year";
        public const string Read = "year.read";
        public const string Write = "year.write";
    }
    public static class Course
    {
        public const string Name = "course";
        public const string Read = "course.read";
        public const string Write = "course.write";
    }
    public static class Config
    {
        public const string Name = "config";
        public const string Read = "config.read";
        public const string Write = "config.write";
    }
    public static class Credentials
    {
        public const string Name = "creds";
        public const string Read = "creds.read";
        public const string Write = "creds.write";
    }
    public static class Account
    {
        public const string Name = "account";
        public const string Manage = "account.manage";
    }
}
