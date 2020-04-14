using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 系统用户表
	/// </summary>
	[Table(Name = "sys_user")]
	public partial class SysUser
	{

		/// <summary>
		/// 用户主键
		/// </summary>
		[Column(Name = "user_id", IsPrimary = true, CanUpdate = false, IsIdentity = true)]
		public int UserId { get; set; }

		/// <summary>
		/// 组织ID
		/// </summary>
		[Column(Name = "org_id")]
		public int OrgId
		{
			get => _OrgId; set
			{
				if (_OrgId == value) return;
				_OrgId = value;
				sys_organization = null;
			}
		}
		private int _OrgId;

		/// <summary>
		/// 删除
		/// </summary>
		[Column(Name = "is_delete")]
		public bool IsDelete { get; set; } = false;

		/// <summary>
		/// 启用
		/// </summary>
		[Column(Name = "is_use")]
		public bool? IsUse { get; set; } = true;

		/// <summary>
		/// 用户关联老师ID
		/// </summary>
		[Column(Name = "teacher_id")]
		public int? TeacherId { get; set; }

		/// <summary>
		/// 添加时间
		/// </summary>
		[Column(Name = "user_addtime", DbType = "datetime2")]
		public DateTime UserAddtime { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		[Column(Name = "user_gender", DbType = "nchar(1)")]
		public string UserGender { get; set; }

		/// <summary>
		/// 最后登录时间
		/// </summary>
		[Column(Name = "user_lasttime", DbType = "datetime2")]
		public DateTime UserLasttime { get; set; }

		/// <summary>
		/// 登录ID
		/// </summary>
		[Column(Name = "user_login", DbType = "varchar(255)")]
		public string UserLogin { get; set; }

		/// <summary>
		/// 姓名
		/// </summary>
		[Column(Name = "user_name", DbType = "varchar(64)")]
		public string UserName { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		[Column(Name = "user_pwd", DbType = "varchar(255)")]
		public string UserPwd { get; set; }

		/// <summary>
		/// 手机
		/// </summary>
		[Column(Name = "user_tel", DbType = "varchar(32)")]
		public string UserTel { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		[Column(Name = "remark", DbType = "varchar(512)")]
		public string Remark { get; set; }

		#region 外键 => 导航属性，ManyToOne/OneToOne

		[Navigate("OrgId")]
		public virtual SysOrganization sys_organization { get; set; }

		#endregion

		#region 外键 => 导航属性，OneToMany

		[Navigate("UserId")]
		public virtual List<SysUserRole> sys_user_roles { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
