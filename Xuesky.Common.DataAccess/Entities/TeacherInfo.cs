using FreeSql.DataAnnotations;
using System.Collections.Generic;

namespace Xuesky.Common.DataAccess
{

    /// <summary>
    /// 老师基本信息表
    /// </summary>
    [Table(Name = "teacher_info")]
    public partial class TeacherInfo
    {

        /// <summary>
        /// 班级ID
        /// </summary>
        [Column(Name = "teacher_id", IsPrimary = true, IsIdentity = true, CanUpdate = false)]
        public int TeacherId { get; set; }

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
        /// 姓名
        /// </summary>
        [Column(Name = "teacher_gender", DbType = "nchar(1)")]
        public string TeacherGender { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Column(Name = "teacher_mobile", DbType = "varchar(255)")]
        public string TeacherMobile { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(Name = "teacher_name", DbType = "varchar(255)")]
        public string TeacherName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [Column(Name = "teacher_no", DbType = "varchar(255)")]
        public string TeacherNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(Name = "remark", DbType = "varchar(512)")]
        public string Remark { get; set; }


        #region 外键 => 导航属性，OneToMany

        [Navigate("TeacherId")]
        public virtual List<ClassTeacherInfo> class_teacher_infos { get; set; }

        #endregion

        #region 外键 => 导航属性，ManyToMany

        #endregion
    }

}
