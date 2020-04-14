using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xuesky.Common.ClassLibary.Wrap;
using Xuesky.Common.DataAccess;
using Xuesky.Common.Service;

namespace Xuesky.Common.Web.Areas.Admin.Controllers
{
    [Area("admin")]
    public class StuController : Controller
    {
        private readonly IStuService stuService;
        private readonly IClassService classService;

        public StuController(IStuService stuService,IClassService classService)
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
        public async Task<IActionResult> EditStu(int StuId)
        {
            ViewBag.Classes = await classService.GetClassList(s => true);
            ViewBag.StuId = StuId;
            return View();
        }
        public async Task<JsonResult> GetStu(int StuId)
        {
            var stu = await stuService.GetStu(StuId);
            return new JsonResult(JsonResultWrap.Success("获取成功", 1, stu));
        }
        [HttpPost]
        public async Task<JsonResult> EditStu(StuInfo vmStu)
        {
            var stuList = await stuService.GetStuList(s => s.StuNo == vmStu.StuNo);
            if (stuList.Any() && stuList[0].StuId != vmStu.StuId)
            {
                return new JsonResult(JsonResultWrap.Fail($"修改失败,学号:[{vmStu.StuNo}]已经存在"));
            }
            var result = await stuService.UpdateStu(s => s.StuId == vmStu.StuId,
                o => new { vmStu.StuNo, vmStu.StuName, vmStu.StuGender, vmStu.StuMobile, vmStu.IsUse, vmStu.ClassId });
            return new JsonResult(result > 0 ? JsonResultWrap.Success("修改成功", result) : JsonResultWrap.Fail("修改失败"));
        }
        /// <summary>
        /// AddStu
        /// </summary>
        /// <param name="vmStu"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        [HttpPost]
        public async Task<JsonResult> AddStu(StuInfo vmStu)
        {
            var stuList = await stuService.GetStuList(s => s.StuNo == vmStu.StuNo);
            if (stuList.Any() && stuList.First().StuId
                != vmStu.StuId)
            {
                return new JsonResult(JsonResultWrap.Fail($"添加失败,工号:[{vmStu.StuNo}]已经被占用"));
            }
            var result = await stuService.InsertStu(vmStu);
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