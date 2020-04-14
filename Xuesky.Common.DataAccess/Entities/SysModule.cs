using FreeSql.DataAnnotations;
using System.Collections.Generic;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 系统模块表
	/// </summary>
	[Table(Name = "sys_module")]
	public partial class SysModule
	{
		/// <summary>
		/// 模块主键
		/// </summary>
		[Column(Name = "module_id", IsPrimary = true, IsIdentity = true)]
		public int ModuleId { get; set; }

		/// <summary>
		/// 图标样式
		/// </summary>
		[Column(Name = "font_class", DbType = "varchar(64)")]
		public string FontClass { get; set; }

		/// <summary>
		/// 删除
		/// </summary>
		[Column(Name = "is_delete")]
		public bool IsDelete { get; set; } = false;

		/// <summary>
		/// 启用
		/// </summary>
		[Column(Name = "is_use")]
		public bool IsUse { get; set; } = true;

		/// <summary>
		/// 模块编码
		/// </summary>
		[Column(Name = "module_code", DbType = "varchar(128)")]
		public string ModuleCode { get; set; }

		/// <summary>
		/// 模块名称
		/// </summary>
		[Column(Name = "module_name", DbType = "varchar(128)")]
		public string ModuleName { get; set; }

		/// <summary>
		/// url
		/// </summary>
		[Column(Name = "module_url", DbType = "varchar(255)")]
		public string ModuleUrl { get; set; }

		/// <summary>
		/// 排序
		/// </summary>
		[Column(Name = "order")]
		public int? Order { get; set; }


		#region 外键 => 导航属性，OneToMany

		[Navigate("ModuleId")]
		public virtual List<SysRoleModule> sys_role_modules { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
