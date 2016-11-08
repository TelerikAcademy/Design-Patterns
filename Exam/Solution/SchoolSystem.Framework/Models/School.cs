using SchoolSystem.Framework.Models.Contracts;
using System;
using System.Collections.Generic;

namespace SchoolSystem.Framework.Models
{
    public class School : ISchool
    {
        private readonly IDictionary<int, IStudent> students;
        private readonly IDictionary<int, ITeacher> teachers;

        public School()
        {
            this.students = new Dictionary<int, IStudent>();
            this.teachers = new Dictionary<int, ITeacher>();
        }

        public void AddStudent(int studentId, IStudent student)
        {
            this.students.Add(studentId, student);
        }

        public void AddTeacher(int teacherId, ITeacher teacher)
        {
            this.teachers.Add(teacherId, teacher);
        }

        public IStudent GetStudent(int studentId)
        {
            return this.students[studentId];
        }

        public ITeacher GetTeacher(int teacherId)
        {
            return this.teachers[teacherId];
        }

        public void RemoveStudent(int studentId)
        {
            if (!this.students.ContainsKey(studentId))
            {
                throw new ArgumentException("The given key was not present in the dictionary.");
            }

            this.students.Remove(studentId);
        }

        public void RemoveTeacher(int teacherId)
        {
            if (!this.teachers.ContainsKey(teacherId))
            {
                throw new ArgumentException("The given key was not present in the dictionary.");
            }

            this.teachers.Remove(teacherId);
        }
    }
}