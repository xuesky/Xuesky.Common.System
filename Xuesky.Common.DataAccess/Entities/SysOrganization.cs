using FreeSql.DataAnnotations;
using System.Collections.Generic;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 组织机构表
	/// </summary>
	[Table(Name = "sys_organization")]
	public partial class SysOrganization
	{

		/// <summary>
		/// 组织主键
		/// </summary>
		[Column(Name = "org_id", IsPrimary = true, IsIdentity = true)]
		public int OrgId { get; set; }

		/// <summary>
		/// 删除
		/// </summary>
		[Column(Name = "is_delete")]
		public bool? IsDelete { get; set; }

		/// <summary>
		/// 是否启用
		/// </summary>
		[Column(Name = "is_use")]
		public bool? IsUse { get; set; }

		/// <summary>
		/// 组织编码
		/// </summary>
		[Column(Name = "org_code", DbType = "varchar(128)")]
		public string OrgCode { get; set; }

		/// <summary>
		/// 部门负责人
		/// </summary>
		[Column(Name = "org_contact", DbType = "varchar(255)")]
		public string OrgContact { get; set; }

		/// <summary>
		/// 负责人电话
		/// </summary>
		[Column(Name = "org_contact_tel", DbType = "varchar(255)")]
		public string OrgContactTel { get; set; }

		/// <summary>
		/// 组织名称
		/// </summary>
		[Column(Name = "org_name", DbType = "varchar(128)")]
		public string OrgName { get; set; }


		#region 外键 => 导航属性，OneToMany

		[Navigate("OrgId")]
		public virtual List<SysUser> sys_users { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
