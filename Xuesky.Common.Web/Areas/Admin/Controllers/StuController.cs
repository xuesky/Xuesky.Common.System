using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xuesky.Common.ClassLibary;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 学生信息管理
    /// </summary>
    [Area("admin")]
    public class StuController : Controller
    {
        private readonly IStuService stuService;
        private readonly IClassService classService;

        public StuController(IStuService stuService, IClassService classService)
        {
            this.stuService = stuService;
            this.classService = classService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddStu()
        {
            ViewBag.Classes = await classService.GetClassList(s => true);
            return View();
        }
        public async Task<IActionResult> EditStu(int stuId)
        {
            ViewBag.Classes = await classService.GetClassList(s => true);
            ViewBag.StuId = stuId;
            return View();
        }
        public async Task<JsonResult> GetStu(int stuId)
        {
            var stu = await stuService.GetStu(stuId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, stu));
        }
        [HttpPost]
        public async Task<JsonResult> EditStu(StuInfoUpdateInput stuInfoUpdateInput)
        {
            var stuList = await stuService.GetStuList(s => s.StuNo == stuInfoUpdateInput.StuNo);
            if (stuList.Any() && stuList[0].StuId != stuInfoUpdateInput.StuId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,学号:[{stuInfoUpdateInput.StuNo}]已经存在"));
            }
            var result = await stuService.UpdateStu(s => s.StuId == stuInfoUpdateInput.StuId,
                stuInfoUpdateInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddStu
        /// </summary>
        /// <param name="stuInfoAddInput"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddStu(StuInfoAddInput stuInfoAddInput)
        {
            var stuList = await stuService.GetStuList(s => s.StuNo == stuInfoAddInput.StuNo);
            if (stuList.Any() && stuList.First().StuId
                != stuInfoAddInput.StuId)
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,工号:[{stuInfoAddInput.StuNo}]已经被占用"));
            }
            var result = await stuService.InsertStu(stuInfoAddInput);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("添加成功", result) : JsonResultWrap.Fail("添加失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int stuId)
        {
            var result = await stuService.DeleteStu(new int[] { stuId });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("删除成功", result) : JsonResultWrap.Fail("删除失败"));
        }
        [HttpPost]
        public async Task<JsonResult> Use(int stuId, bool isUse)
        {
            var result = await stuService.UseOrStopStu(new int[] { stuId }, isUse);
            return new JsonResult(result > 0 ? JsonResultWrap.Success("更新成功", result) : JsonResultWrap.Fail("更新失败"));
        }
        public async Task<JsonResult> GetStuList(int page, int limit, string key)
        {
            var (total, list) = await stuService.GetStuListPage(page, limit, key);
            return new JsonResult(JsonResultWrap.Success("OK", total, list));
        }
    }
}