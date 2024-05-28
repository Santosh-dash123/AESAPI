using AESAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AESAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLoginAPIController : ControllerBase
    {
        private readonly AESDBContext _context;

        public StudentLoginAPIController(AESDBContext context)
        {
            _context = context;
        }

        [HttpPost("ValidateCredentialsOfStudent")]
        public IActionResult ValidateCredentialsOfStudent(string StdMobNo, string StdPwd)
        {
            var user = _context.TblMStudents
                .FirstOrDefault(u => u.StdMobNo == StdMobNo && u.StdPwd == StdPwd);
            ValidateCredentialsResponse response = new ValidateCredentialsResponse();
            if (user != null)
            {
                response.IsSuccess = true;
                response.Message = "Credentials are correct";
                return Ok(response);
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid Student Mobile number or password!";
                return Ok(response);
            }
        }

        [HttpGet("GetStudentData/{stdMobNo}")]
        public async Task<ActionResult<StudentData>> GetStudentData(string stdMobNo)
        {
            var student = await _context.TblMStudents
                .Where(s => s.StdMobNo == stdMobNo && s.DFlag != 1)
                .FirstOrDefaultAsync();
            var studentData = new StudentData
            {
                StdFrstname = student.StdFrstname,
                StdLstname = student.StdLstname,
                StdId = student.StdId
            };
            return Ok(studentData);
        }

        [HttpGet("GetStudentInfo/{stdid}")]
        public async Task<ActionResult<List<StudentInfo>>> GetStudentInfo(long stdid)
        {
            var student = await _context.TblMStudents
                .Where(s => s.StdId == stdid && s.DFlag != 1)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            var studentInfoList = new List<StudentInfo>();

            var examSetupStudents = await _context.TblMExamSetupStudents
                .Include(es => es.Std)
                .Where(es => es.StdId == student.StdId)
                .ToListAsync();

            foreach (var examSetupStudent in examSetupStudents)
            {
                
                    var studentInfo = new StudentInfo
                    {
                        AttndStatus = examSetupStudent.AttndStatus
                    };

                    var examSetup = await _context.TblMExamSetups
                        .Where(es => es.ExamsetupId == examSetupStudent.ExamsetupId)
                        .FirstOrDefaultAsync();

                if (examSetup != null)
                {
                    studentInfo.ExamDate = examSetup.ExamDate ?? DateTime.MinValue;
                    studentInfo.Fromtime = examSetup.Fromtime ?? string.Empty;
                    studentInfo.Totime = examSetup.Totime ?? string.Empty;

                    var exam = await _context.TblMExams
                        .Where(e => e.ExamId == examSetup.ExamId)
                        .FirstOrDefaultAsync();

                    if (exam != null)
                    {
                        studentInfo.ExamId = exam.ExamId;
                        studentInfo.ExamName = exam.ExamName ?? string.Empty;
                        studentInfo.ExamDuration = exam.ExamDuration ?? 0;
                        studentInfo.ExamSubject = exam.ExamSubject ?? string.Empty;
                        studentInfo.Tnoqans = exam.Tnoqans ?? 0;
                        studentInfo.Passmark = exam.Passmark ?? 0;
                        studentInfo.ExamDescr = exam.ExamDescr ?? string.Empty;
                    }

                    studentInfoList.Add(studentInfo);
                }
            }

            return studentInfoList;
        }

        [HttpGet("GetQuestionAndOptionsUsingExamId/{examid}")]
        public async Task<ActionResult<List<Questionandoptions>>> GetQuestionAndOptionsUsingExamId(long examid)
        {
            var exam = await _context.TblMExams.FirstOrDefaultAsync(e => e.ExamId == examid && e.DFlag != 1);
            if (exam == null)
            {
                return NotFound();
            }

            var questionandoptionsList = await _context.TblMQuestionans
                .Where(q => q.ExamId == examid && q.DFlag != 1)
                .Select(q => new Questionandoptions
                {
                    QansId = q.QansId,
                    QansContent = q.QansContent,
                    Options = _context.TblMOptions
                        .Where(o => o.QansId == q.QansId && o.DFlag != 1)
                        .Select(o => new options
                        {
                            OptionId = o.OptionId,
                            OptionContent = o.OptionContent,
                            OptionStatus = o.OptionStatus
                        })
                        .ToList()
                })
                .ToListAsync();

            return questionandoptionsList;
        }
    }
}
