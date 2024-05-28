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
    public class StudentAPIController : ControllerBase
    {
        private readonly AESDBContext _context;
        public StudentAPIController(AESDBContext context)
        {
            _context = context;
        }

        #region Student_Api
        [Route("IUDStudent")]
        [AcceptVerbs("POST", "PUT", "DELETE")]
        public async Task<IActionResult> IUDStudent(TblMStudent student, string action)
        {
            try
            {
                if (action == "INSERT" || action == "UPDATE" || action == "DELETE")
                {
                    if (action == "DELETE")
                    {
                        // For DELETE action, set StdDob to DateTime.Now
                        student.StdDob = DateTime.Now;
                    }
                    var outputparameter = new SqlParameter("@msg", SqlDbType.VarChar, 400) { Direction = ParameterDirection.Output };
                    var parameters = new[]
                    {
                        new SqlParameter("@StdId", student.StdId),
                        new SqlParameter("@StdFrstname", student.StdFrstname),
                        new SqlParameter("@StdLstname", student.StdLstname),
                        new SqlParameter("@StdEmail", student.StdEmail),
                        new SqlParameter("@StdGnder", student.StdGnder),
                        new SqlParameter("@StdDob", student.StdDob),
                        new SqlParameter("@StdAdharno", student.StdAdharno),
                        new SqlParameter("@StdMobNo", student.StdMobNo),
                        new SqlParameter("@StdPhoto", student.StdPhoto),
                        new SqlParameter("@StdPermntadrs", student.StdPermntadrs),
                        new SqlParameter("@StdCurntadrs", student.StdCurntadrs),
                        new SqlParameter("@StdBldgrp", student.StdBldgrp),
                        new SqlParameter("@StdNationality", student.StdNationality),
                        new SqlParameter("@StdReligion", student.StdReligion),
                        new SqlParameter("@StdState", student.StdState),
                        new SqlParameter("@StdPwd", student.StdPwd),
                        new SqlParameter("@DFlag", student.DFlag ?? 0), // Default value for DFlag
                        new SqlParameter("@CreatedBy", student.CreatedBy ?? 0), // Default value for CreatedBy
                        new SqlParameter("@CreatedDate",student.CreatedDate ?? DateTime.Now),
                        new SqlParameter("@CreatedIp", student.CreatedIp ?? ""),
                        new SqlParameter("@ModifyBy", student.ModifyBy ?? 0), // Default value for ModifyBy
                        new SqlParameter("@ModifyDate", student.ModifyDate ?? DateTime.Now),
                        new SqlParameter("@ModifyIp", student.ModifyIp ?? ""),
                        new SqlParameter("@action", action),
                        outputparameter
                    };
                    // Execute the stored procedure
                    var affectedRows = await _context.Database.ExecuteSqlRawAsync
                        ("EXECUTE [USP_Tbl_M_Student_IUD] @StdId, @StdFrstname, @StdLstname, @StdEmail, @StdGnder, @StdDob, @StdAdharno," +
                        "@StdMobNo, @StdPhoto, @StdPermntadrs, @StdCurntadrs, @StdBldgrp, @StdNationality, @StdReligion," +
                        "@StdState, @StdPwd,@DFlag,@CreatedBy,@CreatedDate,@CreatedIp,@ModifyBy,@ModifyDate,@ModifyIp,@action,@msg OUTPUT", parameters);
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

        [Route("VIEWSTUDENT")]
        [HttpGet]
        public async Task<IActionResult> VIEWSTUDENT(string action, long std_id)
        {
            if (action == "All" && std_id <= 0)
            {
                var students = await _context.TblMStudents.Where(user => user.DFlag == 0).ToListAsync();
                if (students != null)
                {
                    return Ok(students);
                }
                return Ok(students);
            }
            else if (action == "AllById" && std_id >= 0)
            {
                var user = await _context.TblMStudents
                .Where(u => u.StdId == std_id).FirstOrDefaultAsync();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound($"User with id {std_id} not found.");
                }
            }
            else
            {
                return NotFound("Please provide valid action !");
            }
        }

        [HttpGet("GetListOfStudentNameAndStudentId")]
        public IActionResult GetListOfStudentNameAndStudentId()
        {
            try
            {
                var students = _context.TblMStudents.Where(std => std.DFlag == 0).Select(std => new { std.StdId, std.StdFrstname, std.StdLstname }).ToList();
                return Ok(students);
            }
            catch (Exception)
            {
                return BadRequest("Student not present in this table.");
            }
        }
        #endregion
    }
}
