using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStudentStatistics(name);
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException();

            var averageGrades = (from s in Students select s.AverageGrade).OrderByDescending(c => c).ToList();
            var rank = averageGrades.FindIndex(x => Math.Abs(x - averageGrade) < 0.001) + 1;
            var percentile = (double)rank / Students.Count;

            if (percentile <= .2)
                return 'A';
            else if (percentile <= .4)
                return 'B';
            else if (percentile <= .6)
                return 'C';
            else if (percentile <= .8)
                return 'D';
            else
                return 'F';
        }
    }

}
