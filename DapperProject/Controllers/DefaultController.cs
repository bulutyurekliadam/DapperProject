using Dapper;
using DapperProject.DapperContext;
using DapperProject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DapperProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly Context _context;

        public DefaultController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string query = "Select * From Courses ";
            var connection = _context.CreateConnection();
            var values = await connection.QueryAsync<ResultCoursesDto>(query);
            return View(values.ToList());
        }
        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            string query = "insert into Courses(Title,Price,Duration,ImageUrl,CategoryID,InstructorID,CourseDescription) values (@title,@price,@duration,@imageurl,@CategoryID,@InstructorID,@CourseDescription)";
            var parameters = new DynamicParameters();
            parameters.Add("@title", createCourseDto.Title);
            parameters.Add("@price", createCourseDto.Price);
            parameters.Add("@duration", createCourseDto.Duration);
            parameters.Add("@imageurl", createCourseDto.ImageUrl);
            parameters.Add("@CategoryID", createCourseDto.CategoryID);
            parameters.Add("@InstructorID", createCourseDto.InstructorID);
            parameters.Add("@CourseDescription", createCourseDto.CourseDescription);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
            return RedirectToAction("Index");


        }
        public async Task<IActionResult> DeleteCourse(int id)
        {
            string query = "Delete From Courses where CourseID=@CourseID";
            var parameters = new DynamicParameters();
            parameters.Add("@CourseID", id);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            string query = "Select * From Courses where CourseID=@CourseID";
            var parameters = new DynamicParameters();
            parameters.Add("@CourseID", id);
            var connection = _context.CreateConnection();
            var values = await connection.QueryFirstOrDefaultAsync<UpdateCourseDto>(query, parameters);
            return View(values);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            //WHERE KISMINA ÖZELLİKLE BAK NASIL YAPILMIŞ. SON DAKİKALARDA. 
            string query = "Update Courses Set Title = @title, Price = @price, Duration =@duration, ImageUrl=@imageurl, CategoryID=@CategoryID, InstructorID=@InstructorID,CourseDescription=@CourseDescription, Where  CourseID =@CourseID ";
            var parameters = new DynamicParameters();
            parameters.Add("@title", updateCourseDto.Title);
            parameters.Add("@price", updateCourseDto.Price);
            parameters.Add("@duration", updateCourseDto.Duration);
            parameters.Add("@imageurl", updateCourseDto.ImageUrl);
            parameters.Add("@CategoryID", updateCourseDto.CategoryID);
            parameters.Add("@InstructorID", updateCourseDto.InstructorID);
            parameters.Add("@CourseDescription", updateCourseDto.CourseDescription);
            var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);
            return RedirectToAction("Index");
        }


    }
}
