using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary.Wrap;
using Xuesky.Common.DataAccess;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddTeacher()
        {
            return View();
        }
        public IActionResult EditTeacher(int teacherId)
        {
            ViewBag.TeacherId = teacherId;
            return View();
        }
        public async Task<JsonResult> GetTeacher(int teacherId)
        {
            var teacher = await teacherService.GetTeacher(teacherId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, teacher));
        }
        [HttpPost]
        public async Task<JsonResult> EditTeacher(TeacherInfo vmTeacher)
        {
            var TeacherList = await teacherService.GetTeacherList(s => s.TeacherNo == vmTeacher.TeacherNo);
            if (TeacherList.Any() && TeacherList[0].TeacherId != vmTeacher.TeacherId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,工号:[{vmTeacher.TeacherNo}]已经存在"));
            }
            var result = await teacherService.UpdateTeacher(s => s.TeacherId == vmTeacher.TeacherId,
                o => new { vmTeacher.TeacherNo, vmTeacher.TeacherName, vmTeacher.TeacherGender, vmTeacher.TeacherMobile, vmTeacher.IsUse, vmTeacher.Remark });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddTeacher
        /// </summary>
        /// <param name="vmTeacher"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddTeacher(TeacherInfo vmTeacher)
        {
            var TeacherList = await teacherService.GetTeacherList(s => s.TeacherNo == vmTeacher.TeacherNo);
            if (TeacherList.Any() && TeacherList.First().TeacherId
                != vmTeacher.TeacherId)
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,工号:[{vmTeacher.TeacherNo}]已经被占用"));
            }
            var result = await teacherService.InsertTeacher(vmTeacher);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("添加成功", result) : JsonResultWrap.Fail("添加失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int teacherId)
        {
            var result = await teacherService.DeleteTeacher(new int[] { teacherId });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("删除成功", result) : JsonResultWrap.Fail("删除失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Use(int teacherId, bool isUse)
        {
            var result = await teacherService.UseOrStopTeacher(new int[] { teacherId }, isUse);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("更新成功", result) : JsonResultWrap.Fail("更新失败"));
        }
        public async Task<JsonResult> GetTeacherList(int page, int limit, string key)
        {
            var (total, list) = await teacherService.GetTeacherListPage(page, limit, key);
            return new JsonResult(JsonResultWrap.Success("OK", total, list));
        }
    }
}