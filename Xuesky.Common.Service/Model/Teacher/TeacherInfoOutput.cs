
namespace Xuesky.Common.Service
{

    /// <summary>
    /// 老师基本信息视图
    /// </summary>
    public class TeacherInfoOutput
    {
        /// <summary>
        /// 班级ID
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string TeacherNo { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string TeacherGender { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string TeacherMobile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool IsUse { get; set; } = true;

    }

}
