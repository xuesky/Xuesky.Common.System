
namespace Xuesky.Common.Service
{

    /// <summary>
    /// 学生基本信息视图
    /// </summary>
    public class StuInfoOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int StuId { get; set; }

        /// <summary>
        /// 班级ID
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string StuGender { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string StuMobile { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string StuName { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StuNo { get; set; }

        /// <summary>
        /// 学分
        /// </summary>
        public int? StuScore { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool IsUse { get; set; } = true;
    }

}
