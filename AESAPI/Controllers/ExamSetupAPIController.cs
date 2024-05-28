using AESAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AESAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamSetupAPIController : ControllerBase
    {
        private readonly AESDBContext _context;
        public ExamSetupAPIController(AESDBContext context)
        {
            _context = context;
        }

        [Route("InsertExamWithStudents")]
        [HttpPost]
        public IActionResult InsertExamWithStudents([FromBody] ExamsetupModel examsetupmodel)
        {
            try
            {
                //Write this sqlcommand attribute as same to database 
                var sqlCommand = "DECLARE @Students dbo.ExamSetupType; " +
                                 "INSERT INTO @Students (std_id) " +
                                 "VALUES {0}; " +
                                 "EXEC [dbo].[InsertExamWithStudent] " +
                                 "@exam_id = @p0, " +
                                 "@exam_date = @p1, " +
                                 "@fromtime = @p2, " +
                                 "@totime = @p3, " +
                                 "@d_flag = @p4, " +
                                 "@created_by = @p5, " +
                                 "@created_date = @p6, " +
                                 "@created_ip = @p7, " +
                                 "@modify_by = @p8, " +
                                 "@modify_date = @p9, " +
                                 "@modify_ip = @p10, " +
                                 "@ExamSetupStudent = @Students;";

                var values = string.Join(", ", examsetupmodel.Students.Select(o => $"({o.StudentId})"));
                sqlCommand = string.Format(sqlCommand, values);

                _context.Database.ExecuteSqlRaw(sqlCommand,
                    examsetupmodel.ExamId,
                    examsetupmodel.ExamDate,
                    examsetupmodel.FromTime,
                    examsetupmodel.ToTime,
                    examsetupmodel.DFlag,
                    examsetupmodel.CreatedBy,
                    examsetupmodel.CreatedDate,
                    examsetupmodel.CreatedIp,
                    examsetupmodel.ModifyBy,
                    examsetupmodel.ModifyDate,
                    examsetupmodel.ModifyIp);

                return Ok("Exam setup saved successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("updateExamWithStudents/{examsetupId}")]
        public async Task<IActionResult> UpdateQuestionAndOptions(long examsetupId, [FromBody] ExamsetupModel updateModel)
        {
            var examsetup = await _context.TblMExamSetups.FindAsync(examsetupId);

            if (examsetup == null)
            {
                return NotFound();
            }

            // Step 3: Update properties of TblMExamSetup
            examsetup.ExamId = updateModel.ExamId;
            examsetup.ExamDate = updateModel.ExamDate;
            examsetup.Fromtime = updateModel.FromTime;
            examsetup.Totime = updateModel.ToTime;
            examsetup.DFlag = updateModel.DFlag;
            examsetup.CreatedBy = updateModel.CreatedBy;
            examsetup.CreatedDate = updateModel.CreatedDate;
            examsetup.CreatedIp = updateModel.CreatedIp;
            examsetup.ModifyBy = updateModel.ModifyBy;
            examsetup.ModifyDate = updateModel.ModifyDate;
            examsetup.ModifyIp = updateModel.ModifyIp;

            // Step 4: Remove existing students for the exam
            var existingStudents = await _context.TblMExamSetupStudents.Where(o => o.ExamsetupId == examsetupId).ToListAsync();
            _context.TblMExamSetupStudents.RemoveRange(existingStudents);

            // Step 5: Add new options
            foreach (var student in updateModel.Students)
            {
                var newStudent = new TblMExamSetupStudent
                {
                    ExamsetupId = examsetupId, // Set the foreign key to link to the TblMExamSetup record
                    StdId = student.StudentId,
                    AttndStatus = "N",
                    DFlag = updateModel.DFlag,
                    CreatedBy = updateModel.CreatedBy,
                    CreatedDate = updateModel.CreatedDate,
                    CreatedIp = updateModel.CreatedIp,
                    ModifyBy = updateModel.ModifyBy,
                    ModifyDate = updateModel.ModifyDate,
                    ModifyIp = updateModel.ModifyIp
                };

                _context.TblMExamSetupStudents.Add(newStudent);
            }

            // Step 6: Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Exams setup updated successfully!");
        }


        [HttpDelete("DeleteExamWithStudents/{examsetupId}")]
        public IActionResult DeleteExamWithStudents(long examsetupId)
        {
            try
            {
                // Update DFlag to 1 in TblMExamSetup for the specified QansId
                var examsRecord = _context.TblMExamSetups
                    .FirstOrDefault(e => e.ExamsetupId == examsetupId && e.DFlag != 1);

                if (examsRecord != null)
                {
                    examsRecord.DFlag = 1;

                    // Update DFlag to 1 in TblMOptions for the corresponding QansId
                    var studentRecords = _context.TblMExamSetupStudents
                        .Where(o => o.ExamsetupId == examsetupId && o.DFlag != 1)
                        .ToList();

                    foreach (var student in studentRecords)
                    {
                        student.DFlag = 1;
                    }

                    // Save changes to the database
                    _context.SaveChanges();

                    return Ok($"Exam setup deleted successfully!");
                }
                else
                {
                    return NotFound($"No record found for examsetupid {examsetupId} or DFlag is already 1.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating DFlag: {ex.Message}");
            }
        }

        [HttpGet("GetExamAndSetupData")]
        public IActionResult GetExamAndSetupData()
        {
            try
            {
                var examSetups = _context.TblMExamSetups
               .Where(s => s.DFlag != 1)
               .Join(
                   _context.TblMExams,
                   s => s.ExamId,
                   e => e.ExamId,
                   (s, e) => new
                   {
                       s.ExamsetupId,
                       ExamName = e.ExamName, // Include ExamName in the result
                       s.ExamDate,
                       s.Fromtime,
                       s.Totime
                   })
               .ToList();

                return Ok(examSetups);
            }
            catch (Exception ex)
            {

                // Return a meaningful error response
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }

        [HttpGet("GetStudentWithExamSetupId/{examSetupId}")]
        public IActionResult GetStudentWithExamSetupId(long examSetupId)
        {
            try
            {
                var students = _context.TblMExamSetupStudents
                    .Where(s => s.ExamsetupId == examSetupId && s.DFlag != 1)
                     .Join(
                       _context.TblMStudents,
                       s => s.StdId,
                       std => std.StdId,
                       (s, std) => new
                       {
                           StdName = std.StdFrstname+" "+std.StdLstname,
                       })
                   .ToList();

                return Ok(students);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetExamWithStudents/{examsetupid}")]
        public IActionResult GetExamWithStudents(long examsetupid)
        {
            try
            {
                // Retrieve exam setup details
                var examSetup = _context.TblMExamSetups
                    .FirstOrDefault(e => e.ExamsetupId == examsetupid);

                if (examSetup == null)
                {
                    return NotFound($"Exam Setup with ID {examsetupid} not found");
                }

                // Retrieve exam students with their details
                var examStudents = _context.TblMExamSetupStudents
                    .Where(es => es.ExamsetupId == examsetupid)
                    .Include(es => es.Std) // Include the student details
                    .ToList();

                // Create a response object
                var response = new
                {
                    ExamId = examSetup.ExamId,
                    ExamDate = examSetup.ExamDate,
                    FromTime = examSetup.Fromtime,
                    ToTime = examSetup.Totime,
                    Students = examStudents.Select(es => new
                    {
                        StudentId = es.Std.StdId,
                        StudentName = es.Std.StdFrstname + " "+ es.Std.StdLstname
                    })
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
