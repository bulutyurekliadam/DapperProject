namespace DapperProject.Dtos
{
    public class UpdateCourseDto
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryID { get; set; }
        public int InstructorID { get; set; }
        public string CourseDescription { get; set; }
    }
}
