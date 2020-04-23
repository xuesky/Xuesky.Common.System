using FreeSql.DataAnnotations;
using System.Collections.Generic;

namespace Xuesky.Common.DataAccess
{

    /// <summary>
    /// 班级基本信息表
    /// </summary>
    [Table(Name = "class_info")]
    public partial class ClassInfo
    {

        /// <summary>
        /// 班级ID
        /// </summary>
        [Column(Name = "class_id", IsPrimary = true, IsIdentity = true, CanUpdate = false)]
        public int ClassId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(Name = "class_name", DbType = "varchar(255)")]
        public string ClassName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Column(Name = "class_no", DbType = "varchar(255)")]
        public string ClassNo { get; set; }

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


        #region 外键 => 导航属性，OneToMany

        [Navigate("ClassId")]
        public virtual List<ClassTeacherInfo> class_teacher_infos { get; set; }

        [Navigate("ClassId")]
        public virtual List<StuInfo> stu_infos { get; set; }

        #endregion

        #region 外键 => 导航属性，ManyToMany

        #endregion
    }

}
