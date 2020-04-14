using FreeSql.DataAnnotations;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 角色功能模块表
	/// </summary>
	[Table(Name = "sys_role_module")]
	public partial class SysRoleModule
	{

		/// <summary>
		/// 角色模块主键
		/// </summary>
		[Column(Name = "role_module_id", IsPrimary = true, IsIdentity = true)]
		public int RoleModuleId { get; set; }

		[Column(Name = "module_id")]
		public int ModuleId
		{
			get => _ModuleId; set
			{
				if (_ModuleId == value) return;
				_ModuleId = value;
				sys_module = null;
			}
		}
		private int _ModuleId;

		[Column(Name = "role_id")]
		public int RoleId
		{
			get => _RoleId; set
			{
				if (_RoleId == value) return;
				_RoleId = value;
				sys_role = null;
			}
		}
		private int _RoleId;

		/// <summary>
		/// 是否删除
		/// </summary>
		[Column(Name = "is_delete")]
		public bool IsDelete { get; set; } = false;


		#region 外键 => 导航属性，ManyToOne/OneToOne

		[Navigate("RoleId")]
		public virtual SysRole sys_role { get; set; }

		[Navigate("ModuleId")]
		public virtual SysModule sys_module { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
