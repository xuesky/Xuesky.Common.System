using FreeSql.DataAnnotations;

namespace Xuesky.Common.DataAccess
{

	/// <summary>
	/// 班级基本信息表
	/// </summary>
	[Table(Name = "class_teacher_info")]
	public partial class ClassTeacherInfo
	{

		/// <summary>
		/// 班级ID
		/// </summary>
		[Column(Name = "class_teacher_id", IsPrimary = true, IsIdentity = true)]
		public int ClassTeacherId { get; set; }

		/// <summary>
		/// 编码
		/// </summary>
		[Column(Name = "class_id")]
		public int ClassId
		{
			get => _ClassId; set
			{
				if (_ClassId == value) return;
				_ClassId = value;
				class_info = null;
			}
		}
		private int _ClassId;

		/// <summary>
		/// 名称
		/// </summary>
		[Column(Name = "teacher_id")]
		public int TeacherId
		{
			get => _TeacherId; set
			{
				if (_TeacherId == value) return;
				_TeacherId = value;
				teacher_info = null;
			}
		}
		private int _TeacherId;

		/// <summary>
		/// 是否删除
		/// </summary>
		[Column(Name = "is_delete")]
		public bool IsDelete { get; set; }


		#region 外键 => 导航属性，ManyToOne/OneToOne

		[Navigate("ClassId")]
		public virtual ClassInfo class_info { get; set; }

		[Navigate("TeacherId")]
		public virtual TeacherInfo teacher_info { get; set; }

		#endregion

		#region 外键 => 导航属性，ManyToMany

		#endregion
	}

}
