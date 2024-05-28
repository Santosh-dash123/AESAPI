using AESAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AESAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamAPIController : ControllerBase
    {
        private readonly AESDBContext _context;
        public ExamAPIController(AESDBContext context)
        {
            _context = context;
        }

        #region ExamAPI
        [Route("Exam")]
        [AcceptVerbs("POST", "PUT")]
        public async Task<IActionResult> Exam(TblMExam exam, string action)
        {
            try
            {
                if (action == "INSERT" || action == "UPDATE")
                {
                    var outputparameter = new SqlParameter("@msg", SqlDbType.VarChar, 400) { Direction = ParameterDirection.Output };
                    var parameters = new[]
                    {
                     new SqlParameter("@exam_id", exam.ExamId),
                     new SqlParameter("@exam_name", exam.ExamName),
                     new SqlParameter("@exam_duration", exam.ExamDuration),
                     new SqlParameter("@exam_subject", exam.ExamSubject),
                     new SqlParameter("@tnoqans", exam.Tnoqans),
                     new SqlParameter("@passmark",exam.Passmark),
                     new SqlParameter("@exam_descr",exam.ExamDescr),
                     new SqlParameter("@d_Flag", exam.DFlag ?? 0), // Default value for DFlag
                     new SqlParameter("@created_by", exam.CreatedBy ?? 0), // Default value for CreatedBy
                     new SqlParameter("@created_date",exam.CreatedDate ?? DateTime.Now),
                     new SqlParameter("@created_ip", exam. CreatedIp?? ""),
                     new SqlParameter("@modify_by", exam. ModifyBy?? 0), // Default value for ModifyBy
                     new SqlParameter("@modify_date", exam. ModifyDate?? DateTime.Now),
                     new SqlParameter("@modify_ip", exam. ModifyIp?? ""),
                     new SqlParameter("@action", action),
                     outputparameter
                 };
                    // Execute the stored procedure
                    var affectedRows = await _context.Database.ExecuteSqlRawAsync
                        ("EXECUTE [USP_Tbl_M_Exam_IUD] @exam_id, @exam_name, @exam_duration, @exam_subject,@tnoqans,@passmark,@exam_descr,@d_Flag,@created_by,@created_date,@created_ip,@modify_by,@modify_date,@modify_ip,@action,@msg OUTPUT", parameters);
                    await _context.SaveChangesAsync();
                    var outputMessage = outputparameter.Value;
                    // Retrieve the output parameter value

                    return Ok(new { Message = $"Rows affected: {affectedRows}", OutputMessage = outputMessage });
                }
                else
                {
                    return BadRequest("Error: Please provide a valid action!!");
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteExam/{examId}")]
        public IActionResult DeleteQuestionAndOptions(long examId)
        {
            try
            {
                // Update DFlag to 1 in TblMQuestionans for the specified QansId
                var examRecord = _context.TblMExams
                    .FirstOrDefault(q => q.ExamId == examId && q.DFlag != 1);

                if (examRecord != null)
                {
                    examRecord.DFlag = 1;

                    // Save changes to the database
                    _context.SaveChanges();

                    return Ok($"Exam deleted successfully!");
                }
                else
                {
                    return NotFound($"No record found for QansId {examId} or DFlag is already 1.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating DFlag: {ex.Message}");
            }
        }

        [Route("VIEWEXAM")]
        [HttpGet]
        public async Task<IActionResult> VIEWEXAM(string action, long exam_id)
        {
            if (action == "All" && exam_id <= 0)
            {
                var exams = await _context.TblMExams.Where(exam => exam.DFlag == 0).ToListAsync();
                if (exams != null)
                {
                    return Ok(exams);
                }
                return Ok(exams);
            }
            else if (action == "AllById" && exam_id >= 0)
            {
                var exam = await _context.TblMExams
                .Where(e => e.ExamId == exam_id)  // Assuming UId is the property representing the user id
                .FirstOrDefaultAsync();
                if (exam != null)
                {
                    return Ok(exam);
                }
                else
                {
                    return NotFound($"Exam with id {exam_id} not found.");
                }
            }
            else
            {
                return NotFound("Please provide valid action !");
            }
        }

        #endregion
    }
}
