using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
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
        public async Task<JsonResult> EditTeacher(TeacherInfoUpdateInput teacherInfoUpdateInput)
        {
            var TeacherList = await teacherService.GetTeacherList(s => s.TeacherNo == teacherInfoUpdateInput.TeacherNo);
            if (TeacherList.Any() && TeacherList[0].TeacherId != teacherInfoUpdateInput.TeacherId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,工号:[{teacherInfoUpdateInput.TeacherNo}]已经存在"));
            }
            var result = await teacherService.UpdateTeacher(s => s.TeacherId == teacherInfoUpdateInput.TeacherId,
                teacherInfoUpdateInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddTeacher
        /// </summary>
        /// <param name="teacherInfoAddInput"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddTeacher(TeacherInfoAddInput teacherInfoAddInput)
        {
            var TeacherList = await teacherService.GetTeacherList(s => s.TeacherNo == teacherInfoAddInput.TeacherNo);
            if (TeacherList.Any() && TeacherList.First().TeacherId
                != teacherInfoAddInput.TeacherId)
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,工号:[{teacherInfoAddInput.TeacherNo}]已经被占用"));
            }
            var result = await teacherService.InsertTeacher(teacherInfoAddInput);
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