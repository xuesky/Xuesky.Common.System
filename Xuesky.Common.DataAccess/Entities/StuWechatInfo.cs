using FreeSql.DataAnnotations;
using System;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 微信关注表
	/// </summary>
	[Table(Name = "stu_wechat_info")]
	public partial class StuWechatInfo
	{

		/// <summary>
		/// 主键
		/// </summary>
		[Column(Name = "chat_id", IsPrimary = true, IsIdentity = true)]
		public int ChatId { get; set; }

		/// <summary>
		/// 学生ID
		/// </summary>
		[Column(Name = "stu_id")]
		public int? StuId
		{
			get => _StuId; set
			{
				if (_StuId == value) return;
				_StuId = value;
				stu_info = null;
			}
		}
		private int? _StuId;

		/// <summary>
		/// 城市
		/// </summary>
		[Column(Name = "city", DbType = "varchar(255)")]
		public string City { get; set; }

		/// <summary>
		/// 头像
		/// </summary>
		[Column(Name = "headimgurl", DbType = "varchar(512)")]
		public string Headimgurl { get; set; }

		/// <summary>
		/// 删除
		/// </summary>
		[Column(Name = "is_delete")]
		public bool? IsDelete { get; set; } = false;

		/// <summary>
		/// 启用
		/// </summary>
		[Column(Name = "is_use")]
		public bool? IsUse { get; set; } = true;

		/// <summary>
		/// 昵称
		/// </summary>
		[Column(Name = "nick_name", DbType = "varchar(255)")]
		public string NickName { get; set; }

		/// <summary>
		/// open_id
		/// </summary>
		[Column(Name = "open_id", DbType = "varchar(255)")]
		public string OpenId { get; set; }

		/// <summary>
		/// 省份
		/// </summary>
		[Column(Name = "province", DbType = "varchar(255)")]
		public string Province { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		[Column(Name = "sex", DbType = "nchar(2)")]
		public string Sex { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[Column(Name = "state")]
		public bool? State { get; set; }

		/// <summary>
		/// 关注日期
		/// </summary>
		[Column(Name = "sub_time", DbType = "datetime2")]
		public DateTime? SubTime { get; set; }


		#region 外键 => 导航属性，ManyToOne/OneToOne

		[Navigate("StuId")]
		public virtual StuInfo stu_info { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
