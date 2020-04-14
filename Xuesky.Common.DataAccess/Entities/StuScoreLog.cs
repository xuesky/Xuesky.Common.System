using FreeSql.DataAnnotations;
using System;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 学分明细表
	/// </summary>
	[Table(Name = "stu_score_log")]
	public partial class StuScoreLog
	{

		/// <summary>
		/// 主键
		/// </summary>
		[Column(Name = "log_id", IsPrimary = true, IsIdentity = true)]
		public int LogId { get; set; }

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
		/// 处理日期
		/// </summary>
		[Column(Name = "change_date", DbType = "datetime2")]
		public DateTime ChangeDate { get; set; }

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
		/// 操作人编码
		/// </summary>
		[Column(Name = "operator_code", DbType = "varchar(255)")]
		public string OperatorCode { get; set; }

		/// <summary>
		/// 操作人姓名
		/// </summary>
		[Column(Name = "operator_name", DbType = "varchar(255)")]
		public string OperatorName { get; set; }

		/// <summary>
		/// 学分
		/// </summary>
		[Column(Name = "score")]
		public int Score { get; set; }


		#region 外键 => 导航属性，ManyToOne/OneToOne

		[Navigate("StuId")]
		public virtual StuInfo stu_info { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
