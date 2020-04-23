namespace Xuesky.Common.Service
{
    /// <summary>
    /// 班级基本信息视图
    /// </summary>
    public class ClassInfoOutput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public int ClassId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string ClassNo { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool? IsUse { get; set; }
    }

}
