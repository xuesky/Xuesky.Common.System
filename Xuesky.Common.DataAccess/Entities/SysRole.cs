using FreeSql.DataAnnotations;
using System.Collections.Generic;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 系统角色表
	/// </summary>
	[Table(Name = "sys_role")]
	public partial class SysRole
	{

		/// <summary>
		/// 角色主键
		/// </summary>
		[Column(Name = "role_id", IsPrimary = true, IsIdentity = true)]
		public int RoleId { get; set; }

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
		/// 角色说明
		/// </summary>
		[Column(Name = "role_desc", DbType = "varchar(MAX)")]
		public string RoleDesc { get; set; }

		/// <summary>
		/// 角色名称
		/// </summary>
		[Column(Name = "role_name", DbType = "varchar(64)")]
		public string RoleName { get; set; }


		#region 外键 => 导航属性，OneToMany

		[Navigate("RoleId")]
		public virtual List<SysRoleModule> sys_role_modules { get; set; }

		[Navigate("RoleId")]
		public virtual List<SysUserRole> sys_user_roles { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
