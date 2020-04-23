using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ClassController : Controller
    {
        private readonly IClassService classService;

        public ClassController(IClassService classService)
        {
            this.classService = classService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddClass()
        {
            return View();
        }
        public async Task<IActionResult> EditClass(int classId)
        {
            ViewBag.Classs = await classService.GetClassList(s => true);
            ViewBag.ClassId = classId;
            return View();
        }
        public async Task<JsonResult> GetClass(int classId)
        {
            var role = await classService.GetClass(classId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, role));
        }
        [HttpPost]
        public async Task<JsonResult> EditClass(ClassInfoUpdateInput classInfoUpdateInput)
        {
            var ClassList = await classService.GetClassList(s => s.ClassNo == classInfoUpdateInput.ClassNo);
            if (ClassList.Any() && ClassList[0].ClassId != classInfoUpdateInput.ClassId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,班级编号:[{classInfoUpdateInput.ClassNo}]已经存在"));
            }
            var result = await classService.UpdateClass(s => s.ClassId == classInfoUpdateInput.ClassId,
                classInfoUpdateInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddClass
        /// </summary>
        /// <param name="classInfoAddInput"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddClass(ClassInfoAddInput classInfoAddInput)
        {
            var classList = await classService.GetClassList(s => s.ClassNo == classInfoAddInput.ClassNo);
            if (classList.Any())
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,班级编号:[{classInfoAddInput.ClassNo}]已经被占用"));
            }
            var result = await classService.InsertClass(classInfoAddInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("添加成功", result) : JsonResultWrap.Fail("添加失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int classId)
        {
            var result = await classService.DeleteClass(new int[] { classId });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("删除成功", result) : JsonResultWrap.Fail("删除失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Use(int ClassId, bool isUse)
        {
            var result = await classService.UseOrStopClass(new int[] { ClassId }, isUse);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("更新成功", result) : JsonResultWrap.Fail("更新失败"));
        }
        public async Task<JsonResult> GetClassList(int page, int limit, string key)
        {
            var (total, list) = await classService.GetClassListPage(page, limit, key);
            return new JsonResult(JsonResultWrap.Success("OK", total, list));
        }
    }
}