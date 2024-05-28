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
    public class UserAPIController : ControllerBase
    {
        private readonly AESDBContext _context;
        public UserAPIController(AESDBContext context)
        {
            _context = context;
        }

        #region User_Api
        [Route("IUDUser")]
        [AcceptVerbs("POST", "PUT", "DELETE")]
        public async Task<IActionResult> IUDUser(TblMUser user, string action)
        {
            try
            {
                if(action == "INSERT" || action == "UPDATE" || action == "DELETE")
                {
                    var outputparameter = new SqlParameter("@msg", SqlDbType.VarChar, 400) { Direction = ParameterDirection.Output };
                    var parameters = new[]
                    {
                        new SqlParameter("@UId", user.UId),
                        new SqlParameter("@UName", user.UName),
                        new SqlParameter("@UMobno", user.UMobno),
                        new SqlParameter("@UEmail", user.UEmail),
                        new SqlParameter("@UAddrss", user.UAddrss),
                        new SqlParameter("@UType", user.UType),
                        new SqlParameter("@UPwd", user.UPwd),
                        new SqlParameter("@DFlag", user.DFlag ?? 0), // Default value for DFlag
                        new SqlParameter("@CreatedBy", user.CreatedBy ?? 0), // Default value for CreatedBy
                        new SqlParameter("@CreatedDate",user.CreatedDate ?? DateTime.Now),
                        new SqlParameter("@CreatedIp", user.CreatedIp ?? ""),
                        new SqlParameter("@ModifyBy", user.ModifyBy ?? 0), // Default value for ModifyBy
                        new SqlParameter("@ModifyDate", user.ModifyDate ?? DateTime.Now),
                        new SqlParameter("@ModifyIp", user.ModifyIp ?? ""),
                        new SqlParameter("@action", action),
                        outputparameter
                    };
                    // Execute the stored procedure
                    var affectedRows = await _context.Database.ExecuteSqlRawAsync
                        ("EXECUTE [USP_Tbl_M_User_IUD] @UId, @UName, @UMobno, @UEmail, @UAddrss, @UType, @UPwd,@DFlag,@CreatedBy,@CreatedDate,@CreatedIp,@ModifyBy,@ModifyDate,@ModifyIp,@action,@msg OUTPUT", parameters);
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

        [Route("VIEWUSER")]
        [HttpGet]
        public async Task<IActionResult> VIEWUSER(string action, long u_id)
        {
            if (action == "All" && u_id <= 0)
            {
                var users = await _context.TblMUsers.Where(user => user.DFlag == 0).ToListAsync();
                if (users != null)
                {
                    return Ok(users);
                }
                return Ok(users);
            }
            else if (action == "AllById" && u_id >= 0)
            {
                var user = await _context.TblMUsers
                .Where(u => u.UId == u_id)  // Assuming UId is the property representing the user id
                .FirstOrDefaultAsync();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound($"User with id {u_id} not found.");
                }
            }
            else
            {
                return NotFound("Please provide valid action !");
            }
        }
        #endregion

        #region question_options_api
        [Route("InsertQuestionWithOptions")]
        [HttpPost]
        public IActionResult InsertQuestionWithOptions([FromBody] QuestionModel questionModel)
        {
            try
            {
                var sqlCommand = "DECLARE @Options dbo.OptionCSType; " +
                                 "INSERT INTO @Options (OptionContent, OptionStatus) " +
                                 "VALUES {0}; " +
                                 "EXEC InsertQuestionWithOptions @ExamId = @p0, @QuestionContent = @p1, @OptionsList = @Options;";

                var values = string.Join(", ", questionModel.Options.Select(o => $"('{o.content}', '{o.status}')"));

                sqlCommand = string.Format(sqlCommand, values);

                _context.Database.ExecuteSqlRaw(sqlCommand, questionModel.ExamId, questionModel.QuestionContent);

                return Ok("Question and options saved Successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("GetQuestionAndOptionsData")]
        public IActionResult GetData()
        {
            try
            {
                    var questions = _context.TblMQuestionans
                   .Where(q => q.DFlag != 1)
                   .Join(
                       _context.TblMExams,
                       q => q.ExamId,
                       e => e.ExamId,
                       (q, e) => new
                       {
                           q.QansId,
                           ExamName = e.ExamName, // Include ExamName in the result
                           q.QansContent
                       })
                   .ToList();

                    return Ok(questions);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetOptionsByQuestionId/{questionId}")]
        public IActionResult GetOptionsByQuestionId(long questionId)
        {
            try
            {
                var options = _context.TblMOptions
                    .Where(o => o.QansId == questionId && o.DFlag != 1)
                    .Select(o => new
                    {
                        o.OptionContent,
                        o.OptionStatus
                    })
                    .ToList();

                return Ok(options);
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getQuestionAndOptions/{qansId}")]
        public IActionResult GetQuestionAndOptions(long qansId)
        {
            // Retrieve the question based on QansId
            var question = _context.TblMQuestionans.SingleOrDefault(q => q.QansId == qansId);

            if (question == null)
            {
                return NotFound();
            }

            // Retrieve the options based on QansId
            var options = _context.TblMOptions.Where(o => o.QansId == qansId).ToList();

            // Map the data to the DTO
            var questionDTO = new QuestionModel
            {
                ExamId = Convert.ToInt32(question.ExamId ?? 0),
                QuestionContent = question.QansContent,
                Options = options.Select(o => new OptionModel
                {
                    content = o.OptionContent,
                    status = o.OptionStatus
                }).ToList()
            };

            return Ok(questionDTO);
        }

        [HttpPut("updateQuestionAndOptions/{id}")]
        public async Task<IActionResult> UpdateQuestionAndOptions(long id, [FromBody] QuestionModel updateModel)
        {
            var question = await _context.TblMQuestionans.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            // Update exam ID and question content
            question.ExamId = updateModel.ExamId;
            question.QansContent = updateModel.QuestionContent;

            // Remove existing options for the question
            var existingOptions = await _context.TblMOptions.Where(o => o.QansId == id).ToListAsync();
            _context.TblMOptions.RemoveRange(existingOptions);

            // Add new options
            foreach (var option in updateModel.Options)
            {
                var newOption = new TblMOption
                {
                    ExamId = updateModel.ExamId,
                    QansId = id,
                    OptionContent = option.content,
                    OptionStatus = option.status
                };

                _context.TblMOptions.Add(newOption);
            }
            await _context.SaveChangesAsync();

            return Ok("Question and options updated successfully!");
        }

        [HttpDelete("DeleteQuestionAndOptions/{questionId}")]
        public IActionResult DeleteQuestionAndOptions(long questionId)
        {
            try
            {
                // Update DFlag to 1 in TblMQuestionans for the specified QansId
                var questionanRecord = _context.TblMQuestionans
                    .FirstOrDefault(q => q.QansId == questionId && q.DFlag != 1);

                if (questionanRecord != null)
                {
                    questionanRecord.DFlag = 1;

                    // Update DFlag to 1 in TblMOptions for the corresponding QansId
                    var optionRecords = _context.TblMOptions
                        .Where(o => o.QansId == questionId && o.DFlag != 1)
                        .ToList();

                    foreach (var option in optionRecords)
                    {
                        option.DFlag = 1;
                    }

                    // Save changes to the database
                    _context.SaveChanges();

                    return Ok($"Question and options deleted successfully!");
                }
                else
                {
                    return NotFound($"No record found for QansId {questionId} or DFlag is already 1.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating DFlag: {ex.Message}");
            }
        }

        [HttpGet("GetListOfExamnameAndExamid")]
        public IActionResult GetListOfExamnameAndExamid()
        {
            try
            {
                var examList = _context.TblMExams.Where(e => e.DFlag == 0).Select(e => new { e.ExamId, e.ExamName }).ToList();
                return Ok(examList);
            }
            catch(Exception)
            {
                return BadRequest("Exam not present in this table.");
            }
        }

        #endregion
    }
}
