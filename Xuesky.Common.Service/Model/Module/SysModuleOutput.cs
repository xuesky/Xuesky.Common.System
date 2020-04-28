namespace Xuesky.Common.Service
{

    /// <summary>
    /// 系统模块视图
    /// </summary>
    public class SysModuleOutput
    {
        /// <summary>
        /// 模块主键
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        public string FontClass { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string ModuleUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool IsUse { get; set; }
        /// <summary>
        /// 是否包含该模块权限
        /// </summary>
        public bool IsProcess { get; set; }
    }
}
